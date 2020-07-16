using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract partial class IntervalTests<T, TComparer> : TestsBase where TComparer : struct, IBoundaryValueComparer<T>
    {
        private static readonly Parser<T> PlaceholderParser = new Parser<T>(x => default);

        [Fact]
        public void ParsingMethodsThrowArgumentNullExceptionOnNullInputs()
        {
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.Parse(input: null));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.Parse(input: null, PlaceholderParser.ParseString));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.TryParse(input: null, out _));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.TryParse(input: null, PlaceholderParser.TryParseString, out _));
        }

        [Fact]
        public void ParsingMethodsThrowArgumentNullExceptionOnNullParsers()
        {
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.Parse(string.Empty, elementParser: null));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.Parse(ReadOnlySpan<char>.Empty, elementParser: null));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.TryParse(string.Empty, elementParser: null, out _));
            Assert.Throws<ArgumentNullException>(() => Interval<T, TComparer>.TryParse(null, elementParser: null, out _));
        }

        [Property]
        public Property SimpleParseSpanIsEquivalentToFullParseSpan(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span)).Equals(
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span, GetSimpleParser<ParseSpan<T>>("Parse")))).ToProperty();

        [Property]
        public Property SimpleParseStringIsEquivalentToFullParseString(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.String)).Equals(
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.String, GetSimpleParser<Parse<T>>("Parse")))).ToProperty();

        [Property]
        public Property FullParseStringIsEquivalentToFullParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser) =>
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.String, parser.ParseString)).Equals(
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span, parser.ParseSpan))).
            When(intervalString.String != null);

        [Property]
        public Property SimpleTryParseSpanIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, GetSimpleParser<TryParseSpan<T>>("TryParse"), out var interval), interval))).
            Or(TypeHasNoSimpleParser<TryParseSpan<T>>("TryParse"));

        [Property]
        public Property SimpleTryParseStringIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.String, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, GetSimpleParser<TryParseSpan<T>>("TryParse"), out var interval), interval))).
            When(intervalString.String != null).
            Or(TypeHasNoSimpleParser<TryParse<T>>("TryParse"));

        [Property]
        public Property FullTryParseStringIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.String, parser.TryParseString, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, parser.TryParseSpan, out var interval), interval))).
            When(intervalString.String != null);

        [Property]
        public Property FullTryParseSpanIsEquivalentToFullParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser)
        {
            var parseResult = Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span, parser.ParseSpan));

            return (Interval<T, TComparer>.TryParse(intervalString.Span, parser.TryParseSpan, out var triedInterval) ?
                    parseResult.HasValue && parseResult.Value == triedInterval : !parseResult.HasValue).
                    Or(!parser.IsSupported);
        }

        private static bool TypeHasNoSimpleParser<TParser>(string name) where TParser : Delegate => !TryGetSimpleParser<TParser>(name, out _);

        private static TParser GetSimpleParser<TParser>(string name) where TParser : Delegate => TryGetSimpleParser<TParser>(name, out var parser) ? parser : throw new InvalidOperationException($"{typeof(T)} does not have a suitable simple parser.");

        private static bool TryGetSimpleParser<TParser>(string name, out TParser parser) where TParser : Delegate
        {
            TParser CreateDelegate(MethodInfo methodInfo)
            {
                var result = Delegate.CreateDelegate(typeof(TParser), methodInfo, throwOnBindFailure: false);
                if (result is null)
                {
                    
                }

                return (TParser)result;
            }

            var type = typeof(T);
            var parserMethod = typeof(TParser).GetMethod("Invoke");
            var methods = from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                          where method.Name == name
                          where method.ReturnType == parserMethod.ReturnType
                          let parameters = method.GetParameters().Where(x => !x.IsOptional)
                          let parserParameters = parserMethod.GetParameters().Where(x => !x.IsOptional)
                          where parameters.Count() == parserParameters.Count() &&
                                parameters.Zip(parserParameters).
                                All(x => x.First.ParameterType == x.Second.ParameterType && x.First.IsOut == x.Second.IsOut)
                          select method;
            
            parser = methods.Count() == 1 ? CreateDelegate(methods.Single()) : null;
            return parser is null;
        }
    }
}