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
    }

    public class IntervalOfDoubleCreateTests : IntervalCreateTests<double> { }
    public class IntervalOfSingleCreateTests : IntervalCreateTests<float> { }
    public class IntervalOfInt32CreateTests : IntervalCreateTests<int> { }
    public class IntervalOfValueStructCreateTests : IntervalCreateTests<ValueStruct> { }
    public class IntervalOfValueClassCreateTests : IntervalCreateTests<ValueClass> { }
}
