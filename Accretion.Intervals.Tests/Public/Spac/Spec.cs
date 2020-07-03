using System;

namespace Accretion.Intervals.Tests
{
    public static class Spec
    {
        public static bool HasInvalidValues<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            default(T) is null ||
            (default(TComparer) is DefaultValueComparer<float> && typeof(T) == typeof(float)) ||
            (default(TComparer) is DefaultValueComparer<double> && typeof(T) == typeof(double)) ||
            (default(TComparer) is DefaultValueComparer<DateTime> && typeof(T) == typeof(DateTime));

        public static bool IsInvalidBoundaryValue<T, TComparer>(T value) where TComparer : struct, IBoundaryValueComparer<T> => 
            (value is null) ||
            (default(TComparer) is DefaultValueComparer<float> && value is float.NaN) ||
            (default(TComparer) is DefaultValueComparer<double> && value is double.NaN) ||
            (default(TComparer) is DefaultValueComparer<DateTime> && value is DateTime dateTime && dateTime.Kind != DateTimeKind.Utc);
    }
}
