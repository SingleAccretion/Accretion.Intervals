using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    //This is a marker interface that be used
    //Both by IComparable and IComparer components
    //As well as the middle of the stack
    //It does not constrain the T to IComparable<T> to save on JIT costs in the downlevel comparison methods
    internal readonly struct DefaultComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y) => throw new NotSupportedException();
    }
}
