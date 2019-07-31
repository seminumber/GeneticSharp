using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticSharp.Domain.Chromosomes.Generic
{
    /// <summary>
    /// Integer chromosome with binary values (0 and 1).
    /// </summary>
    public class IntegerChromosome : IBinaryChromosome<bool>
	{
		private readonly int m_minValue;
		private readonly int m_maxValue;
		private readonly BitArray m_originalValue;

        public double? Fitness { get; set; }
        public int Length => m_originalValue.Length;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneticSharp.Domain.Chromosomes.IntegerChromosome"/> class.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        public IntegerChromosome(int minValue, int maxValue)
		{
			m_minValue = minValue;
			m_maxValue = maxValue;
			var intValue = RandomizationProvider.Current.GetInt(m_minValue, m_maxValue);
			m_originalValue = new BitArray(new int[] { intValue });

			//CreateGenes();
		}

        public IntegerChromosome() : this(int.MinValue, int.MaxValue)
        {

        }

        public IntegerChromosome(int minValue, int maxValue, int geneValue)
        {
            m_minValue = minValue;
            m_maxValue = maxValue;
            m_originalValue = new BitArray(new int[] { geneValue });
        }

		/// <summary>
		/// Generates the gene.
		/// </summary>
		/// <returns>The gene.</returns>
		/// <param name="geneIndex">Gene index.</param>
		public bool GenerateGene(int geneIndex)
		{
			var value = m_originalValue[geneIndex];

            return value;
		}

		/// <summary>
		/// Creates the new.
		/// </summary>
		/// <returns>The new.</returns>
		public IChromosome<bool> CreateNew()
		{
			return new IntegerChromosome(m_minValue, m_maxValue);
		}

		/// <summary>
		/// Converts the chromosome to its integer representation.
		/// </summary>
		/// <returns>The integer.</returns>
		public int ToInteger()
		{
			var array = new int[1];
			var genes = GetGenes().ToArray();
			var bitArray = new BitArray(genes);
			bitArray.CopyTo(array, 0);

			return array[0];
		}

        /// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Chromosomes.FloatingPointChromosome"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Chromosomes.FloatingPointChromosome"/>.</returns>
		public override string ToString()
        {
            return String.Join("", GetGenes().Reverse().Select(g => g ? "1" : "0").ToArray());
        }

        /// <summary>
        /// Flips the gene.
        /// </summary>
        /// <remarks>>
        /// If gene's value is 0, the it will be flip to 1 and vice-versa.</remarks>
        /// <param name="index">The gene index.</param>
        public void FlipGene(int index)
        {
            ReplaceGene(index, !GetGene(index));
            //var realIndex = Math.Abs(31 - index);
            //var value = GetGene(realIndex);

            //ReplaceGene(realIndex, !value);
        }

        public void ReplaceGene(int index, bool gene)
        {
            this.m_originalValue[index] = gene;
        }

        public void ReplaceGenes(int startIndex, IList<bool> genes)
        {
            int currentIndex = startIndex;
            foreach (var gene in genes)
            {
                if (currentIndex >= this.Length) break;
                m_originalValue[currentIndex++] = gene;
            }
        }

        public void Resize(int newLength)
        {
            throw new InvalidOperationException();
        }

        public bool GetGene(int index)
        {
            return m_originalValue[index];
        }

        public IList<bool> GetGenes()
        {
            var ret = new List<bool>(32);
            foreach (bool gene in m_originalValue)
                ret.Add(gene);
            return ret;
        }

        public IChromosome<bool> Clone()
        {
            return new IntegerChromosome(m_minValue, m_maxValue, this.ToInteger());
        }

        public int CompareTo(IChromosome<bool> other)
        {
            return Comparer<double?>.Default.Compare(this.Fitness, other.Fitness);
        }
    }
}

