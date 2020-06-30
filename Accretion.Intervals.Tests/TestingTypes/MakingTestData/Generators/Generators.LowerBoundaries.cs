using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Generators
    {
        public static Arbitrary<LowerBoundary<double, DoubleComparerByExponent>> ArbitraryLowerBoundaryWithComparerForDouble() =>
            Arb.From(from value in Arb.Generate<double>() where value > -1
                     from type in Arb.Generate<BoundaryType>() 
                     select new LowerBoundary<double, DoubleComparerByExponent>(value, type));

        public static Arbitrary<LowerBoundary<ValueClass, ValueClassBackwardsComparer>> ArbitraryLowerBoundaryWithComparerForClass() =>
            Arb.From(from value in Arb.Generate<int?>()
                     from type in Arb.Generate<BoundaryType>()
                     select new LowerBoundary<ValueClass, ValueClassBackwardsComparer>(value, type));
    }
}
