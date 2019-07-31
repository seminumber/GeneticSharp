using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeneticSharp.Domain.Crossovers.Generic;
using GeneticSharp.Infrastructure.Framework.Reflection;

namespace GeneticSharp.Domain.Crossovers
{
    /// <summary>
    /// Crossover service.
    /// </summary>
    public static partial class CrossoverService
    {
        #region Methods
        /// <summary>
        /// Gets available crossover types.
        /// </summary>
        /// <returns>All available crossover types.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<Type> GetCrossoverTypes<T>()
        {
            return TypeHelper.GetTypesByInterface<ICrossover<T>>();
        }

        /// <summary>
        /// Gets the available crossover names.
        /// </summary>
        /// <returns>The crossover names.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<string> GetCrossoverNames<T>()
        {
            return TypeHelper.GetDisplayNamesByInterface<ICrossover<T>>();
        }

        /// <summary>
        /// Creates the ICrossover's implementation with the specified name.
        /// </summary>
        /// <returns>The crossover implementation instance.</returns>
        /// <param name="name">The crossover name.</param>
        /// <param name="constructorArgs">Constructor arguments.</param>
        public static ICrossover<T> CreateCrossoverByName<T>(string name, params object[] constructorArgs)
        {
            return TypeHelper.CreateInstanceByName<ICrossover<T>>(name, constructorArgs);
        }

        /// <summary>
        /// Gets the crossover type by the name.
        /// </summary>
        /// <returns>The crossover type.</returns>
        /// <param name="name">The name of crossover.</param>
        public static Type GetCrossoverTypeByName<T>(string name)
        {
            return TypeHelper.GetTypeByName<ICrossover<T>>(name);
        }
        #endregion
    }
}