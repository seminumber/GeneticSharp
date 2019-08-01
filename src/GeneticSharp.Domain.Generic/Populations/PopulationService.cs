using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeneticSharp.Infrastructure.Framework.Reflection;

namespace GeneticSharp.Domain.Populations.Generic
{
    /// <summary>
    /// Population service.
    /// </summary>
    public static class PopulationService
    {
        #region Methods
        /// <summary>
        /// Gets available generation strategy types.
        /// </summary>
        /// <returns>All available generation strategy types.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<Type> GetGenerationStrategyTypes<T>()
        {
            return TypeHelper.GetTypesByInterface<IGenerationStrategy<T>>();
        }

        /// <summary>
        /// Gets the available generation strategy names.
        /// </summary>
        /// <returns>The generation strategy names.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<string> GetGenerationStrategyNames<T>()
        {
            return TypeHelper.GetDisplayNamesByInterface<IGenerationStrategy<T>>();
        }

        /// <summary>
        /// Creates the IGenerationStrategy's implementation with the specified name.
        /// </summary>
        /// <returns>The generation strategy implementation instance.</returns>
        /// <param name="name">The generation strategy name.</param>
        /// <param name="constructorArgs">Constructor arguments.</param>
        public static IGenerationStrategy<T> CreateGenerationStrategyByName<T>(string name, params object[] constructorArgs)
        {
            return TypeHelper.CreateInstanceByName<IGenerationStrategy<T>>(name, constructorArgs);
        }

        /// <summary>
        /// Gets the generation strategy type by the name.
        /// </summary>
        /// <returns>The generation strategy type.</returns>
        /// <param name="name">The name of generation strategy.</param>
        public static Type GetGenerationStrategyTypeByName<T>(string name)
        {
            return TypeHelper.GetTypeByName<IGenerationStrategy<T>>(name);
        }
        #endregion
    }
}