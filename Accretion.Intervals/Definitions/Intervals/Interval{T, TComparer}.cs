using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public readonly struct Interval<T, TComparer> where TComparer : struct, IComparer<T> 
    {
        internal static Interval<T, TComparer> CreateUnchecked(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary)
        {
            throw null;
        }

        internal static bool TryCreate(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary, out Interval<T, TComparer> interval, out Exception exception)
        {
            throw null;
        }

        public static Interval<T, TComparer> Empty { get; }
    }
}
