using System;

namespace Accretion.Intervals
{
    public static class Lengths
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static int Length(this CompositeInterval<sbyte> interval) => CompositeIntervalLength<sbyte, int>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static int Length(this CompositeInterval<byte> interval) => CompositeIntervalLength<byte, int>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static int Length(this CompositeInterval<short> interval) => CompositeIntervalLength<short, int>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static int Length(this CompositeInterval<ushort> interval) => CompositeIntervalLength<ushort, int>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static long Length(this CompositeInterval<int> interval) => CompositeIntervalLength<int, long>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static long Length(this CompositeInterval<uint> interval) => CompositeIntervalLength<uint, long>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static ulong Length(this CompositeInterval<long> interval) => CompositeIntervalLength<long, ulong>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static ulong Length(this CompositeInterval<ulong> interval) => CompositeIntervalLength<ulong, ulong>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static float Length(this CompositeInterval<float> interval) => CompositeIntervalLength<float, float>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static double Length(this CompositeInterval<double> interval) => CompositeIntervalLength<double, double>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents. May overflow!
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="OverflowException" />
        public static decimal Length(this CompositeInterval<decimal> interval) => CompositeIntervalLength<decimal, decimal>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static TimeSpan Length(this CompositeInterval<DateTime> interval) => CompositeIntervalLength<DateTime, TimeSpan>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static TimeSpan Length(this CompositeInterval<DateTimeOffset> interval) => CompositeIntervalLength<DateTimeOffset, TimeSpan>(interval);

        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static R Length<T, R>(this CompositeInterval<T> interval) where T : ISubtractable<T, R> where R : IAddable<R> => CompositeIntervalLength<T, R>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static int Length(this Interval<sbyte> interval) => IntervalLength<sbyte, int>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static int Length(this Interval<byte> interval) => IntervalLength<byte, int>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static int Length(this Interval<short> interval) => IntervalLength<short, int>(interval);

        /// <summary>
        /// Computes the length of this interval: difference reduced between values of its boundaries.
        /// </summary>
        public static int Length(this Interval<ushort> interval) => IntervalLength<ushort, int>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static long Length(this Interval<int> interval) => IntervalLength<int, long>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static long Length(this Interval<uint> interval) => IntervalLength<uint, long>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static ulong Length(this Interval<long> interval) => IntervalLength<long, ulong>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between reduced values of its boundaries.
        /// </summary>
        public static ulong Length(this Interval<ulong> interval) => IntervalLength<ulong, ulong>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static float Length(this Interval<float> interval) => IntervalLength<float, float>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static double Length(this Interval<double> interval) => IntervalLength<double, double>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed. May overflow!
        /// </summary>
        /// <exception cref="OverflowException" />
        public static decimal Length(this Interval<decimal> interval) => IntervalLength<decimal, decimal>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static TimeSpan Length(this Interval<DateTime> interval) => IntervalLength<DateTime, TimeSpan>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static TimeSpan Length(this Interval<DateTimeOffset> interval) => IntervalLength<DateTimeOffset, TimeSpan>(interval);

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries.
        /// </summary>
        public static R Length<T, R>(this Interval<T> interval) where T : ISubtractable<T, R> => IntervalLength<T, R>(interval);

        internal static R CompositeIntervalLength<T, R>(CompositeInterval<T> interval) where T : IComparable<T>
        {
            throw new NotImplementedException("Generic length cannot inferred! This has to be worked around somehow.");
            /*
            var intervals = interval.CompositeIntervals;
            var length = GenericSpecializer<R>.ZeroValueOfThisType;

            for (int i = 0; i < intervals.Count; i++)
            {
                length = Add(length, IntervalLength<T, R>(intervals[i]));
            }

            return length;
            */
        }

        private static R IntervalLength<T, R>(Interval<T> interval) where T : IComparable<T>
        {
            if (interval.IsEmpty)
            {
                return GenericSpecializer<R>.ZeroValueOfThisType;
            }

            return Subtract<T, R>(interval.UpperBoundary.Value, interval.LowerBoundary.Value);
        }

