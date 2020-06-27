using Xunit;

namespace Accretion.Intervals.Tests
{
    public class OverlappingStrategiesTests
    {
        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, false)]
        public void TestInvariantOverlapLowerIsLessThanLower(BoundaryType firstLowerBoundaryType, BoundaryType secondLowerBoundaryType, bool expectedResult) =>
            Assert.Equal(expectedResult, default(Invariant).LowerIsLessThanLower(firstLowerBoundaryType, secondLowerBoundaryType));

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, false)]
        public void TestInvariantOverlapUpperIsLessThanUpper(BoundaryType firstUpperBoundaryType, BoundaryType secondUpperBoundaryType, bool expectedResult) =>
            Assert.Equal(expectedResult, default(Invariant).LowerIsLessThanLower(firstUpperBoundaryType, secondUpperBoundaryType));

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, true)]
        public void TestFullOverlap(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType, bool overlaps)
        {
            Assert.Equal(overlaps, default(FullOverlap).LowerIsLessThanUpper(lowerBoundaryType, upperBoundaryType));
            Assert.Equal(!overlaps, default(FullOverlap).UpperIsLessThanLower(upperBoundaryType, lowerBoundaryType));
        }

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, false)]
        public void TestNoOverlap(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType, bool overlaps)
        {
            Assert.Equal(overlaps, default(NoOverlap).LowerIsLessThanUpper(lowerBoundaryType, upperBoundaryType));
            Assert.Equal(!overlaps, default(NoOverlap).UpperIsLessThanLower(upperBoundaryType, lowerBoundaryType));
        }

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, false)]
        public void TestOverlapClosed(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType, bool overlaps)
        {
            Assert.Equal(overlaps, default(OverlapClosed).LowerIsLessThanUpper(lowerBoundaryType, upperBoundaryType));
            Assert.Equal(!overlaps, default(OverlapClosed).UpperIsLessThanLower(upperBoundaryType, lowerBoundaryType));
        }

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, true)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, false)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, false)]
        public void TestOverlapFullyClosedLowerIsLessThanUpper(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType, bool overlaps)
        {
            Assert.Equal(overlaps, default(OverlapFullyClosed).LowerIsLessThanUpper(lowerBoundaryType, upperBoundaryType));
            Assert.Equal(!overlaps, default(OverlapFullyClosed).UpperIsLessThanLower(upperBoundaryType, lowerBoundaryType));
        }
    }
}
