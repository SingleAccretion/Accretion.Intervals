using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct DateTimeComparerByHour : IBoundaryValueComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y) => x.Hour.CompareTo(y.Hour);
        public int GetHashCode(DateTime value) => value.Hour;
        public bool IsInvalidBoundaryValue(DateTime value) => value.Kind == DateTimeKind.Unspecified;
        public string ToString(DateTime value, string format, IFormatProvider formatProvider) => value.Hour.ToString(format, formatProvider);
    }
}
