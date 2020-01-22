using System;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals.Experimental
{
    public static class Reduces
    {
        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>     
        /// <exception cref="ArgumentNullException" />
        public static Interval<sbyte> Reduce(this Interval<sbyte> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<byte> Reduce(this Interval<byte> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        /// <exception cref="ArgumentNullException" />
        public static Interval<short> Reduce(this Interval<short> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        /// <exception cref="ArgumentNullException" />
        public static Interval<ushort> Reduce(this Interval<ushort> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        /// <exception cref="ArgumentNullException" />
        public static Interval<char> Reduce(this Interval<char> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static Interval<int> Reduce(this Interval<int> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<uint> Reduce(this Interval<uint> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>     
        /// <exception cref="ArgumentNullException" />
        public static Interval<long> Reduce(this Interval<long> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>   
        /// <exception cref="ArgumentNullException" />
        public static Interval<ulong> Reduce(this Interval<ulong> interval) => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>   
        /// <exception cref="ArgumentNullException" />
        public static Interval<T> Reduce<T>(this Interval<T> interval) where T : IDiscreteValue<T> => ReduceInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<sbyte> Reduce(this ContinuousInterval<sbyte> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<byte> Reduce(this ContinuousInterval<byte> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<short> Reduce(this ContinuousInterval<short> interval) => ReduceContinuousInterval(interval);
        
        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<ushort> Reduce(this ContinuousInterval<ushort> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<char> Reduce(this ContinuousInterval<char> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<int> Reduce(this ContinuousInterval<int> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<uint> Reduce(this ContinuousInterval<uint> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<long> Reduce(this ContinuousInterval<long> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones. 
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<ulong> Reduce(this ContinuousInterval<ulong> interval) => ReduceContinuousInterval(interval);

        /// <summary>
        /// Returns a new identical continuous interval, but with open boundaries eliminated or replaced with closed ones.
        /// </summary>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ContinuousInterval<T> Reduce<T>(this ContinuousInterval<T> interval) where T : IDiscreteValue<T> => ReduceContinuousInterval(interval);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ContinuousInterval<T> ReduceContinuousInterval<T>(ContinuousInterval<T> interval) where T : IComparable<T>
        {
            if (GenericSpecializer<T>.TypeInstanceCanBeNull && interval.IsEmpty)
            {
                return ContinuousInterval<T>.EmptyInterval;
            }

            return new ContinuousInterval<T>(interval.LowerBoundary.ReducedValue(), false, interval.UpperBoundary.ReducedValue(), false);
        }

        private static Interval<T> ReduceInterval<T>(Interval<T> interval) where T : IComparable<T>
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var packedBoundaries = new Boundary<T>[interval.Boundaries.Count];
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;
            int j = 0;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                //packedBoundaries[j] = new Boundary<T>(boundariesArray[i].ReducedLowerValue(), false, true);
                //packedBoundaries[j + 1] = new Boundary<T>(boundariesArray[i + 1].ReducedUpperValue(), false, false);
                j += 2;
            }

            return new Interval<T>(new ArraySegment<Boundary<T>>(packedBoundaries, 0, j), sorted: true, mayOverlap: false);
        }
    }
}