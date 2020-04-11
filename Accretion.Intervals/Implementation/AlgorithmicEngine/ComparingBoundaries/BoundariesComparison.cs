using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal static class BoundariesComparison
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T>(this in LowerBoundary<T> that, in LowerBoundary<T> other) where T : IComparable<T>
        {            
            return that.Value.IsLessThan(other.Value) || (that.Value.IsEqualTo(other.Value) && OverlapStrategies<T>.Invariant.IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, S>(this in UpperBoundary<T> that, in LowerBoundary<T> other, S strategy) where T : IComparable<T> where S : IOverlappingStrategy<T>
        {
            return that.Value.IsLessThan(other.Value) || (that.Value.IsEqualTo(other.Value) && strategy.IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, S>(this in LowerBoundary<T> that, in UpperBoundary<T> other, S strategy) where T : IComparable<T> where S : IOverlappingStrategy<T>
        {
            return that.Value.IsLessThan(other.Value) || (that.Value.IsEqualTo(other.Value) && strategy.IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T>(this in UpperBoundary<T> that, in UpperBoundary<T> other) where T : IComparable<T>
        {
            return that.Value.IsLessThan(other.Value) || (that.Value.IsEqualTo(other.Value) && OverlapStrategies<T>.Invariant.IsLess(that, other));
        }
    }
}
