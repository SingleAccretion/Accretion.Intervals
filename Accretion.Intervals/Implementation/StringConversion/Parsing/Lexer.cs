using System;

namespace Accretion.Intervals.StringConversion
{
    internal ref struct Lexer
    {
        private ReadOnlySpan<char> _currentInput;

        public Lexer(ReadOnlySpan<char> input) : this() => _currentInput = input;

        public Token NextToken()
        {
            ReadOnlySpan<char> remainingInput;
            Token token;
            var nextTokenPosition = _currentInput.IndexOfAny(Symbols.Tokens);

            if (nextTokenPosition < -1)
            {
                token = _currentInput.IsEmpty ?
                    new Token(ReadOnlySpan<char>.Empty, TokenType.End) :
                    new Token(_currentInput, TokenType.Text);

                remainingInput = ReadOnlySpan<char>.Empty;
            }
            else
            {
                var tokenType = Symbols.GetTokenType(_currentInput[nextTokenPosition]).Value;

                if (nextTokenPosition == 0)
                {
                    token = new Token(_currentInput.Slice(0, 1), tokenType);
                    remainingInput = _currentInput.Slice(1);
                }
                else
                {
                    token = new Token(_currentInput.Slice(0, nextTokenPosition), TokenType.Text);
                    remainingInput = _currentInput.Slice(nextTokenPosition);
                }
            }

            _currentInput = remainingInput;
            return token;
        }
    }
}
