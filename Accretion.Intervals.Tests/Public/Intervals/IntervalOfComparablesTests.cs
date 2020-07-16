using System;
using System.Globalization;
using FsCheck;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract partial class IntervalTests<T> : IntervalTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        public Property IntervalOfComparablesIsEmptyIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.IsEmpty.Equals(((Interval<T>)interval).IsEmpty).ToProperty();

        public Property IntervalOfComparablesLowerBoundaryIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.LowerBoundary).Equals(Result.From(() => ((Interval<T>)interval).LowerBoundary)).ToProperty();

        public Property IntervalOfComparablesUpperBoundaryIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.UpperBoundary).Equals(Result.From(() => ((Interval<T>)interval).UpperBoundary)).ToProperty();

        public Property IntervalOfComparablesContainsIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval, T value) =>
            interval.Contains(value).Equals(((Interval<T>)interval).Contains(value)).ToProperty();

        public Property IntervalOfComparablesEqualsIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> left, Interval<T, DefaultValueComparer<T>> rigth) =>
            left.Equals(rigth).Equals(((Interval<T>)left).Equals(rigth)).ToProperty();

        public Property IntervalOfComparablesGetHashCodeIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.GetHashCode().Equals(((Interval<T>)interval).GetHashCode()).ToProperty();

        public Property IntervalOfComparablesToStringDefaultIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.ToString().Equals(((Interval<T>)interval).ToString()).ToProperty();

        public Property IntervalOfComparablesToStringIsEquivalentToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval, FormatString format, CultureInfo cultureInfo) =>
            interval.ToString(format, cultureInfo).Equals(((Interval<T>)interval).ToString(format, cultureInfo)).ToProperty();

        public Property FullParseSpanOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString, Parser<T> parser) =>
            Result.From(() => Interval<T>.Parse(intervalString.Span, parser.ParseSpan)).Equals(
            Result.From(() => Interval<T, DefaultValueComparer<T>>.Parse(intervalString.Span, parser.ParseSpan))).ToProperty();

        public Property FullParseStringOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString, Parser<T> parser) =>
            Result.From(() => Interval<T>.Parse(intervalString.String, parser.ParseString)).Equals(
            Result.From(() => Interval<T, DefaultValueComparer<T>>.Parse(intervalString.String, parser.ParseString))).ToProperty();

        public Property SimpleParseSpanOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString) =>
            Result.From(() => Interval<T>.Parse(intervalString.Span)).Equals(
            Result.From(() => Interval<T, DefaultValueComparer<T>>.Parse(intervalString.Span))).ToProperty();

        public Property SimpleParseStringOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString) =>
            Result.From(() => Interval<T>.Parse(intervalString.String)).Equals(
            Result.From(() => Interval<T, DefaultValueComparer<T>>.Parse(intervalString.String))).ToProperty();

        public Property FullTryParseSpanOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString, Parser<T> parser) =>
            Result.From(() => (Interval<T>.TryParse(intervalString.Span, parser.TryParseSpan, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, DefaultValueComparer<T>>.TryParse(intervalString.Span, parser.TryParseSpan, out var interval), interval))).ToProperty();

        public Property FullTryParseStringOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString, Parser<T> parser) =>
            Result.From(() => (Interval<T>.TryParse(intervalString.String, parser.TryParseString, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, DefaultValueComparer<T>>.TryParse(intervalString.String, parser.TryParseString, out var interval), interval))).ToProperty();

        public Property SimpleTryParseSpanOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString) =>
            Result.From(() => (Interval<T>.TryParse(intervalString.Span, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, DefaultValueComparer<T>>.TryParse(intervalString.Span, out var interval), interval))).ToProperty();

        public Property SimpleTryParseStringOfComparablesIsEquivalentToComparerBasedOne(IntervalString<T, DefaultValueComparer<T>> intervalString) =>
            Result.From(() => (Interval<T>.TryParse(intervalString.String, out var interval), interval)).Equals(
            Result.From(() => (Interval<T, DefaultValueComparer<T>>.TryParse(intervalString.String, out var interval), interval))).ToProperty();

        public Property ImplicitConversionsWork(Interval<T, DefaultValueComparer<T>> left, Interval<T, DefaultValueComparer<T>> right) =>
            (((Interval<T>)left).Equals(right) == right.Equals((Interval<T>)left)).ToProperty();
    }

    public class IntervalOfSingleTests : IntervalTests<float> { }
    public class IntervalOfDoubleTests : IntervalTests<double> { }
    public class IntervalOfDecimalTests : IntervalTests<decimal> { }
    public class IntervalOfInt32Tests : IntervalTests<int> { }
    public class IntervalOfDateTimeTests : IntervalTests<DateTime> { }
    public class IntervalOfValueClassTests : IntervalTests<ValueClass> { }
    public class IntervalOfValueStructTests : IntervalTests<ValueStruct> { }
}
