using System;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.Boundaries
{
    public abstract class UpperBoundaryTests<T, TComparer> : BoundaryTests<UpperBoundary<T, TComparer>, T, TComparer> where TComparer : struct, IBoundaryValueComparer<T>
    {
        protected override UpperBoundary<T, TComparer> CreateBoundary(T value, BoundaryType boundaryType) => new UpperBoundary<T, TComparer>(value, boundaryType);
    }

    public class UpperBoundaryOfSingleWithComparerTests : UpperBoundaryTests<float, SingleComparerByExponent> { }
    public class UpperBoundaryOfDoubleWithComparerTests : UpperBoundaryTests<double, DoubleComparerByExponent> { }
    public class UpperBoundaryEvenIntegerWithComparerTests : UpperBoundaryTests<int, EvenIntegerComaparer> { }
    public class UpperBoundaryDateTimeWithComparerTests : UpperBoundaryTests<DateTime, DateTimeComparerByHour> { }
    public class UpperBoundaryOfValueClassWithComparerTests : UpperBoundaryTests<ValueClass, ValueClassBackwardsComparer> { }

    public abstract class UpperBoundaryTests<T> : UpperBoundaryTests<T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property DefaultComparerEqualityMatchesDirectEquality(UpperBoundary<T, DefaultValueComparer<T>> left, UpperBoundary<T, DefaultValueComparer<T>> right) =>
            (left.Equals(right) == (((UpperBoundary<T>)left) == ((UpperBoundary<T>)right))).ToProperty();

        [Property]
        public Property ImplicitConversionsWork(UpperBoundary<T, DefaultValueComparer<T>> left, UpperBoundary<T, DefaultValueComparer<T>> right) =>
            (((UpperBoundary<T>)left).Equals(right) == right.Equals((UpperBoundary<T>)left)).ToProperty();
    }

    public class UpperBoundaryOfSingleTests : UpperBoundaryTests<float> { }
    public class UpperBoundaryOfDoubleTests : UpperBoundaryTests<double> { }
    public class UpperBoundaryOfDecimalTests : UpperBoundaryTests<decimal> { }
    public class UpperBoundaryOfInt32Tests : UpperBoundaryTests<int> { }
    public class UpperBoundaryOfDateTimeTests : UpperBoundaryTests<DateTime> { }
    public class UpperBoundaryOfValueClassTests : UpperBoundaryTests<ValueClass> { }
    public class UpperBoundaryOfValueStructTests : UpperBoundaryTests<ValueStruct> { }
}
