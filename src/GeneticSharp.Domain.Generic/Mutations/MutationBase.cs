using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Infrastructure.Framework.Commons;

namespace GeneticSharp.Domain.Mutations.Generic
{
    /// <summary>
    /// Base class for IMutation's implementation.
    /// </summary>
    public abstract class MutationBase<T> : IMutation<T>
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the operator is ordered (if can keep the chromosome order).
        /// </summary>
        public bool IsOrdered { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        public void Mutate(IChromosome<T> chromosome, float probability)
        {
            ExceptionHelper.ThrowIfNull("chromosome", chromosome);

            PerformMutate(chromosome, probability);
        }

        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        protected abstract void PerformMutate(IChromosome<T> chromosome, float probability);
        #endregion
    }
}
