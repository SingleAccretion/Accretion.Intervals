using System;

namespace Accretion.Intervals
{
    public readonly struct LowerBoundary<T> where T : IComparable<T>
    {
        private readonly LowerBoundary<T, DefaultComparer<T>> _boundary;

        internal LowerBoundary(T value, BoundaryType type) => _boundary = new LowerBoundary<T, DefaultComparer<T>>(value, type);

        public BoundaryType Type => _boundary.Type;
        public T Value => _boundary.Value;

        internal bool IsClosed => _boundary.IsClosed;
        internal bool IsOpen => _boundary.IsOpen;

        public bool Equals(LowerBoundary<T> other) => _boundary == other._boundary;
        public override bool Equals(object obj) => obj is LowerBoundary<T> boundary && Equals(boundary);
        public override int GetHashCode() => _boundary.GetHashCode();

        public override string ToString() => _boundary.ToString();

        public static bool operator ==(LowerBoundary<T> left, LowerBoundary<T> right) => left.Equals(right);
        public static bool operator !=(LowerBoundary<T> left, LowerBoundary<T> right) => !left.Equals(right);
    }
}
