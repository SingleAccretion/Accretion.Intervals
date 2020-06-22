using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.StringConversion
{
    //Parser-specific infrastructure to avoid allocations when converting between parsing delegates.
    //Unfortunately, the specific parsers cannot be generic because the underlying delegates cannot be, which is a consequence of Span not being legal as a type parameter
    internal static partial class Parser
    {
        private interface IElementParser<T>
        {
            bool TryParse(ReadOnlySpan<char> input, out T value, out Exception exception);
        }

        private readonly struct StringElementParser<T> : IElementParser<T>
        {
            private readonly Parse<T> _parse;

            public StringElementParser(Parse<T> parser) => _parse = parser;

            public bool TryParse(ReadOnlySpan<char> input, out T value, out Exception exception)
            {
                try
                {
                    value = _parse(input.ToString());
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
        }

        private readonly struct SpanElementParser<T> : IElementParser<T>
        {
            private readonly ParseSpan<T> _parseSpan;

            public SpanElementParser(ParseSpan<T> parser) => _parseSpan = parser;

            public bool TryParse(ReadOnlySpan<char> input, out T value, out Exception exception)
            {
                try
                {
                    value = _parseSpan(input);
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
        }

        private readonly struct StringElementTryParser<T> : IElementParser<T>
        {
            private readonly TryParse<T> _tryParse;

            public StringElementTryParser(TryParse<T> parser) => _tryParse = parser;

            public bool TryParse(ReadOnlySpan<char> input, out T value, out Exception exception)
            {
                if (_tryParse(input.ToString(), out value))
                {
                    exception = null;
                    return true;
                }
                else
                {
                    exception = _elementParsingFailedMarker;
                    return true;
                }
            }
        }

        private readonly struct SpanElementTryParser<T> : IElementParser<T>
        {
            private readonly TryParseSpan<T> _tryParse;

            public SpanElementTryParser(TryParseSpan<T> parser) => _tryParse = parser;

            public bool TryParse(ReadOnlySpan<char> input, out T value, out Exception exception)
            {
                if (_tryParse(input, out value))
                {
                    exception = null;
                    return true;
                }
                else
                {
                    exception = _elementParsingFailedMarker;
                    return true;
                }
            }
        }
    }
}
