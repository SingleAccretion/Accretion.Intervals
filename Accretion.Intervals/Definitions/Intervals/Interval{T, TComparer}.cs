using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public readonly struct Interval<T, TComparer> where TComparer : struct, IComparer<T> 
    {
        internal static Interval<T, TComparer> CreateUnchecked()
        {
            throw null;
        }

        internal static Interval<T, TComparer> CreateChecked()
        {
            throw null;
        }
    }
}
