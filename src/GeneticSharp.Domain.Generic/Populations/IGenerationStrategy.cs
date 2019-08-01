namespace GeneticSharp.Domain.Populations.Generic
{
    /// <summary>
    /// Defines a strategy to some key points of generation behavior inside a population.
    /// </summary>
    public interface IGenerationStrategy<T>
    {
        #region Methods
        /// <summary>
        /// Register that a new generation has been created.
        /// </summary>
        /// <param name="population">The population where the new generation has been created.</param>
        void RegisterNewGeneration(IPolulation<T> population);
        #endregion
    }
}
