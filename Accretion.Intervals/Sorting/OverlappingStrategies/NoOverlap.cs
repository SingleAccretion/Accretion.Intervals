using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal struct NoOverlap : IOverlappingStrategy
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Resolve(bool thisBoundaryIsLower, bool thisBoundaryIsOpen, bool otherBoundaryIsOpen)
        {
            return thisBoundaryIsLower ? 1 : -1;
        }
    }
}
