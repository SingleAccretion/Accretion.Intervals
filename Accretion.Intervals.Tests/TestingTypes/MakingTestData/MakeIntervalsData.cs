using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public static class MakeCompositeIntervalsData
    {
        public static IEnumerable<object[]> OfDouble(IEnumerable<(string, string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDouble(x.Item1), ToCompositeIntervalOfDouble(x.Item2), ToCompositeIntervalOfDouble(x.Item3))));

        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, string, T)> data) => 
            Make.Data(data.Select(x => (ToCompositeIntervalOfDouble(x.Item1), ToCompositeIntervalOfDouble(x.Item2), x.Item3)));
        
        public static IEnumerable<object[]> OfDouble(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDouble(x.Item1), ToCompositeIntervalOfDouble(x.Item2))));

        public static IEnumerable<object[]> OfDouble<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDouble(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDouble<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDouble(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfInt(IEnumerable<(string, string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfInt(x.Item1), ToCompositeIntervalOfInt(x.Item2), ToCompositeIntervalOfInt(x.Item3))));

        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfInt(x.Item1), ToCompositeIntervalOfInt(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfInt(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfInt(x.Item1), ToCompositeIntervalOfInt(x.Item2))));

        public static IEnumerable<object[]> OfInt<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfInt(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfInt<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfInt(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfDay(IEnumerable<(string, string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDay(x.Item1), ToCompositeIntervalOfDay(x.Item2), ToCompositeIntervalOfDay(x.Item3))));

        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDay(x.Item1), ToCompositeIntervalOfDay(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfDay(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDay(x.Item1), ToCompositeIntervalOfDay(x.Item2))));

        public static IEnumerable<object[]> OfDay<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDay(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfDay<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfDay(x.Item1), x.Item2, x.Item3)));


        public static IEnumerable<object[]> OfCoordinate(IEnumerable<(string, string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfCoordinate(x.Item1), ToCompositeIntervalOfCoordinate(x.Item2), ToCompositeIntervalOfCoordinate(x.Item3))));

        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfCoordinate(x.Item1), ToCompositeIntervalOfCoordinate(x.Item2), x.Item3)));

        public static IEnumerable<object[]> OfCoordinate(IEnumerable<(string, string)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfCoordinate(x.Item1), ToCompositeIntervalOfCoordinate(x.Item2))));

        public static IEnumerable<object[]> OfCoordinate<T>(IEnumerable<(string, T)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfCoordinate(x.Item1), x.Item2)));

        public static IEnumerable<object[]> OfCoordinate<T1, T2>(IEnumerable<(string, T1, T2)> data) =>
            Make.Data(data.Select(x => (ToCompositeIntervalOfCoordinate(x.Item1), x.Item2, x.Item3)));

        private static CompositeInterval<int> ToCompositeIntervalOfInt(string s) => CompositeInterval<int>.Parse(s, int.Parse);
        private static CompositeInterval<double> ToCompositeIntervalOfDouble(string s) => CompositeInterval<double>.Parse(s, x => double.Parse(x, CultureInfo.InvariantCulture));
        private static CompositeInterval<Day> ToCompositeIntervalOfDay(string s) => CompositeInterval<Day>.Parse(s, Day.Parse);
        private static CompositeInterval<Coordinate> ToCompositeIntervalOfCoordinate(string s) => CompositeInterval<Coordinate>.Parse(s, Coordinate.Parse);
    }
}
