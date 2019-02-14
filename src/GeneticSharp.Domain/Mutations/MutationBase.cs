using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Infrastructure.Framework.Commons;
using GeneticSharp.Infrastructure.Framework.Reflection;

namespace GeneticSharp.Domain.Mutations
{
    /// <summary>
    /// Base class for IMutation's implementation.
    /// </summary>
    public abstract class MutationBase : IMutation
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
        public void Mutate(IChromosome chromosome, float probability)
        {
            ExceptionHelper.ThrowIfNull("chromosome", chromosome);

            PerformMutate(chromosome, probability);
        }

        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        protected abstract void PerformMutate(IChromosome chromosome, float probability);

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Mutations.MutationBase"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Mutations.MutationBase"/>.</returns>
        public override string ToString()
        {
            return this.GetDisplayName();
        }
        #endregion
    }
}
