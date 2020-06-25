using System;

namespace Accretion.Intervals
{
    public delegate bool TryParse<T>(string value, out T result);
    public delegate bool TryParseSpan<T>(ReadOnlySpan<char> span, out T result);
    
    public delegate T Parse<T>(string value);
    public delegate T ParseSpan<T>(ReadOnlySpan<char> span);
}