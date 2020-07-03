using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<Interval<T, TComparer>> AtomicInterval<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(from lowerBoundary in Arb.Generate<LowerBoundary<T, TComparer>>()
                     from upperBoundary in Arb.Generate<UpperBoundary<T, TComparer>>()
                     let intervalResult = Result.From(() => Interval.Create<T, TComparer>(lowerBoundary.Type, lowerBoundary.Value, upperBoundary.Value, upperBoundary.Type))
                     where intervalResult.HasValue
                     let nonEmptyInterval = intervalResult.Value
                     from interval in Gen.Frequency(Tuple.Create(1, Gen.Constant(Interval<T, TComparer>.Empty)), Tuple.Create(10, Gen.Constant(nonEmptyInterval)))
                     select interval);
    }
}
