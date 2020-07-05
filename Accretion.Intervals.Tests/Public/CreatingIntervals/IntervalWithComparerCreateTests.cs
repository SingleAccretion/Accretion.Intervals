namespace Accretion.Intervals.Tests.CreatingIntervals
{
    public class IntervalOfSingleWithComparerCreateTests : IntervalCreateTests<float, SingleComparerByExponent> { }
    public class IntervalOfDoubleWithComparerCreateTests : IntervalCreateTests<double, DoubleComparerByExponent> { }
    public class IntervalOfValueClassWithComparerCreateTests : IntervalCreateTests<ValueClass, ValueClassBackwardsComparer> { }
}
