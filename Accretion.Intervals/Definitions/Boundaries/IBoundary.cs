using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Accretion.Intervals.Tests")]
namespace Accretion.Intervals
{
    public interface IBoundary<T>
    {
        T Value { get; }
        BoundaryType Type { get; }
    }
}
