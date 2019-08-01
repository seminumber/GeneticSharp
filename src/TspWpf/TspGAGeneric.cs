using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Crossovers.Generic;
using GeneticSharp.Domain.Fitnesses.Generic;
using GeneticSharp.Domain.Generic;
using GeneticSharp.Domain.Mutations.Generic;
using GeneticSharp.Domain.Populations.Generic;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections.Generic;
using GeneticSharp.Domain.Terminations.Generic;
using GeneticSharp.Infrastructure.Framework.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TspWpf
{
    public class TspGAGeneric
    {
        GeneticAlgorithm<TspCity> ga;

        public IList<TspCity> Cities { get; private set; }

        CancellationTokenSource cts;

        public event Action GenerationRan;
        //public IFitness<TspCity> Fitness { get; private set; }

        public IChromosome<TspCity> BestChromosome =>
            ga?.BestChromosome; //as TspChromosome;

        public int GenerationNumber =>
            ga?.GenerationsNumber ?? 0;
        public bool IsRunning => cts != null;


        public void Initialize(int numCities, int areaWidth, int areaHeight)
        {
            if (IsRunning)
                Stop();

            InitializeCities(numCities, areaWidth, areaHeight);
            var fitness = new TspFitnessGeneric();
            var chromosome = new TspChromosomeGeneric(Cities);

            var crossover = new OrderedCrossover<TspCity>();
            var mutation = new ReverseSequenceMutation<TspCity>();
            var selection = new EliteSelection<TspCity>();
            var population = new Population<TspCity>(50, 100, chromosome);
            ga = new GeneticAlgorithm<TspCity>(population, fitness, selection,
                crossover, mutation);

        }


        public void InitializeCities(int numCities, double areaWidth, double areaHeight)
        {
            Cities = new List<TspCity>(numCities);
            for (int i = 0; i < numCities; i++)
                Cities.Add(GetRandomCity(areaWidth, areaHeight));
        }

        private TspCity GetRandomCity(double width, double height)
        {
            return new TspCity(
                RandomizationProvider.Current.GetDouble(0, width),
                RandomizationProvider.Current.GetDouble(0, height));
        }




        public void DesignStart()
        {
            if (Cities == null || Cities.Count == 0)
                Initialize(10, 100, 100);
            ga.TaskExecutor = new LinearTaskExecutor();
            ga.Termination = new GenerationNumberTermination<TspCity>(1);
            ga.Start();
        }

        public async Task StartAsync()
        {
            if (cts != null)
                cts.Cancel();
            cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
            await StartAsync(cts.Token);
            cts = null;
        }

        public async Task StartAsync(CancellationToken token)
        {
            //ga.GenerationRan += delegate
            //{
            //    var distance = ((TspChromosome)ga.BestChromosome).Distance;
            //};
            await InternalLoopAsync(token);
        }


        private async Task InternalLoopAsync(CancellationToken token)
        {
            // run until cancelled.. cancellation will indicate termination
            ga.Termination = new GenerationNumberTermination<TspCity>(1);
            ga.TaskExecutor = new ParallelTaskExecutor() { MinThreads = 2, MaxThreads = 8 };
            //ga.TaskExecutor = new LinearTaskExecutor();
            await Task.Run(ga.Start);

            while (!token.IsCancellationRequested)
            {
                int next = ga.GenerationsNumber + 30;
                ga.Termination = new GenerationNumberTermination<TspCity>(next);
                if (GenerationRan != null)
                    ga.GenerationRan += (_, __) => GenerationRan.Invoke();
                await Task.Run(ga.Resume);
                if (GenerationRan != null)
                    await Task.Run(GenerationRan.Invoke);
            }
        }


        public void Stop()
        {
            if (cts != null)
                cts.Cancel();
        }
    }
}
