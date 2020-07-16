using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct IntervalStringWithInvalidValues<T, TComparer> : IEquatable<IntervalStringWithInvalidValues<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public IntervalStringWithInvalidValues(string @string) => String = @string;

        public string String { get; }
        public ReadOnlySpan<char> Span => String;

        public override bool Equals(object obj) => obj is IntervalStringWithInvalidValues<T, TComparer> values && Equals(values);
        public bool Equals(IntervalStringWithInvalidValues<T, TComparer> other) => String == other.String;
        public override int GetHashCode() => HashCode.Combine(String);
        public override string ToString() => String;

        public static bool operator ==(IntervalStringWithInvalidValues<T, TComparer> left, IntervalStringWithInvalidValues<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(IntervalStringWithInvalidValues<T, TComparer> left, IntervalStringWithInvalidValues<T, TComparer> right) => !(left == right);
    }
}
