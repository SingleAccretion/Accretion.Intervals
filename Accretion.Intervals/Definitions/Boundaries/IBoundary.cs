using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Accretion.Intervals.Tests")]
namespace Accretion.Intervals
{
    public interface IBoundary<T> : IFormattable
    {
        T Value { get; }
        BoundaryType Type { get; }
        bool IsValid { get; }
    }
}
