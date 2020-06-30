using FsCheck;
using FsCheck.Xunit;
using System;


namespace Accretion.Intervals.Tests
{
    [Properties(Arbitrary = new[] { typeof(Generators) })]
    public class LowerBoundaryWithComparerTests
    {
        [Property]
        public Property EqualityIsCommutativeForDouble(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property EqualityThroughOperatorsIsTheSameAsThroughMethodsForDouble(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.Equals(right) == (right == left)).ToProperty();

        [Property]
        public Property UnequalBoundariesMustBeDifferentForDouble(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.Type != right.Type || !left.Value.IsEqualTo<double, DoubleComparerByExponent>(right.Value)).When(left != right);

        [Property]
        public Property EqualBoundariesMustHaveEqualHashCodesForDouble(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.GetHashCode() == right.GetHashCode()).When(left == right);

        [Property]
        public Property ToStringEqualityIsBoundToBoundaryEqualityForDouble(LowerBoundary<double, DoubleComparerByExponent> left, LowerBoundary<double, DoubleComparerByExponent> right) =>
            (left.ToString() == right.ToString() == (left == right)).ToProperty();

        [Property]
        public Property TypePropertyIsIdempotentForDouble(LowerBoundary<double, DoubleComparerByExponent> boundary) =>
            (boundary.Type == boundary.Type).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotentForDouble(LowerBoundary<double, DoubleComparerByExponent> boundary) =>
            boundary.Value.IsEqualTo<double, DoubleComparerByExponent>(boundary.Value).ToProperty();

        [Property]
        public Property EqualityIsCommutativeForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> left, LowerBoundary<ValueClass, ValueClassBackwardsComparer> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property EqualityThroughOperatorsIsTheSameAsThroughMethodsForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> left, LowerBoundary<ValueClass, ValueClassBackwardsComparer> right) =>
            (left.Equals(right) == (right == left)).ToProperty();

        [Property]
        public Property UnequalBoundariesMustBeDifferentForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> left, LowerBoundary<ValueClass, ValueClassBackwardsComparer> right) =>
            (left.Type != right.Type || !left.Value.IsEqualTo<ValueClass, ValueClassBackwardsComparer>(right.Value)).When(left != right);

        [Property]
        public Property EqualBoundariesMustHaveEqualHashCodesForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> left, LowerBoundary<ValueClass, ValueClassBackwardsComparer> right) =>
            (left.GetHashCode() == right.GetHashCode()).When(left == right);

        [Property]
        public Property ToStringEqualityIsBoundToBoundaryEqualityForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> left, LowerBoundary<ValueClass, ValueClassBackwardsComparer> right) =>
            (left.ToString() == right.ToString() == (left == right)).ToProperty();

        [Property]
        public Property TypePropertyIsIdempotentForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> boundary) =>
            (boundary.Type == boundary.Type).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotentForClass(LowerBoundary<ValueClass, ValueClassBackwardsComparer> boundary) =>
            boundary.Value.IsEqualTo<ValueClass, ValueClassBackwardsComparer>(boundary.Value).ToProperty();
    }
}
