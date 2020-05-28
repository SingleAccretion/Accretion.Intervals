using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Accretion.Intervals
{    
    internal readonly struct UpperBoundary<T> : IEquatable<UpperBoundary<T>> where T : IComparable<T>
    {
        private readonly T _value;
        private readonly bool _isClosed;

        private UpperBoundary(T value, bool isOpen)
        {
            _isClosed = !isOpen;
            _value = value;
        }

        public bool IsOpen { get => !_isClosed; }
        public bool IsClosed { get => _isClosed; }
        public T Value { get => _value; }

        public static UpperBoundary<T> CreateUnchecked(T value, bool isOpen) => new UpperBoundary<T>(value, isOpen);

        public static UpperBoundary<T> CreateChecked(T value, bool isOpen, out bool isValid)
        {
            isValid = true;
            var boundary = new UpperBoundary<T>(value, isOpen);

            if (typeof(T) == typeof(sbyte))
            {
                isValid = !((sbyte)(object)value == sbyte.MinValue && isOpen);
            }
            if (typeof(T) == typeof(byte))
            {
                isValid = !((byte)(object)value == byte.MinValue && isOpen);
            }
            if (typeof(T) == typeof(short))
            {
                isValid = !((short)(object)value == short.MinValue && isOpen);
            }
            if (typeof(T) == typeof(ushort))
            {
                isValid = !((ushort)(object)value == ushort.MinValue && isOpen);
            }
            if (typeof(T) == typeof(char))
            {
                isValid = !((char)(object)value == char.MinValue && isOpen);
            }
            if (typeof(T) == typeof(int))
            {
                isValid = !((int)(object)value == int.MinValue && isOpen);
            }
            if (typeof(T) == typeof(uint))
            {
                isValid = !((uint)(object)value == uint.MinValue && isOpen);
            }
            if (typeof(T) == typeof(long))
            {
                isValid = !((long)(object)value == long.MinValue && isOpen);
            }
            if (typeof(T) == typeof(ulong))
            {
                isValid = !((ulong)(object)value == ulong.MinValue && isOpen);
            }
            if (typeof(T) == typeof(float))
            {
                isValid = ((float)(object)value).IsFinite() || float.IsPositiveInfinity((float)(object)value);
            }
            if (typeof(T) == typeof(double))
            {
                isValid = ((double)(object)value).IsFinite() || double.IsPositiveInfinity((double)(object)value);
            }

            if (Checker.IsNull(value))
            {
                isValid = false;
            }
            else if (GenericSpecializer<T>.TypeImplementsIDiscrete && isOpen)
            {
                isValid = ((IDiscreteValue<T>)value).IsDecrementable;
            }

            return boundary;
        }

        public bool Equals(UpperBoundary<T> other)
        {
            if (GenericSpecializer<T>.TypeIsDiscrete && 
              !(GenericSpecializer<T>.DefaultTypeValueCannotBeDecremented && (Value.IsEqualTo(default) || other.Value.IsEqualTo(default))))
            {
                return ReducedValue().IsEqualTo(other.ReducedValue());
            }
            else
            {
                return Value.IsEqualTo(other.Value) && _isClosed == other._isClosed;
            }
        }

        public override bool Equals(object obj) => obj is UpperBoundary<T> boundary && Equals(boundary);

        public override int GetHashCode()
        {
            if (GenericSpecializer<T>.TypeIsDiscrete && !(GenericSpecializer<T>.DefaultTypeValueCannotBeDecremented && Value.IsEqualTo(default)))
            {
                return HashCode.Combine(ReducedValue());
            }

            return HashCode.Combine(Value, _isClosed);
        }

        public override string ToString() => $"{Value}{(IsOpen ? IntervalSymbols.RightOpenBoundarySymbol : IntervalSymbols.RightClosedBoundarySymbol)}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ReducedValue()
        {
            if (typeof(T) == typeof(byte))
            {
                return (T)(object)(byte)((byte)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(sbyte))
            {
                return (T)(object)(sbyte)((sbyte)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(short))
            {
                return (T)(object)(short)((short)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(char))
            {
                return (T)(object)(char)((char)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(ushort))
            {
                return (T)(object)(ushort)((ushort)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(object)((int)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(uint))
            {
                return (T)(object)(uint)((uint)(object)Value - IsOpen.ToLong());
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(object)((long)(object)Value - IsOpen.ToLong());
            }
            if (typeof(T) == typeof(ulong))
            {
                return (T)(object)((ulong)(object)Value - (ulong)IsOpen.ToLong());
            }

            if (GenericSpecializer<T>.TypeImplementsIDiscrete && IsOpen)
            {
                return ((IDiscreteValue<T>)Value).Decrement();
            }
            else
            {
                return Value;
            }
        }

        public static bool operator ==(UpperBoundary<T> left, UpperBoundary<T> right) => left.Equals(right);

        public static bool operator !=(UpperBoundary<T> left, UpperBoundary<T> right) => !left.Equals(right);
    }
}
