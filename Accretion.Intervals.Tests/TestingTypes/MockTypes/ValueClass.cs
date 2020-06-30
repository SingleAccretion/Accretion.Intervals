using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Accretion.Intervals.Tests
{
    public class ValueClass : IComparable<ValueClass>, IEquatable<ValueClass>
    {
        public ValueClass(int value) => Value = value;

        public int Value { get; }

        public int CompareTo([AllowNull] ValueClass other) => Value.CompareTo(other?.Value);

        public override bool Equals(object obj) => Equals(obj as ValueClass);
        public bool Equals(ValueClass other) => other != null && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static implicit operator ValueClass(int? value) => value.HasValue ? new ValueClass(value.Value) : null;

        public static bool operator ==(ValueClass left, ValueClass right) => EqualityComparer<ValueClass>.Default.Equals(left, right);
        public static bool operator !=(ValueClass left, ValueClass right) => !(left == right);
    }
}
