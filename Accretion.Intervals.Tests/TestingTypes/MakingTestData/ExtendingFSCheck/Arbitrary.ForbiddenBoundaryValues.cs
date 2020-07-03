using FsCheck;

namespace Accretion.Intervals.Tests
{
    public partial class Arbitrary
    {
        public static Arbitrary<InvalidBoundaryValue<T, TComparer>> Values<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(!Spec.HasInvalidValues<T, TComparer>() ? Gen.Constant(new InvalidBoundaryValue<T, TComparer>()) :
                      from value in Arb.Generate<T>()
                      where Spec.IsInvalidBoundaryValue<T, TComparer>(value)
                      select new InvalidBoundaryValue<T, TComparer>(value));
    }
}
