using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    internal readonly struct UpperBoundary<T> : IBoundary<T>, IEquatable<UpperBoundary<T>> where T : IComparable<T>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "Layout compatibility with Boundary<T>")]
        private readonly bool _isLowerFiller;
        private readonly bool _isClosed;
        private readonly T _value;

        private UpperBoundary(T value, bool isOpen)
        {
            _isLowerFiller = false;
            _isClosed = !isOpen;
            _value = value;
        }

        public bool IsOpen { get => !_isClosed; }
        public bool IsClosed { get => _isClosed; }
        public T Value { get => _value; }

        public static UpperBoundary<T> Create(T value, bool isOpen) => new UpperBoundary<T>(value, isOpen);

        public static bool TryCreate(T value, bool isOpen, out UpperBoundary<T> boundary)
        {
            boundary = new UpperBoundary<T>(value, isOpen);

            if (typeof(T) == typeof(double))
            {
                return (((double)(object)value).IsFinite() || double.IsPositiveInfinity((double)(object)value)) &&
                      !((double)(object)value == double.MinValue && isOpen);
            }
            if (typeof(T) == typeof(float))
            {
                return (((float)(object)value).IsFinite() || float.IsPositiveInfinity((float)(object)value)) &&
                      !((float)(object)value == float.MinValue && isOpen);
            }
            if (typeof(T) == typeof(byte))
            {
                return !((byte)(object)value == byte.MinValue && isOpen);
            }
            if (typeof(T) == typeof(sbyte))
            {
                return !((sbyte)(object)value == sbyte.MinValue && isOpen);
            }
            if (typeof(T) == typeof(short))
            {
                return !((short)(object)value == short.MinValue && isOpen);
            }
            if (typeof(T) == typeof(char))
            {
                return !((char)(object)value == char.MinValue && isOpen);
            }
            if (typeof(T) == typeof(ushort))
            {
                return !((ushort)(object)value == ushort.MinValue && isOpen);
            }
            if (typeof(T) == typeof(uint))
            {
                return !((uint)(object)value == uint.MinValue && isOpen);
            }
            if (typeof(T) == typeof(int))
            {
                return !((int)(object)value == int.MinValue && isOpen);
            }
            if (typeof(T) == typeof(ulong))
            {
                return !((ulong)(object)value == ulong.MinValue && isOpen);
            }
            if (typeof(T) == typeof(long))
            {
                return !((long)(object)value == long.MinValue && isOpen);
            }
            if (typeof(T) == typeof(DateTime))
            {
                return !((DateTime)(object)value == DateTime.MinValue && isOpen);
            }
            if (typeof(T) == typeof(DateTimeOffset))
            {
                return !((DateTimeOffset)(object)value == DateTimeOffset.MinValue && isOpen);
            }

            if (GenericSpecializer<T>.TypeInstanceCanBeNull && (NullChecker.IsNull(value) || NullChecker.IsNull(value)))
            {
                return false;
            }
            if (GenericSpecializer<T>.TypeImplementsIDiscrete)
            {
                var overflowed = false;
                if (isOpen)
                {
                    ((IDiscreteValue<T>)value).Decrement(out overflowed);
                }

                return !overflowed;
            }

            return true;
        }

        public bool Equals(UpperBoundary<T> other)
        {
            if (GenericSpecializer<T>.TypeIsDiscrete)
            {
                return ReducedValue().IsEqualTo(other.ReducedValue());
            }

            return Value.IsEqualTo(other.Value) && _isClosed == other._isClosed;
        }
        
        public override bool Equals(object obj) => obj is UpperBoundary<T> boundary && Equals(boundary);

        public override int GetHashCode()
        {
            if (GenericSpecializer<T>.TypeIsDiscrete)
            {
                return HashCode.Combine(ReducedValue());
            }

            return HashCode.Combine(Value, _isClosed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T ReducedValue()
        {
            if (typeof(T) == typeof(byte))
            {
                return (T)(object)((byte)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(sbyte))
            {
                return (T)(object)((sbyte)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(short))
            {
                return (T)(object)((short)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(char))
            {
                return (T)(object)((char)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(ushort))
            {
                return (T)(object)((ushort)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(object)((int)(object)Value - IsOpen.ToInt());
            }
            if (typeof(T) == typeof(uint))
            {
                return (T)(object)((uint)(object)Value - IsOpen.ToLong());
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(object)((long)(object)Value - IsOpen.ToLong());
            }
            if (typeof(T) == typeof(ulong))
            {
                return (T)(object)((ulong)(object)Value - (ulong)IsOpen.ToLong());
            }

            if (GenericSpecializer<T>.TypeImplementsIDiscrete)
            {
                return IsOpen ? ((IDiscreteValue<T>)Value).Decrement(out _) : Value;
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