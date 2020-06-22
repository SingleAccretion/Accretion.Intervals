using System;
using System.Collections.Generic;

namespace Accretion.Intervals.StringConversion
{
   /* Out decision tree, optimized for best exceptions
    * E - an unexpected token
    * B - text cannot be parsed
    * G - text can be parsed
    * * - text token
    * ^ - the end token
    * 
    *     token[0]                      Content
    *      / | \                          / \
    *     E  {  \___________________     B   G
    *       / \            /        \       / \
    *      E   *          (          [     E   ,
    *         / \       / | \       / \       / \
    *        B   G     E  )  *     E   *     E   *
    *           / \      / \  \        |        / \
    *          E   }    E   ^  |    Content    B   G
    *             / \          |                  / \
    *            E   ^      Content              E  )]
    *                                              /  \
    *                                             E    ^
    * This class is a prime example of extreme overengineering. Its original version had like one core method, null-handling and probably much better performance.
    * It was also, you know, one file and had no horrific code duplication of Parser.ElementParsers.
    * It also encapsulated all parsing (had no lexer).
    * Someone, please, clean this mess up.
    */
    internal static partial class Parser
    {
        private static readonly Exception _elementParsingFailedMarker = new Exception();

        public static bool TryParseInterval<T, TComparer>(string input, TryParse<T> elementParser, out Interval<T, TComparer> interval) where TComparer : struct, IComparer<T> => 
            TryParseInterval(input.AsSpan(), new StringElementTryParser<T>(elementParser), out interval, out _);

        public static bool TryParseInterval<T, TComparer>(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out Interval<T, TComparer> interval) where TComparer : struct, IComparer<T> =>
            TryParseInterval(input, new SpanElementTryParser<T>(elementParser), out interval, out _);

        public static Interval<T, TComparer> ParseInterval<T, TComparer>(string input, Parse<T> elementParser) where TComparer : struct, IComparer<T> =>
            TryParseInterval<T, TComparer, StringElementParser<T>>(input.AsSpan(), new StringElementParser<T>(elementParser), out var interval, out var exception) ? interval : throw exception;

        public static Interval<T, TComparer> ParseInterval<T, TComparer>(ReadOnlySpan<char> input, ParseSpan<T> elementParser) where TComparer : struct, IComparer<T> =>
            TryParseInterval<T, TComparer, SpanElementParser<T>>(input, new SpanElementParser<T>(elementParser), out var interval, out var exception) ? interval : throw exception;

        private static bool TryParseInterval<T, TComparer, TParser>(ReadOnlySpan<char> input, TParser elementParser, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T> where TParser : IElementParser<T>
        {
            var lexer = new Lexer(input);
            var nextToken = lexer.NextToken();
            if (nextToken.Type == TokenType.StartSingleton)
            {
                return TryCompletingSingletonInterval(lexer, elementParser, out interval, out exception);
            }
            else if (nextToken.Type == TokenType.StartOpen)
            {
                nextToken = lexer.NextToken();

                if (nextToken.Type == TokenType.EndOpen)
                {
                    return TryCompletingEmptyInterval(lexer, out interval, out exception);
                }
                else if (nextToken.Type == TokenType.Text)
                {
                    return TryCompletingRegularInterval(lexer, elementParser, nextToken.Content, BoundaryType.Open, out interval, out exception);
                }
                else
                {
                    exception = ParsingExceptions.IntervalMustHaveBoundaries;
                }
            }
            else if (nextToken.Type == TokenType.StartClosed)
            {
                nextToken = lexer.NextToken();
                if (nextToken.Type == TokenType.Text)
                {
                    return TryCompletingRegularInterval(lexer, elementParser, nextToken.Content, BoundaryType.Closed, out interval, out exception);
                }
                else
                {
                    exception = ParsingExceptions.IntervalMustHaveBoundaries;
                }
            }
            else
            {
                exception = ParsingExceptions.InputMustStartWithValidStartCharacter;
            }

            return exception is null;
        }

        private static bool TryCompletingRegularInterval<T, TComparer, TParser>(Lexer lexer, TParser elementParser, ReadOnlySpan<char> lowerBoundaryInput, BoundaryType lowerBoundaryType, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T> where TParser : IElementParser<T>
        {
            static bool TryParseUpperBoundaryType(TokenType tokenType, out BoundaryType boundaryType)
            {
                if (tokenType == TokenType.StartClosed || tokenType == TokenType.EndClosed)
                {
                    boundaryType = BoundaryType.Closed;
                    return true;
                }

                boundaryType = default;
                return false;
            }

            Token nextToken;
            if (!elementParser.TryParse(lowerBoundaryInput, out var lowerBoundaryValue, out exception)) { }
            else if (lexer.NextToken().Type != TokenType.Separator)
            {
                exception = ParsingExceptions.BoundariesMustBeSeparated;
            }
            else if ((nextToken = lexer.NextToken()).Type != TokenType.Text)
            {
                exception = ParsingExceptions.IntervalMustHaveUpperBoundary;
            }
            else if (!elementParser.TryParse(nextToken.Content, out var upperBoundaryValue, out exception)) { }
            else if (!TryParseUpperBoundaryType(lexer.NextToken().Type, out var upperBoundaryType))
            {
                exception = ParsingExceptions.IntervalMustEndWithEndClosedOrEndOpen;
            }
            else
            {
                Interval<T, TComparer>.TryCreate(
                    new LowerBoundary<T, TComparer>(lowerBoundaryValue, lowerBoundaryType),
                    new UpperBoundary<T, TComparer>(upperBoundaryValue, upperBoundaryType),
                    out interval, out exception);
            }

            return exception is null;
        }

        private static bool TryCompletingSingletonInterval<T, TComparer, TParser>(Lexer lexer, TParser elementParser, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T> where TParser : IElementParser<T>
        {
            interval = default;

            var nextToken = lexer.NextToken();
            if (nextToken.Type != TokenType.Text)
            {
                exception = ParsingExceptions.IntervalMustHaveBoundaries;
            }
            else if (!elementParser.TryParse(nextToken.Content, out T value, out exception)) { }
            else if (lexer.NextToken().Type != TokenType.EndSingleton)
            {
                exception = ParsingExceptions.SingletonIntervalMustEndWithSingletonEnd;
            }
            else if (lexer.NextToken().Type != TokenType.End)
            {
                exception = ParsingExceptions.InputMustNotHaveTrailingCharacters;
            }
            else
            {
                Interval<T, TComparer>.TryCreate(new LowerBoundary<T, TComparer>(value, BoundaryType.Closed), new UpperBoundary<T, TComparer>(value, BoundaryType.Closed), out interval, out exception);
            }

            return exception is null;
        }

        private static bool TryCompletingEmptyInterval<T, TComparer>(Lexer lexer, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T>
        {
            if (lexer.NextToken().Type == TokenType.End)
            {
                exception = null;
                interval = Interval<T, TComparer>.Empty;
            }
            else
            {
                exception = ParsingExceptions.InputMustNotHaveTrailingCharacters;
                interval = default;
            }

            return exception is null;
        }
    }
}
