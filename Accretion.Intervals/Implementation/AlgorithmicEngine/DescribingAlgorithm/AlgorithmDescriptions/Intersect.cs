using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal readonly struct Intersect<T> : IAlgorithmDescription<T> where T : IComparable<T>
    {
        public bool OperationIsCommutative => true;

        public bool OperationStateMatchesTheBeginningOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) => 
            state == OperationState.Highest;
        public bool OperationStateMatchesTheEndOfContinuousInterval(OperationState state = OperationState.Lowest, OperationStatus status = OperationStatus.Up, OperationDirection direction = OperationDirection.FirstToFirst) 
            => state == OperationState.Middle && status == OperationStatus.Down;

        public bool IsLess(in UpperBoundary<T> thisBoundary, in LowerBoundary<T> otherBoundary) => OverlapStrategies<T>.OverlapFullyClosed.IsLess(thisBoundary, otherBoundary);
        public bool IsLess(in LowerBoundary<T> thisBoundary, in UpperBoundary<T> otherBoundary) => OverlapStrategies<T>.OverlapFullyClosed.IsLess(thisBoundary, otherBoundary);
    }
}
