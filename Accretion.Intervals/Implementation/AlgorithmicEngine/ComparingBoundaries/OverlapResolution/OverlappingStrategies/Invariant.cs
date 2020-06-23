using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
