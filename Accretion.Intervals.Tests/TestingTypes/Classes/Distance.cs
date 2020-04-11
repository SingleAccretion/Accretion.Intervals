using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class Distance : IEquatable<Distance>, IAddable<Distance>, IComparable<Distance>
    {
        [ZeroValue]
        public static Distance Zero { get; } = new Distance(0);

        private readonly double _value;

        public Distance(double value) => _value = value >= 0 ? value : throw new ArgumentException(nameof(value));

        public Distance Add(Distance addend) => new Distance(_value + addend._value);
        public int CompareTo([AllowNull] Distance other) => _value.CompareTo(other._value);
        
        public bool Equals([AllowNull] Distance other) => other != null && _value == other._value;
        public override bool Equals(object obj) => Equals(obj as Distance);
        public override int GetHashCode() => HashCode.Combine(_value);
        public override string ToString() => _value.ToString();

        public static bool operator ==(Distance left, Distance right) => Equals(left, right);
        public static bool operator !=(Distance left, Distance right) => !(left == right);

        public static bool operator <(Distance left, Distance right) => left is null ? right is object : left.CompareTo(right) < 0;
        public static bool operator <=(Distance left, Distance right) => left is null || left.CompareTo(right) <= 0;
        public static bool operator >(Distance left, Distance right) => left is object && left.CompareTo(right) > 0;
        public static bool operator >=(Distance left, Distance right) => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
