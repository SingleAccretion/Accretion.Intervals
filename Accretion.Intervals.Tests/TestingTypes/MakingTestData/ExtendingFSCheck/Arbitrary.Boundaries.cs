using FsCheck;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
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
    }
}
