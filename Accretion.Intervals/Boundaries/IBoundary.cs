using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal interface IBoundary<T> where T : IComparable<T>
    {
        public T Value { get; }        
        public bool IsOpen { get; }
        public bool IsClosed { get; }
    }
}
