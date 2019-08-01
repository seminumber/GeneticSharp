using System;
using GeneticSharp.Domain.Chromosomes.Generic;

namespace GeneticSharp.Domain.Generic
{
    /// <summary>
    /// Defines a interface for a genetic algorithm.
    /// </summary>
    public interface IGeneticAlgorithm<T>
    {
        #region Properties
        /// <summary>
        /// Gets the generations number.
        /// </summary>
        /// <value>The generations number.</value>
        int GenerationsNumber { get; }

        /// <summary>
        /// Gets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        IChromosome<T> BestChromosome { get; }

        /// <summary>
        /// Gets the time evolving.
        /// </summary>
        /// <value>The time evolving.</value>
        TimeSpan TimeEvolving { get; }
        #endregion
    }
}
