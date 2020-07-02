using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct SingleComparerByExponent : IComparer<float>
    {
        public int Compare(float x, float y) => default(DoubleComparerByExponent).Compare(x, y);
    }
}
