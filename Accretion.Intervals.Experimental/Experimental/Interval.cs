using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    /*
    public class Interval<T> where T : IComparable<T>
    {
        /// <summary>
        /// Instance of an empty <see cref="Interval{T}"/>
        /// </summary>
        public static Interval<T> EmptyInterval { get; } = new Interval<T>(new ArraySegment<Boundary<T>>(Array.Empty<Boundary<T>>()));
        /// <summary>
        /// An equality comparer that performs value comparison on instances of <see cref="Interval{T}"/>. Tests intervals starting with the lowermost boundary. Hashes in O(n) time.
        /// </summary>
        public static IEqualityComparer<Interval<T>> LinearComparerByValue { get; } = LinearComparer<T>.Instance;

        private static readonly Parser<Interval<T>, T> _parser = new Parser<Interval<T>, T>(TryParse);
        private static readonly Comparer<Boundary<T>> _constructorUnionComparer = Comparer<Boundary<T>>.Create((x, y) => x.InComparisonWith(y, new NoOverlap()));

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> class from the specified <see cref="ContinuousInterval{T}"/>s. Merges overlapping intervals if necessary.
        /// </summary>
        /// <param name="intervals">Continuous intervals. Cannot be null. Does not need to be sorted.</param>
        public Interval(params ContinuousInterval<T>[] intervals) : this(IntervalsToBoundaries(intervals)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> class from the specified enumerable of <see cref="ContinuousInterval{T}"/>. Merges overlapping intervals if necessary.
        /// </summary>
        /// <param name="intervals">Continuous intervals. Cannot be null. Does not need to be sorted.</param>
        public Interval(IEnumerable<ContinuousInterval<T>> intervals) : this(IntervalsToBoundaries(intervals)) { }

        /// <summary>
        /// Before using this constructor, be sure that boundaries are in a new array!
        /// </summary>        
        internal Interval(ArraySegment<Boundary<T>> boundaries, bool sorted = false, bool mayOverlap = true)
        {
            if (boundaries.Count == 0)
            {
                IsEmpty = true;
                Boundaries = new ArraySegment<Boundary<T>>(Array.Empty<Boundary<T>>());
                return;
            }

            if (!sorted)
            {
                Array.Sort(boundaries.Array, boundaries.Offset, boundaries.Count, _constructorUnionComparer);
            }

            if (mayOverlap)
            {
                Boundaries = UnionSortedBoundaries(boundaries);
            }
            else
            {
                Boundaries = boundaries;
            }
        }

        /// <summary>
        /// Specifies whether this interval is empty. Empty intervals contain no values.
        /// </summary>
        public bool IsEmpty { get; }

        internal ArraySegment<Boundary<T>> Boundaries { get; }

        /// <summary>
        /// Tries to convert the string representation of an interval to an instance of <see cref="Interval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, TryParse<T> tryParseElement, out Interval<T> interval)
        {            
            return _parser.TryParse(str, tryParseElement, out interval);
        }

        /// <summary>
        /// Tries to convert the string representation of an interval to an instance of <see cref="Interval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, Func<string, T> parseElement, out Interval<T> interval)
        {
            return _parser.TryParse(str, parseElement, out interval);
        }

        /// <summary>
        /// Converts the string representation of an interval to an instance of <see cref="Interval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static Interval<T> Parse(string str, TryParse<T> tryParseElement)
        {
            return _parser.Parse(str, tryParseElement);
        }

        /// <summary>
        /// Converts the string representation of an interval to an instance of <see cref="Interval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static Interval<T> Parse(string str, Func<string, T> parseElement)
        {
            return _parser.Parse(str, parseElement);
        }

        /// <summary>
        /// Determines whether this <see cref="Interval{T}"/> contains the specified value. It is an O(log n) operation.
        /// </summary>
        /// <param name="value">The value to be tested.</param>   
        public bool Contains(T value)
        {
            static bool IsBelowBoundary(T val, Boundary<T> upperBoundary) => val.IsLessThan(upperBoundary.Value) || (val.IsEqualTo(upperBoundary.Value) && upperBoundary.IsClosed);
            static bool IsAboveBoundary(T val, Boundary<T> lowerBoundary) => val.IsGreaterThan(lowerBoundary.Value) || (val.IsEqualTo(lowerBoundary.Value) && lowerBoundary.IsClosed);
            static int MeanForUpperBoundary(int lowerIndex, int upperIndex) => 2 * ((upperIndex - lowerIndex + 3) / 4) - 1 + lowerIndex;
            static int MeanForLowerBoundary(int lowerIndex, int upperIndex) => 2 * ((upperIndex - lowerIndex + 1) / 4) + lowerIndex;

            if (IsEmpty || NullChecker.IsNull(value))
            {
                return false;
            }

            var offset = Boundaries.Offset;
            var lowerBoundaryIndex = 0;
            var upperBoundaryIndex = Boundaries.Count - 1;
            int meanLowerBoundaryIndex;
            int meanUpperBoundaryIndex;

            if (!IsBelowBoundary(value, Boundaries.Array[offset + upperBoundaryIndex]) || !IsAboveBoundary(value, Boundaries.Array[offset + lowerBoundaryIndex]))
            {
                return false;
            }

            while (upperBoundaryIndex > lowerBoundaryIndex + 1)
            {
                meanUpperBoundaryIndex = MeanForUpperBoundary(lowerBoundaryIndex, upperBoundaryIndex);
                meanLowerBoundaryIndex = MeanForLowerBoundary(lowerBoundaryIndex, upperBoundaryIndex);

                if (IsBelowBoundary(value, Boundaries.Array[offset + meanUpperBoundaryIndex]))
                {
                    upperBoundaryIndex = meanUpperBoundaryIndex;
                }
                else if (IsAboveBoundary(value, Boundaries.Array[offset + meanLowerBoundaryIndex]))
                {
                    lowerBoundaryIndex = meanLowerBoundaryIndex;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that contains values from this interval and the specified one. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval<T> Union(Interval<T> otherInterval)
        {
            if (otherInterval is null)
            {
                throw new ArgumentNullException(nameof(otherInterval));
            }

            var boundaries = MergingAlgorithms.MergeSortedArrays(Boundaries.Array, Boundaries.Count, otherInterval.Boundaries.Array, otherInterval.Boundaries.Count, new NoOverlap());

            return new Interval<T>(new ArraySegment<Boundary<T>>(boundaries), sorted: true);
        }

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that only contains values present both in this and the specified interval. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>
        /// <exception cref="ArgumentNullException" />
        public Interval<T> Intersect(Interval<T> otherInterval)
        {
            if (otherInterval is null)
            {
                throw new ArgumentNullException(nameof(otherInterval));
            }

            if (IsEmpty || otherInterval.IsEmpty)
            {
                return EmptyInterval;
            }

            var boundaries = MergingAlgorithms.MergeSortedArrays(Boundaries.Array, Boundaries.Count, otherInterval.Boundaries.Array, otherInterval.Boundaries.Count, new FullOverlap());

            int depth = 0;
            var lastLowerBoundary = boundaries[0];
            var j = 0;
            
            foreach (var boundary in boundaries)
            {
                if (boundary.IsLower)
                {
                    depth++;

                    if (depth == 2)
                    {
                        lastLowerBoundary = boundary;
                    }
                }
                else
                {
                    depth--;

                    if (depth == 1)
                    {
                        if (!ContinuousInterval<T>.BoundariesProduceEmptyInterval(LowerBoundary<T>.Create(lastLowerBoundary.Value, lastLowerBoundary.IsOpen),
                                                                                  UpperBoundary<T>.Create(boundary.Value, boundary.IsOpen)))
                        {
                            boundaries[j] = lastLowerBoundary;
                            boundaries[j + 1] = boundary;
                            j += 2;
                        }                        
                    }
                }
            }
            
            return new Interval<T>(new ArraySegment<Boundary<T>>(boundaries, 0, j), sorted: true, mayOverlap: false);
        }

        /// <summary>
        /// Returns a new <see cref="Interval{T}"/> that contains values present in either this interval or the specified one, but not both. It is an O(n) operation.
        /// </summary>
        /// <param name="otherInterval">The specified interval. Cannot be null.</param>     
        /// <exception cref="ArgumentNullException" />
        public Interval<T> SymmetricDifference(Interval<T> otherInterval)
        {
            if (otherInterval is null)
            {
                throw new ArgumentNullException(nameof(otherInterval));
            }

            var boundaries = MergingAlgorithms.MergeSortedArrays(Boundaries.Array, Boundaries.Count, otherInterval.Boundaries.Array, otherInterval.Boundaries.Count, new OverlapClosed());

            if (boundaries.Length == 0)
            {
                return EmptyInterval;
            }

            int depth = 1;
            var lastLowerBoundary = boundaries[0];
            var j = 0;

            void WriteBackValidBoundaries(Boundary<T> currentBoundary)
            {
                var closingBoundary = new Boundary<T>(currentBoundary.Value, currentBoundary.IsOpen != currentBoundary.IsLower, false);

                if (!ContinuousInterval<T>.BoundariesProduceEmptyInterval(LowerBoundary<T>.Create(lastLowerBoundary.Value, lastLowerBoundary.IsOpen),
                                                                          UpperBoundary<T>.Create(closingBoundary.Value, closingBoundary.IsOpen)))
                {
                    boundaries[j] = lastLowerBoundary;
                    boundaries[j + 1] = closingBoundary;
                    j += 2;
                }
            }

            for (int i = 1; i < boundaries.Length - 1; i++)
            {
                var boundary = boundaries[i];

                depth += boundary.IsLower ? 1 : -1;

                if (depth == 1)
                {
                    lastLowerBoundary = new Boundary<T>(boundary.Value, boundary.IsOpen == boundary.IsLower, true);
                }
                else
                {
                    WriteBackValidBoundaries(boundary);
                }
            }

            WriteBackValidBoundaries(boundaries[boundaries.Length - 1]);

            return new Interval<T>(new ArraySegment<Boundary<T>>(boundaries, 0, j), sorted: true, mayOverlap: false);
        }

        /// <summary>
        /// Returns a new sorted read-only list of continuous intervals that represent this <see cref="Interval{T}"/>.
        /// </summary>        
        public IReadOnlyList<ContinuousInterval<T>> ToContinuousIntervals()
        {
            if (IsEmpty)
            {
                return new[] { ContinuousInterval<T>.EmptyInterval };
            }

            var intervals = new ContinuousInterval<T>[Boundaries.Count / 2];
            var boundariesArray = Boundaries.Array;
            var lastBoundaryIndex = Boundaries.Offset + Boundaries.Count - 1;

            for (int i = Boundaries.Offset; i <= lastBoundaryIndex; i += 2)
            {
                intervals[i] = new ContinuousInterval<T>(boundariesArray[i].Value, boundariesArray[i].IsOpen, boundariesArray[i + 1].Value, boundariesArray[i + 1].IsOpen);
            }

            return intervals;
        }

        /// <summary>
        /// Returns a string that represents this interval.
        /// </summary>   
        public override string ToString()
        {
            if (!IsEmpty)
            {
                IList<Boundary<T>> boundaries = Boundaries;
                var stringRepresentationBuilder = new StringBuilder();

                for (int i = 0; i < boundaries.Count; i += 2)
                {
                    var interval = new ContinuousInterval<T>(boundaries[i].Value, boundaries[i].IsOpen, boundaries[i + 1].Value, boundaries[i + 1].IsOpen);

                    stringRepresentationBuilder.Append(interval);
                    stringRepresentationBuilder.Append(Interval.UnionSymbol);
                }
                stringRepresentationBuilder.Remove(stringRepresentationBuilder.Length - 1, 1);

                return stringRepresentationBuilder.ToString();
            }
            else
            {
                return Interval.EmptySetSymbol.ToString();
            }
        }

        private static ArraySegment<Boundary<T>> UnionSortedBoundaries(ArraySegment<Boundary<T>> sortedBoundaries)
        {
            var offset = sortedBoundaries.Offset;
            var count = sortedBoundaries.Count;
            var boundaries = sortedBoundaries.Array;
            var j = offset;
            var depth = 1;
            var lastLowerBoundary = boundaries[offset];

            void WriteBackValidBoundaries(Boundary<T> lowerBoundary, Boundary<T> upperBoundary)
            {
                boundaries[j] = lowerBoundary;
                boundaries[j + 1] = upperBoundary;
                j += 2;
            }

            for (int i = offset + 1; i < offset + count - 1; i++)
            {
                var boundary = boundaries[i];

                if (boundary.IsLower)
                {
                    depth++;

                    if (depth == 1)
                    {
                        lastLowerBoundary = boundary;
                    }
                }
                else
                {
                    depth--;

                    if (depth == 0)
                    {
                        WriteBackValidBoundaries(lastLowerBoundary, boundary);
                    }
                }
            }

            WriteBackValidBoundaries(lastLowerBoundary, boundaries[offset + count - 1]);

            return new ArraySegment<Boundary<T>>(sortedBoundaries.Array, sortedBoundaries.Offset, j);
        }

        private static ArraySegment<Boundary<T>> IntervalsToBoundaries(IEnumerable<ContinuousInterval<T>> intervals)
        {
            if (intervals is null)
            {
                throw new ArgumentNullException(nameof(intervals));
            }

            var boundaries = new Boundary<T>[intervals.Count() * 2];

            int i = 0;
            foreach (var interval in intervals)
            {
                if (!interval.IsEmpty)
                {
                    boundaries[i] = new Boundary<T>(interval.LowerBoundary.Value, interval.LowerBoundary.IsOpen, true);
                    boundaries[i + 1] = new Boundary<T>(interval.UpperBoundary.Value, interval.UpperBoundary.IsOpen, false);
                    i += 2;
                }
            }

            return new ArraySegment<Boundary<T>>(boundaries, 0, i);
        }

        private static void TryParse(string str, out Interval<T> result, out FormatException exception, ElementParsingAction<T> tryParseElement)
        {
            exception = null;
            result = null;

            var continuousIntervalStrings = str.Split(Interval.UnionSymbol);
            var intervals = new ContinuousInterval<T>[str.Length];

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
                if (firstInterval.Boundaries.Count != secondInterval.Boundaries.Count)
                {
                    return false;
                }

                var maxFirstIndex = firstInterval.Boundaries.Offset + firstInterval.Boundaries.Count;
                for (int i = firstInterval.Boundaries.Offset, j = secondInterval.Boundaries.Offset; i < maxFirstIndex; i++, j++)
                {
                    if (firstInterval.Boundaries.Array[i] != secondInterval.Boundaries.Array[j])
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(Interval<U> interval)
            {
                var hash = new HashCode();
                var maxIndex = interval.Boundaries.Offset + interval.Boundaries.Count;
                for (int i = interval.Boundaries.Offset; i < maxIndex; i++)
                {
                    hash.Add(interval.Boundaries.Array[i].GetHashCode());
                }

                return hash.ToHashCode();
            }
        }
    }
    */
}
