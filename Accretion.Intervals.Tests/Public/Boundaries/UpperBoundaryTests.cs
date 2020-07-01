using FsCheck;
using FsCheck.Xunit;
using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests.Boundaries
{
    public class UpperBoundaryOfDoubleWithComparerTests : UpperBoundaryTests<double, DoubleComparerByExponent> { }
    public class UpperBoundaryOfValueClassWithComparerTests : UpperBoundaryTests<ValueClass, ValueClassBackwardsComparer> { }

    public abstract class UpperBoundaryTests<T, TComparer> : BoundaryTests<UpperBoundary<T, TComparer>, T, TComparer> where TComparer : struct, IComparer<T> { }

    public class UpperBoundaryOfDoubleWithDefaultComparerTests : UpperBoundaryTests<double, DefaultValueComparer<double>> { }
    public class UpperBoundaryOfInt32WithDefaultComparerTests : UpperBoundaryTests<int, DefaultValueComparer<int>> { }
    public class UpperBoundaryOfValueClassWithDefaultComparerTests : UpperBoundaryTests<ValueClass, DefaultValueComparer<ValueClass>> { }
    public class UpperBoundaryOfValueStructWithDefaultComparerTests : UpperBoundaryTests<ValueStruct, DefaultValueComparer<ValueStruct>> { }

    public abstract class UpperBoundaryTests<T> : BoundaryTests<UpperBoundary<T>, T, DefaultValueComparer<T>> where T : IComparable<T>
    {
        [Property]
        public Property DefaultComparerEqualityMatchesDirectEquality(UpperBoundary<T, DefaultValueComparer<T>> left, UpperBoundary<T, DefaultValueComparer<T>> right) =>
            (left.Equals(right) == (((UpperBoundary<T>)left) == ((UpperBoundary<T>)right))).ToProperty();

        [Property]
        public Property ImplicitConversionsWork(UpperBoundary<T, DefaultValueComparer<T>> left, UpperBoundary<T, DefaultValueComparer<T>> right) =>
            (((UpperBoundary<T>)left).Equals(right) == right.Equals((UpperBoundary<T>)left)).ToProperty();
    }

    public class UpperBoundaryOfDoubleTests : UpperBoundaryTests<double> { }
    public class UpperBoundaryOfInt32Tests : UpperBoundaryTests<int> { }
    public class UpperBoundaryOfValueClassTests : UpperBoundaryTests<ValueClass> { }
    public class UpperBoundaryOfValueStructTests : UpperBoundaryTests<ValueStruct> { }
}
