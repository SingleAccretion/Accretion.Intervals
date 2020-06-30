using FsCheck;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<LowerBoundary<double, DoubleComparerByExponent>> LowerBoundaryWithComparerOfDouble() =>
            LowerBoundary<double, double, DoubleComparerByExponent>(x => x);

        public static Arbitrary<LowerBoundary<ValueClass, ValueClassBackwardsComparer>> LowerBoundaryWithComparerOfValuecClass() =>
            LowerBoundary<int?, ValueClass, ValueClassBackwardsComparer>(x => x);

        public static Arbitrary<LowerBoundary<double, DefaultValueComparer<double>>> LowerBoundaryOfDouble() =>
            LowerBoundary<double, double, DefaultValueComparer<double>>(x => x);

        public static Arbitrary<LowerBoundary<int, DefaultValueComparer<int>>> LowerBoundaryOfInt32() =>
            LowerBoundary<int, int, DefaultValueComparer<int>>(x => x);

        public static Arbitrary<LowerBoundary<ValueClass, DefaultValueComparer<ValueClass>>> LowerBoundaryOfValueClass() =>
            LowerBoundary<int?, ValueClass, DefaultValueComparer<ValueClass>>(x => x);

        public static Arbitrary<LowerBoundary<ValueStruct, DefaultValueComparer<ValueStruct>>> LowerBoundaryOfValueStruct() => 
            LowerBoundary<int, ValueStruct, DefaultValueComparer<ValueStruct>>(x => x);

        private static Arbitrary<LowerBoundary<T, TComparer>> LowerBoundary<TSource, T, TComparer>(Func<TSource, T> converter) where TComparer : struct, IComparer<T> =>
            Arb.From(from value in Arb.Generate<TSource>()
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<T, TComparer>(converter(value), type));
    }
}
