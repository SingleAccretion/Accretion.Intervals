using System;
using System.Globalization;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public abstract partial class IntervalTests<T, TComparer> : TestsBase where TComparer : struct, IBoundaryValueComparer<T>
    {

    }
}
