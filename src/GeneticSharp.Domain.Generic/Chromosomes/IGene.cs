using System;

namespace GeneticSharp.Domain.Chromosomes.Generic
{
    public interface IGene
    {
        object Value { get; }
    }

    public interface IGene<T>
    {
        T Value { get; }
    }
}