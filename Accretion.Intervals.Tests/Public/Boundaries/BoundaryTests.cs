using FsCheck;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public abstract class BoundaryTests<TBoundary, T, TComparer> : BaseTests where TComparer : struct, IComparer<T> where TBoundary : IBoundary<T>
    {
        [Property]
        public Property EqualityIsCommutative(TBoundary left, TBoundary right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property UnequalBoundariesMustBeDifferent(TBoundary left, TBoundary right) =>
            (left.Type != right.Type || !left.Value.IsEqualTo<T, TComparer>(right.Value)).When(!left.Equals(right));

        [Property(StartSize = 0, EndSize = 100)]
        public Property EqualBoundariesMustHaveEqualHashCodes(TBoundary[] boundaries) =>
            boundaries.All(x => boundaries.All(y => !x.Equals(y) || x.GetHashCode() == y.GetHashCode())).ToProperty();

        [Property]
        public Property ToStringEqualityIsBoundToBoundaryEquality(TBoundary left, TBoundary right) =>
            (left.ToString().Equals(right.ToString()) == left.Equals(right)).ToProperty();

        [Property]
        public Property TypePropertyIsIdempotent(TBoundary boundary) =>
            (boundary.Type == boundary.Type).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotent(TBoundary boundary) =>
            (boundary.Value.IsEqualTo<T, TComparer>(boundary.Value) || boundary.Value is double.NaN).ToProperty();
    }
}
