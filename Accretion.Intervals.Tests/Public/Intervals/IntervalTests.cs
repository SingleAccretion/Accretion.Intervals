using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract class IntervalTests<T, TComparer> : TestsBase where TComparer : struct, IComparer<T>
    {
        [Fact]
        public void DefaultIntervalIsEmptyInterval() => Assert.Equal(default, Interval<T, TComparer>.Empty);

        [Fact]
        public void EmptyIntervalIsEmpty() => Assert.True(Interval<T, TComparer>.Empty.IsEmpty);

        [Property]
        public void AllIntervalsNotEqualToEmptyIntervalAreNotEmpty(Interval<T, TComparer> interval) => (interval.IsEmpty || !interval.Equals(Interval<T, TComparer>.Empty)).ToProperty();

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
                return (lowerBoundaryResult.HasValue && upperBoundaryResult.HasValue &&
                        Facts.BoundariesAreValid(lowerBoundaryResult.Value, upperBoundaryResult.Value)).ToProperty();
            }
        }

        [Property]
        public Property LowerBoundaryPropertyIsIdempotent(Interval<T, TComparer> boundary) =>
            Result.From(() => boundary.LowerBoundary).Equals(Result.From(() => boundary.LowerBoundary)).ToProperty();

        [Property]
        public Property UpperBoundaryPropertyIsIdempotent(Interval<T, TComparer> boundary) =>
            Result.From(() => boundary.UpperBoundary).Equals(Result.From(() => boundary.UpperBoundary)).ToProperty();

        [Property]
        public Property EqualityIsCommutative(Interval<T, TComparer> left, Interval<T, TComparer> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property UnequalIntervalsMustBeDifferent(Interval<T, TComparer> left, Interval<T, TComparer> right) =>
            (!left.Equals(right)).Implies
            (!Result.From(() => left.LowerBoundary).Equals(Result.From(() => right.LowerBoundary)) ||
             !Result.From(() => left.UpperBoundary).Equals(Result.From(() => right.UpperBoundary)));

        [Property(StartSize = 0, EndSize = 100)]
        public Property EqualIntervalsMustHaveEqualHashCodes(Interval<T, TComparer>[] intervals) =>
            intervals.All(x => intervals.All(y => !x.Equals(y) || x.GetHashCode() == y.GetHashCode())).ToProperty();

        [Property]
        public Property ToStringEqualityIsBoundToIntervalEquality(Interval<T, TComparer> left, Interval<T, TComparer> rigth) =>
            (left.Equals(rigth) == left.ToString().Equals(rigth.ToString())).ToProperty();

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
        public Property IntervalsDoNotContainValuesLessThanTheirLowerBoundary(Interval<T, TComparer> interval, T value) => interval.IsEmpty ? true.ToProperty() :
            value.IsLessThan<T, TComparer>(interval.LowerBoundary.Value).Implies(!interval.Contains(value));

        [Property]
        public Property IntervalsDoNotContainValuesGreaterThanTheirUpperBoundary(Interval<T, TComparer> interval, T value) => interval.IsEmpty ? true.ToProperty() :
            value.IsGreaterThan<T, TComparer>(interval.UpperBoundary.Value).Implies(!interval.Contains(value));

        [Property]
        public Property IntervalsContainValuesBetweenTheirBoundaries(Interval<T, TComparer> interval, T value) => interval.IsEmpty ? true.ToProperty() :
            (value.IsLessThan<T, TComparer>(interval.UpperBoundary.Value) && value.IsGreaterThan<T, TComparer>(interval.LowerBoundary.Value)).Implies(interval.Contains(value));

        [Property]
        public Property IntervalsDoNotContainForbiddenValues(Interval<T, TComparer> interval, ForbiddenBoundaryValue<T> value) =>
            (value.DoesExist && !interval.Contains(value.Value)).Or(!value.DoesExist);
    }
}
