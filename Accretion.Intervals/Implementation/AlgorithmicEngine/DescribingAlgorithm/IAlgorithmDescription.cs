using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal interface IAlgorithmDescription<T> : IOverlappingStrategy<T> where T : IComparable<T>
    {
        bool OperationStateMatchesTheBeginningOfContinuousInterval(OperationState state = default, OperationStatus status = default, OperationDirection direction = default);
        bool OperationStateMatchesTheEndOfContinuousInterval(OperationState state = default, OperationStatus status = default, OperationDirection direction = default);

        public bool OperationIsCommutative { get; }
    }
}
