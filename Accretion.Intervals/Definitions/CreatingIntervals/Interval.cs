using System;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    public static class Interval
    {
        public static Interval<T> Create<T>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> =>
            Create<T, DefaultValueComparer<T>>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType);

        public static Interval<T, TComparer> Create<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where TComparer : struct, IComparer<T> =>
            TryCreate<T, TComparer>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType, out var interval, out var exception) ?
            interval : Throw.Exception<Interval<T, TComparer>>(exception);

        public static Interval<T> CreateClosed<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T> =>
            Create(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);

        public static Interval<T, TComparer> CreateClosed<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where TComparer : struct, IComparer<T> =>
            Create<T, TComparer>(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);

        public static Interval<T> CreateOpen<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T> =>
            Create(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);

        public static Interval<T, TComparer> CreateOpen<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where TComparer : struct, IComparer<T> =>
            Create<T, TComparer>(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);

        public static Interval<T> CreateSingleton<T>(T value) where T : IComparable<T> =>
            Create(BoundaryType.Closed, value, value, BoundaryType.Closed);

        public static Interval<T, TComparer> CreateSingleton<T, TComparer>(T value) where TComparer : struct, IComparer<T> =>
            Create<T, TComparer>(BoundaryType.Closed, value, value, BoundaryType.Closed);

        public static CompositeInterval<T> Join<T>(ICollection<Interval<T>> intervals) where T : IComparable<T>
            => throw new NotImplementedException();

        public static CompositeInterval<T, TComparer> Join<T, TComparer>(ICollection<Interval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => throw new NotImplementedException();

        public static CompositeInterval<T> Join<T>(IReadOnlyCollection<Interval<T>> intervals) where T : IComparable<T>
            => throw new NotImplementedException();

        public static CompositeInterval<T, TComparer> Join<T, TComparer>(IReadOnlyCollection<Interval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => throw new NotImplementedException();

        internal static bool TryCreate<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType, out Interval<T, TComparer> interval, out Exception exception) where TComparer : struct, IComparer<T>
        {
            interval = default;

            if (Checker.IsNull(lowerBoundaryValue) || Checker.IsNull(upperBoundaryValue))
            {
                exception = IntervalCreationExceptions.BoundariesCannotBeNull;
            }
            else if (Checker.IsNaN(lowerBoundaryValue) || Checker.IsNaN(upperBoundaryValue))
            {
                exception = IntervalCreationExceptions.BoundariesCannotBeNaN;
            }
            else if (typeof(T) == typeof(DateTime) && !(Checker.IsUtcDateTime(lowerBoundaryValue) && Checker.IsUtcDateTime(upperBoundaryValue)))
            {
                exception = IntervalCreationExceptions.DateTimeBoundariesMustBeUtc;
            }
            else
            {
                var lowerBoundary = new LowerBoundary<T, TComparer>(lowerBoundaryValue, lowerBoundaryType);
                var upperBoundary = new UpperBoundary<T, TComparer>(upperBoundaryValue, upperBoundaryType);

                if (upperBoundary.IsLessThan<T, TComparer, OverlapFullyClosed>(lowerBoundary))
                {
                    exception = IntervalCreationExceptions.BoundariesMustProduceNonEmptyInterval;
                }
                else
                {
                    exception = null;
                    interval = new Interval<T, TComparer>(lowerBoundary, upperBoundary);
                }
            }

            return exception is null;
        }
    }
}