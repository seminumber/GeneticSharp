using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Populations.Generic;

namespace GeneticSharp.Domain.Reinsertions.Generic
{
    /// <summary>
    /// Elitist reinsertion.
    /// <remarks>
    /// When there are less offspring than parents, select the best parents to be reinserted together with the offspring. 
    /// <see href="http://usb-bg.org/Bg/Annual_Informatics/2011/SUB-Informatics-2011-4-29-35.pdf">Generalized Nets Model of offspring Reinsertion in Genetic Algorithm</see>
    /// </remarks>
    /// </summary>
    [DisplayName("Elitist")]
    public class ElitistReinsertion<T> : ReinsertionBase<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Reinsertions.Generic.ElitistReinsertion<T>"/> class.
        /// </summary>
        public ElitistReinsertion() : base(false, true)
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
            var diff = population.MinSize - offspring.Count;

            if (diff > 0)
            {
                var bestParents = parents.OrderByDescending(p => p.Fitness).Take(diff).ToList();

                for (int i = 0; i < bestParents.Count; i++)
                {
                    offspring.Add(bestParents[i]);
                }
            }

            return offspring;
        }
        #endregion
    }
}