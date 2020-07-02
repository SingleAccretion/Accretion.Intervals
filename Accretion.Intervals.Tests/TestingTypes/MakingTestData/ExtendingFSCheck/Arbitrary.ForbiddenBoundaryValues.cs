using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<ForbiddenBoundaryValue<T>> Values<T>() =>
            Arb.From(!Facts.HasForbiddenValues<T>() ? Gen.Constant(new ForbiddenBoundaryValue<T>()) :
                      from value in Arb.Generate<T>()
                      where Facts.IsForbiddenBoundaryValue(value)
                      select new ForbiddenBoundaryValue<T>(value));
    }
}
