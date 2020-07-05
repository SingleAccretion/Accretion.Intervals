using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct SingleComparerByExponent : IBoundaryValueComparer<float>
    {
        public int Compare(float x, float y) => default(DoubleComparerByExponent).Compare(x, y);
        public int GetHashCode(float value) => default(DoubleComparerByExponent).GetHashCode(value);
        public bool IsInvalidBoundaryValue(float value) => value <= 0f || float.IsNaN(value);
        public string ToString(float value, string format, IFormatProvider formatProvider) => default(DoubleComparerByExponent).ToString(value, format, formatProvider);
    }
}
