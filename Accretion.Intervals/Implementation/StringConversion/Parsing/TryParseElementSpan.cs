using System;

namespace Accretion.Intervals.StringConversion
{
    internal delegate bool TryParseElementSpan<T>(ReadOnlySpan<char> input, out T value, out Exception exception);
}
