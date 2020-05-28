using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Accretion.Intervals
{    
    internal readonly struct LowerBoundary<T> : IEquatable<LowerBoundary<T>> where T : IComparable<T>
    {        
        private readonly T _value;
        private readonly bool _isClosed;

        private LowerBoundary(T value, bool isOpen)
        {
            _isClosed = !isOpen;
            _value = value;
        }

        public bool IsOpen { get => !_isClosed; }
        public bool IsClosed { get => _isClosed; }
        public T Value { get => _value; }

        public static LowerBoundary<T> CreateUnchecked(T value, bool isOpen) => new LowerBoundary<T>(value, isOpen);

        public static LowerBoundary<T> CreateChecked(T value, bool isOpen, out bool isValid)
        {
            isValid = true;
            var boundary = new LowerBoundary<T>(value, isOpen);

            if (typeof(T) == typeof(sbyte))
            {
                isValid = !((sbyte)(object)value == sbyte.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(byte))
            {
                isValid = !((byte)(object)value == byte.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(short))
            {
                isValid = !((short)(object)value == short.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(ushort))
            {
                isValid = !((ushort)(object)value == ushort.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(char))
            {
                isValid = !((char)(object)value == char.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(int))
            {
                isValid = !((int)(object)value == int.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(uint))
            {
                isValid = !((uint)(object)value == uint.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(long))
            {
                isValid = !((long)(object)value == long.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(ulong))
            {
                isValid = !((ulong)(object)value == ulong.MaxValue && isOpen);
            }
            if (typeof(T) == typeof(float))
            {
                isValid = ((float)(object)value).IsFinite() || float.IsNegativeInfinity((float)(object)value);
            }
            if (typeof(T) == typeof(double))
            {
                isValid = ((double)(object)value).IsFinite() || double.IsNegativeInfinity((double)(object)value);
            }

            if (Checker.IsNull(value))
            {
                isValid = false;
            }
            else if (GenericSpecializer<T>.TypeImplementsIDiscrete && isOpen)
            {
                isValid = ((IDiscreteValue<T>)value).IsIncrementable;
            }

            return boundary;
        }

        public bool Equals(LowerBoundary<T> other)
        {
            if (GenericSpecializer<T>.TypeIsDiscrete && 
              !(GenericSpecializer<T>.DefaultTypeValueCannotBeIncremented && (Value.IsEqualTo(default) || other.Value.IsEqualTo(default))))
            {
                return ReducedValue().IsEqualTo(other.ReducedValue());
            }
            else
            {
                return Value.IsEqualTo(other.Value) && _isClosed == other._isClosed;
            }
        }

        public override bool Equals(object obj) => obj is LowerBoundary<T> boundary && Equals(boundary);

        public override int GetHashCode()
        {
            if (GenericSpecializer<T>.TypeIsDiscrete && !(GenericSpecializer<T>.DefaultTypeValueCannotBeIncremented && Value.IsEqualTo(default)))
            {
                return HashCode.Combine(ReducedValue());
            }
            else
            {
                return HashCode.Combine(Value, _isClosed);
            }
        }

        public override string ToString() => $"{(IsOpen ? IntervalSymbols.LeftOpenBoundarySymbol : IntervalSymbols.LeftClosedBoundarySymbol)}{Value}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ReducedValue()
        {
            if (typeof(T) == typeof(byte))
            {
                return (T)(object)(byte)((byte)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(sbyte))
            {
                return (T)(object)(sbyte)((sbyte)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(short))
            {
                return (T)(object)(short)((short)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(char))
            {
                return (T)(object)(char)((char)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(ushort))
            {
                return (T)(object)(ushort)((ushort)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(object)((int)(object)Value + IsOpen.ToInt());
            }
            if (typeof(T) == typeof(uint))
            {
                return (T)(object)(uint)((uint)(object)Value + IsOpen.ToLong());
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(object)((long)(object)Value + IsOpen.ToLong());
            }
            if (typeof(T) == typeof(ulong))
            {
                return (T)(object)((ulong)(object)Value + (ulong)IsOpen.ToLong());
            }

            if (GenericSpecializer<T>.TypeImplementsIDiscrete && IsOpen)
            {
                return ((IDiscreteValue<T>)Value).Increment();
            }
            else
            {
                return Value;
            }
        }

        public static bool operator ==(LowerBoundary<T> left, LowerBoundary<T> right) => left.Equals(right);

        public static bool operator !=(LowerBoundary<T> left, LowerBoundary<T> right) => !left.Equals(right);
    }
}
