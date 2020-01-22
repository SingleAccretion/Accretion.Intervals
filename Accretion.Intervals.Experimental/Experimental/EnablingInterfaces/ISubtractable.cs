using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    public interface ISubtractable<T, R> : IComparable<T>
    {
        R Subtract(T subtrahend);
    }
}
