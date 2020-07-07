using System;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.Boundaries
{
    public abstract class LowerBoundaryTests<T, TComparer> : BoundaryTests<LowerBoundary<T, TComparer>, T, TComparer> where TComparer : struct, IBoundaryValueComparer<T>
    {
        protected override LowerBoundary<T, TComparer> CreateBoundary(T value, BoundaryType boundaryType) => new LowerBoundary<T, TComparer>(value, boundaryType);
    }

    public class LowerBoundaryOfSingleWithComparerTests : LowerBoundaryTests<float, SingleComparerByExponent> { }
    public class LowerBoundaryOfDoubleWithComparerTests : LowerBoundaryTests<double, DoubleComparerByExponent> { }
    public class LowerBoundaryEvenIntegerWithComparerTests : LowerBoundaryTests<int, EvenIntegerComaparer> { }
    public class LowerBoundaryDateTimeWithComparerTests : LowerBoundaryTests<DateTime, DateTimeComparerByHour> { }
    public class LowerBoundaryOfValueClassWithComparerTests : LowerBoundaryTests<ValueClass, ValueClassBackwardsComparer> { }

    public abstract class LowerBoundaryTests<T> : LowerBoundaryTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property DefaultComparerEqualityMatchesDirectEquality(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (left.Equals(right) == (((LowerBoundary<T>)left) == ((LowerBoundary<T>)right))).ToProperty();

        [Property]
        public Property ImplicitConversionsWork(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (((LowerBoundary<T>)left).Equals(right) == right.Equals((LowerBoundary<T>)left)).ToProperty();
    }

    public class LowerBoundaryOfSingleTests : LowerBoundaryTests<float> { }
    public class LowerBoundaryOfDoubleTests : LowerBoundaryTests<double> { }
    public class LowerBoundaryOfDecimalTests : LowerBoundaryTests<decimal> { }
    public class LowerBoundaryOfInt32Tests : LowerBoundaryTests<int> { }
    public class LowerBoundaryOfDateTimeTests : LowerBoundaryTests<DateTime> { }
    public class LowerBoundaryOfValueClassTests : LowerBoundaryTests<ValueClass> { }
    public class LowerBoundaryOfValueStructTests : LowerBoundaryTests<ValueStruct> { }
}
