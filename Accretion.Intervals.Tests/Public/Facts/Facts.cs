using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public static class Facts
    {
        public static bool BoundariesAreValid<T, TComparer>(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary) where TComparer : struct, IComparer<T> =>
            !IsForbiddenBoundaryValue(lowerBoundary.Value) &&
            !IsForbiddenBoundaryValue(upperBoundary.Value) &&
            !BoundariesProduceEmptyInterval(lowerBoundary, upperBoundary);

        public static bool HasForbiddenValues<T>() =>
            default(T) is null ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(double) ||
            typeof(T) == typeof(DateTime);

        public static bool IsForbiddenBoundaryValue<T>(T value) =>
            value is null ||
            value is float.NaN ||
            value is double.NaN ||
           (value is DateTime dateTime && dateTime.Kind != DateTimeKind.Utc);

        private static bool BoundariesProduceEmptyInterval<T, TComparer>(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary) where TComparer : struct, IComparer<T>
        {
            if (upperBoundary.Value.IsLessThan<T, TComparer>(lowerBoundary.Value))
            {
                return true;
            }
            else if (upperBoundary.Value.IsEqualTo<T, TComparer>(lowerBoundary.Value))
            {
                return lowerBoundary.Type == BoundaryType.Open || upperBoundary.Type == BoundaryType.Open;
            }
            else
            {
                return false;
            }
        }
    }
}
