using System;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.CreatingIntervals
{
    public abstract class IntervalCreateTests<T> : IntervalCreateTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property CreateOverloadForComparablesDelegatesToComparerBasedOne(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) =>
            Result.From(() => (Interval<T>)Interval.Create<T, DefaultValueComparer<T>>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType)).
            Equals(Result.From(() => Interval.Create(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType))).ToProperty();

        [Property]
        public Property CreateClosedOverloadForComparablesDelegatesToComparerBasedOne(T lowerBoundaryValue, T upperBoundaryValue) =>
            Result.From(() => (Interval<T>)Interval.CreateClosed<T, DefaultValueComparer<T>>(lowerBoundaryValue, upperBoundaryValue)).
            Equals(Result.From(() => Interval.CreateClosed(lowerBoundaryValue, upperBoundaryValue))).ToProperty();

        [Property]
        public Property CreateOpenOverloadForComparablesDelegatesToComparerBasedOne(T lowerBoundaryValue, T upperBoundaryValue) =>
            Result.From(() => (Interval<T>)Interval.CreateOpen<T, DefaultValueComparer<T>>(lowerBoundaryValue, upperBoundaryValue)).
            Equals(Result.From(() => Interval.CreateOpen(lowerBoundaryValue, upperBoundaryValue))).ToProperty();

        [Property]
        public Property CreateSingletonOverloadForComparablesDelegatesToComparerBasedOne(T value) =>
            Result.From(() => (Interval<T>)Interval.CreateSingleton<T, DefaultValueComparer<T>>(value)).
            Equals(Result.From(() => Interval.CreateSingleton(value))).ToProperty();
    }

    public class IntervalOfDoubleCreateTests : IntervalCreateTests<double> { }
    public class IntervalOfSingleCreateTests : IntervalCreateTests<float> { }
    public class IntervalOfDateTimeCreateTests : IntervalCreateTests<DateTime> { }
    public class IntervalOfInt32CreateTests : IntervalCreateTests<int> { }
    public class IntervalOfValueStructCreateTests : IntervalCreateTests<ValueStruct> { }
    public class IntervalOfValueClassCreateTests : IntervalCreateTests<ValueClass> { }
}
