using System;

namespace Accretion.Intervals
{
    internal delegate void ParsingAction<T, E>(string str, out T result, out FormatException exception, ElementParsingAction<E> tryParseElement);
}
