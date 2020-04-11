using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct NoOverlap<T> : IOverlappingStrategy<T> where T : IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary) => true;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary) => false;
    }
}
