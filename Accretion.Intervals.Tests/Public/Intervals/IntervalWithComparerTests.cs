using System;

namespace Accretion.Intervals.Tests.AtomicInterval
{
    public class IntervalOfSingleWithComparerTests : IntervalTests<float, SingleComparerByExponent> { }
    public class IntervalOfDoubleWithComparerTests : IntervalTests<double, DoubleComparerByExponent> { }
    public class IntervalOfEvenIntegerWithComparer : IntervalTests<int, EvenIntegerComaparer> { }
    public class IntervalOfDateTimeWithComparer : IntervalTests<DateTime, DateTimeComparerByHour> { }
    public class IntervalOfValueClassWithComparerTests : IntervalTests<ValueClass, ValueClassBackwardsComparer> { }
}
