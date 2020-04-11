using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public static class MakeArbitraryData
    {
        public static IEnumerable<object[]> Of<T>(IEnumerable<T> data) => data.Select(x => new object[] { x });
        public static IEnumerable<object[]> Of<T1, T2>(IEnumerable<(T1, T2)> data) => data.Select(x => new object[] { x.Item1, x.Item2 });
        public static IEnumerable<object[]> Of<T1, T2, T3>(IEnumerable<(T1, T2, T3)> data) => data.Select(x => new object[] { x.Item1, x.Item2, x.Item3 });
        public static IEnumerable<object[]> Of<T1, T2, T3, T4>(IEnumerable<(T1, T2, T3, T4)> data) => data.Select(x => new object[] { x.Item1, x.Item2, x.Item3, x.Item4 });
    }
}
