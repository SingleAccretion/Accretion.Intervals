using System;
using System.Diagnostics.CodeAnalysis;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValueStruct : IComparable<ValueStruct>, IEquatable<ValueStruct>
    {
        public ValueStruct(int value) => Value = value;

        public int Value { get; }

        public int CompareTo([AllowNull] ValueStruct other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj) => obj is ValueStruct @struct && Equals(@struct);
        public bool Equals(ValueStruct other) => Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static implicit operator ValueStruct(int value) => new ValueStruct(value);

        public static bool operator ==(ValueStruct left, ValueStruct right) => left.Equals(right);
        public static bool operator !=(ValueStruct left, ValueStruct right) => !(left == right);
    }
}
