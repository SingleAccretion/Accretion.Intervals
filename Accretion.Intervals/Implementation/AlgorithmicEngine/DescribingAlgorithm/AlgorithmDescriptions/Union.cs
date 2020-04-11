using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct Union<T> : IAlgorithmDescription<T> where T : IComparable<T>
    {        
        public bool OperationIsCommutative => true;

        public bool OperationStateMatchesTheBeginningOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) =>
            state == OperationState.Middle && status == OperationStatus.Up;
        public bool OperationStateMatchesTheEndOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) => 
            state == OperationState.Lowest;

        public bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary) => OverlapStrategies<T>.OverlapClosed.IsLess(thisBoundary, otherBoundary);
        public bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary) => OverlapStrategies<T>.OverlapClosed.IsLess(thisBoundary, otherBoundary);        
    }
}
