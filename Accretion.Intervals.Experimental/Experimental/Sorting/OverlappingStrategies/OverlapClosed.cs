using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    internal struct OverlapClosed : IOverlappingStrategy
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Resolve(bool thisBoundaryIsLower, bool thisBoundaryIsOpen, bool otherBoundaryIsOpen)
        {
            return thisBoundaryIsOpen && otherBoundaryIsOpen ?
                   thisBoundaryIsLower ? 1 : -1 :
                   thisBoundaryIsLower ? -1 : 1;
        }
    }
}
