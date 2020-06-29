using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    internal static class MakeIntervalsData
    {
        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDouble(x.Item1), ToIntervalOfDouble(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDouble(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDouble(x.Item1), ToIntervalOfDouble(x.Item2))));

        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDouble(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDouble<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDouble(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfInt(x.Item1), ToIntervalOfInt(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfInt(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfInt(x.Item1), ToIntervalOfInt(x.Item2))));

        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfInt(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfInt<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfInt(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfChar<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfChar(x.Item1), ToIntervalOfChar(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfChar(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfChar(x.Item1), ToIntervalOfChar(x.Item2))));

        public static IEnumerable<object[]> OfChar<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfChar(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfChar<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfChar(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDay(x.Item1), ToIntervalOfDay(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDay(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDay(x.Item1), ToIntervalOfDay(x.Item2))));

        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDay(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDay<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfDay(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfCoordinate(x.Item1), ToIntervalOfCoordinate(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfCoordinate(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfCoordinate(x.Item1), ToIntervalOfCoordinate(x.Item2))));

        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfCoordinate(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfCoordinate<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToIntervalOfCoordinate(x.Item1), x.Item2, x.Item3)));

        private static Interval<int> ToIntervalOfInt(string s) => Interval<int>.Parse(s, int.Parse);
        private static Interval<char> ToIntervalOfChar(string s) => Interval<char>.Parse(s, char.Parse);
        private static Interval<double> ToIntervalOfDouble(string s) => Interval<double>.Parse(s, x => double.Parse(x, CultureInfo.InvariantCulture));
        private static Interval<Day> ToIntervalOfDay(string s) => Interval<Day>.Parse(s, Day.Parse);
        private static Interval<Coordinate> ToIntervalOfCoordinate(string s) => Interval<Coordinate>.Parse(s, Coordinate.Parse);
    }
}
