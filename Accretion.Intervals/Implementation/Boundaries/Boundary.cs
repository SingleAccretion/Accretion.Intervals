using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Accretion.Profiling")]
namespace Accretion.Intervals
{
    internal readonly struct Boundary<T> : IBoundary<T>, IEquatable<Boundary<T>> where T : IComparable<T>
    {
        public static readonly Boundary<T> DefaultOpenUpperBoundary = new Boundary<T>(default, true, false);
        public static readonly Boundary<T> DefaultOpenLowerBoundary = new Boundary<T>(default, true, true);

        private readonly bool _isLower;
        private readonly bool _isClosed;        
        private readonly T _value;
        
        public Boundary(T value, bool isOpen, bool isLower)
        {            
            _isClosed = !isOpen;
            _isLower = isLower;
            _value = value;
        }

        public T Value { get => _value; }
        public bool IsOpen { get => !_isClosed; }
        public bool IsClosed { get => _isClosed; }
        public bool IsLower { get => _isLower; }
        public bool IsUpper { get => !_isLower; }

        public bool Equals(Boundary<T> other) => _value.IsEqualTo(other._value) && _isClosed == other._isClosed && _isLower == other._isLower;

        public override bool Equals(object obj) => (obj is Boundary<T> boundary) && Equals(boundary);

        public override int GetHashCode() => HashCode.Combine(_value, _isClosed, _isLower);

        public override string ToString()
        {
            return _isLower ?
                   IsOpen ? $"{Interval.LeftOpenBoundarySymbol}{_value}" : $"{Interval.LeftClosedBoundarySymbol}{_value}" :
                   IsOpen ? $"{_value}{Interval.RightOpenBoundarySymbol}" : $"{_value}{Interval.RightClosedBoundarySymbol}";
        }

        public static bool operator ==(Boundary<T> first, Boundary<T> second) => first.Equals(second);

        public static bool operator !=(Boundary<T> first, Boundary<T> second) => !first.Equals(second);
    }
}
