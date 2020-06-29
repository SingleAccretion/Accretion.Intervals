using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Generators
    {
        public static Arbitrary<LowerBoundary<double, DoubleComparerByExponent>> ArbitraryLowerBoundaryWithComparer() =>
            Arb.From(from value in Arb.Generate<int>() 
                     from type in Arb.Generate<BoundaryType>() 
                     select new LowerBoundary<double, DoubleComparerByExponent>(value, type));
    }
}
