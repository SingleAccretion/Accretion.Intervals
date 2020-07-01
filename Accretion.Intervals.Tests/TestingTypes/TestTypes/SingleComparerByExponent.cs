using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public readonly struct SingleComparerByExponent : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            var rawResult = Math.Round(Math.Log10(x) - Math.Log10(y), MidpointRounding.AwayFromZero);
            return double.IsNaN(rawResult) ? 0 : (int)rawResult;
        }
    }
}