        private static R Subtract<T, R>(T upper, T lower) where T : IComparable<T>
        {
            if (typeof(T) == typeof(sbyte) && typeof(R) == typeof(int))
            {
                return (R)(object)((sbyte)(object)upper - (sbyte)(object)lower);
            }
            if (typeof(T) == typeof(byte) && typeof(R) == typeof(int))
            {
                return (R)(object)((byte)(object)upper - (byte)(object)lower);
            }
            if (typeof(T) == typeof(short) && typeof(R) == typeof(int))
            {
                return (R)(object)((short)(object)upper - (short)(object)lower);
            }
            if (typeof(T) == typeof(ushort) && typeof(R) == typeof(int))
            {
                return (R)(object)((ushort)(object)upper - (ushort)(object)lower);
            }
            if (typeof(T) == typeof(int) && typeof(R) == typeof(long))
            {
                return (R)(object)((long)(int)(object)upper - (int)(object)lower);
            }
            if (typeof(T) == typeof(uint) && typeof(R) == typeof(long))
            {
                return (R)(object)((long)(uint)(object)upper - (uint)(object)lower);
            }
            if (typeof(T) == typeof(long) && typeof(R) == typeof(ulong))
            {
                return (R)(object)((ulong)(long)(object)upper - (ulong)(long)(object)lower);
            }
            if (typeof(T) == typeof(ulong) && typeof(R) == typeof(ulong))
            {
                return (R)(object)((ulong)(object)upper - (ulong)(object)lower);
            }
            if (typeof(T) == typeof(float) && typeof(R) == typeof(float))
            {
                return (R)(object)((float)(object)upper - (float)(object)lower);
            }
            if (typeof(T) == typeof(double) && typeof(R) == typeof(double))
            {
                return (R)(object)((double)(object)upper - (double)(object)lower);
            }
            if (typeof(T) == typeof(decimal) && typeof(R) == typeof(decimal))
            {
                return (R)(object)((decimal)(object)upper - (decimal)(object)lower);
            }
            if (typeof(T) == typeof(DateTime) && typeof(R) == typeof(TimeSpan))
            {
                return (R)(object)((DateTime)(object)upper - (DateTime)(object)lower);
            }
            if (typeof(T) == typeof(DateTimeOffset) && typeof(R) == typeof(TimeSpan))
            {
                return (R)(object)((DateTimeOffset)(object)upper - (DateTimeOffset)(object)lower);
            }
            
            if (GenericSpecializer<T, R>.TypeImplementsISubtractable)
            {
                return ((ISubtractable<T, R>)upper).Subtract(lower);
            }
            else
            {
                throw new NotSupportedException($"{typeof(T).FullName} does not support subtraction");
            }
        }

        private static R Add<R>(R one, R two)
        {
            if (typeof(R) == typeof(sbyte))
            {
                return (R)(object)((sbyte)(object)one + (sbyte)(object)two);
            }
            if (typeof(R) == typeof(byte))
            {
                return (R)(object)((byte)(object)one + (byte)(object)two);
            }
            if (typeof(R) == typeof(short))
            {
                return (R)(object)((short)(object)one + (short)(object)two);
            }
            if (typeof(R) == typeof(ushort))
            {
                return (R)(object)((ushort)(object)one + (ushort)(object)two);
            }            
            if (typeof(R) == typeof(int))
            {
                return (R)(object)((int)(object)one + (int)(object)two);
            }
            if (typeof(R) == typeof(uint))
            {
                return (R)(object)((uint)(object)one + (uint)(object)two);
            }
            if (typeof(R) == typeof(long))
            {
                return (R)(object)((long)(object)one + (long)(object)two);
            }
            if (typeof(R) == typeof(ulong))
            {
                return (R)(object)((ulong)(object)one + (ulong)(object)two);
            }
            if (typeof(R) == typeof(float))
            {
                return (R)(object)((float)(object)one + (float)(object)two);
            }
            if (typeof(R) == typeof(double))
            {
                return (R)(object)((double)(object)one + (double)(object)two);
            }
            if (typeof(R) == typeof(decimal))
            {
                return (R)(object)((decimal)(object)one + (decimal)(object)two);
            }
            if (typeof(R) == typeof(TimeSpan))
            {
                return (R)(object)((TimeSpan)(object)one + (TimeSpan)(object)two);
            }
            
            if (GenericSpecializer<R>.TypeImplementsIAddable)
            {
                return ((IAddable<R>)one).Add(two);
            }
            else
            {
                throw new NotSupportedException($"{typeof(R).FullName} does not support addition");
            }
        }
    }

    public static class SByteLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static sbyte Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, sbyte> => Lengths.CompositeIntervalLength<T, sbyte>(interval);
    }

    public static class ByteLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static byte Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, byte> => Lengths.CompositeIntervalLength<T, byte>(interval);
    }
    
    public static class Int16Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static short Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, short> => Lengths.CompositeIntervalLength<T, short>(interval);
    }

    public static class UInt16Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static ushort Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, ushort> => Lengths.CompositeIntervalLength<T, ushort>(interval);
    }

    public static class Int32Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static int Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, int> => Lengths.CompositeIntervalLength<T, int>(interval);
    }

    public static class UInt32Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static uint Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, uint> => Lengths.CompositeIntervalLength<T, uint>(interval);
    }

    public static class Int64Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static long Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, long> => Lengths.CompositeIntervalLength<T, long>(interval);
    }

    public static class UInt64Length
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static ulong Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, ulong> => Lengths.CompositeIntervalLength<T, ulong>(interval);
    }

    public static class SingleLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static float Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, float> => Lengths.CompositeIntervalLength<T, float>(interval);
    }

    public static class DoubleLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static double Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, double> => Lengths.CompositeIntervalLength<T, double>(interval);
    }

    public static class DecimalLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static decimal Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, decimal> => Lengths.CompositeIntervalLength<T, decimal>(interval);
    }

    public static class TimeSpanLength
    {
        /// <summary>
        /// Computes the length of this interval: sum of the lengths of the continuous intervals it represents.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static TimeSpan Length<T>(this CompositeInterval<T> interval) where T : ISubtractable<T, TimeSpan> => Lengths.CompositeIntervalLength<T, TimeSpan>(interval);
    }
}