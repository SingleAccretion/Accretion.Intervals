using System;
using System.Collections.Generic;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        /*
        public static Arbitrary<ValidIntervalString<T, TComparer>> ValidIntervalString<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(Gen.Frequency(
                Tuple.Create(8, ValidNormalIntervalString<T, TComparer>()), 
                Tuple.Create(1, ValidSingletonIntervalString<T, TComparer>()), 
                Tuple.Create(1, ValidEmptyIntervalString<T, TComparer>())));

        private static Gen<ValidIntervalString<T, TComparer>> ValidNormalIntervalString<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            ValidIntervalString<T, TComparer>(new[] { "[", "(" }, new[] { ")", "]" });

        private static Gen<ValidIntervalString<T, TComparer>> ValidSingletonIntervalString<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            ValidIntervalString<T, TComparer>(new[] { "{" }, new[] { "}" });

        private static Gen<ValidIntervalString<T, TComparer>> ValidEmptyIntervalString<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from lowerPadding in WhiteSpacePadding()
            from upperPadding in WhiteSpacePadding()
            select new ValidIntervalString<T, TComparer>(lowerPadding, "(", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ")", upperPadding);
        
        private static Gen<ValidIntervalString<T, TComparer>> ValidIntervalString<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from interval in Arb.Generate<Interval<T, TComparer>>()
            where !interval.IsEmpty
            from lowerPadding in WhiteSpacePadding()
            from lowerBoundaryType in Gen.Elements("[", "(")
            from lowerBoundaryTypePadding in WhiteSpacePadding()
            let lowerBoundaryValue = interval.LowerBoundary.Value.ToString()
            from lowerBoundaryValuePadding in WhiteSpacePadding()
            from separatorPadding in WhiteSpacePadding()
            let upperBoundaryValue = interval.UpperBoundary.Value.ToString()
            from upperBoundaryValuePadding in WhiteSpacePadding()
            from upperBoundaryType in Gen.Elements(upperBoundaryTypes)
            from upperPadding in WhiteSpacePadding()
            select new ValidIntervalString<T, TComparer>(
                lowerPadding, lowerBoundaryType, lowerBoundaryTypePadding, lowerBoundaryValue, lowerBoundaryValuePadding, ",",
                separatorPadding, upperBoundaryValue, upperBoundaryValuePadding, upperBoundaryType, upperPadding);

        private static Gen<string> WhiteSpacePadding() => Gen.Choose(0, 10).Select(x => new string(' ', x));*/
    }
}
