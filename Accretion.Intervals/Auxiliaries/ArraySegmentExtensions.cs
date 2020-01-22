using System;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class ArraySegmentExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndex<T>(this ArraySegment<T> segment)
        {
            return segment.Offset + segment.Count - 1;
        }
    }
}
