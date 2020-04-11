using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal static class Copying
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy<T>(T[] source, int sourceStartIndex, T[] destination, int destionationStartIndex, int length)
        {
            source.AsSpan(sourceStartIndex, length).CopyTo(destination.AsSpan(destionationStartIndex, length));
        }
    }
}
