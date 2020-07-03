using System;
using System.Collections.Generic;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.CreatingIntervals
{
    public abstract class IntervalCreateTests<T, TComparer> : TestsBase where TComparer : struct, IBoundaryValueComparer<T>
    {
        [Property]
        public Property IntervalCreatesValidIntervalsOrThrows(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType)
        {
            var result = Result.From(() => Interval.Create<T, TComparer>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType));

            if (result.HasValue)
            {
                return (!result.Value.IsEmpty).ToProperty();
            }
            else if (lowerBoundaryValue is null || upperBoundaryValue is null)
            {
                return (result.Exception is ArgumentNullException).ToProperty();
            }
            else
            {
                return ((Spec.IsInvalidBoundaryValue<T, TComparer>(lowerBoundaryValue) || 
                         Spec.IsInvalidBoundaryValue<T, TComparer>(upperBoundaryValue) ||
                         BoundariesProduceEmptyInterval(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType)) &&
                        (result.Exception is ArgumentException)).ToProperty();
            }
        }

        [Property]
        public Property CreateOpenIsTheSameAsCreateWithOpenBoundaryTypes(T lowerBoundaryValue, T upperBoundaryValue) =>
            Result.From(() => Interval.Create<T, TComparer>(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open)).
            Equals(Result.From(() => Interval.CreateOpen<T, TComparer>(lowerBoundaryValue, upperBoundaryValue))).ToProperty();

        [Property]
        public Property CreateClosedIsTheSameAsCreateWithClosedBoundaryTypes(T lowerBoundaryValue, T upperBoundaryValue) =>
            Result.From(() => Interval.Create<T, TComparer>(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed)).
            Equals(Result.From(() => Interval.CreateClosed<T, TComparer>(lowerBoundaryValue, upperBoundaryValue))).ToProperty();

        [Property]
        public Property CreateSingletonIsTheSameAsCreateWithClosedBoundariesAndOneValue(T value) =>
            Result.From(() => Interval.Create<T, TComparer>(BoundaryType.Closed, value, value, BoundaryType.Closed)).
            Equals(Result.From(() => Interval.CreateSingleton<T, TComparer>(value))).ToProperty();

        private static bool BoundariesProduceEmptyInterval(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType)
        {
            if (upperBoundaryValue.IsLessThan<T, TComparer>(lowerBoundaryValue))
            {
                return true;
            }
            else if (upperBoundaryValue.IsEqualTo<T, TComparer>(lowerBoundaryValue))
            {
                return lowerBoundaryType == BoundaryType.Open || upperBoundaryType == BoundaryType.Open;
            }
            else
            {
                return false;
            }
        }
    }
}
