using Accretion.Intervals.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    internal static class Boundaries
    {
        private static readonly Random _random = new Random(1);

        public static Boundary<int>[] MakeBoundaries(int count)
        {
            return Enumerable.Range(0, count).Select(x => RandomBoundary()).OrderBy(x => x).ToArray();
        }

        public static Boundary<int>[] MakeExperimentalBoundaries(int count)
        {
            return Enumerable.Range(0, count).Select(x => RandomExperimentalBoundary()).OrderBy(x => x).ToArray();
        }

        private static Boundary<int> RandomBoundary()
        {
            var a = _random.Next(int.MinValue, int.MaxValue);
            
            return new Boundary<int>(a, a % 2 == 0, a  % 2 == 0);
        }

        private static Boundary<int> RandomExperimentalBoundary()
        {
            var a = _random.Next(int.MinValue, int.MaxValue);

            return new Boundary<int>(a, a % 2 == 0, a % 2 == 0);
        }
    }
}
