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
                return x is null ? ComparingValues.IsEqual : ComparingValues.IsLess;
            }
            if (x is null)
            {
                return y is null ? ComparingValues.IsEqual : ComparingValues.IsGreater;
            }

            return Math.Clamp(y.Value - x.Value, ComparingValues.IsLess, ComparingValues.IsGreater);
        }
    }
}
