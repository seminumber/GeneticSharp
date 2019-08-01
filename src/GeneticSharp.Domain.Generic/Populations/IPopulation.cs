using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes.Generic;

namespace GeneticSharp.Domain.Populations.Generic
{
    /// <summary>
    /// Defines an interface for a population of candidate solutions (chromosomes).
    /// </summary>
    public interface IPolulation<T>
    {
        #region Events
        /// <summary>
        /// Occurs when best chromosome changed.
        /// </summary>
        event EventHandler BestChromosomeChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the creation date.
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Gets the generations.
        /// <remarks>
        /// The information of Generations can vary depending of the IGenerationStrategy used.
        /// </remarks>
        /// </summary>
        /// <value>The generations.</value>
        IList<Generation<T>> Generations { get; }

        /// <summary>
        /// Gets the current generation.
        /// </summary>
        /// <value>The current generation.</value>
        Generation<T> CurrentGeneration { get; }

        /// <summary>
        /// Gets the total number of generations executed.
        /// <remarks>
        /// Use this information to know how many generations have been executed, because Generations.Count can vary depending of the IGenerationStrategy used.
        /// </remarks>
        /// </summary>
        int GenerationsNumber { get; }

        /// <summary>
        /// Gets or sets the minimum size.
        /// </summary>
        /// <value>The minimum size.</value>
        int MinSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the max.
        /// </summary>
        /// <value>The size of the max.</value>
        int MaxSize { get; set; }

        /// <summary>
        /// Gets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        IChromosome<T> BestChromosome { get; }

        /// <summary>
        /// Gets or sets the generation strategy.
        /// </summary>
        IGenerationStrategy<T> GenerationStrategy { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the initial generation.
        /// </summary>
        void CreateInitialGeneration();

        /// <summary>
        /// Creates a new generation.
        /// </summary>
        /// <param name="chromosomes">The chromosomes for new generation.</param>
        void CreateNewGeneration(IList<IChromosome<T>> chromosomes);

        /// <summary>
        /// Ends the current generation.
        /// </summary>        
        void EndCurrentGeneration();
        #endregion
    }
}
