using System.Collections.Generic;

namespace Accretion.Intervals
{
    internal interface IOverlappingStrategy
    {
        bool UpperIsLessThanLower(BoundaryType upperBoundaryType, BoundaryType lowerBoundaryType);
        bool LowerIsLessThanUpper(BoundaryType lowerBoundaryType, BoundaryType upperBoundaryType);
    }
}
