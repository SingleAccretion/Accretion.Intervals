using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct Parser<T> : IEquatable<Parser<T>>
    {
        private readonly ParseSpan<T> _parser;

        public Parser(ParseSpan<T> parser) => _parser = parser;

        public bool IsSupported => _parser != null;

        public T ParseSpan(ReadOnlySpan<char> input) => IsSupported ? _parser(input) : throw new InvalidOperationException($"Parsing {typeof(T)} not supported");

        public T ParseString(string input) => ParseSpan(input);

        public bool TryParseSpan(ReadOnlySpan<char> input, out T value)
        {
            try
            {
                value = ParseSpan(input);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }

        public bool TryParseString(string input, out T value) => TryParseSpan(input, out value);

        public override bool Equals(object obj) => obj is Parser<T> parser && Equals(parser);
        public bool Equals(Parser<T> other) => _parser == other._parser && IsSupported == other.IsSupported;
        public override int GetHashCode() => HashCode.Combine(_parser, IsSupported);
        public override string ToString() => $"{typeof(T)} parser";

        public static bool operator ==(Parser<T> left, Parser<T> right) => left.Equals(right);
        public static bool operator !=(Parser<T> left, Parser<T> right) => !(left == right);
    }
}
