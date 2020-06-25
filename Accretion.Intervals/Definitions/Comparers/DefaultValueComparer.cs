using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct DefaultValueComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y) => x.CompareTo(y);
    }
}