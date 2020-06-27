using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal readonly struct NoOverlap : IOverlappingStrategy
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LowerIsLessThanUpper(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType) => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool UpperIsLessThanLower(BoundaryType upperBoundaryType, BoundaryType lowerBoundaryType) => true;
    }
}
