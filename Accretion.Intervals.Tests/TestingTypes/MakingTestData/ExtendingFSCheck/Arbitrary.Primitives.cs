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

        public static Arbitrary<float> Single() =>
            Arb.From(from value in Arb.Default.DoNotSizeInt32().Generator.Select(x => BitConverter.Int32BitsToSingle(x.Item))
                     from specialValue in Gen.Elements(0.0f, -0.0f, float.NaN, float.PositiveInfinity, float.NegativeInfinity, float.MaxValue, float.MinValue)
                     from result in Gen.Frequency(Tuple.Create(5, Gen.Constant(value)), Tuple.Create(1, Gen.Constant(specialValue)))
                     select result);

        public static Arbitrary<double> Double() =>
            Arb.From(from value in Arb.Default.DoNotSizeInt64().Generator.Select(x => BitConverter.Int64BitsToDouble(x.Item))
                     from specialValue in Gen.Elements(0.0d, -0.0d, double.NaN, double.PositiveInfinity, double.NegativeInfinity, double.MaxValue, double.MinValue)
                     from result in Gen.Frequency(Tuple.Create(5, Gen.Constant(value)), Tuple.Create(1, Gen.Constant(specialValue)))
                     select result);

        public static Arbitrary<BoundaryType> BoundaryType() => Arb.From(Arb.Generate<bool>().Select(x => x ? Intervals.BoundaryType.Open : Intervals.BoundaryType.Closed));

        public static Arbitrary<ValueClass> ValueClass() => Arb.From(Arb.Generate<int?>().Select(x => (ValueClass)x));

        public static Arbitrary<InvalidBoundaryValue<T, TComparer>> InvalidBoundaryValues<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(!InvalidBoundaryValue.HasInvalidValues<T, TComparer>() ? Gen.Constant(new InvalidBoundaryValue<T, TComparer>()) :
                      from value in Arb.Generate<T>()
                      where InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(value)
                      select new InvalidBoundaryValue<T, TComparer>(value));

        public static Arbitrary<ValidBoundaryValue<T, TComparer>> ValidBoundaryValues<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Arb.From(from value in Arb.Generate<T>()
                     where !InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(value)
                     select new ValidBoundaryValue<T, TComparer>(value));

        public static Arbitrary<FormatString> Format() => Arb.From(Gen.Elements("G", "N", "P", "F", "C").Select(x => new FormatString(x)));
    }
}
