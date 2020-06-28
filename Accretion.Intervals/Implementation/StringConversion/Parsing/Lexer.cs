using System;

namespace Accretion.Intervals.StringConversion
{
    internal ref struct Lexer
    {
        private readonly ReadOnlySpan<char> _input;
        private int _currentPosition;

        public Lexer(ReadOnlySpan<char> input) : this() => _input = input.Trim();

        public bool IsDone => _currentPosition == _input.Length;

        public Token ConsumeNext()
        {
            var nextToken = PeekNextToken();
            _currentPosition += nextToken.Content.Length;

            return nextToken;
        }

        public ReadOnlySpan<char> ConsumeContentUntil(TokenType tokenType)
        {
            var initialPosition = _currentPosition;
            var consumedLength = 0;
            
            for (Token nextToken = PeekNextToken(); nextToken.Type != TokenType.End && nextToken.Type != tokenType; nextToken = PeekNextToken())
            {
                var contentLength = nextToken.Content.Length;

                _currentPosition += contentLength;
                consumedLength += contentLength;
            }

            return _input.Slice(initialPosition, consumedLength);
        }

        private Token PeekNextToken()
        {
            Token token;
            var inputAhead = _input.Slice(_currentPosition);
            var nextTokenPosition = inputAhead.IndexOfAny(Symbols.Tokens);

            if (nextTokenPosition < -1)
            {
                token = inputAhead.IsEmpty ? new Token(ReadOnlySpan<char>.Empty, TokenType.End) : new Token(inputAhead, TokenType.Text);
            }
            else
            {
                var tokenType = Symbols.GetTokenType(inputAhead[nextTokenPosition]).Value;

                if (nextTokenPosition == 0)
                {
                    token = new Token(inputAhead.Slice(0, 1), tokenType);
                }
                else
                {
                    token = new Token(inputAhead.Slice(0, nextTokenPosition), TokenType.Text);
                }
            }

            return token;
        }
    }
}
