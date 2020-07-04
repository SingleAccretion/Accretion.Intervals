using System;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<int> Int32() =>
            Arb.From(from result in Gen.Frequency(
                     Tuple.Create(10, Arb.Default.DoNotSizeInt32().Generator.Select(x => x.Item)),
                     Tuple.Create(1, Gen.Elements(0, int.MinValue, int.MaxValue)))
                     select result);

        public static Arbitrary<BoundaryType> BoundaryType() => Arb.From(Arb.Generate<bool>().Select(x => x ? Intervals.BoundaryType.Open : Intervals.BoundaryType.Closed));

        public static Arbitrary<ValueClass> ValueClass() => Arb.From(Arb.Generate<int?>().Select(x => (ValueClass)x));

        public static Arbitrary<InvalidBoundaryValue<T, TComparer>> Values<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(!InvalidBoundaryValue.HasInvalidValues<T, TComparer>() ? Gen.Constant(new InvalidBoundaryValue<T, TComparer>()) :
                      from value in Arb.Generate<T>()
                      where InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(value)
                      select new InvalidBoundaryValue<T, TComparer>(value));
    }
}
