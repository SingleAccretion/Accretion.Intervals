using System;

namespace Accretion.Intervals
{
    public readonly struct LowerBoundary<T> : IEquatable<LowerBoundary<T>>, IBoundary<T> where T : IComparable<T>
    {
        private readonly LowerBoundary<T, DefaultValueComparer<T>> _boundary;

        internal LowerBoundary(LowerBoundary<T, DefaultValueComparer<T>> boundary) => _boundary = boundary;
        internal LowerBoundary(T value, BoundaryType type) : this(new LowerBoundary<T, DefaultValueComparer<T>>(value, type)) { }

        public BoundaryType Type => _boundary.Type;
        public T Value => _boundary.Value;
        public bool IsValid => _boundary.IsValid;

        internal bool IsClosed => _boundary.IsClosed;
        internal bool IsOpen => _boundary.IsOpen;

        public bool Equals(LowerBoundary<T> other) => _boundary == other._boundary;
        public override bool Equals(object obj) => obj is LowerBoundary<T> boundary && Equals(boundary);
        public override int GetHashCode() => _boundary.GetHashCode();

        public override string ToString() => _boundary.ToString();
        public string ToString(string format, IFormatProvider formatProvider) => _boundary.ToString(format, formatProvider);

        public static implicit operator LowerBoundary<T>(LowerBoundary<T, DefaultValueComparer<T>> boundary) => new LowerBoundary<T>(boundary);
        public static implicit operator LowerBoundary<T, DefaultValueComparer<T>>(LowerBoundary<T> boundary) => boundary._boundary;

        public static bool operator ==(LowerBoundary<T> left, LowerBoundary<T> right) => left.Equals(right);
        public static bool operator !=(LowerBoundary<T> left, LowerBoundary<T> right) => !left.Equals(right);
    }
}
