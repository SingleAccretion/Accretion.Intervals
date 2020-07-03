using System;
using System.Globalization;
using Accretion.Intervals.StringConversion;

namespace Accretion.Intervals
{
    public readonly struct UpperBoundary<T, TComparer> : IEquatable<UpperBoundary<T, TComparer>>, IBoundary<T> where TComparer : struct, IBoundaryValueComparer<T>
    {
        private readonly BoundaryType _type;
        private readonly T _value;

        internal UpperBoundary(T value, BoundaryType type)
        {
            _type = type;
            _value = value;
        }

        public BoundaryType Type => _type;
        public T Value => _value;

        internal bool IsClosed => Type == BoundaryType.Closed;
        internal bool IsOpen => Type == BoundaryType.Open;

        public bool Equals(UpperBoundary<T, TComparer> other) => Value.IsEqualTo<T, TComparer>(other.Value) &&  Type == other.Type;
        public override bool Equals(object obj) => obj is UpperBoundary<T, TComparer> boundary && Equals(boundary);
        public override int GetHashCode() => Checker.IsNull(Value) ? HashCode.Combine(Type) : HashCode.Combine(default(TComparer).GetHashCode(Value), Type);

        public override string ToString() => StringSerializer.Serialize(this, StringSerializer.GeneralFormat, CultureInfo.InvariantCulture);

        public static bool operator ==(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => !left.Equals(right);
    }
}
