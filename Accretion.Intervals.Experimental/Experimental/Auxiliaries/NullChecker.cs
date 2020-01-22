using System.Runtime.CompilerServices;

namespace Accretion.Intervals.Experimental
{
    internal static class NullChecker
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(T obj)
        {
            return !(obj is object);
        }
    }
}
