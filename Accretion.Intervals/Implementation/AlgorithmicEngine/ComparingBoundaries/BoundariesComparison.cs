using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class BoundariesComparison
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer>(this in LowerBoundary<T, TComparer> that, in LowerBoundary<T, TComparer> other) where TComparer : struct, IComparer<T>
        {            
            return that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(Invariant).IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer, S>(this in UpperBoundary<T, TComparer> that, in LowerBoundary<T, TComparer> other, S strategy) where TComparer : struct, IComparer<T> where S : IOverlappingStrategy<T, TComparer>
        {
            return that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && strategy.IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer, S>(this in LowerBoundary<T, TComparer> that, in UpperBoundary<T, TComparer> other, S strategy) where TComparer : struct, IComparer<T> where S : IOverlappingStrategy<T, TComparer>
        {
            return that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && strategy.IsLess(that, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer>(this in UpperBoundary<T, TComparer> that, in UpperBoundary<T, TComparer> other) where TComparer : struct, IComparer<T>
        {
            return that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(Invariant).IsLess(that, other));
        }
    }
}
