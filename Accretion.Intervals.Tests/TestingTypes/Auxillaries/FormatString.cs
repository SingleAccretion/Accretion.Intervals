using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct FormatString : IEquatable<FormatString>
    {
        public FormatString(string value) => Value = value;

        public string Value { get; }

        public override bool Equals(object obj) => obj is FormatString @string && Equals(@string);
        public bool Equals(FormatString other) => Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value;

        public static implicit operator string(FormatString formatString) => formatString.Value;

        public static bool operator ==(FormatString left, FormatString right) => left.Equals(right);
        public static bool operator !=(FormatString left, FormatString right) => !(left == right);
    }
}