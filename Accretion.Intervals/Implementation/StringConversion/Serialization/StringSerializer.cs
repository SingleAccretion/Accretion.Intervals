using System;

namespace Accretion.Intervals.StringConversion
{
    internal static class StringSerializer
    {
        public const string GeneralFormat = "G";
        public const string InvalidBoundary = "invalid";

        public static string Serialize<T, TComparer>(Interval<T, TComparer> interval, string format, IFormatProvider formatProvider) where TComparer : struct, IBoundaryValueComparer<T>
        {
            if (interval.IsEmpty)
            {
                return Symbols.EmptySetString;
            }
            else if (interval.LowerBoundary.Value.IsEqualTo<T, TComparer>(interval.UpperBoundary.Value))
            {
                return Symbols.GetSymbol(TokenType.StartSingleton) +
                       Serialize<T, TComparer>(interval.LowerBoundary.Value, format, formatProvider) +
                       Symbols.GetSymbol(TokenType.EndSingleton);
            }
            else
            {
                return Serialize(interval.LowerBoundary, format, formatProvider) +
                       Symbols.GetSymbol(TokenType.Separator) +
                       Serialize(interval.UpperBoundary, format, formatProvider);
            }
        }

        public static string Serialize<T, TComparer>(LowerBoundary<T, TComparer> boundary, string format, IFormatProvider formatProvider) where TComparer : struct, IBoundaryValueComparer<T> =>
            (boundary.IsClosed ? Symbols.GetSymbol(TokenType.StartClosed) : Symbols.GetSymbol(TokenType.StartOpen)) +
            Serialize<T, TComparer>(boundary.Value, format, formatProvider);

        public static string Serialize<T, TComparer>(UpperBoundary<T, TComparer> boundary, string format, IFormatProvider formatProvider) where TComparer : struct, IBoundaryValueComparer<T> =>
            Serialize<T, TComparer>(boundary.Value, format, formatProvider) +
            (boundary.IsClosed ? Symbols.GetSymbol(TokenType.EndClosed) : Symbols.GetSymbol(TokenType.EndOpen));

        private static string Serialize<T, TComparer>(T value, string format, IFormatProvider formatProvider) where TComparer : struct, IBoundaryValueComparer<T> =>
            value is null ? string.Empty : default(TComparer).ToString(value, format, formatProvider);
    }
}
