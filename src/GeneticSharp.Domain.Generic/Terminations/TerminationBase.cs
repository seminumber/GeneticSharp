using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Infrastructure.Framework.Commons;
using GeneticSharp.Domain.Generic;

namespace GeneticSharp.Domain.Terminations.Generic
{
    /// <summary>
    /// Base class for ITerminations implementations.
    /// </summary>
    public abstract class TerminationBase<T> : ITermination<T>
    {
        #region Fields
        private bool m_hasReached;
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified geneticAlgorithm reached the termination condition.
        /// </summary>
        /// <returns>True if termination has been reached, otherwise false.</returns>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        public bool HasReached(IGeneticAlgorithm<T> geneticAlgorithm)
        {
            ExceptionHelper.ThrowIfNull("geneticAlgorithm", geneticAlgorithm);

            m_hasReached = PerformHasReached(geneticAlgorithm);

            return m_hasReached;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="GeneticSharp.Domain.Terminations.Generic.LogicalOperatorTerminationBase<T>"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="GeneticSharp.Domain.Terminations.Generic.LogicalOperatorTerminationBase<T>"/>.</returns>
        public override string ToString()
        {
            return "{0} (HasReached: {1})".With(GetType().Name, m_hasReached);
        }

        /// <summary>
        /// Determines whether the specified geneticAlgorithm reached the termination condition.
        /// </summary>
        /// <returns>True if termination has been reached, otherwise false.</returns>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        protected abstract bool PerformHasReached(IGeneticAlgorithm<T> geneticAlgorithm);
        #endregion
    }
}
