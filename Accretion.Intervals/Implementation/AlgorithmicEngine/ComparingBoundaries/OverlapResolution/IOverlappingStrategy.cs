using System.Collections.Generic;

namespace Accretion.Intervals
{
    internal interface IOverlappingStrategy<T, TComparer> where TComparer : struct, IComparer<T>
    {
        bool IsLess(in UpperBoundary<T, TComparer> thisBoundary, in LowerBoundary<T, TComparer> otherBoundary);
        bool IsLess(in LowerBoundary<T, TComparer> thisBoundary, in UpperBoundary<T, TComparer> otherBoundary);
    }
}
