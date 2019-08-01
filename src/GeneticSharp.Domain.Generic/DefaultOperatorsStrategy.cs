using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes.Generic;
using GeneticSharp.Domain.Crossovers.Generic;
using GeneticSharp.Domain.Mutations.Generic;
using GeneticSharp.Domain.Populations.Generic;
using GeneticSharp.Domain.Randomizations;

namespace GeneticSharp.Domain.Generic
{
    /// <summary>
    /// Defines an operators strategy which use a linear execution
    /// </summary>
    public class DefaultOperatorsStrategy<T> : IOperatorStrategy<T>
    {
        /// <summary>
        /// Crosses the specified parents.
        /// </summary>
        /// <param name="crossover">The crossover class.</param>
        /// <param name="crossoverProbability">The crossover probability.</param>
        /// <param name="parents">The parents.</param>
        /// <returns>The result chromosomes.</returns>
        public IList<IChromosome<T>> Cross(IPolulation<T> population,
            ICrossover<T> crossover, float crossoverProbability,
            IList<IChromosome<T>> parents)
        {
            var minSize = population.MinSize;
            var offspring = new List<IChromosome<T>>(minSize);

            for (int i = 0; i < minSize; i += crossover.ParentsNumber)
            {
                var selectedParents = parents.Skip(i).Take(crossover.ParentsNumber).ToList();

                // If match the probability cross is made, otherwise the offspring is an exact copy of the parents.
                // Checks if the number of selected parents is equal which the crossover expect, because the in the end of the list we can
                // have some rest chromosomes.
                if (selectedParents.Count == crossover.ParentsNumber && RandomizationProvider.Current.GetDouble() <= crossoverProbability)
                {
                    offspring.AddRange(crossover.Cross(selectedParents));
                }
            }

            return offspring;
        }

        /// <summary>
        /// Mutate the specified chromosomes.
        /// </summary>
        /// <param name="mutation">The mutation class.</param>
        /// <param name="mutationProbability">The mutation probability.</param>
        /// <param name="chromosomes">The chromosomes.</param>
        public void Mutate(IMutation<T> mutation, float mutationProbability, IList<IChromosome<T>> chromosomes)
        {
            for (int i = 0; i < chromosomes.Count; i++)
            {
                mutation.Mutate(chromosomes[i], mutationProbability);
            }
        }
    }
}
