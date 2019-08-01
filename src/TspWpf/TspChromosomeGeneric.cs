using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TspWpf
{
    public class TspChromosomeGeneric : ChromosomeBase<TspCity>
    {
        private IList<TspCity> Cities { get; }

        public TspChromosomeGeneric(IList<TspCity> cities) : base(cities.Count)
        {
            Cities = cities;
            var cityIndices = RandomizationProvider.Current.GetUniqueInts
                (cities.Count, 0, cities.Count);
            int i = 0;
            foreach (var idx in cityIndices)
            {
                ReplaceGene(i++, cities[idx]);
            }
        }

        public double Distance { get; internal set; }

        public override TspCity GenerateGene(int geneIndex)
        {
            return Cities[RandomizationProvider.Current.GetInt(0, Cities.Count)];
        }

        public override IChromosome<TspCity> CreateNew()
        {
            return new TspChromosomeGeneric(Cities);
        }

        public override IChromosome<TspCity> Clone()
        {
            // this line calls CreateNew and ReplaceGene, Fitness,
            var clone = base.Clone() as TspChromosomeGeneric;
            clone.Distance = Distance;
            return clone;
        }
    }
}
