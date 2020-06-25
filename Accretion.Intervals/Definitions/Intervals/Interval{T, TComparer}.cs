using Accretion.Intervals.StringConversion;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct Interval<T, TComparer> where TComparer : struct, IComparer<T>
    {
        private readonly LowerBoundary<T, TComparer> _lowerBoundary;
        private readonly UpperBoundary<T, TComparer> _upperBoundary;

        private Interval(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary)
        {
            _lowerBoundary = lowerBoundary;
            _upperBoundary = upperBoundary;
        }

        public static Interval<T, TComparer> Empty { get; }

        public bool IsEmpty => throw new NotImplementedException();

        public LowerBoundary<T, TComparer> LowerBoundary => throw new NotImplementedException();
        public UpperBoundary<T, TComparer> UpperBoundary => throw new NotImplementedException();
        
        #region Parsing
        public static bool TryParse(string input, TryParse<T> elementParser, out Interval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.TryParseInterval(input, elementParser, out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out Interval<T, TComparer> interval)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.TryParseInterval(input, elementParser, out interval);
        }

        public static bool TryParse(string input, out Interval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }

            return Parser.TryParseInterval(input, ElementParsers.GetTryElementParser<T>(), out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, out Interval<T, TComparer> interval) => Parser.TryParseInterval(input, ElementParsers.GetTrySpanElementParser<T>(), out interval);

        public static Interval<T, TComparer> Parse(string input, Parse<T> elementParser)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseInterval<T, TComparer>(input, elementParser);
        }

        public static Interval<T, TComparer> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseInterval<T, TComparer>(input, elementParser);
        }

        public static Interval<T, TComparer> Parse(string input)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }

            return Parser.ParseInterval<T, TComparer>(input, ElementParsers.GetElementParser<T>());
        }

        public static Interval<T, TComparer> Parse(ReadOnlySpan<char> input) => Parser.ParseInterval<T, TComparer>(input, ElementParsers.GetSpanElementParser<T>());
        #endregion Parsing

        public bool Contains(T value) => throw new NotImplementedException();

        public override string ToString() => $"{LowerBoundary}{Symbols.GetSymbol(TokenType.Separator)}{UpperBoundary}";

        internal static Interval<T, TComparer> CreateUnchecked(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary) => new Interval<T, TComparer>(lowerBoundary, upperBoundary);

        internal static bool TryCreate(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary, out Interval<T, TComparer> interval, out Exception exception)
        {
            throw null;
        }
    }
}