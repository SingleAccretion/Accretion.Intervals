using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    /*
    public class Interval<T> where T : IComparable<T>
    {
        /// <summary>
        /// Instance of an empty <see cref="Interval{T}"/>
        /// </summary>
        public static Interval<T> EmptyInterval { get; } = new Interval<T>(ReadOnlyArray<ContinuousInterval<T>>.Empty);
        /// <summary>
        /// An equality comparer that performs value comparison on instances of <see cref="Interval{T}"/>. Tests intervals starting with the lowermost boundary. Hashes in O(n) time.
        /// </summary>
        public static IEqualityComparer<Interval<T>> LinearComparerByValue { get; } = LinearComparer<T>.Instance;

        private static readonly Parser<Interval<T>, T> _parser = new Parser<Interval<T>, T>(TryParse);

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> class from the specified <see cref="ContinuousInterval{T}"/>s. Merges overlapping intervals if necessary.
        /// </summary>
        /// <param name="intervals">Continuous intervals. Cannot be null. Does not need to be sorted.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval(params ContinuousInterval<T>[] intervals) : this(IntervalOperations.Merge(intervals)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> class from the specified enumerable of <see cref="ContinuousInterval{T}"/>. Merges overlapping intervals if necessary.
        /// </summary>
        /// <param name="intervals">Continuous intervals. Cannot be null. Does not need to be sorted.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval(IEnumerable<ContinuousInterval<T>> intervals) : this(IntervalOperations.Merge(intervals)) { }

        internal Interval(ReadOnlyArray<ContinuousInterval<T>> intervals) => Intervals = intervals;

        /// <summary>
        /// Specifies whether this interval is empty. Empty intervals contain no values.
        /// </summary>
        public bool IsEmpty => Intervals.Count == 0;

        internal readonly ReadOnlyArray<ContinuousInterval<T>> Intervals;

        /// <summary>
        /// Tries to convert the string representation of an interval to an instance of <see cref="Interval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, TryParse<T> tryParseElement, out Interval<T> interval) => _parser.TryParse(str, tryParseElement, out interval);

        /// <summary>
        /// Tries to convert the string representation of an interval to an instance of <see cref="Interval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, Func<string, T> parseElement, out Interval<T> interval) => _parser.TryParse(str, parseElement, out interval);

        /// <summary>
        /// Converts the string representation of an interval to an instance of <see cref="Interval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static Interval<T> Parse(string str, TryParse<T> tryParseElement) => _parser.Parse(str, tryParseElement);

        /// <summary>
        /// Converts the string representation of an interval to an instance of <see cref="Interval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static Interval<T> Parse(string str, Func<string, T> parseElement) => _parser.Parse(str, parseElement);

        /// <summary>
        /// Determines whether this <see cref="Interval{T}"/> contains the specified value. It is an O(log n) operation.
        /// </summary>
        /// <param name="value">The value to be tested.</param>   
        public bool Contains(T value) => IntervalOperations.Contains(Intervals, value);

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that contains values from this interval and the specified one. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval<T> Union(Interval<T> otherInterval) => otherInterval is null ? throw new ArgumentNullException(nameof(otherInterval)) : new Interval<T>(IntervalOperations.Union(Intervals, otherInterval.Intervals));

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that only contains values present both in this and the specified interval. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval<T> Intersect(Interval<T> otherInterval) => otherInterval is null ? throw new ArgumentNullException(nameof(otherInterval)) : new Interval<T>(IntervalOperations.Intersect(Intervals, otherInterval.Intervals));

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that contains values present in either this interval or the specified one, but not both. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>     
        /// <exception cref="ArgumentNullException" />
        public Interval<T> SymmetricDifference(Interval<T> otherInterval) => otherInterval is null ? throw new ArgumentNullException(nameof(otherInterval)) : new Interval<T>(IntervalOperations.Merge(Intervals, otherInterval.Intervals, AlgorithmDescriptions<T>.SymmetricDifference));

        public IReadOnlyList<ContinuousInterval<T>> ToContinuousIntervals() => Intervals;

        /// <summary>
        /// Returns a string that represents this interval.
        /// </summary>   
        public override string ToString() => IsEmpty ? IntervalSymbols.EmptySetString.ToString() : string.Join(IntervalSymbols.UnionSymbol.ToString(), Intervals);

        private static void TryParse(string str, out Interval<T> result, out FormatException exception, ElementParsingAction<T> tryParseElement)
        {
            exception = null;
            result = null;

            var continuousIntervalStrings = str.Split(IntervalSymbols.UnionSymbol);
            var intervals = new ContinuousInterval<T>[continuousIntervalStrings.Length];

            for (int i = 0; i < continuousIntervalStrings.Length; i++)
            {
                var continuousIntervalString = continuousIntervalStrings[i];
                ContinuousInterval<T>.TryParse(continuousIntervalString, out var continuousInterval, out var continuousIntervalException, tryParseElement);

                if (continuousIntervalException == null)
                {
                    intervals[i] = continuousInterval;
                }
                else
                {
                    exception = ParsingExceptions.CannotParseOneOfContinuousIntervals(continuousIntervalException);
                    return;
                }
            }

            result = new Interval<T>(intervals);
        }

        private class LinearComparer<U> : IEqualityComparer<Interval<U>> where U : IComparable<U>
        {
            public static LinearComparer<U> Instance { get; } = new LinearComparer<U>();

            private LinearComparer() { }

            public bool Equals(Interval<U> firstInterval, Interval<U> secondInterval)
            {
                if (firstInterval.Intervals.Count != secondInterval.Intervals.Count)
                {
                    return false;
                }

                for (int i = 0, j = 0; i < firstInterval.Intervals.Count; i++, j++)
                {
                    if (firstInterval.Intervals[i] != secondInterval.Intervals[j])
                    {
                        return false;
                    }
                }
                
                return true;
            }

            public int GetHashCode(Interval<U> interval)
            {
                var hash = new HashCode();

                foreach (var continuousInterval in interval.Intervals)
                {
                    hash.Add(continuousInterval);
                }

                return hash.ToHashCode();
            }
        }
    }
    */
}
