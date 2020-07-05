using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct EvenIntegerComaparer : IBoundaryValueComparer<int>
    {
        public int Compare(int x, int y) => x.CompareTo(y);
        public int GetHashCode(int value) => value;
        public bool IsInvalidBoundaryValue(int value) => value % 2 != 0;
        public string ToString(int value, string format, IFormatProvider formatProvider) => value.ToString(format, formatProvider);
    }
}
