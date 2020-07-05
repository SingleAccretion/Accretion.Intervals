using System;
using System.Globalization;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract class IntervalTests<T, TComparer> : TestsBase where TComparer : struct, IBoundaryValueComparer<T>
    {
        [Fact]
        public void DefaultIntervalIsEmptyInterval() => Assert.Equal(default, Interval<T, TComparer>.Empty);

        [Fact]
        public void EmptyIntervalIsEmpty() => Assert.True(Interval<T, TComparer>.Empty.IsEmpty);

        [Property]
        public void AllIntervalsNotEqualToEmptyIntervalAreNotEmpty(Interval<T, TComparer> interval) =>
            (interval.IsEmpty || !interval.Equals(Interval<T, TComparer>.Empty)).ToProperty();

        [Property]
        public Property AllNotEmptyIntervalsHaveBoundariesWhileEmptyOnesDont(Interval<T, TComparer> interval)
        {
            var lowerBoundaryResult = Result.From(() => interval.LowerBoundary);
            var upperBoundaryResult = Result.From(() => interval.UpperBoundary);

            if (interval.IsEmpty)
            {
                return (!lowerBoundaryResult.HasValue && lowerBoundaryResult.Exception is InvalidOperationException &&
                        !upperBoundaryResult.HasValue && upperBoundaryResult.Exception is InvalidOperationException).ToProperty();
            }
            else
            {
                return (lowerBoundaryResult.HasValue && upperBoundaryResult.HasValue).ToProperty();
            }
        }

        [Property]
        public Property LowerBoundaryPropertyIsIdempotent(Interval<T, TComparer> interval) =>
            Result.From(() => interval.LowerBoundary).Equals(Result.From(() => interval.LowerBoundary)).ToProperty();

        [Property]
        public Property UpperBoundaryPropertyIsIdempotent(Interval<T, TComparer> interval) =>
            Result.From(() => interval.UpperBoundary).Equals(Result.From(() => interval.UpperBoundary)).ToProperty();

        [Property]
        public Property EqualityIsCommutative(Interval<T, TComparer> left, Interval<T, TComparer> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property UnequalIntervalsMustBeDifferent(Interval<T, TComparer> left, Interval<T, TComparer> right) =>
            (!left.Equals(right)).Implies
            (!Result.From(() => left.LowerBoundary).Equals(Result.From(() => right.LowerBoundary)) ||
             !Result.From(() => left.UpperBoundary).Equals(Result.From(() => right.UpperBoundary)));

        [Property(EndSize = 100)]
        public Property EqualIntervalsMustHaveEqualHashCodes(Interval<T, TComparer>[] intervals) =>
            intervals.All(x => intervals.All(y => !x.Equals(y) || x.GetHashCode() == y.GetHashCode())).ToProperty();

        [Property]
        public Property ToStringDefaultDoesNotChangeWithCulture(Interval<T, TComparer> interval, CultureInfo cultureInfo)
        {
            var inititalString = interval.ToString();
            var initialCulture = CultureInfo.CurrentCulture;

            CultureInfo.CurrentCulture = cultureInfo;
            var changedString = interval.ToString();
            CultureInfo.CurrentCulture = initialCulture;
            
            return inititalString.Equals(changedString).ToProperty();
        }

        [Property]
        public Property ToStringOutputIsDifferentForDifferentFormats(Interval<T, TComparer> interval, FormatString firstFormat, FormatString secondFormat) =>
            (!firstFormat.Equals(secondFormat)).
            Implies(!Result.From(() => interval.ToString(firstFormat, null)).Equals(Result.From(() => interval.ToString(secondFormat, null)))).
            When(!interval.IsEmpty && 
                 !Result.From(() => default(TComparer).ToString(interval.LowerBoundary.Value, firstFormat, null)).Equals(
                  Result.From(() => default(TComparer).ToString(interval.LowerBoundary.Value, secondFormat, null))) &&
                 !Result.From(() => default(TComparer).ToString(interval.UpperBoundary.Value, firstFormat, null)).Equals(
                  Result.From(() => default(TComparer).ToString(interval.UpperBoundary.Value, secondFormat, null)))).
            Or(interval.IsEmpty || !(interval.LowerBoundary.Value is IFormattable));

        [Property]
        public Property ToStringOutputIsDifferentForDifferentFormatProviders(Interval<T, TComparer> interval, CultureInfo firstCulture, CultureInfo secondCulture, FormatString format) =>
            (!firstCulture.Equals(secondCulture)).
            Implies(!Result.From(() => interval.ToString(format, firstCulture)).Equals(Result.From(() => interval.ToString(format, secondCulture)))).
            When(!interval.IsEmpty &&
                 !Result.From(() => default(TComparer).ToString(interval.LowerBoundary.Value, format, firstCulture)).Equals(
                  Result.From(() => default(TComparer).ToString(interval.LowerBoundary.Value, format, secondCulture))) &&
                 !Result.From(() => default(TComparer).ToString(interval.UpperBoundary.Value, format, firstCulture)).Equals(
                  Result.From(() => default(TComparer).ToString(interval.UpperBoundary.Value, format, secondCulture)))).
            Or(interval.IsEmpty || !(interval.LowerBoundary.Value is IFormattable));

        [Property]
        public Property EmptyIntervalsContainNoValues(T value) =>
            (!Interval<T, TComparer>.Empty.Contains(value)).ToProperty();

        [Property]
        public Property OpenIntervalsDoNotContainTheirBoundaries(Interval<T, TComparer> interval) => interval.IsEmpty ? true.ToProperty() :
            interval.LowerBoundary.IsOpen.Implies(!interval.Contains(interval.LowerBoundary.Value)).And(interval.UpperBoundary.IsOpen.Implies(!interval.Contains(interval.UpperBoundary.Value)));

        [Property]
        public Property ClosedIntervalsContainTheirBoundaries(Interval<T, TComparer> interval) => interval.IsEmpty ? true.ToProperty() :
            interval.LowerBoundary.IsClosed.Implies(interval.Contains(interval.LowerBoundary.Value)).And(interval.UpperBoundary.IsClosed.Implies(interval.Contains(interval.UpperBoundary.Value)));

        [Property]
        public Property IntervalsDoNotContainValuesLessThanTheirLowerBoundary(Interval<T, TComparer> interval, ValidBoundaryValue<T, TComparer> value) => interval.IsEmpty ? true.ToProperty() :
            value.Value.IsLessThan<T, TComparer>(interval.LowerBoundary.Value).Implies(!interval.Contains(value));

        [Property]
        public Property IntervalsDoNotContainValuesGreaterThanTheirUpperBoundary(Interval<T, TComparer> interval, ValidBoundaryValue<T, TComparer> value) => interval.IsEmpty ? true.ToProperty() :
            value.Value.IsGreaterThan<T, TComparer>(interval.UpperBoundary.Value).Implies(!interval.Contains(value));

        [Property]
        public Property IntervalsContainValidValuesBetweenTheirBoundaries(Interval<T, TComparer> interval, ValidBoundaryValue<T, TComparer> value) => interval.IsEmpty ? true.ToProperty() :
           (value.Value.IsLessThan<T, TComparer>(interval.UpperBoundary.Value) && value.Value.IsGreaterThan<T, TComparer>(interval.LowerBoundary.Value)).Implies(interval.Contains(value));

        [Property]
        public Property IntervalsDoNotContainInvalidValues(Interval<T, TComparer> interval, InvalidBoundaryValue<T, TComparer> value) =>
            (value.DoesExist && !interval.Contains(value.Value)).Or(!value.DoesExist);
    }
}
