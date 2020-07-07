using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<Interval<T, TComparer>> RandomInterval<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(Gen.Frequency(
                Tuple.Create(8, NormalInterval<T, TComparer>()),
                Tuple.Create(1, SingletonInterval<T, TComparer>()),
                Tuple.Create(1, Gen.Constant(Interval<T, TComparer>.Empty))));

        public static Arbitrary<Interval<T, TComparer>> NonEmptyInterval<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(Gen.Frequency(Tuple.Create(9, NormalInterval<T, TComparer>()), Tuple.Create(1, SingletonInterval<T, TComparer>())));

        private static Gen<Interval<T, TComparer>> NormalInterval<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from lowerBoundaryValue in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            from upperBoundaryValue in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            where upperBoundaryValue.Value.IsGreaterThan<T, TComparer>(lowerBoundaryValue.Value)
            from lowerBoundaryType in Arb.Generate<BoundaryType>()
            from upperBoundaryType in Arb.Generate<BoundaryType>()
            select Interval.Create<T, TComparer>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType);

        private static Gen<Interval<T, TComparer>> SingletonInterval<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from value in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            select Interval.CreateSingleton<T, TComparer>(value);
    }
}
