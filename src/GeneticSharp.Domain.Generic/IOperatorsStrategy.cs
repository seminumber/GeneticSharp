using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Crossovers.Generic;
using GeneticSharp.Domain.Mutations.Generic;
using GeneticSharp.Domain.Populations.Generic;

namespace GeneticSharp.Domain.Generic
{
    /// <summary>
    /// Defines an interface for operators strategy.
    /// </summary>
    public interface IOperatorStrategy<T>
    {
        /// <summary>
        /// Crosses the specified parents.
        /// </summary>
        /// <param name="crossover">The crossover class.</param>
        /// <param name="crossoverProbability">The crossover probability.</param>
        /// <param name="parents">The parents.</param>
        /// <returns>The result chromosomes.</returns>
        IList<IChromosome<T>> Cross(IPolulation<T> population, ICrossover<T> crossover, float crossoverProbability, IList<IChromosome<T>> parents);

        /// <summary>
        /// Mutate the specified chromosomes.
        /// </summary>
        /// <param name="mutation">The mutation class.</param>
        /// <param name="mutationProbability">The mutation probability.</param>
        /// <param name="chromosomes">The chromosomes.</param>
        void Mutate(IMutation<T> mutation, float mutationProbability, IList<IChromosome<T>> chromosomes);
    }
}
