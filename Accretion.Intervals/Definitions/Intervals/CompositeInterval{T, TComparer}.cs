using Accretion.Intervals.StringConversion;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public readonly struct CompositeInterval<T, TComparer> where TComparer : struct, IComparer<T> 
    {
        public static CompositeInterval<T, TComparer> Empty { get; }

        public static bool TryParse(string input, TryParse<T> elementParser, out CompositeInterval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            throw new NotImplementedException();
            //return Parser.TryParseCompositeInterval(input, elementParser, out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out CompositeInterval<T, TComparer> interval)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }
            
            return Parser.TryParseCompositeInterval(input, elementParser, out interval);
        }

        public static bool TryParse(string input, out CompositeInterval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            
            return Parser.TryParseCompositeInterval(input, ElementParsers.GetTryElementParser<T>(), out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, out CompositeInterval<T, TComparer> interval) => Parser.TryParseCompositeInterval(input, ElementParsers.GetTrySpanElementParser<T>(), out interval);

        public static CompositeInterval<T, TComparer> Parse(string input, Parse<T> elementParser)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseCompositeInterval<T, TComparer>(input, elementParser);
        }

        public static CompositeInterval<T, TComparer> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseCompositeInterval<T, TComparer>(input, elementParser);
        }

        public static CompositeInterval<T, TComparer> Parse(string input)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }

            return Parser.ParseCompositeInterval<T, TComparer>(input, ElementParsers.GetElementParser<T>());
        }

        public static CompositeInterval<T, TComparer> Parse(ReadOnlySpan<char> input) => Parser.ParseCompositeInterval<T, TComparer>(input, ElementParsers.GetSpanElementParser<T>());
    }
}
