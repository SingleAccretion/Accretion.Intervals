using System.Runtime.CompilerServices;

namespace Accretion.Intervals.Experimental
{
    internal static class BoolExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool boolean)
        {
            return Unsafe.As<bool, byte>(ref boolean);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToLong(this bool boolean)
        {
            return Unsafe.As<bool, byte>(ref boolean);
        }
    }
}
