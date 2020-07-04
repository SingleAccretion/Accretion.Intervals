using System.Globalization;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests.Boundaries
{
    public abstract class BoundaryTests<TBoundary, T, TComparer> : TestsBase where TComparer : struct, IBoundaryValueComparer<T> where TBoundary : IBoundary<T>
    {
        [Property]
        public Property EqualityIsCommutative(TBoundary left, TBoundary right) =>
            (left.Equals(right) == right.Equals(left)).ToProperty();

        [Property]
        public Property UnequalBoundariesMustBeDifferent(TBoundary left, TBoundary right) =>
            (left.Type != right.Type || !left.Value.IsEqualTo<T, TComparer>(right.Value)).When(!left.Equals(right));

        [Property(EndSize = 100)]
        public Property EqualBoundariesMustHaveEqualHashCodes(TBoundary[] boundaries) =>
            boundaries.All(x => boundaries.All(y => !x.Equals(y) || x.GetHashCode() == y.GetHashCode())).ToProperty();

        [Property]
        public Property ToStringEqualityIsBoundToBoundaryEquality(TBoundary left, TBoundary right) =>
            (left.Equals(right) == left.ToString().Equals(right.ToString())).ToProperty();

        [Property]
        public Property ToStringDefaultDoesNotChangeWithCulture(TBoundary boundary, CultureInfo cultureInfo)
        {
            var inititalString = boundary.ToString();
            var initialCulture = CultureInfo.CurrentCulture;

            CultureInfo.CurrentCulture = cultureInfo;
            var changedString = boundary.ToString();
            CultureInfo.CurrentCulture = initialCulture;

            return inititalString.Equals(changedString).ToProperty();
        }

        [Property]
        public Property TypePropertyIsIdempotent(TBoundary boundary) =>
            (boundary.Type == boundary.Type).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotent(TBoundary boundary) =>
            boundary.Value.IsEqualTo<T, TComparer>(boundary.Value).ToProperty();
    }
}
