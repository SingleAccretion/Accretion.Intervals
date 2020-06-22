using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct UpperBoundary<T, TComparer> where TComparer : struct, IComparer<T>
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

        public bool Equals(UpperBoundary<T, TComparer> other) => Value.IsEqualTo<T, DefaultComparer<T>>(other.Value) && Type == other.Type;
        public override bool Equals(object obj) => obj is UpperBoundary<T, TComparer> boundary && Equals(boundary);
        public override int GetHashCode() => HashCode.Combine(Value, Type);

        public override string ToString() => base.ToString();

        public static bool operator ==(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(UpperBoundary<T, TComparer> left, UpperBoundary<T, TComparer> right) => !left.Equals(right);
    }
}
