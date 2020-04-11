using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class Coordinate : IEquatable<Coordinate>, IDiscreteValue<Coordinate>, ISubtractable<Coordinate, Distance>, ISubtractable<Coordinate, long>
    {
        public static readonly Coordinate MaxValue = new Coordinate(int.MaxValue);
        public static readonly Coordinate MinValue = new Coordinate(int.MinValue);

        private readonly int _value;
        
        public Coordinate(int value) => _value = value;

        public bool IsIncrementable => _value != int.MaxValue;
        public bool IsDecrementable => _value != int.MinValue;

        public static Coordinate Parse(string s) => new Coordinate(int.Parse(s));

        public int CompareTo([AllowNull] Coordinate other) => other is null ? 1 : _value.CompareTo(other._value);

        Distance ISubtractable<Coordinate, Distance>.Subtract(Coordinate subtrahend) => new Distance(Math.Abs((long)_value - subtrahend._value));
        long ISubtractable<Coordinate, long>.Subtract(Coordinate subtrahend) => Math.Abs((long)_value - subtrahend._value);

        public Coordinate Increment() => new Coordinate(_value + 1);
        public Coordinate Decrement() => new Coordinate(_value - 1);

        public override bool Equals(object obj) => Equals(obj as Coordinate);
        public bool Equals([AllowNull] Coordinate other) => other != null && _value == other._value;
        public override int GetHashCode() => HashCode.Combine(_value);
        public override string ToString() => _value.ToString();

        public static bool operator ==(Coordinate left, Coordinate right) => Equals(left, right);
        public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);
        public static bool operator <(Coordinate left, Coordinate right) => left is null ? right is object : left.CompareTo(right) < 0;
        public static bool operator <=(Coordinate left, Coordinate right) => left is null || left.CompareTo(right) <= 0;
        public static bool operator >(Coordinate left, Coordinate right) => left is object && left.CompareTo(right) > 0;
        public static bool operator >=(Coordinate left, Coordinate right) => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
