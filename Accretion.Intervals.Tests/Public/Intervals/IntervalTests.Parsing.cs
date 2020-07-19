using System;
using System.Linq;
using System.Reflection;
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
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.String, GetSimpleParser<Parse<T>>("Parse")))).
            When(intervalString.String != null);

        [Property]
        public Property FullParseStringIsEquivalentToFullParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser) =>
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.String, parser.ParseString)).Equals(
            Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span, parser.ParseSpan))).
            When(intervalString.String != null).
            Or(!parser.IsSupported);

        [Property]
        public Property SimpleTryParseSpanIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, GetSimpleParser<TryParseSpan<T>>("TryParse"), out var interval), interval))).ToProperty();

        [Property]
        public Property SimpleTryParseStringIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.String, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, GetSimpleParser<TryParseSpan<T>>("TryParse"), out var interval), interval))).
            When(intervalString.String != null);

        [Property]
        public Property FullTryParseStringIsEquivalentToFullTryParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser) =>
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.String, parser.TryParseString, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, TComparer>.TryParse(intervalString.Span, parser.TryParseSpan, out var interval), interval))).
            When(intervalString.String != null).
            Or(!parser.IsSupported);

        [Property]
        public Property FullTryParseSpanIsEquivalentToFullParseSpan(IntervalString<T, TComparer> intervalString, Parser<T> parser)
        {
            var parseResult = Result.From(() => Interval<T, TComparer>.Parse(intervalString.Span, parser.ParseSpan));

            return (Interval<T, TComparer>.TryParse(intervalString.Span, parser.TryParseSpan, out var triedInterval) ?
                    parseResult.HasValue && parseResult.Value == triedInterval : !parseResult.HasValue).
                    Or(!parser.IsSupported);
        }

        [Property]
        public Property ParseCanParseToStringOutput(Interval<T, TComparer> interval, Parser<T> parser) => 
            Interval<T, TComparer>.Parse(interval.ToString()).Equals(interval).Or(!parser.IsSupported);

        private static TParser GetSimpleParser<TParser>(string name) where TParser : Delegate
        {
            var type = typeof(T);
            var parserMethod = typeof(TParser).GetMethod("Invoke");
            var parserParameters = parserMethod.GetParameters();

            var methods = (from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                           where method.Name == name
                           where method.ReturnType == parserMethod.ReturnType
                           let parameters = method.GetParameters()
                           let requiredParameters = parameters.Where(x => !x.HasDefaultValue)
                           where requiredParameters.Count() == parserParameters.Length &&
                                 requiredParameters.Zip(parserParameters).
                                 All(x => x.First.ParameterType == x.Second.ParameterType && x.First.IsOut == x.Second.IsOut)
                           group method by parameters.Length into overloads
                           orderby overloads.Key
                           select overloads).
                           First();

            if (methods.Count() == 0)
            {
                throw new InvalidOperationException($"{type} has no suitable parser methods.");
            }
            if (methods.Count() > 1)
            {
                throw new InvalidOperationException($"{type} has too many suitable parser methods.");
            }

            //We depend here on the correctness of ShimGenerator, which is probably one of the most complex APIs in the library
            //This is only possible because it is tested separately
            return ShimGenerator.WithDefaultParametersPassed<TParser>(methods.Single());
        }
    }
}
