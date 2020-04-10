using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal interface IOverlappingStrategy
    {
        int Resolve(bool thisBoundaryIsLower, bool thisBoundaryIsOpen, bool otherBoundaryIsOpen);
    }
}
