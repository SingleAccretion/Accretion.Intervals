using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct NonEmptyInterval<T, TComparer> : IEquatable<NonEmptyInterval<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        private readonly Interval<T, TComparer> _interval;

        public NonEmptyInterval(Interval<T, TComparer> interval) => _interval = !interval.IsEmpty ? interval : throw new ArgumentException("NonEmptyInterval cannot be empty.");

        public LowerBoundary<T, TComparer> LowerBoundary => _interval.LowerBoundary;
        public UpperBoundary<T, TComparer> UpperBoundary => _interval.UpperBoundary;

        public bool Contains(T value) => _interval.Contains(value);

        public override bool Equals(object obj) => obj is NonEmptyInterval<T, TComparer> interval && Equals(interval);
        public bool Equals(NonEmptyInterval<T, TComparer> other) => _interval.Equals(other._interval);
        public override int GetHashCode() => _interval.GetHashCode();
        public override string ToString() => _interval.ToString();

        public static implicit operator Interval<T, TComparer>(NonEmptyInterval<T, TComparer> interval) => interval._interval;

        public static bool operator ==(NonEmptyInterval<T, TComparer> left, NonEmptyInterval<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(NonEmptyInterval<T, TComparer> left, NonEmptyInterval<T, TComparer> right) => !(left == right);
    }
}
