using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal static class BoundariesComparison
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int InComparisonWith<T, S>(this Boundary<T> that, Boundary<T> other, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            var valuesCompared = that.Value.CompareTo(other.Value);

            if (valuesCompared != 0)
            {
                return valuesCompared;
            }
            else if (that.IsLower != other.IsLower)
            {
                return overlappingStrategy.Resolve(that.IsLower, that.IsOpen, other.IsOpen);
            }
            else if (that.IsOpen != other.IsOpen)
            {
                return that.IsLower ?
                       that.IsOpen ? 1 : -1 :
                       that.IsOpen ? -1 : 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
