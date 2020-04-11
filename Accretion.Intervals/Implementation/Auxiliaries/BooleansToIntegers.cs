using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class BooleansToIntegers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool boolean) => Unsafe.As<bool, byte>(ref boolean);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUInt(this bool boolean) => Unsafe.As<bool, byte>(ref boolean);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToLong(this bool boolean) => Unsafe.As<bool, byte>(ref boolean);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ToBool(this int integer)
        {
            var a = (byte)integer;
            return Unsafe.As<byte, bool>(ref a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ToBool(this uint integer)
        {
            var a = (byte)integer;
            return Unsafe.As<byte, bool>(ref a);
        }
    }
}
