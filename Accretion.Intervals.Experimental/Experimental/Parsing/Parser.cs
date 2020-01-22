using System;

namespace Accretion.Intervals.Experimental
{
    internal class Parser<T, E>
    {
        private static readonly FormatException _elementCouldNotBeParsed = new FormatException("Element could not be parsed from the input string");

        private readonly ParsingAction<T, E> _tryParse;

        public Parser(ParsingAction<T, E> tryParse)
        {
            _tryParse = tryParse;
        }

        public bool TryParse(string str, TryParse<E> tryParseElement, out T result)
        {
            if (tryParseElement is null)
            {
                throw new ArgumentNullException(nameof(tryParseElement));
            }

            TryParse(str, out result, out var exception, tryParseElement: tryParseElement);

            return exception == null;
        }

        public bool TryParse(string str, Func<string, E> parseElement, out T result)
        {
            if (parseElement is null)
            {
                throw new ArgumentNullException(nameof(parseElement));
            }

            TryParse(str, out result, out var exception, parseElement: parseElement);

            return exception == null;
        }

        public T Parse(string str, TryParse<E> tryParseElement)
        {
            if (tryParseElement is null)
            {
                throw new ArgumentNullException(nameof(tryParseElement));
            }

            TryParse(str, out var result, out var exception, tryParseElement: tryParseElement);

            return exception == null ? result : throw exception;
        }

        public T Parse(string str, Func<string, E> parseElement)
        {
            if (parseElement is null)
            {
                throw new ArgumentNullException(nameof(parseElement));
            }

            TryParse(str, out var result, out var exception, parseElement: parseElement);

            return exception == null ? result : throw exception;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private void TryParse(string str, out T result, out FormatException exception, TryParse<E> tryParseElement = null, Func<string, E> parseElement = null)
        {
            void TryParseElement(string elementString, out E element, out Exception elementParsingException)
            {
                if (tryParseElement != null)
                {
                    elementParsingException = tryParseElement(elementString, out element) ? null : _elementCouldNotBeParsed;
                }
                else
                {
                    try
                    {
                        element = parseElement(elementString);
                        elementParsingException = null;
                    }
                    catch (Exception ex)
                    {
                        element = default;
                        elementParsingException = ex;                        
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Input string cannot be empty or null", nameof(str));
            }

            _tryParse(str, out result, out exception, TryParseElement);
        }
    }
}
