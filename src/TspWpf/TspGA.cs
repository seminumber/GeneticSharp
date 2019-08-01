using GeneticSharp.Domain.Generic;
using GeneticSharp.Domain.Crossovers.Generic;
using GeneticSharp.Domain.Mutations.Generic;
using GeneticSharp.Domain.Populations.Generic;
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
    public class TspGA
    {
        GeneticAlgorithm<int> ga;

        CancellationTokenSource cts;

        public event Action GenerationRan;
        public TspFitness Fitness { get; private set; }

        public TspChromosome BestChromosome =>
            ga?.BestChromosome as TspChromosome;

        public int GenerationNumber =>
            ga?.GenerationsNumber ?? 0;
        public bool IsRunning => cts != null;


        public void Initialize(int numCities, int areaWidth, int areaHeight)
        {
            if (IsRunning)
                Stop();
            Fitness = new TspFitness(numCities, areaWidth, areaHeight);
            var chromosome = new TspChromosome(numCities);

            var crossover = new OrderedCrossover<int>();
            var mutation = new ReverseSequenceMutation<int>();
            var selection = new EliteSelection<int>();
            var population = new Population<int>(50, 100, chromosome);
            ga = new GeneticAlgorithm<int>(population, Fitness, selection, crossover, mutation);

        }

        public void DesignStart()
        {
            ga.Termination = new GenerationNumberTermination<int>(1);
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
            ga.Termination = new GenerationNumberTermination<int>(1);
            //ga.TaskExecutor = new ParallelTaskExecutor() { MinThreads = 2, MaxThreads = 8 };
            ga.TaskExecutor = new LinearTaskExecutor();
            await Task.Run(ga.Start);

            while(!token.IsCancellationRequested)
            {
                int next = ga.GenerationsNumber + 30;
                ga.Termination = new GenerationNumberTermination<int>(next);
                if (GenerationRan != null)
                    ga.GenerationRan += (_, __) => GenerationRan.Invoke();
                await Task.Run(ga.Resume);
                if(GenerationRan != null)
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
