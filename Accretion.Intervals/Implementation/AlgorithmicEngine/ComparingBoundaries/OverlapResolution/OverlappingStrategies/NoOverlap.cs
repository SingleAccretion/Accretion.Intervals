using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct NoOverlap<T, TComparer> : IOverlappingStrategy<T, TComparer> where TComparer : struct, IComparer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in UpperBoundary<T, TComparer> thisBoundary, in LowerBoundary<T, TComparer> otherBoundary) => true;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess(in LowerBoundary<T, TComparer> thisBoundary, in UpperBoundary<T, TComparer> otherBoundary) => false;
    }
}
