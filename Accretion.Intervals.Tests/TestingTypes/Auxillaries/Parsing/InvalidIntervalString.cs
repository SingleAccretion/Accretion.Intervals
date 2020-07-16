using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct InvalidIntervalString<T, TComparer> : IEquatable<InvalidIntervalString<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public InvalidIntervalString(string @string) => String = @string;

        public string String { get; }
        public ReadOnlySpan<char> Span => String;

        public override bool Equals(object obj) => obj is InvalidIntervalString<T, TComparer> values && Equals(values);
        public bool Equals(InvalidIntervalString<T, TComparer> other) => String == other.String;
        public override int GetHashCode() => HashCode.Combine(String);
        public override string ToString() => String;

        public static bool operator ==(InvalidIntervalString<T, TComparer> left, InvalidIntervalString<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(InvalidIntervalString<T, TComparer> left, InvalidIntervalString<T, TComparer> right) => !(left == right);
    }
}
