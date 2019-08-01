using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeneticSharp.Infrastructure.Framework.Reflection;

namespace GeneticSharp.Domain.Reinsertions.Generic
{
    /// <summary>
    /// Reinsertion service.
    /// </summary>
    public static class ReinsertionService
    {
        #region Methods
        /// <summary>
        /// Gets available reinsertion types.
        /// </summary>
        /// <returns>All available reinsertion types.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<Type> GetReinsertionTypes<T>()
        {
            return TypeHelper.GetTypesByInterface<IReinsertion<T>>();
        }

        /// <summary>
        /// Gets the available reinsertion names.
        /// </summary>
        /// <returns>The reinsertion names.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IList<string> GetReinsertionNames<T>()
        {
            return TypeHelper.GetDisplayNamesByInterface<IReinsertion<T>>();
        }

        /// <summary>
        /// Creates the IReinsertion's implementation with the specified name.
        /// </summary>
        /// <returns>The reinsertion implementation instance.</returns>
        /// <param name="name">The reinsertion name.</param>
        /// <param name="constructorArgs">Constructor arguments.</param>
        public static IReinsertion<T> CreateReinsertionByName<T>(string name, params object[] constructorArgs)
        {
            return TypeHelper.CreateInstanceByName<IReinsertion<T>>(name, constructorArgs);
        }

        /// <summary>
        /// Gets the reinsertion type by the name.
        /// </summary>
        /// <returns>The reinsertion type.</returns>
        /// <param name="name">The name of reinsertion.</param>
        public static Type GetReinsertionTypeByName<T>(string name)
        {
            return TypeHelper.GetTypeByName<IReinsertion<T>>(name);
        }
        #endregion
    }
}