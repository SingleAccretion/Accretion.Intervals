using System;

namespace Accretion.Intervals.StringConversion
{
    internal static class Symbols
    {
        public static ReadOnlySpan<char> Tokens => new char[] { ',', '|', '(', ')', '[', ']', '{', '}' };

        public static string EmptySetString { get; } = GetSymbol(TokenType.StartOpen) + GetSymbol(TokenType.EndOpen);

        public static string GetSymbol(TokenType tokenType) => tokenType switch
        {
            TokenType.Separator => ",",
            TokenType.Union => "|",
            TokenType.StartOpen => "(",
            TokenType.EndOpen => ")",
            TokenType.StartClosed => "[",
            TokenType.EndClosed => "]",
            TokenType.StartSingleton => "{",
            TokenType.EndSingleton => "}",
            _ => null
        };

        public static TokenType? GetTokenType(char input) => input switch
        {
            ',' => TokenType.Separator,
            '|' => TokenType.Union,
            '(' => TokenType.StartOpen,
            ')' => TokenType.EndOpen,
            '[' => TokenType.StartClosed,
            ']' => TokenType.EndClosed,
            '{' => TokenType.StartSingleton,
            '}' => TokenType.EndSingleton,
            _ => null
        };
    }
}