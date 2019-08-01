﻿using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Fitnesses.Generic;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspWpf
{
    public class TspFitness : IFitness<int>
    {
        int areaWidth;
        int areaHeight;

        public IList<TspCity> Cities { get; private set; }

        public TspFitness(int numCities, int areaWidth, int areaHeight)
        {
            Cities = new List<TspCity>(numCities);
            this.areaWidth = areaWidth;
            this.areaHeight = areaHeight;
            for (int i = 0; i < numCities; i++)
                Cities.Add(GetRandomCity());
        }

        private TspCity GetRandomCity()
        {
            return new TspCity(
                RandomizationProvider.Current.GetDouble(0, areaWidth),
                RandomizationProvider.Current.GetDouble(0, areaHeight));
        }

        public double Evaluate(IChromosome<int> chromosome)
        {
            var indices = chromosome.GetGenes();
            //pairwise
            double sum = indices.Skip(1).Concat(new[] { indices.First() })
                                .Zip(indices,
                                        (second, first) =>
                                            Cities[first].DistanceTo(Cities[second]))
                                .Sum();
            var fitness = 1.0 - sum / 1000 / Cities.Count;
            ((TspChromosome)chromosome).Distance = sum;
            // expecting 1
            var dup = indices.Count - indices.Distinct().Count();


            if (dup > 0) fitness /= dup;

            if (fitness < 0) fitness = 0;
            return fitness;
        }
    }
}
