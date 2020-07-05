using System;

namespace Accretion.Intervals
{
    public readonly struct UpperBoundary<T> : IEquatable<UpperBoundary<T>>, IBoundary<T> where T : IComparable<T>
    {
        private readonly UpperBoundary<T, DefaultValueComparer<T>> _boundary;

        internal UpperBoundary(UpperBoundary<T, DefaultValueComparer<T>> boundary) => _boundary = boundary;
        internal UpperBoundary(T value, BoundaryType type) : this(new UpperBoundary<T, DefaultValueComparer<T>>(value, type)) { }

        public BoundaryType Type => _boundary.Type;
        public T Value => _boundary.Value;

        internal bool IsClosed => _boundary.IsClosed;
        internal bool IsOpen => _boundary.IsOpen;

        public bool Equals(UpperBoundary<T> other) => _boundary == other._boundary;
        public override bool Equals(object obj) => obj is UpperBoundary<T> boundary && Equals(boundary);
        public override int GetHashCode() => _boundary.GetHashCode();

        public override string ToString() => _boundary.ToString();

        public static implicit operator UpperBoundary<T>(UpperBoundary<T, DefaultValueComparer<T>> boundary) => new UpperBoundary<T>(boundary);
        public static implicit operator UpperBoundary<T, DefaultValueComparer<T>>(UpperBoundary<T> boundary) => boundary._boundary;

        public static bool operator ==(UpperBoundary<T> left, UpperBoundary<T> right) => left.Equals(right);
        public static bool operator !=(UpperBoundary<T> left, UpperBoundary<T> right) => !left.Equals(right);
    }
}
