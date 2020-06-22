using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public readonly struct CompositeInterval<T, TComparer> where TComparer : struct, IComparer<T> 
    {
        public static CompositeInterval<T, TComparer> Empty { get; }
    }
}
