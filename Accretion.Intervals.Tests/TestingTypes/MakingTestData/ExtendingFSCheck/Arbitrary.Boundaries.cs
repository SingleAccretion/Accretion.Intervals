using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<LowerBoundary<T, TComparer>> LowerBoundary<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(from value in Arb.Generate<T>()
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<T, TComparer>(value, type));

        public static Arbitrary<LowerBoundary<T>> LowerBoundary<T>() where T : IComparable<T> =>
            Arb.From(from value in Arb.Generate<T>()
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<T>(value, type));

        public static Arbitrary<UpperBoundary<T, TComparer>> UpperBoundary<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(from value in Arb.Generate<T>()
                     from type in Arb.Generate<BoundaryType>()
                     select new UpperBoundary<T, TComparer>(value, type));

        public static Arbitrary<UpperBoundary<T>> UpperBoundary<T>() where T : IComparable<T> =>
            Arb.From(from value in Arb.Generate<T>()
                     from type in Arb.Generate<BoundaryType>()
                     select new UpperBoundary<T>(value, type));

        public static Arbitrary<LowerBoundary<float, SingleComparerByExponent>> LowerBoundaryOfSingleWithComparer() =>
            Arb.From(from value in Arb.Generate<float>()
                     where value > -10.1
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<float, SingleComparerByExponent>(value, type));

        public static Arbitrary<UpperBoundary<float, SingleComparerByExponent>> UpperBoundaryOfSingleWithComparer() =>
            Arb.From(from value in Arb.Generate<float>()
                     where value > -10.1
                     from type in Arb.Generate<BoundaryType>()
                     select new UpperBoundary<float, SingleComparerByExponent>(value, type));

        public static Arbitrary<LowerBoundary<double, DoubleComparerByExponent>> LowerBoundaryOfDoubleWithComparer() =>
            Arb.From(from value in Arb.Generate<double>()
                     where value > -10.1
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<double, DoubleComparerByExponent>(value, type));

        public static Arbitrary<UpperBoundary<double, DoubleComparerByExponent>> UpperBoundaryOfDoubleWithComparer() =>
            Arb.From(from value in Arb.Generate<double>()
                     where value > -10.1
                     from type in Arb.Generate<BoundaryType>()
                     select new UpperBoundary<double, DoubleComparerByExponent>(value, type));
    }
}
