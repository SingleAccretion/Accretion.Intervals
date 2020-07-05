using System;
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
            (!left.Equals(right)).Implies(
             !Result.From(() => left.Type).Equals(Result.From(() => right.Type)) ||
             !Result.From(() => left.Value).Equals<TComparer>(Result.From(() => right.Value)));

        [Property]
        public Property AllInvalidBoundariesAreEqual(InvalidBoundaryValue<T, TComparer> firstValue, BoundaryType firstBoundaryType, InvalidBoundaryValue<T, TComparer> secondValue, BoundaryType secondBoundaryType) =>
            (firstValue.DoesExist && CreateBoundary(firstValue.Value, firstBoundaryType).Equals(CreateBoundary(secondValue.Value, secondBoundaryType))).Or(!firstValue.DoesExist);

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
            Result.From(() => boundary.Type).Equals(Result.From(() => boundary.Type)).ToProperty();

        [Property]
        public Property ValuePropertyIsIdempotent(TBoundary boundary) =>
            Result.From(() => boundary.Value).Equals<TComparer>(Result.From(() => boundary.Value)).ToProperty();

        [Property]
        public Property InvalidBoundariesThrowAndValidOnesDoNot(BoundaryType boundaryType, T value)
        {
            var boundary = CreateBoundary(value, boundaryType);
            var valueResult = Result.From(() => boundary.Value);
            var typeResult = Result.From(() => boundary.Type);

            return InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(value) ?
                   (valueResult.Exception is InvalidOperationException && typeResult.Exception is InvalidOperationException).ToProperty() :
                   (valueResult.HasValue && typeResult.HasValue).ToProperty();
        }

        protected abstract TBoundary CreateBoundary(T value, BoundaryType boundaryType);
    }
}
