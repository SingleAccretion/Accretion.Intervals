using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct Interval<T, TComparer> where TComparer : struct, IComparer<T> 
    {
        public static Interval<T, TComparer> Empty { get; }

        internal static Interval<T, TComparer> CreateUnchecked(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary)
        {
            throw null;
        }

        internal static bool TryCreate(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary, out Interval<T, TComparer> interval, out Exception exception)
        {
            throw null;
        }
    }
}
