﻿namespace GeneticSharp.Domain.Chromosomes.Generic
{
    /// <summary>
    /// Defines a basic interface for operators which works with chromosomes.
    /// </summary>
    public interface IChromosomeOperator
    {
        /// <summary>
        /// Gets a value indicating whether the operator is ordered (if can keep the chromosome order).
        /// </summary>
        bool IsOrdered { get; }
    }
}