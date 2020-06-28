using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Accretion.Intervals.Experimental")]
[assembly: InternalsVisibleTo("Accretion.Intervals.Tests")]
[assembly: InternalsVisibleTo("Accretion.Profiling")]
namespace Accretion.Intervals
{
    public readonly struct ContinuousInterval<T> : IEquatable<ContinuousInterval<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Instance of an empty <see cref="ContinuousInterval{T}"/>
        /// </summary>
        public static readonly ContinuousInterval<T> EmptyInterval = new ContinuousInterval<T>();

        /// <summary>
        /// Specifies whether this interval is empty. Empty intervals contain no values.
        /// </summary>
        public bool IsEmpty => Checker.IsDefault(this);

        internal LowerBoundary<T> LowerBoundary { get; }
        internal UpperBoundary<T> UpperBoundary { get; }

        /// <summary>
        /// Tries to convert the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        //public static bool TryParse(string str, TryParse<T> tryParseElement, out ContinuousInterval<T> interval) => _parser.TryParse(str, tryParseElement, out interval);

        /// <summary>
        /// Tries to convert the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        //public static bool TryParse(string str, Func<string, T> parseElement, out ContinuousInterval<T> interval) => _parser.TryParse(str, parseElement, out interval);

        /// <summary>
        /// Converts the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        //public static ContinuousInterval<T> Parse(string str, TryParse<T> tryParseElement) => _parser.Parse(str, tryParseElement);

        /// <summary>
        /// Converts the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        //public static ContinuousInterval<T> Parse(string str, Func<string, T> parseElement) => _parser.Parse(str, parseElement);

        /// <summary>
        /// Determines whether this <see cref="ContinuousInterval{T}"/> contains the specified value. It is an O(1) operation.
        /// </summary>
        /// <param name="value">The value to be tested.</param>        
        public bool Contains(T value)
        {
            if (IsEmpty || Checker.IsNull(value))
            {
                return false;
            }
            throw null;
            /*return (value.IsGreaterThan(LowerBoundary.Value) && value.IsLessThan(UpperBoundary.Value)) ||
                   (value.IsEqualTo(LowerBoundary.Value) && LowerBoundary.IsClosed) ||
                   (value.IsEqualTo(UpperBoundary.Value) && UpperBoundary.IsClosed);*/
        }

        public bool Equals(ContinuousInterval<T> other) => LowerBoundary == other.LowerBoundary && UpperBoundary == other.UpperBoundary;

        public override bool Equals(object obj) => obj is ContinuousInterval<T> interval && Equals(interval);

        public override int GetHashCode() => HashCode.Combine(LowerBoundary, UpperBoundary);

        /// <summary>
        /// Returns a string that represents this interval.
        /// </summary>
        public override string ToString() => throw null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool BoundariesProduceEmptyInterval(LowerBoundary<T> lowerBoundary, UpperBoundary<T> upperBoundary)
        {
            throw null;
            //return lowerBoundary.Value.IsGreaterThan(upperBoundary.Value) || (lowerBoundary.Value.IsEqualTo(upperBoundary.Value) && (lowerBoundary.IsOpen || upperBoundary.IsOpen));
        }        

        public static bool operator ==(ContinuousInterval<T> first, ContinuousInterval<T> second) => first.Equals(second);

        public static bool operator !=(ContinuousInterval<T> first, ContinuousInterval<T> second) => !first.Equals(second);
    }
}
