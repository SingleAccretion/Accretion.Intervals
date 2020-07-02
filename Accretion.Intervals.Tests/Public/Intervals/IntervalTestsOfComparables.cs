using System;
using FsCheck;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract class IntervalTests<T> : IntervalTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        public Property IntervalOfComparablesIsEmptyDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.IsEmpty).Equals(Result.From(() => ((Interval<T>)interval).IsEmpty)).ToProperty();

        public Property IntervalOfComparablesLowerBoundaryDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.LowerBoundary).Equals(Result.From(() => ((Interval<T>)interval).LowerBoundary)).ToProperty();

        public Property IntervalOfComparablesUpperBoundaryDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.UpperBoundary).Equals(Result.From(() => ((Interval<T>)interval).UpperBoundary)).ToProperty();

        public Property IntervalOfComparablesContainsDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval, T value) =>
            Result.From(() => interval.Contains(value)).Equals(Result.From(() => ((Interval<T>)interval).Contains(value))).ToProperty();

        public Property IntervalOfComparablesEqualsDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> left, Interval<T, DefaultValueComparer<T>> rigth) =>
            Result.From(() => left.Equals(rigth)).Equals(Result.From(() => ((Interval<T>)left).Equals(rigth))).ToProperty();

        public Property IntervalOfComparablesGetHashCodeDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.GetHashCode()).Equals(Result.From(() => ((Interval<T>)interval).GetHashCode())).ToProperty();

        public Property IntervalOfComparablesToStringDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.ToString()).Equals(Result.From(() => ((Interval<T>)interval).ToString())).ToProperty();
    }

    public class IntervalOfDoubleTests : IntervalTests<double> { }
    public class IntervalOfSingleTests : IntervalTests<float> { }
    public class IntervalOfInt32Tests : IntervalTests<int> { }
    public class IntervalOfDateTimeTests : IntervalTests<DateTime> { }
    public class IntervalOfValueClassTests : IntervalTests<ValueClass> { }
    public class IntervalOfValueStructTests : IntervalTests<ValueStruct> { }
}
