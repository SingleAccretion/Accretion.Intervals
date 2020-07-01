namespace Accretion.Intervals.Tests.CreatingIntervals
{
    public class IntervalOfDoubleWithComparerCreateTests : IntervalCreateTests<double, DoubleComparerByExponent> { }
    public class IntervalOfSingleWithComparerCreateTests : IntervalCreateTests<float, SingleComparerByExponent> { }
    public class IntervalOfValueClassWithComparerCreateTests : IntervalCreateTests<ValueClass, ValueClassBackwardsComparer> { }
}
