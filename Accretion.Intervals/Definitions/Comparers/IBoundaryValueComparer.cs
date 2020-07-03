using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public interface IBoundaryValueComparer<T> : IComparer<T>
    {
        int GetHashCode(T value);
        string ToString(T value, string format, IFormatProvider formatProvider);
        bool IsInvalidBoundaryValue(T value);
    }
}
