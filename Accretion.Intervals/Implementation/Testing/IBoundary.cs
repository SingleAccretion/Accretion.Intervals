using System.Collections.Generic;

namespace Accretion.Intervals
{
    internal interface IBoundary<T, TComparer> where TComparer : struct, IComparer<T>
    {
        T Value { get; }
        BoundaryType Type { get; }
    }
}
