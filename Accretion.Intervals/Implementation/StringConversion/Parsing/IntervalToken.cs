using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Accretion.Intervals.StringConversion
{
    internal readonly ref struct Token
    {
        public Token(ReadOnlySpan<char> content, TokenType type)
        {
            Content = content;
            Type = type;
        }

        public ReadOnlySpan<char> Content { get; }
        public TokenType Type { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => throw new NotSupportedException("ByRefLike types do not support Equals() and GetHashCode()");
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => throw new NotSupportedException("ByRefLike types do not support Equals() and GetHashCode()");

        public bool Equals(Token other) => Content.SequenceEqual(other.Content) && Type == other.Type;
        public override string ToString() => $"{Type}: {Content.ToString()}";

        public static bool operator ==(Token left, Token right) => left.Equals(right);
        public static bool operator !=(Token left, Token right) => !(left == right);
    }
}
