using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests
{
    public class LowerBoundaryWithComparerTests : TestBase
    {
        [Property]
        public Property EqualityIsCommutative(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) => 
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property EqualityThroughOperatorsIsTheSameAsThroughMethods(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property EqualBoundariesMustHaveEqualValues(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (!left.Equals(right) || (default(DoubleComparerByExponent).Compare(left.Value, right.Value) == 0)).ToProperty();

        [Property]
        public Property EqualBoundariesMustHaveEqualTypes(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (!left.Equals(right) || (left.Type == right.Type)).ToProperty();
    }
}
