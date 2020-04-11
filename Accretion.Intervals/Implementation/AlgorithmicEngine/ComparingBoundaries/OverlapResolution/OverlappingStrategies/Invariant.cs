using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct Invariant
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess<T>(LowerBoundary<T> firstBoundary, LowerBoundary<T> secondBoundary) where T : IComparable<T> => firstBoundary.IsClosed & secondBoundary.IsOpen;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLess<T>(UpperBoundary<T> firstBoundary, UpperBoundary<T> secondBoundary) where T : IComparable<T> => firstBoundary.IsOpen & secondBoundary.IsClosed;
    }
}
