using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class BoundariesComparison
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer>(this in LowerBoundary<T, TComparer> that, in LowerBoundary<T, TComparer> other) where TComparer : struct, IComparer<T> => 
            that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(Invariant).IsLess(that, other));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer, S>(this in UpperBoundary<T, TComparer> that, in LowerBoundary<T, TComparer> other) where TComparer : struct, IComparer<T> where S : struct, IOverlappingStrategy => 
            that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(S).UpperIsLessThanLower(that.Type, other.Type));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer, S>(this in LowerBoundary<T, TComparer> that, in UpperBoundary<T, TComparer> other) where TComparer : struct, IComparer<T> where S : struct, IOverlappingStrategy => 
            that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(S).LowerIsLessThanUpper(that.Type, other.Type));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer>(this in UpperBoundary<T, TComparer> that, in UpperBoundary<T, TComparer> other) where TComparer : struct, IComparer<T> => 
            that.Value.IsLessThan<T, TComparer>(other.Value) || (that.Value.IsEqualTo<T, TComparer>(other.Value) && default(Invariant).IsLess(that, other));
    }
}
