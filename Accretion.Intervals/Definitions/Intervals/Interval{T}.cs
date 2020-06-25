using System;

namespace Accretion.Intervals
{
    public readonly struct Interval<T> where T : IComparable<T> 
    {
        private readonly Interval<T, DefaultValueComparer<T>> _interval;

        public static Interval<T> Empty { get; }

        public bool IsEmpty => _interval.IsEmpty;

        public LowerBoundary<T> LowerBoundary => _interval.LowerBoundary;
        public UpperBoundary<T> UpperBoundary => _interval.UpperBoundary;
    }
}
