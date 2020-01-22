using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Experimental
{    
    public interface IDiscreteValue<T> : IComparable<T>
    {
        T Increment(out bool overflowed);
        T Decrement(out bool overflowed);        
    }
}
