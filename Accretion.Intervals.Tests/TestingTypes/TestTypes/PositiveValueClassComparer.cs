using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct PositiveValueClassComparer : IBoundaryValueComparer<ValueClass>
    {
        public int Compare(ValueClass x, ValueClass y) => x.CompareTo(y);
        public int GetHashCode(ValueClass value) => value.Value;
        public bool IsInvalidBoundaryValue(ValueClass value) => value.Value <= 0;
        public string ToString(ValueClass value, string format, IFormatProvider formatProvider) => value.Value.ToString(format, formatProvider);
    }
}
