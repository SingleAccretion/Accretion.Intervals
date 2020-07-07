using System;
using System.Globalization;
using FsCheck;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract partial class IntervalTests<T> : IntervalTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {

    }
}
