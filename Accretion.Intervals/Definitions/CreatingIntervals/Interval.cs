using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Accretion.Intervals.Definitions
{
    public static class Interval
    {
        public static Interval<T> Create<T>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T>
        {
            throw new NotImplementedException();
        }

        public static Interval<T, TComparer> Create<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }     
    }
}
