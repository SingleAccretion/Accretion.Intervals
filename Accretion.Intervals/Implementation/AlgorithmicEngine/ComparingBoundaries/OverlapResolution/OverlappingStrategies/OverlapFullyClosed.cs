using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal readonly struct OverlapFullyClosed : IOverlappingStrategy
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LowerIsLessThanUpper(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType) =>
            lowerBoundaryType == BoundaryType.Closed & upperBoundaryType == BoundaryType.Closed;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool UpperIsLessThanLower(BoundaryType upperBoundaryType, BoundaryType lowerBoundaryType) =>
            upperBoundaryType == BoundaryType.Open | lowerBoundaryType == BoundaryType.Open;
    }
}
