using System;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.Boundaries
{
    public class LowerBoundaryOfDoubleWithComparerTests : LowerBoundaryTests<double, DoubleComparerByExponent> { }
    public class LowerBoundaryOfValueClassWithComparerTests : LowerBoundaryTests<ValueClass, ValueClassBackwardsComparer> { }

    public abstract class LowerBoundaryTests<T, TComparer> : BoundaryTests<LowerBoundary<T, TComparer>, T, TComparer> where TComparer : struct, IBoundaryValueComparer<T> { }

    public class LowerBoundaryOfDoubleWithDefaultComparerTests : LowerBoundaryTests<double, DefaultValueComparer<double>> { }
    public class LowerBoundaryOfInt32WithDefaultComparerTests : LowerBoundaryTests<int, DefaultValueComparer<int>> { }
    public class LowerBoundaryOfValueClassWithDefaultComparerTests : LowerBoundaryTests<ValueClass, DefaultValueComparer<ValueClass>> { }
    public class LowerBoundaryOfValueStructWithDefaultComparerTests : LowerBoundaryTests<ValueStruct, DefaultValueComparer<ValueStruct>> { }

    public abstract class LowerBoundaryTests<T> : BoundaryTests<LowerBoundary<T>, T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property DefaultComparerEqualityMatchesDirectEquality(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (left.Equals(right) == (((LowerBoundary<T>)left) == ((LowerBoundary<T>)right))).ToProperty();

        [Property]
        public Property ImplicitConversionsWork(LowerBoundary<T, DefaultValueComparer<T>> left, LowerBoundary<T, DefaultValueComparer<T>> right) =>
            (((LowerBoundary<T>)left).Equals(right) == right.Equals((LowerBoundary<T>)left)).ToProperty();
    }

    public class LowerBoundaryOfDoubleTests : LowerBoundaryTests<double> { }
    public class LowerBoundaryOfInt32Tests : LowerBoundaryTests<int> { }
    public class LowerBoundaryOfValueClassTests : LowerBoundaryTests<ValueClass> { }
    public class LowerBoundaryOfValueStructTests : LowerBoundaryTests<ValueStruct> { }
}
