using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    internal interface IOverlappingStrategy
    {
        int Resolve(bool thisBoundaryIsLower, bool thisBoundaryIsOpen, bool otherBoundaryIsOpen);
    }
}
