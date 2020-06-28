using Accretion.Intervals.StringConversion;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct UpperBoundary<T, TComparer> : IEquatable<UpperBoundary<T, TComparer>> where TComparer : struct, IComparer<T>
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

        public bool Equals(UpperBoundary<T, TComparer> other) => Value.IsEqualTo<T, TComparer>(other.Value) && Type == other.Type;
        public override bool Equals(object obj) => obj is UpperBoundary<T, TComparer> boundary && Equals(boundary);
        public override int GetHashCode() => HashCode.Combine(Value, Type);

        public override string ToString() => StringSerializer.Serialize(this);

        public static bool operator ==(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => !left.Equals(right);
    }
}
