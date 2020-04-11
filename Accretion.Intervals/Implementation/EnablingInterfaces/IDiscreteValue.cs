using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{    
    public interface IDiscreteValue<T> : IComparable<T>
    {
        T Increment();
        T Decrement();
        public bool IsIncrementable { get; }
        public bool IsDecrementable { get; }
    }
}
