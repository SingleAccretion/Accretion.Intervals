using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<BoundaryType> BoundaryType() => Arb.From(Arb.Generate<bool>().Select(x => x ? Intervals.BoundaryType.Open : Intervals.BoundaryType.Closed));
    }
}
