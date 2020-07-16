using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValidIntervalString<T, TComparer> : IEquatable<ValidIntervalString<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public ValidIntervalString(string @string) => String = @string;

        public string String { get; }
        public ReadOnlySpan<char> Span => String;

        public override bool Equals(object obj) => obj is ValidIntervalString<T, TComparer> intervalString && Equals(intervalString);
        public bool Equals(ValidIntervalString<T, TComparer> other) => String == other.String;
        public override int GetHashCode() => HashCode.Combine(String);
        public override string ToString() => String;

        public static bool operator ==(ValidIntervalString<T, TComparer> left, ValidIntervalString<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(ValidIntervalString<T, TComparer> left, ValidIntervalString<T, TComparer> right) => !(left == right);
    }
}