using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal class SymmetricDifference<T> : IAlgorithmDescription<T> where T : IComparable<T>
    {
        public bool OperationIsCommutative => true;
        
        public bool OperationStateMatchesTheBeginningOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) => throw new NotImplementedException();
        public bool OperationStateMatchesTheEndOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) => throw new NotImplementedException();
        
        public bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary) => throw new NotImplementedException();
        public bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary) => throw new NotImplementedException();        
    }
}
