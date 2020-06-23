using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public readonly struct CompositeInterval<T> where T : IComparable<T> 
    {
        public static CompositeInterval<T> Empty { get; }

        public CompositeInterval<T> SymmetricDifference(CompositeInterval<T> interval) => throw new NotImplementedException();
        public CompositeInterval<T> SymmetricDifference(Interval<T> interval) => throw new NotImplementedException();
    }
}
