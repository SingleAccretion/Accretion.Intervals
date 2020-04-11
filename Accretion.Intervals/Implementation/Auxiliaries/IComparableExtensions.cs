using System;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class IComparableExtensions
    {
        public const int IsLess = -1;
        public const int IsEqual = 0;
        public const int IsGreater = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T>(this T that, T other) where T : IComparable<T>
        {
            if (typeof(T) == typeof(sbyte))
            {
                return (sbyte)(object)that < (sbyte)(object)other;
            }
            else if (typeof(T) == typeof(byte))
            {
                return (byte)(object)that < (byte)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that < (char)(object)other;
            }
            else if (typeof(T) == typeof(short))
            {
                return (short)(object)that < (short)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that < (char)(object)other;
            }
            else if (typeof(T) == typeof(ushort))
            {
                return (ushort)(object)that < (ushort)(object)other;
            }
            else if (typeof(T) == typeof(int))
            {
                return (int)(object)that < (int)(object)other;
            }
            else if (typeof(T) == typeof(uint))
            {
                return (uint)(object)that < (uint)(object)other;
            }
            else if (typeof(T) == typeof(long))
            {
                return (long)(object)that < (long)(object)other;
            }
            else if (typeof(T) == typeof(ulong))
            {
                return (ulong)(object)that < (ulong)(object)other;
            }
            else if (typeof(T) == typeof(float))
            {
                return (float)(object)that < (float)(object)other;
            }
            else if (typeof(T) == typeof(double))
            {
                return (double)(object)that < (double)(object)other;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (decimal)(object)that < (decimal)(object)other;
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (DateTime)(object)that < (DateTime)(object)other;
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                return (DateTimeOffset)(object)that < (DateTimeOffset)(object)other;
            }
            else
            {
                if (Checker.IsNull(that))
                {
                    return !Checker.IsNull(other);
                }

                return that.CompareTo(other) < 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this T that, T other) where T : IComparable<T>
        {
            if (typeof(T) == typeof(sbyte))
            {
                return (sbyte)(object)that == (sbyte)(object)other;
            }
            else if (typeof(T) == typeof(byte))
            {
                return (byte)(object)that == (byte)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that == (char)(object)other;
            }
            else if (typeof(T) == typeof(short))
            {
                return (short)(object)that == (short)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that == (char)(object)other;
            }
            else if (typeof(T) == typeof(ushort))
            {
                return (ushort)(object)that == (ushort)(object)other;
            }
            else if (typeof(T) == typeof(int))
            {
                return (int)(object)that == (int)(object)other;
            }
            else if (typeof(T) == typeof(uint))
            {
                return (uint)(object)that == (uint)(object)other;
            }
            else if (typeof(T) == typeof(long))
            {
                return (long)(object)that == (long)(object)other;
            }
            else if (typeof(T) == typeof(ulong))
            {
                return (ulong)(object)that == (ulong)(object)other;
            }
            else if (typeof(T) == typeof(float))
            {
                return (float)(object)that == (float)(object)other;
            }
            else if (typeof(T) == typeof(double))
            {
                return (double)(object)that == (double)(object)other;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (decimal)(object)that == (decimal)(object)other;
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (DateTime)(object)that == (DateTime)(object)other;
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                return (DateTimeOffset)(object)that == (DateTimeOffset)(object)other;
            }

            if (Checker.IsNull(that))
            {
                return Checker.IsNull(other);
            }

            return that.CompareTo(other) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan<T>(this T that, T other) where T : IComparable<T>
        {
            if (typeof(T) == typeof(sbyte))
            {
                return (sbyte)(object)that > (sbyte)(object)other;
            }
            else if (typeof(T) == typeof(byte))
            {
                return (byte)(object)that > (byte)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that > (char)(object)other;
            }
            else if (typeof(T) == typeof(short))
            {
                return (short)(object)that > (short)(object)other;
            }
            else if (typeof(T) == typeof(char))
            {
                return (char)(object)that > (char)(object)other;
            }
            else if (typeof(T) == typeof(ushort))
            {
                return (ushort)(object)that > (ushort)(object)other;
            }
            else if (typeof(T) == typeof(int))
            {
                return (int)(object)that > (int)(object)other;
            }
            else if (typeof(T) == typeof(uint))
            {
                return (uint)(object)that > (uint)(object)other;
            }
            else if (typeof(T) == typeof(long))
            {
                return (long)(object)that > (long)(object)other;
            }
            else if (typeof(T) == typeof(ulong))
            {
                return (ulong)(object)that > (ulong)(object)other;
            }
            else if (typeof(T) == typeof(float))
            {
                return (float)(object)that > (float)(object)other;
            }
            else if (typeof(T) == typeof(double))
            {
                return (double)(object)that > (double)(object)other;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (decimal)(object)that > (decimal)(object)other;
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (DateTime)(object)that > (DateTime)(object)other;
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                return (DateTimeOffset)(object)that > (DateTimeOffset)(object)other;
            }
            else
            {
                if (Checker.IsNull(that))
                {
                    return false;
                }

                return that.CompareTo(other) > 0;
            }
        }
    }
}
