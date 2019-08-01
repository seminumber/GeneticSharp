using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Fitnesses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspWpf
{

    public class TspFitnessGeneric : IFitness<TspCity>
    {
        public double Evaluate(IChromosome<TspCity> chromosome)
        {
            var cities = chromosome.GetGenes();
            //pairwise
            double sum = cities.Skip(1).Concat(new[] { cities.First() })
                                .Zip(cities,
                                        (second, first) =>
                                            first.DistanceTo(second))
                                .Sum();
            var fitness = 1.0 - sum / 1000 / cities.Count;
            if (chromosome is TspChromosomeGeneric tspchromo)
                tspchromo.Distance = sum;

            // expecting 1
            var dup = cities.Count - cities.Distinct().Count();


            if (dup > 0) fitness /= dup;

            if (fitness < 0) fitness = 0;
            return fitness;
        }
    }
}
