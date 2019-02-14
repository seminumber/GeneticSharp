using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Extensions.Sudoku;
using GeneticSharp.Infrastructure.Framework.Threading;

namespace GeneticSharp.Benchmarks
{
    [Config(typeof(DefaultConfig))]
    public class SudokuBenchmark
    {
        [Params(120)]
        public float SecondsEvolving { get; set; }

        [Params(50, 100, 200)]
        public int PopulationSize { get; set; }

        [Params(
            // Super easy - Population 250 - generation 16 - 0.2s
            "9.2..54.31...63.255.84.7.6..263.9..1.57.1.29..9.67.53.24.53.6..7.52..3.4.8..4195.",

            // Easy - Population 5000 - generation 24 - 10s
            "..48...1767.9.....5.8.3...43..74.1...69...78...1.69..51...8.3.6.....6.9124...15..",

            // Medium - Population 100000 - generation 30  - 10mn
            "..6.......8..542...4..9..7...79..3......8.4..6.....1..2.3.67981...5...4.478319562",

            // Hard - Population 300000 - generation 37 - 1h30mn
            "....9.4.8.....2.7..1.7....32.4..156...........952..7.19....5.1..3.4.....1.2.7...."
         )]        

        public string RawBoard { get; set; }

        [ParamsSource(nameof(ValuesForSelection))]
        public ISelection Selection { get; set; }

        [ParamsSource(nameof(ValuesForCrossover))]
        public ICrossover Crossover { get; set; }

        [ParamsSource(nameof(ValuesForMutation))]
        public IMutation Mutation { get; set; }

        public static IEnumerable<ISelection> ValuesForSelection => new ISelection[]
        {
            //new EliteSelection(),
            //new TournamentSelection(),
            //new RouletteWheelSelection(),
            new StochasticUniversalSamplingSelection()
        };

        public static IEnumerable<ICrossover> ValuesForCrossover => new ICrossover[]
        {
            //new AlternatingPositionCrossover(),
            //new CycleCrossover(),
            //new OrderBasedCrossover(),
            //new OrderedCrossover(),
            //new PartiallyMappedCrossover(),
            //new PositionBasedCrossover(),
          
            //new CutAndSpliceCrossover(),

            //new OnePointCrossover(),
            //new ThreeParentCrossover(),
            //new TwoPointCrossover(),
            new UniformCrossover(),
            //new VotingRecombinationCrossover(),
        };

        public static IEnumerable<IMutation> ValuesForMutation => new IMutation[]
        {
            //new DisplacementMutation(),
            //new InsertionMutation(),
            //new PartialShuffleMutation(),
            //new ReverseSequenceMutation(),
            //new TworsMutation(),

            //new FlipBitMutation(),

            new UniformMutation(true)
        };

        [Benchmark]
        public void Run()
        {
            var board = SudokuBoard.Parse(RawBoard);
            var chromosome = new SudokuPermutationsChromosome(board);
            var population = new Population(PopulationSize, PopulationSize, chromosome);
            var fitness = new SudokuFitness(board);
            var selection = new EliteSelection();
           
            var ga = new GeneticAlgorithm(population, fitness, selection, Crossover, Mutation)
            {
                Termination = new OrTermination(new FitnessThresholdTermination(0), new TimeEvolvingTermination(TimeSpan.FromSeconds(SecondsEvolving))),
                TaskExecutor = new LinearTaskExecutor()
            };

            ga.Start();            
        }
    }
}