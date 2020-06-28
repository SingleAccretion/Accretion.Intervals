using System;
using System.Collections.Generic;

namespace Accretion.Intervals.StringConversion
{
    internal static partial class Parser
    {
        public static bool TryParseCompositeInterval<T, TComparer>(string input, TryParse<T> elementParser, out CompositeInterval<T, TComparer> interval) where TComparer : struct, IComparer<T> =>
            TryParseCompositeInterval(input.AsSpan(), new StringElementTryParser<T>(elementParser), out interval, out _);

        public static bool TryParseCompositeInterval<T, TComparer>(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out CompositeInterval<T, TComparer> interval) where TComparer : struct, IComparer<T> =>
            TryParseCompositeInterval(input, new SpanElementTryParser<T>(elementParser), out interval, out _);

        public static CompositeInterval<T, TComparer> ParseCompositeInterval<T, TComparer>(string input, Parse<T> elementParser) where TComparer : struct, IComparer<T> =>
            TryParseCompositeInterval<T, TComparer, StringElementParser<T>>(input.AsSpan(), new StringElementParser<T>(elementParser), out var interval, out var exception) ?
            interval : Throw.Exception<CompositeInterval<T, TComparer>>(exception);

        public static CompositeInterval<T, TComparer> ParseCompositeInterval<T, TComparer>(ReadOnlySpan<char> input, ParseSpan<T> elementParser) where TComparer : struct, IComparer<T> =>
            TryParseCompositeInterval<T, TComparer, SpanElementParser<T>>(input, new SpanElementParser<T>(elementParser), out var interval, out var exception) ?
            interval : Throw.Exception<CompositeInterval<T, TComparer>>(exception);

        private static bool TryParseCompositeInterval<T, TComparer, TParser>(ReadOnlySpan<char> input, TParser elementParser, out CompositeInterval<T, TComparer> compositeInterval, out Exception exception) where TComparer : struct, IComparer<T> where TParser : IElementParser<T>
        {
            var lexer = new Lexer(input);
            var intervals = new List<Interval<T, TComparer>>();
            compositeInterval = default;

            while (!lexer.IsDone)
            {
                var intervalContent = lexer.ConsumeContentUntil(TokenType.Union);
                if (TryParseInterval(intervalContent, elementParser, out Interval<T, TComparer> interval, out exception) && !interval.IsEmpty)
                {
                    intervals.Add(interval);
                }
                else
                {
                    return false;
                }
            }

            exception = null;
            compositeInterval = Interval.Join(intervals);
            return true;
        }
    }
}
