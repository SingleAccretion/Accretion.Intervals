using System;
using System.Globalization;
using FsCheck;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract class IntervalTests<T> : IntervalTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        public Property IntervalOfComparablesIsEmptyDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.IsEmpty.Equals(((Interval<T>)interval).IsEmpty).ToProperty();

        public Property IntervalOfComparablesLowerBoundaryDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.LowerBoundary).Equals(Result.From(() => ((Interval<T>)interval).LowerBoundary)).ToProperty();

        public Property IntervalOfComparablesUpperBoundaryDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            Result.From(() => interval.UpperBoundary).Equals(Result.From(() => ((Interval<T>)interval).UpperBoundary)).ToProperty();

        public Property IntervalOfComparablesContainsDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval, T value) =>
            interval.Contains(value).Equals(((Interval<T>)interval).Contains(value)).ToProperty();

        public Property IntervalOfComparablesEqualsDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> left, Interval<T, DefaultValueComparer<T>> rigth) =>
            left.Equals(rigth).Equals(((Interval<T>)left).Equals(rigth)).ToProperty();

        public Property IntervalOfComparablesGetHashCodeDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.GetHashCode().Equals(((Interval<T>)interval).GetHashCode()).ToProperty();

        public Property IntervalOfComparablesToStringDefaultDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval) =>
            interval.ToString().Equals(((Interval<T>)interval).ToString()).ToProperty();

        public Property IntervalOfComparablesToStringDelegatesToComparerBasedOne(Interval<T, DefaultValueComparer<T>> interval, FormatString format, CultureInfo cultureInfo) =>
            interval.ToString(format, cultureInfo).Equals(((Interval<T>)interval).ToString(format, cultureInfo)).ToProperty();

        public Property ImplicitConversionsWork(Interval<T, DefaultValueComparer<T>> left, Interval<T, DefaultValueComparer<T>> right) =>
            (((Interval<T>)left).Equals(right) == right.Equals((Interval<T>)left)).ToProperty();
    }

    public class IntervalOfDoubleTests : IntervalTests<double> { }
    public class IntervalOfSingleTests : IntervalTests<float> { }
    public class IntervalOfInt32Tests : IntervalTests<int> { }
    public class IntervalOfDateTimeTests : IntervalTests<DateTime> { }
    public class IntervalOfValueClassTests : IntervalTests<ValueClass> { }
    public class IntervalOfValueStructTests : IntervalTests<ValueStruct> { }
}
