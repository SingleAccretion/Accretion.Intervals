using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<Parser<T>> Parsers<T>() => Arb.From(
            from value in Arb.Generate<T>()
            select default(Parser<T>));
    }
}
