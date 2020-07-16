using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<Interval<T, TComparer>> Intervals<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            Gen.Frequency(
                Tuple.Create(8, NormalIntervals<T, TComparer>()),
                Tuple.Create(1, SingletonIntervals<T, TComparer>()),
                Tuple.Create(1, Gen.Constant(Interval<T, TComparer>.Empty))));

        public static Arbitrary<NonEmptyInterval<T, TComparer>> NonEmptyIntervals<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            Gen.Frequency(
                Tuple.Create(9, NormalIntervals<T, TComparer>().Select(x => new NonEmptyInterval<T, TComparer>(x))), 
                Tuple.Create(1, SingletonIntervals<T, TComparer>().Select(x => new NonEmptyInterval<T, TComparer>(x)))));

        private static Gen<Interval<T, TComparer>> NormalIntervals<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from lowerBoundaryValue in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            from upperBoundaryValue in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            where upperBoundaryValue.Value.IsGreaterThan<T, TComparer>(lowerBoundaryValue.Value)
            from lowerBoundaryType in Arb.Generate<BoundaryType>()
            from upperBoundaryType in Arb.Generate<BoundaryType>()
            select Interval.Create<T, TComparer>(lowerBoundaryType, lowerBoundaryValue, upperBoundaryValue, upperBoundaryType);

        private static Gen<Interval<T, TComparer>> SingletonIntervals<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from value in Arb.Generate<ValidBoundaryValue<T, TComparer>>()
            select Interval.CreateSingleton<T, TComparer>(value);
    }
}
