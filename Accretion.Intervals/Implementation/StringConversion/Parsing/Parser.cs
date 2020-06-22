using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Transactions;

namespace Accretion.Intervals.StringConversion
{
    internal static class Parser
    {
        private static readonly Exception _elementParsingFailed = new Exception();
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
         */

        public static Interval<T, TComparer> ParseInterval<T, TComparer>(string input, Parse<T> elementParser) where TComparer : struct, IComparer<T>
        {
            bool TryParseElement(ReadOnlySpan<char> input, out T value, out Exception exception)
            {
                try
                {
                    //This causes an allocation + copy, but that's unavoidable given the signature of Parse<T>
                    //It also causes a closure over elementParser
                    value = elementParser(input.ToString());
                    exception = null;
                    return true;
                }
                catch (Exception ex)
                {
                    value = default;
                    exception = ex;
                    return false;
                }
            }

            //This allocates a closure + delegate which is suboptimal and could perhaps be avoided
            return TryParseInterval<T, TComparer>(input.AsSpan(), TryParseElement, out var interval, out var exception) ? interval : throw exception;
        }

        private static bool TryParseInterval<T, TComparer>(ReadOnlySpan<char> input, TryParseElementSpan<T> elementParser, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T>
        {
            var lexer = new Lexer(input);
            exception = null;

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

        private static bool TryCompletingRegularInterval<T, TComparer>(Lexer lexer, TryParseElementSpan<T> elementParser, ReadOnlySpan<char> lowerBoundaryInput, BoundaryType lowerBoundaryType, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T>
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
            if (!elementParser(lowerBoundaryInput, out var lowerBoundaryValue, out exception)) { }
            else if (lexer.NextToken().Type != TokenType.Separator)
            {
                exception = ParsingExceptions.BoundariesMustBeSeparated;
            }
            else if ((nextToken = lexer.NextToken()).Type != TokenType.Text)
            {
                exception = ParsingExceptions.IntervalMustHaveUpperBoundary;
            }
            else if (!elementParser(nextToken.Content, out var upperBoundaryValue, out exception)) { }
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

        private static bool TryCompletingSingletonInterval<T, TComparer>(Lexer lexer, TryParseElementSpan<T> elementParser, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T>
        {
            interval = default;

            var nextToken = lexer.NextToken();
            if (nextToken.Type != TokenType.Text)
            {
                exception = ParsingExceptions.IntervalMustHaveBoundaries;
            }
            else if (!elementParser(nextToken.Content, out T value, out exception)) { }
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
