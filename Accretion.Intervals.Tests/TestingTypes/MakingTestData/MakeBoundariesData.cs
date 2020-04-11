using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public static class MakeBoundariesData
    {
        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToBoundaryOfDouble(x.Item1), ToBoundaryOfDouble(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDouble(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToBoundaryOfDouble(x.Item1), ToBoundaryOfDouble(x.Item2))));

        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToBoundaryOfDouble(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDouble<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToBoundaryOfDouble(x.Item1), x.Item2, x.Item3)));
        
        private static object ToBoundaryOfDouble(string s)
        {
            if (s.StartsWith(Interval.LeftOpenBoundarySymbol) || s.StartsWith(Interval.LeftClosedBoundarySymbol))
            {
                return LowerBoundary<double>.CreateUnchecked(double.Parse(s[1..^0], CultureInfo.InvariantCulture), s[0] == Interval.LeftOpenBoundarySymbol);
            }
            else if (s.EndsWith(Interval.RightOpenBoundarySymbol) || s.EndsWith(Interval.RightClosedBoundarySymbol))
            {
                return UpperBoundary<double>.CreateUnchecked(double.Parse(s[0..^1], CultureInfo.InvariantCulture), s[^1] == Interval.RightOpenBoundarySymbol);
            }
            else
            {
                throw new ArgumentException($"Boundary cannot be parsed from {s}");
            }
        }
    }
}
