using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspWpf
{
    public class TspChromosome : ChromosomeBase<int>
    {
        private readonly int numCities;

        public TspChromosome(int numCities) : base(numCities)
        {
            this.numCities = numCities;
            var cityIndices = RandomizationProvider.Current.GetUniqueInts(numCities, 0, numCities);
            int i = 0;
            foreach (var idx in cityIndices)
            {
                ReplaceGene(i++, idx);
            }
        }

        public double Distance { get; internal set; }

        public override int GenerateGene(int geneIndex)
        {
            return RandomizationProvider.Current.GetInt(0, numCities);
        }

        public override IChromosome<int> CreateNew()
        {
            return new TspChromosome(numCities);
        }

        public override IChromosome<int> Clone()
        {
            // this line calls CreateNew and ReplaceGene, Fitness,
            var clone = base.Clone() as TspChromosome;
            clone.Distance = Distance;
            return clone;
        }

    }
}
