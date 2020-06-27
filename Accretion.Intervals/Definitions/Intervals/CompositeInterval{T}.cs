using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public readonly struct CompositeInterval<T> where T : IComparable<T>
    {
        public static CompositeInterval<T> Empty { get; }

        #region Parsing
        #region Parsing
        public static bool TryParse(string input, TryParse<T> elementParser, out CompositeInterval<T> interval) =>
            Interval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out interval);

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out CompositeInterval<T> interval) =>
            Interval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out interval);

        public static bool TryParse(string input, out CompositeInterval<T> interval) =>
            Interval<T, DefaultValueComparer<T>>.TryParse(input, out interval);

        public static bool TryParse(ReadOnlySpan<char> input, out CompositeInterval<T> interval) =>
            Interval<T, DefaultValueComparer<T>>.TryParse(input, out interval);

        public static CompositeInterval<T> Parse(string input, Parse<T> elementParser) =>
            Interval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static CompositeInterval<T> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser) =>
            Interval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static CompositeInterval<T> Parse(string input) =>
            Interval<T, DefaultValueComparer<T>>.Parse(input);

        public static CompositeInterval<T> Parse(ReadOnlySpan<char> input) =>
            Interval<T, DefaultValueComparer<T>>.Parse(input);
        #endregion

        public CompositeInterval<T> SymmetricDifference(CompositeInterval<T> interval) => throw new NotImplementedException();
        public CompositeInterval<T> SymmetricDifference(Interval<T> interval) => throw new NotImplementedException();
    }
}
