using System;
using System.Linq;
using System.Text;
using FsCheck;

namespace Accretion.Intervals.Tests
{
    public static partial class Arbitrary
    {
        public static Arbitrary<IntervalString<T, TComparer>> IntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            Gen.OneOf(
                ValidIntervalStrings<T, TComparer>().Generator.Select(x => new IntervalString<T, TComparer>(x.String)),
                InvalidIntervalStrings<T, TComparer>().Generator.Select(x => new IntervalString<T, TComparer>(x.String))));

        public static Arbitrary<ValidIntervalString<T, TComparer>> ValidIntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            Gen.Frequency(
                Tuple.Create(8, ValidNormalIntervalStrings<T, TComparer>()),
                Tuple.Create(1, ValidSingletonIntervalStrings<T, TComparer>()),
                Tuple.Create(1, ValidEmptyIntervalStrings<T, TComparer>())));

        public static Arbitrary<InvalidIntervalString<T, TComparer>> InvalidIntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            Gen.OneOf(
                IntervalStringsWithInvalidFormat<T, TComparer>().Generator.Select(x => new InvalidIntervalString<T, TComparer>(x.String)),
                IntervalStringsWithInvalidValues<T, TComparer>().Generator.Select(x => new InvalidIntervalString<T, TComparer>(x.String)),
                Arb.Generate<string>().Resize(50).Select(x => new InvalidIntervalString<T, TComparer>(x))));

        public static Arbitrary<IntervalStringWithInvalidFormat<T, TComparer>> IntervalStringsWithInvalidFormat<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            from interval in NonEmptyIntervals<T, TComparer>().Generator
            from lowerBoundaryType in Gen.OneOf(Gen.Elements('[', '(', '{'), Arb.Generate<char>())
            let lowerBoundaryValue = interval.LowerBoundary.Value
            from separator in Gen.OneOf(Gen.Constant(','), Arb.Generate<char>())
            let upperBoundaryValue = interval.UpperBoundary.Value
            from upperBoundaryType in Gen.OneOf(Gen.Elements(']', ')', '}'), Arb.Generate<char>())
            select new IntervalStringWithInvalidFormat<T, TComparer>(Padded(lowerBoundaryType, lowerBoundaryValue, separator, upperBoundaryValue, upperBoundaryType)));

        public static Arbitrary<IntervalStringWithInvalidValues<T, TComparer>> IntervalStringsWithInvalidValues<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> => Arb.From(
            from interval in NormalIntervals<T, TComparer>()
            from lowerBoundaryType in Gen.Elements("[", "(")
            from invalidValue in InvalidBoundaryValues<T, TComparer>().Generator
            let boundaryValues = (Func<T, Gen<T>>)((T value) => Gen.OneOf(invalidValue.DoesExist ? Gen.Elements(value, invalidValue.Value) : Gen.Constant(value)))
            from lowerBoundaryValue in boundaryValues(interval.LowerBoundary.Value)
            from upperBoundaryValue in boundaryValues(interval.UpperBoundary.Value)
            from upperBoundaryType in Gen.Elements("]", ")")
            select new IntervalStringWithInvalidValues<T, TComparer>(Padded(lowerBoundaryType, upperBoundaryValue, ",", lowerBoundaryValue, upperBoundaryType)));
        
        private static Gen<ValidIntervalString<T, TComparer>> ValidNormalIntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from interval in NormalIntervals<T, TComparer>()
            from lowerBoundaryType in Gen.Elements("[", "(")
            let lowerBoundaryValue = interval.LowerBoundary.Value
            let upperBoundaryValue = interval.UpperBoundary.Value
            from upperBoundaryType in Gen.Elements("]", ")")
            select new ValidIntervalString<T, TComparer>(Padded(lowerBoundaryType, lowerBoundaryValue, ",", upperBoundaryValue, upperBoundaryType));

        private static Gen<ValidIntervalString<T, TComparer>> ValidSingletonIntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            from value in ValidBoundaryValues<T, TComparer>().Generator
            select new ValidIntervalString<T, TComparer>(Padded("{", value, "}"));

        private static Gen<ValidIntervalString<T, TComparer>> ValidEmptyIntervalStrings<T, TComparer>() where TComparer : struct, IBoundaryValueComparer<T> =>
            Gen.Fresh(() => new ValidIntervalString<T, TComparer>(Padded("()")));

        private static string Padded(params object[] values)
        {
            var random = new System.Random();
            string GeneratePadding() => new string(' ', random.Next(0, 11));

            var builder = new StringBuilder();
            foreach (var value in values)
            {
                builder.Append(GeneratePadding());
                builder.Append(value);
            }
            builder.Append(GeneratePadding());

            return builder.ToString();
        }
    }
}
