using System;

namespace Accretion.Intervals
{
    public interface ISubtractable<in T, out R> : IComparable<T> 
    {
        R Subtract(T subtrahend);
    }
}
