using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct OverlapClosed<T> : IOverlappingStrategy<T> where T : IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary) => thisBoundary.IsOpen & otherBoundary.IsOpen;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary) => thisBoundary.IsClosed | otherBoundary.IsClosed;
    }
}
