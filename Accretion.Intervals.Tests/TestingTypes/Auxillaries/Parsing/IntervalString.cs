using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct IntervalString<T, TComparer> : IEquatable<IntervalString<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public IntervalString(string @string) => String = @string;

        public string String { get; }
        public ReadOnlySpan<char> Span => String;

        public override bool Equals(object obj) => obj is IntervalString<T, TComparer> values && Equals(values);
        public bool Equals(IntervalString<T, TComparer> other) => String == other.String;
        public override int GetHashCode() => HashCode.Combine(String);
        public override string ToString() => String;

        public static bool operator ==(IntervalString<T, TComparer> left, IntervalString<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(IntervalString<T, TComparer> left, IntervalString<T, TComparer> right) => !(left == right);
    }
}
