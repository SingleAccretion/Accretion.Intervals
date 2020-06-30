using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct DoubleComparerByExponent : IComparer<double>
    {
        public int Compare(double x, double y)
        {

            var rawResult = Math.Round(Math.Log10(x) - Math.Log10(y), MidpointRounding.AwayFromZero);
            return double.IsNaN(rawResult) ? 0 : (int)rawResult;
        }
    }
}
