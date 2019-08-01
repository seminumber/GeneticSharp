using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Populations.Generic;
using GeneticSharp.Infrastructure.Framework.Commons;

namespace GeneticSharp.Domain.Reinsertions.Generic
{
    /// <summary>
    /// Base class for IReinsertion's implementations.
    /// </summary>
    public abstract class ReinsertionBase<T> : IReinsertion<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Reinsertions.Generic.ReinsertionBase<T>"/> class.
        /// </summary>
        /// <param name="canCollapse">If set to <c>true</c> can collapse the number of selected chromosomes for reinsertion.</param>
        /// <param name="canExpand">If set to <c>true</c> can expand the number of selected chromosomes for reinsertion.</param>
        protected ReinsertionBase(bool canCollapse, bool canExpand)
        {
            CanCollapse = canCollapse;
            CanExpand = canExpand;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether can collapse the number of selected chromosomes for reinsertion.
        /// </summary>
        public bool CanCollapse { get; private set; }

        /// <summary>
        /// Gets a value indicating whether can expand the number of selected chromosomes for reinsertion.
        /// </summary>
        public bool CanExpand { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Selects the chromosomes which will be reinserted.
        /// </summary>
        /// <returns>The chromosomes to be reinserted in next generation..</returns>
        /// <param name="population">The population.</param>
        /// <param name="offspring">The offspring.</param>
        /// <param name="parents">The parents.</param>
        public IList<IChromosome<T>> SelectChromosomes(IPolulation<T> population, IList<IChromosome<T>> offspring, IList<IChromosome<T>> parents)
        {
            ExceptionHelper.ThrowIfNull("population", population);
            ExceptionHelper.ThrowIfNull("offspring", offspring);
            ExceptionHelper.ThrowIfNull("parents", parents);

            if (!CanExpand && offspring.Count < population.MinSize)
            {
                throw new ReinsertionException<T>(this, "Cannot expand the number of chromosome in population. Try another reinsertion!");
            }

            if (!CanCollapse && offspring.Count > population.MaxSize)
            {
                throw new ReinsertionException<T>(this, "Cannot collapse the number of chromosome in population. Try another reinsertion!");
            }

            return PerformSelectChromosomes(population, offspring, parents);
        }

        /// <summary>
        /// Selects the chromosomes which will be reinserted.
        /// </summary>
        /// <returns>The chromosomes to be reinserted in next generation..</returns>
        /// <param name="population">The population.</param>
        /// <param name="offspring">The offspring.</param>
        /// <param name="parents">The parents.</param>
        protected abstract IList<IChromosome<T>> PerformSelectChromosomes(IPolulation<T> population, IList<IChromosome<T>> offspring, IList<IChromosome<T>> parents);
        #endregion
    }
}
