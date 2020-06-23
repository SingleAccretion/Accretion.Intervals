using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct Invariant
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess<T, TComparer>(LowerBoundary<T, TComparer> firstBoundary, LowerBoundary<T, TComparer> secondBoundary) where TComparer : struct, IComparer<T> => 
            firstBoundary.IsClosed & secondBoundary.IsOpen;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess<T, TComparer>(UpperBoundary<T, TComparer> firstBoundary, UpperBoundary<T, TComparer> secondBoundary) where TComparer : struct, IComparer<T> => 
            firstBoundary.IsOpen & secondBoundary.IsClosed;
    }
}
