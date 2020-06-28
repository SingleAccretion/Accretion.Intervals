using System;
using System.Collections.Generic;

namespace Accretion.Intervals.StringConversion
{
    internal static partial class Parser
    {
        private static bool TryParseCompositeInterval<T, TComparer, TParser>(ReadOnlySpan<char> input, TParser elementParser, out CompositeInterval<T, TComparer> compositeInterval, out Exception exception) where TComparer : struct, IComparer<T> where TParser : IElementParser<T>
        {
            var lexer = new Lexer(input);
            var intervals = new List<Interval<T, TComparer>>();

            while (!lexer.IsDone)
            {
                var intervalContent = lexer.ConsumeContentUntil(TokenType.Union);
                if (TryParseInterval(intervalContent, elementParser, out Interval<T, TComparer> interval, out exception))
                {
                    if (!interval.IsEmpty)
                    {
                        intervals.Add(interval);
                    }
                }
                else
                {
                    return false;
                }
            }

            compositeInterval = Interval.Join(intervals);
            exception = null;
            return true;
        }
    }
}
