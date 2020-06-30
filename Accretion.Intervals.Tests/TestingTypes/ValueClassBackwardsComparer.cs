using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValueClassBackwardsComparer : IComparer<ValueClass>
    {
        public int Compare(ValueClass x, ValueClass y)
        {
            if (y is null)
            {
                return x is null ? 0 : -1;
            }
            if (x is null)
            {
                return y is null ? 0 : 1;
            }

            return Math.Clamp(y.Value - x.Value, -1, 1);
        }
    }
}
