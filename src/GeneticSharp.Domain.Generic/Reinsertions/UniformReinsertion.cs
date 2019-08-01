using System.Collections.Generic;
using System.ComponentModel;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Populations.Generic;
using GeneticSharp.Domain.Randomizations;

namespace GeneticSharp.Domain.Reinsertions.Generic
{
    /// <summary>
    /// Uniform Reinsertion.
    /// <remarks>
    /// When there are less offspring than parents, select the offspring uniformly at random to be reinserted, the parents are discarded. 
    /// <see href="http://usb-bg.org/Bg/Annual_Informatics/2011/SUB-Informatics-2011-4-29-35.pdf">Generalized Nets Model of offspring Reinsertion in Genetic Algorithm</see>
    /// </remarks>
    /// </summary>
    [DisplayName("Uniform")]
    public class UniformReinsertion<T> : ReinsertionBase<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Reinsertions.Generic.UniformReinsertion"/> class.
        /// </summary>
        public UniformReinsertion() : base(false, true)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Selects the chromosomes which will be reinserted.
        /// </summary>
        /// <returns>The chromosomes to be reinserted in next generation..</returns>
        /// <param name="population">The population.</param>
        /// <param name="offspring">The offspring.</param>
        /// <param name="parents">The parents.</param>
        protected override IList<IChromosome<T>> PerformSelectChromosomes(IPolulation<T> population, IList<IChromosome<T>> offspring, IList<IChromosome<T>> parents)
        {
            if (offspring.Count == 0)
            {
                throw new ReinsertionException<T>(this, "The minimum size of the offspring is 1.");
            }

            var rnd = RandomizationProvider.Current;

            while (offspring.Count < population.MinSize)
            {
                offspring.Add(offspring[rnd.GetInt(0, offspring.Count)]);
            }

            return offspring;
        }
        #endregion
    }
}
