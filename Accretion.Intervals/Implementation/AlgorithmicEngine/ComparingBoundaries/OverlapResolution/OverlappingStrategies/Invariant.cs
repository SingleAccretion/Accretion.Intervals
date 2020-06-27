using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal readonly struct Invariant
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LowerIsLessThanLower(BoundaryType firstBoundaryType, BoundaryType secondBoundaryType) => 
            firstBoundaryType == BoundaryType.Closed & secondBoundaryType == BoundaryType.Open;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool UpperIsLessThanUpper(BoundaryType firstBoundaryType, BoundaryType secondBoundaryType) => 
            firstBoundaryType == BoundaryType.Open & secondBoundaryType == BoundaryType.Closed;
    }
}
