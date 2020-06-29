using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValueClassBackwardsComparer : IComparer<ValueClass>
    {
        public int Compare(ValueClass x, ValueClass y) => Math.Clamp(y.Value - x.Value, -1, 1);
    }
}
