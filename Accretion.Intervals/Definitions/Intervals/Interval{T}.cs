using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct Interval<T> : IEquatable<Interval<T>> where T : IComparable<T> 
    {
        private readonly Interval<T, DefaultValueComparer<T>> _interval;

        public static Interval<T> Empty { get; }

        public bool IsEmpty => _interval.IsEmpty;

        public LowerBoundary<T> LowerBoundary => _interval.LowerBoundary;
        public UpperBoundary<T> UpperBoundary => _interval.UpperBoundary;

        public override bool Equals(object obj) => obj is Interval<T> interval && Equals(interval);
        public bool Equals(Interval<T> other) => _interval == other._interval;
        public override int GetHashCode() => HashCode.Combine(_interval);

        public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);
        public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);
    }
}
