using System.Collections.Generic;

namespace Accretion.Intervals.StringConversion
{
    internal static class Serializer
    {
        public static string Serialize<T, TComparer>(Interval<T, TComparer> interval) where TComparer : struct, IComparer<T>
        {
            if (interval.IsEmpty)
            {
                return Symbols.EmptySetString;
            }
            else if (interval.LowerBoundary.Value.IsEqualTo<T, TComparer>(interval.UpperBoundary.Value))
            {
                return Symbols.GetSymbol(TokenType.StartSingleton) + interval.LowerBoundary.Value.ToString() + Symbols.GetSymbol(TokenType.EndSingleton);
            }
            else
            {
                return Serialize(interval.LowerBoundary) + Symbols.GetSymbol(TokenType.Separator) + Serialize(interval.UpperBoundary);
            }
        }

        public static string Serialize<T, TComparer>(LowerBoundary<T, TComparer> boundary) where TComparer : struct, IComparer<T> => 
            $"{(boundary.IsClosed ? Symbols.GetSymbol(TokenType.StartClosed) : Symbols.GetSymbol(TokenType.StartOpen))}{boundary.Value}";

        public static string Serialize<T, TComparer>(UpperBoundary<T, TComparer> boundary) where TComparer : struct, IComparer<T> =>
            $"{boundary.Value}{(boundary.IsClosed ? Symbols.GetSymbol(TokenType.EndClosed) : Symbols.GetSymbol(TokenType.EndOpen))}";
    }
}
