using FsCheck;
using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public abstract class LowerBoundaryTests<T, TComparer> : BaseTests where TComparer : struct, IComparer<T>
    {
        [Property]
        public Property EqualityIsCommutative(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property EqualityThroughOperatorsIsTheSameAsThroughMethods(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) =>
            (left.Equals(right) == (right == left)).ToProperty();

        [Property]
        public Property UnequalBoundariesMustBeDifferent(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) =>
            (left.Type != right.Type || !left.Value.IsEqualTo<T, TComparer>(right.Value)).When(left != right);

        [Property(StartSize = 0, EndSize = 100)]
        public Property EqualBoundariesMustHaveEqualHashCodes(LowerBoundary<T, TComparer>[] boundaries) =>
            boundaries.All(x => boundaries.All(y => x != y || x.GetHashCode() == y.GetHashCode())).ToProperty();

        [Property]
        public Property ToStringEqualityIsBoundToBoundaryEquality(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) =>
            (left.ToString() == right.ToString() == (left == right)).ToProperty();

        [Property]
        public Property TypePropertyIsIdempotent(LowerBoundary<T, TComparer> boundary) =>
            (boundary.Type == boundary.Type).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotent(LowerBoundary<T, TComparer> boundary) =>
            (boundary.Value.IsEqualTo<T, TComparer>(boundary.Value) || boundary.Value is double.NaN).ToProperty();
    }

    public abstract class LowerBoundaryTests<T> : LowerBoundaryTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property DefaultComparerEqualityMatchesDirectEquality(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (left.Equals(right) == (((LowerBoundary<T>)left) == ((LowerBoundary<T>)right))).ToProperty();

        [Property]
        public Property ImplicitConversionsWork(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (((LowerBoundary<T>)left).Equals(right) == right.Equals((LowerBoundary<T>)left)).ToProperty();
    }

    public class LowerBoundaryOfDoubleWithComparerTests :  LowerBoundaryTests<double, DoubleComparerByExponent> { }
    public class LowerBoundaryOfValueClassWithComparerTests : LowerBoundaryTests<ValueClass, ValueClassBackwardsComparer> { }
    public class LowerBoundaryOfDoubleTests : LowerBoundaryTests<double> { }
    public class LowerBoundaryOfInt32Tests : LowerBoundaryTests<int> { }
    public class LowerBoundaryOfValueClassTests : LowerBoundaryTests<ValueClass> { }
    public class LowerBoundaryOfValueStructTests : LowerBoundaryTests<ValueStruct> { }
}
