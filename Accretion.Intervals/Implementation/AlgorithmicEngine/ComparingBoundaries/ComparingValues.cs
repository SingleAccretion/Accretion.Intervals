using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class ComparingValues
    {
        public const int IsLess = -1;
        public const int IsEqual = 0;
        public const int IsGreater = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T, TComparer>(this T that, T other) where TComparer : struct, IComparer<T>
        {
            Debug.Assert(that != null && other != null);

            if (GenericSpecializer<TComparer>.TypeIsDefaultValueComparer)
            {
                return IsLessThan(that, other);
            }

            return default(TComparer).Compare(that, other) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T, TComparer>(this T that, T other) where TComparer : struct, IComparer<T>
        {
            if (GenericSpecializer<TComparer>.TypeIsDefaultValueComparer)
            {
                return IsEqualTo(that, other);
            }

            return default(TComparer).Compare(that, other) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan<T, TComparer>(this T that, T other) where TComparer : struct, IComparer<T>
        {
            Debug.Assert(that != null && other != null);

            if (GenericSpecializer<TComparer>.TypeIsDefaultValueComparer)
            {
                return IsGreaterThan(that, other);
            }
            
            return default(TComparer).Compare(that, other) > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsLessThan<T>(T that, T other)
        {
            Debug.Assert(that != null && other != null);

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

            return ((IComparable<T>)that).CompareTo(other) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEqualTo<T>(T that, T other)
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

            return that is null ? other is null : ((IComparable<T>)that).CompareTo(other) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsGreaterThan<T>(T that, T other)
        {
            Debug.Assert(that != null && other != null);

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

            return ((IComparable<T>)that).CompareTo(other) > 0;
        }
    }
}
