using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal readonly struct OverlapClosed<T, TComparer> : IOverlappingStrategy<T, TComparer> where TComparer : struct, IComparer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in UpperBoundary<T, TComparer> thisBoundary, in LowerBoundary<T, TComparer> otherBoundary) => thisBoundary.IsOpen & otherBoundary.IsOpen;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in LowerBoundary<T, TComparer> thisBoundary, in UpperBoundary<T, TComparer> otherBoundary) => thisBoundary.IsClosed | otherBoundary.IsClosed;
    }
}
