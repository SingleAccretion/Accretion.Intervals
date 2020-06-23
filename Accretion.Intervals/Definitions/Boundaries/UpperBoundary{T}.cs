using System;

namespace Accretion.Intervals
{
    public readonly struct UpperBoundary<T> where T : IComparable<T>
    {
        private readonly UpperBoundary<T, DefaultComparer<T>> _boundary;

        internal UpperBoundary(T value, BoundaryType type) => _boundary = new UpperBoundary<T, DefaultComparer<T>>(value, type);

        public BoundaryType Type => _boundary.Type;
        public T Value => _boundary.Value;

        internal bool IsClosed => _boundary.IsClosed;
        internal bool IsOpen => _boundary.IsOpen;

        public bool Equals(UpperBoundary<T> other) => _boundary == other._boundary;
        public override bool Equals(object obj) => obj is UpperBoundary<T> boundary && Equals(boundary);
        public override int GetHashCode() => _boundary.GetHashCode();

        public override string ToString() => _boundary.ToString();

        public static bool operator ==(UpperBoundary<T> left, UpperBoundary<T> right) => left.Equals(right);
        public static bool operator !=(UpperBoundary<T> left, UpperBoundary<T> right) => !left.Equals(right);
    }
}
