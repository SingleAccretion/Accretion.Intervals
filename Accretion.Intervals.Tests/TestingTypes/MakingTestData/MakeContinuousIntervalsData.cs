using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    internal static class Make1ContinuousIntervalsData
    {
        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDouble(x.Item1), ToIntervalOfDouble(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDouble(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDouble(x.Item1), ToIntervalOfDouble(x.Item2))));

        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDouble(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDouble<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDouble(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfInt(x.Item1), ToIntervalOfInt(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfInt(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfInt(x.Item1), ToIntervalOfInt(x.Item2))));

        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfInt(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfInt<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfInt(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfChar<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfChar(x.Item1), ToIntervalOfChar(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfChar(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfChar(x.Item1), ToIntervalOfChar(x.Item2))));

        public static IEnumerable<object[]> OfChar<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfChar(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfChar<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfChar(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDay(x.Item1), ToIntervalOfDay(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDay(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDay(x.Item1), ToIntervalOfDay(x.Item2))));

        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDay(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDay<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfDay(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfCoordinate(x.Item1), ToIntervalOfCoordinate(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfCoordinate(IEnumerable<(string, string)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfCoordinate(x.Item1), ToIntervalOfCoordinate(x.Item2))));

        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, T)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfCoordinate(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfCoordinate<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            MakeArbitraryData.Of(data.Select(x => (ToIntervalOfCoordinate(x.Item1), x.Item2, x.Item3)));

        private static ContinuousInterval<int> ToIntervalOfInt(string s) => ContinuousInterval<int>.Parse(s, int.Parse);
        private static ContinuousInterval<char> ToIntervalOfChar(string s) => ContinuousInterval<char>.Parse(s, char.Parse);
        private static ContinuousInterval<double> ToIntervalOfDouble(string s) => ContinuousInterval<double>.Parse(s, x => double.Parse(x, CultureInfo.InvariantCulture));
        private static ContinuousInterval<Day> ToIntervalOfDay(string s) => ContinuousInterval<Day>.Parse(s, Day.Parse);
        private static ContinuousInterval<Coordinate> ToIntervalOfCoordinate(string s) => ContinuousInterval<Coordinate>.Parse(s, Coordinate.Parse);
    }
}
