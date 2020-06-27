using System;

namespace Accretion.Intervals
{
    public readonly struct CompositeInterval<T> where T : IComparable<T>
    {
        public static CompositeInterval<T> Empty { get; }

        public static bool TryParse(string input, TryParse<T> elementParser, out CompositeInterval<T> interval)
        {
            var result = CompositeInterval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out CompositeInterval<T> interval)
        {
            var result = CompositeInterval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(string input, out CompositeInterval<T> interval)
        {
            var result = CompositeInterval<T, DefaultValueComparer<T>>.TryParse(input, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(ReadOnlySpan<char> input, out CompositeInterval<T> interval)
        {
            var result = CompositeInterval<T, DefaultValueComparer<T>>.TryParse(input, out var comparerInterval);
            interval = comparerInterval;
            return result;            
        }

        public static CompositeInterval<T> Parse(string input, Parse<T> elementParser) => CompositeInterval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static CompositeInterval<T> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser) => CompositeInterval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static CompositeInterval<T> Parse(string input) => CompositeInterval<T, DefaultValueComparer<T>>.Parse(input);

        public static CompositeInterval<T> Parse(ReadOnlySpan<char> input) => CompositeInterval<T, DefaultValueComparer<T>>.Parse(input);

        public CompositeInterval<T> SymmetricDifference(CompositeInterval<T> interval) => throw new NotImplementedException();
        public CompositeInterval<T> SymmetricDifference(Interval<T> interval) => throw new NotImplementedException();

        public static implicit operator CompositeInterval<T>(CompositeInterval<T, DefaultValueComparer<T>> interval) => throw new NotImplementedException();
        public static implicit operator CompositeInterval<T, DefaultValueComparer<T>>(CompositeInterval<T> interval) => throw new NotImplementedException();
    }
}