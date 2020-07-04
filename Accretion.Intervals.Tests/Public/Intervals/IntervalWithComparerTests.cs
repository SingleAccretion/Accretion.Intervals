namespace Accretion.Intervals.Tests.AtomicInterval
{
    public class IntervalOfDoubleWithComparerTests : IntervalTests<double, DoubleComparerByExponent> { }
    public class IntervalOfValueClassWithComparerTests : IntervalTests<ValueClass, ValueClassBackwardsComparer> { }
    public class IntervalOfValueClassWithExclusiveComparerTests : IntervalTests<ValueClass, PositiveValueClassComparer> { }
}
