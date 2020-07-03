using System;

namespace Accretion.Intervals
{
    public readonly struct DefaultValueComparer<T> : IBoundaryValueComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y) => x.CompareTo(y);
        
        public int GetHashCode(T value) => value.GetHashCode();
        
        public bool IsInvalidBoundaryValue(T value) => Checker.IsNaN(value) || Checker.IsNonUtcDateTime(value);
        
        public string ToString(T value, string format, IFormatProvider formatProvider) => value is IFormattable formattable ? formattable.ToString(format, formatProvider) : value.ToString();
    }
}