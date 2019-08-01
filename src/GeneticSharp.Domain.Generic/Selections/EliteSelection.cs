using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Populations.Generic;

namespace GeneticSharp.Domain.Selections.Generic
{
    /// <summary>
    /// Selects the chromosomes with the best fitness.
    /// </summary>
    /// <remarks>
    /// Also know as: Truncation Selection.
    /// </remarks>    
    [DisplayName("Elite")]
    public sealed class EliteSelection<T> : Selection<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Selections.Generic.EliteSelection"/> class.
        /// </summary>
        public EliteSelection() : base(2)
        {
        }
        #endregion

        #region ISelection implementation
        /// <summary>
        /// Performs the selection of chromosomes from the generation specified.
        /// </summary>
        /// <param name="number">The number of chromosomes to select.</param>
        /// <param name="generation">The generation where the selection will be made.</param>
        /// <returns>The select chromosomes.</returns>
        protected override IList<IChromosome<T>> PerformSelectChromosomes(int number, Generation<T> generation)
        {
            var ordered = generation.Chromosomes.OrderByDescending(c => c.Fitness);
            return ordered.Take(number).ToList();
        }

        #endregion
    }
}