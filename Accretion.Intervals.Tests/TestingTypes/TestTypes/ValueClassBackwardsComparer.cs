﻿using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValueClassBackwardsComparer : IBoundaryValueComparer<ValueClass>
    {
        public int Compare(ValueClass x, ValueClass y) => Math.Clamp(y.Value - x.Value, ComparingValues.IsLess, ComparingValues.IsGreater);
        public int GetHashCode(ValueClass value) => value.Value;
        public bool IsInvalidBoundaryValue(ValueClass value) => false;
        public string ToString(ValueClass value, string format, IFormatProvider formatProvider) => value.Value.ToString(format, formatProvider);
    }
}
