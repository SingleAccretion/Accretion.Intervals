using FsCheck;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<Interval<T, TComparer>> AtomicInterval<T, TComparer>() where TComparer : struct, IComparer<T> =>
            Arb.From(from lowerBoundary in Arb.Generate<LowerBoundary<T, TComparer>>()
                     from upperBoundary in Arb.Generate<UpperBoundary<T, TComparer>>()
                     where Facts.BoundariesAreValid(lowerBoundary, upperBoundary)
                     let nonEmptyInterval = new Interval<T, TComparer>(lowerBoundary, upperBoundary)
                     from interval in Gen.Frequency(Tuple.Create(1, Gen.Constant(Interval<T, TComparer>.Empty)), Tuple.Create(10, Gen.Constant(nonEmptyInterval)))
                     select interval);
    }
}
