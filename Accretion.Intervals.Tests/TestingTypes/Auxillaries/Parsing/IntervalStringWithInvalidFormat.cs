using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct IntervalStringWithInvalidFormat<T, TComparer> : IEquatable<IntervalStringWithInvalidFormat<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public IntervalStringWithInvalidFormat(string @string) => String = @string;

        public string String { get; }
        public ReadOnlySpan<char> Span => String;

        public override bool Equals(object obj) => obj is IntervalStringWithInvalidFormat<T, TComparer> values && Equals(values);
        public bool Equals(IntervalStringWithInvalidFormat<T, TComparer> other) => String == other.String;
        public override int GetHashCode() => HashCode.Combine(String);
        public override string ToString() => String;

        public static bool operator ==(IntervalStringWithInvalidFormat<T, TComparer> left, IntervalStringWithInvalidFormat<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(IntervalStringWithInvalidFormat<T, TComparer> left, IntervalStringWithInvalidFormat<T, TComparer> right) => !(left == right);
    }
}
