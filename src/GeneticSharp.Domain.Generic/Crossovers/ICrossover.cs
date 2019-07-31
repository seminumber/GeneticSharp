using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Chromosomes.Generic;

namespace GeneticSharp.Domain.Crossovers.Generic
{
    /// <summary>
    /// Defines a interface for a crossover genetic operator.
    /// <remarks>
    /// In genetic algorithms, crossover is a genetic operator used to vary the programming of a chromosome
    /// or chromosomes from one generation to the next. 
    /// <para>
    /// It is analogous to reproduction and biological crossover, upon which genetic algorithms are based. 
    /// Cross over is a process of taking more than one parent solutions and producing a child solution from them.
    /// </para>
    /// <see href="http://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)">Crossover (Genetic Algorithm)</see>
    /// </remarks>
    /// </summary>
    public interface ICrossover<T> : IChromosomeOperator
    {
        #region Properties
        /// <summary>
        /// Gets the number of parents need for cross.
        /// </summary>
        /// <value>The parents number.</value>
        int ParentsNumber { get; }

        /// <summary>
        /// Gets the number of children generated by cross.
        /// </summary>
        /// <value>The children number.</value>
        int ChildrenNumber { get; }

        /// <summary>
        /// Gets the minimum length of the chromosome supported by the crossover.
        /// </summary>
        /// <value>The minimum length of the chromosome.</value>
        int MinChromosomeLength { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Cross the specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>The offspring (children) of the parents.</returns>
        IList<IChromosome<T>> Cross(IList<IChromosome<T>> parents);
        #endregion
    }
}