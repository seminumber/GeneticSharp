﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes.Generic;

namespace GeneticSharp.Domain.Populations.Generic
{
    /// <summary>
    /// Represents a population of candidate solutions (chromosomes) using TPL to create them.
    /// </summary>
    public class TplPopulation<T> : Population<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Populations.Generic.TplPopulation"/> class.
        /// </summary>
        /// <param name="minSize">The minimum size (chromosomes).</param>
        /// <param name="maxSize">The maximum size (chromosomes).</param>
        /// <param name="adamChromosome">The original chromosome of all population ;).</param>
        public TplPopulation(int minSize, int maxSize, IChromosome<T> adamChromosome) : base(minSize, maxSize, adamChromosome)
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates the initial generation.
        /// </summary>
        public override void CreateInitialGeneration()
        {
            Generations = new List<Generation<T>>();
            GenerationsNumber = 0;

            var chromosomes = new ConcurrentBag<IChromosome<T>>();
            Parallel.For(0, MinSize, i =>
            {
                var c = AdamChromosome.CreateNew();

                if (c == null)
                {
                    throw new InvalidOperationException("The Adam chromosome's 'CreateNew' method generated a null chromosome. This is a invalid behavior, please, check your chromosome code.");
                }

                c.ValidateGenes();

                chromosomes.Add(c);
            });

            CreateNewGeneration(chromosomes.ToList());
        }
        #endregion
    }
}