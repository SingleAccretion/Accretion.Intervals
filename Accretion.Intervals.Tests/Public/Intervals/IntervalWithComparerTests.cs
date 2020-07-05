using System;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public class IntervalOfDateTimeWithComparer : IntervalTests<DateTime, DateTimeComparerByHour> { }
    public class IntervalOfEvenIntegerWithComparer : IntervalTests<int, EvenIntegerComaparer> { }
    public class IntervalOfDoubleWithComparerTests : IntervalTests<double, DoubleComparerByExponent> { }
    public class IntervalOfSingleWithComparerTests : IntervalTests<float, SingleComparerByExponent> { }
    public class IntervalOfValueClassWithComparerTests : IntervalTests<ValueClass, ValueClassBackwardsComparer> { }
}
