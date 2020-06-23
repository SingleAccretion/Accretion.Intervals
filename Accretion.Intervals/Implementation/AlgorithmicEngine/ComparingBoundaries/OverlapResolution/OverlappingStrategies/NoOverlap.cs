using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
