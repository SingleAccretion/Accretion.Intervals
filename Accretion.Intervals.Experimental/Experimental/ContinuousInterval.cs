using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals.Experimental
{
    public readonly struct ContinuousInterval<T> : IEquatable<ContinuousInterval<T>> where T : IComparable<T>
    {
        private static readonly Parser<ContinuousInterval<T>, T> _parser = new Parser<ContinuousInterval<T>, T>(TryParse);

        /// <summary>
        /// Instance of an empty <see cref="ContinuousInterval{T}"/>
        /// </summary>
        public static readonly ContinuousInterval<T> EmptyInterval = new ContinuousInterval<T>();

        private readonly bool _isNotEmpty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinuousInterval{T}"/> class with the specified boundaries.         
        /// </summary>
        /// <param name="lowerBoundaryValue">Value of the lower boundary. Null, NaN or PositiveInfinity will result in an empty interval.</param>
        /// <param name="lowerBoundaryIsOpen">Specifies whether lower boundary will be open. Intervals with open boundaries don't contain values of these boundaries.</param>
        /// <param name="upperBoundaryValue">Value of the upper boundary. Null, NaN or NegativeInfinity will result in an empty interval.</param>
        /// <param name="upperBoundaryIsOpen">Specifies whether upper boundary will be open. Intervals with open boundaries don't contain values of these boundaries.</param>
        public ContinuousInterval(T lowerBoundaryValue, bool lowerBoundaryIsOpen, T upperBoundaryValue, bool upperBoundaryIsOpen)
        {            
            var lowerBoundaryIsValid = LowerBoundary<T>.TryCreate(lowerBoundaryValue, lowerBoundaryIsOpen, out var lowerBoundary);
            var upperBoundaryIsValid = UpperBoundary<T>.TryCreate(upperBoundaryValue, upperBoundaryIsOpen, out var upperBoundary);

            _isNotEmpty = lowerBoundaryIsValid && upperBoundaryIsValid && !BoundariesProduceEmptyInterval(lowerBoundary, upperBoundary);

            if (_isNotEmpty)
            {
                LowerBoundary = lowerBoundary;
                UpperBoundary = upperBoundary;
            }
            else
            {
                UpperBoundary = default;
                LowerBoundary = default;
            }
        }

        /// <summary>
        /// Specifies whether this interval is empty. Empty intervals contain no values.
        /// </summary>
        public bool IsEmpty { get => !_isNotEmpty; }

        internal LowerBoundary<T> LowerBoundary { get; }
        internal UpperBoundary<T> UpperBoundary { get; }

        /// <summary>
        /// Tries to convert the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, TryParse<T> tryParseElement, out ContinuousInterval<T> interval)
        {
            return _parser.TryParse(str, tryParseElement, out interval);
        }

        /// <summary>
        /// Tries to convert the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. Returns whether the operation was successful.
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <param name="interval">Result of conversion.</param>        
        public static bool TryParse(string str, Func<string, T> parseElement, out ContinuousInterval<T> interval)
        {
            return _parser.TryParse(str, parseElement, out interval);
        }

        /// <summary>
        /// Converts the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="tryParseElement">Function that tries to convert the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static ContinuousInterval<T> Parse(string str, TryParse<T> tryParseElement)
        {
            return _parser.Parse(str, tryParseElement);
        }

        /// <summary>
        /// Converts the string representation of a continuous interval to an instance of <see cref="ContinuousInterval{T}"/>. 
        /// </summary>
        /// <param name="str">String representation of the interval.</param>
        /// <param name="parseElement">Function that converts the string representation of <see cref="T"/> to an instance of <see cref="T"/>.</param>
        /// <exception cref="FormatException" />
        public static ContinuousInterval<T> Parse(string str, Func<string, T> parseElement)
        {
            return _parser.Parse(str, parseElement);
        }
           
        /// <summary>
        /// Determines whether this <see cref="ContinuousInterval{T}"/> contains the specified value. It is an O(1) operation.
        /// </summary>
        /// <param name="value">The value to be tested.</param>        
        public bool Contains(T value)
        {
            if (IsEmpty || (GenericSpecializer<T>.TypeInstanceCanBeNull && NullChecker.IsNull(value)))
            {
                return false;
            }

            return (value.IsGreaterThan(LowerBoundary.Value) && value.IsLessThan(UpperBoundary.Value)) ||
                   (value.IsEqualTo(LowerBoundary.Value) && LowerBoundary.IsClosed) ||
                   (value.IsEqualTo(UpperBoundary.Value) && UpperBoundary.IsClosed);
        }

        public bool Equals(ContinuousInterval<T> other) => LowerBoundary == other.LowerBoundary && UpperBoundary == other.UpperBoundary;

        public override bool Equals(object obj) => obj is ContinuousInterval<T> interval && Equals(interval);

        public override int GetHashCode() => HashCode.Combine(LowerBoundary, UpperBoundary);

        /// <summary>
        /// Returns a string that represents this interval.
        /// </summary>        
        public override string ToString()
        {
            return IsEmpty ?
                   Interval.EmptySetSymbol.ToString() :
                   LowerBoundary.IsClosed && UpperBoundary.IsClosed && LowerBoundary.Value.IsEqualTo(UpperBoundary.Value) ?
                   $"{Interval.LeftSingleElementSetBrace}{LowerBoundary.Value}{Interval.RightSingleElementSetBrace}" :
                   $"{LowerBoundary}{Interval.SeparatorSymbol}{UpperBoundary}";
        }

        internal static void TryParse(string str, out ContinuousInterval<T> result, out FormatException exception, ElementParsingAction<T> tryParseElement)
        {
            exception = null;
            result = default;

            str = str.Trim();

            if (str == Interval.EmptySetSymbol.ToString())
            {
                result = EmptyInterval;
                return;
            }

            if (str.Length < 3)
            {
                exception = ParsingExceptions.StringTooSmall;
                return;
            }

            var stringLength = str.Length;

            var firstCharacter = str[0];
            var lastCharacter = str[stringLength - 1];

            if (firstCharacter != Interval.LeftClosedBoundarySymbol && firstCharacter != Interval.LeftOpenBoundarySymbol && firstCharacter != Interval.LeftSingleElementSetBrace)
            {
                exception = ParsingExceptions.InvalidFirstCharacter;
                return;
            }
            var lowerBoundaryIsOpen = firstCharacter == Interval.LeftOpenBoundarySymbol;

            if (lastCharacter != Interval.RightClosedBoundarySymbol && lastCharacter != Interval.RightOpenBoundarySymbol && lastCharacter != Interval.RightSingleElementSetBrace)
            {
                exception = ParsingExceptions.InvalidLastCharacter;
                return;
            }
            var upperBoundaryIsOpen = lastCharacter == Interval.RightOpenBoundarySymbol;

            if ((firstCharacter == Interval.LeftSingleElementSetBrace || lastCharacter == Interval.RightSingleElementSetBrace) &&
               !(firstCharacter == Interval.LeftSingleElementSetBrace && lastCharacter == Interval.RightSingleElementSetBrace))
            {
                exception = ParsingExceptions.BracesUsedIncorrectly;
                return;
            }
            var isOneElementInterval = firstCharacter == Interval.LeftSingleElementSetBrace && lastCharacter == Interval.RightSingleElementSetBrace;

            var separatorIndex = str.IndexOf(Interval.SeparatorSymbol);
            if (separatorIndex != str.LastIndexOf(Interval.SeparatorSymbol) || (separatorIndex < 0 && !isOneElementInterval))
            {
                exception = ParsingExceptions.NoSeparator;
                return;
            }

            T lowerBoundaryValue;
            T upperBoundaryValue;
            if (!isOneElementInterval)
            {
                tryParseElement(str.Substring(1, separatorIndex - 1), out lowerBoundaryValue, out var lowerBoundaryValueParsingException);
                if (lowerBoundaryValueParsingException != null)
                {
                    exception = ParsingExceptions.InvalidLowerBoundary(lowerBoundaryValueParsingException);
                    return;
                }

                tryParseElement(str.Substring(separatorIndex + 1, stringLength - separatorIndex - 2), out upperBoundaryValue, out var upperBoundaryValueParsingException);
                if (upperBoundaryValueParsingException != null)
                {
                    exception = ParsingExceptions.InvalidUpperBoundary(upperBoundaryValueParsingException);
                    return;
                }
            }
            else
            {
                tryParseElement(str.Substring(1, stringLength - 2), out lowerBoundaryValue, out var singleElementParsingException);

                if (singleElementParsingException != null)
                {
                    exception = ParsingExceptions.InvalidSingleElement(singleElementParsingException);
                    return;
                }

                upperBoundaryValue = lowerBoundaryValue;
            }

            result = new ContinuousInterval<T>(lowerBoundaryValue, lowerBoundaryIsOpen, upperBoundaryValue, upperBoundaryIsOpen);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool BoundariesProduceEmptyInterval(LowerBoundary<T> lowerBoundary, UpperBoundary<T> upperBoundary)
        {
            if (GenericSpecializer<T>.TypeIsDiscrete)
            {
                return lowerBoundary.ReducedValue().IsGreaterThan(upperBoundary.ReducedValue());
            }
            else
            {
                return lowerBoundary.Value.IsGreaterThan(upperBoundary.Value) ||
                      (lowerBoundary.Value.IsEqualTo(upperBoundary.Value) && (lowerBoundary.IsOpen || upperBoundary.IsOpen));
            }
        }        

        public static bool operator ==(ContinuousInterval<T> first, ContinuousInterval<T> second) => first.Equals(second);

        public static bool operator !=(ContinuousInterval<T> first, ContinuousInterval<T> second) => !first.Equals(second);
    }
}
