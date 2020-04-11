using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal interface IOverlappingStrategy<T> where T : IComparable<T>
    {
        bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary);
        bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary);
    }
}
