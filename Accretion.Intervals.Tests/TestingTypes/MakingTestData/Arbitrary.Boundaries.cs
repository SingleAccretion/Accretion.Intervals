using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<LowerBoundary<T, TComparer>> LowerBoundaries<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            from value in Arb.Generate<T>()
            from type in Arb.Generate<BoundaryType>()
            select new LowerBoundary<T, TComparer>(value, type));

        public static Arbitrary<LowerBoundary<T>> LowerBoundaries<T>() where T : IComparable<T> => Arb.From(
            from value in Arb.Generate<T>()
            from type in Arb.Generate<BoundaryType>()
            select new LowerBoundary<T>(value, type));

        public static Arbitrary<UpperBoundary<T, TComparer>> UpperBoundaries<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            from value in Arb.Generate<T>()
            from type in Arb.Generate<BoundaryType>()
            select new UpperBoundary<T, TComparer>(value, type));

        public static Arbitrary<UpperBoundary<T>> UpperBoundaries<T>() where T : IComparable<T> => Arb.From(
            from value in Arb.Generate<T>()
            from type in Arb.Generate<BoundaryType>()
            select new UpperBoundary<T>(value, type));

        public static Arbitrary<LowerBoundary<float, SingleComparerByExponent>> LowerBoundariesOfSingleWithComparer() => Arb.From(
            from value in Arb.Generate<float>()
            where value > -10.1
            from type in Arb.Generate<BoundaryType>()
            select new LowerBoundary<float, SingleComparerByExponent>(value, type));

        public static Arbitrary<UpperBoundary<float, SingleComparerByExponent>> UpperBoundariesOfSingleWithComparer() => Arb.From(
            from value in Arb.Generate<float>()
            where value > -10.1
            from type in Arb.Generate<BoundaryType>()
            select new UpperBoundary<float, SingleComparerByExponent>(value, type));

        public static Arbitrary<LowerBoundary<double, DoubleComparerByExponent>> LowerBoundariesOfDoubleWithComparer() => Arb.From(
            from value in Arb.Generate<double>()
            where value > -10.1
            from type in Arb.Generate<BoundaryType>()
            select new LowerBoundary<double, DoubleComparerByExponent>(value, type));

        public static Arbitrary<UpperBoundary<double, DoubleComparerByExponent>> UpperBoundariesOfDoubleWithComparer() => Arb.From(
            from value in Arb.Generate<double>()
            where value > -10.1
            from type in Arb.Generate<BoundaryType>()
            select new UpperBoundary<double, DoubleComparerByExponent>(value, type));
    }
}
