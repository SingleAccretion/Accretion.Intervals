using System.Collections.Generic;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public static class Make
    {
        public static IEnumerable<object[]> Data<T>(params T[] data) => data.Select(x => new object[] { x });
        public static IEnumerable<object[]> Data<T1, T2>(params (T1, T2)[] data) => data.Select(x => new object[] { x.Item1, x.Item2 });
        public static IEnumerable<object[]> Data<T1, T2, T3>(params (T1, T2, T3)[] data) => data.Select(x => new object[] { x.Item1, x.Item2, x.Item3 });
        public static IEnumerable<object[]> Data<T1, T2, T3, T4>(IEnumerable<(T1, T2, T3, T4)> data) => data.Select(x => new object[] { x.Item1, x.Item2, x.Item3, x.Item4 });
    }
}
