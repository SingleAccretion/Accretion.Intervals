using FsCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public partial class Generators
    {
        public static Arbitrary<BoundaryType> ArbitraryBoundaryType() => Arb.From(Arb.Generate<bool>().Select(x => x ? BoundaryType.Open : BoundaryType.Closed));
    }
}
