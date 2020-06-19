using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Accretion.Intervals
{
    internal static class IntervalOperations
    {
        public static ReadOnlyArray<ContinuousInterval<T>> Merge<T>(IEnumerable<ContinuousInterval<T>> continuousIntervals) where T : IComparable<T>
        {
            var sortedIntervals = continuousIntervals.ToArray();
            if (sortedIntervals.Length == 0)
            {
                return ReadOnlyArray<ContinuousInterval<T>>.Empty;
            }

            Array.Sort(sortedIntervals, (x, y) => x.LowerBoundary.IsLessThan(y.LowerBoundary) ? ComparingValues.IsLess : ComparingValues.IsGreater);

            var k = 0;
            var length = 0;
            while (k < sortedIntervals.Length && sortedIntervals[k].IsEmpty)
            {
                k++;
            }
            if (k < sortedIntervals.Length)
            {
                sortedIntervals[0] = sortedIntervals[k];
                length = 1;
                for (int i = k, j = k; i < sortedIntervals.Length; i++)
                {
                    var nextInterval = sortedIntervals[i];
                    var currentInterval = sortedIntervals[j];

                    if (!nextInterval.IsEmpty)
                    {
                        if (!nextInterval.LowerBoundary.IsLessThan(currentInterval.UpperBoundary, OverlapStrategies<T>.OverlapClosed))
                        {
                            length++;
                            j++;
                            sortedIntervals[j] = nextInterval;
                        }
                        else if (!nextInterval.UpperBoundary.IsLessThan(currentInterval.UpperBoundary))
                        {
                            sortedIntervals[j] = new ContinuousInterval<T>(currentInterval.LowerBoundary, nextInterval.UpperBoundary);
                        }
                    }
                }
            }

            return new ReadOnlyArray<ContinuousInterval<T>>(sortedIntervals, length);
        }

        public static ReadOnlyArray<ContinuousInterval<T>> Union<T>(ReadOnlyArray<ContinuousInterval<T>> first, ReadOnlyArray<ContinuousInterval<T>> second) where T : IComparable<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new ContinuousInterval<T>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            LowerBoundary<T> currentLowerBoundary;

            if (maxFirst == -1)
            {
                return MergeTail(mergedIntervals, secondArray, s, maxSecond, m);
            }
            else if (maxSecond == -1)
            {
                return MergeTail(mergedIntervals, firstArray, f, maxFirst, m);
            }

        LowerFirstLowerSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].LowerBoundary))
            {
                currentLowerBoundary = firstArray[f].LowerBoundary;
                goto UpperFirstLowerSecond;
            }
            else
            {
                currentLowerBoundary = secondArray[s].LowerBoundary;
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].UpperBoundary, OverlapStrategies<T>.OverlapClosed))
            {
                operationState++;
                if (operationState == OperationState.Middle)
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (operationState == OperationState.Lowest)
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return MergeTail(mergedIntervals, firstArray, f, maxFirst, m);
                }

                s++;
                goto LowerFirstLowerSecond;
            }

        UpperFirstLowerSecond:
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].LowerBoundary, OverlapStrategies<T>.OverlapClosed))
            {
                operationState--;
                if (operationState == OperationState.Lowest)
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return MergeTail(mergedIntervals, secondArray, s, maxSecond, m);
                }

                f++;
                goto LowerFirstLowerSecond;
            }
            else
            {
                operationState++;
                if (operationState == OperationState.Middle)
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                if (f == maxFirst)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, secondArray, currentLowerBoundary, s, maxSecond, m);
                }
                f++;

                goto LowerFirstUpperSecond;
            }
            else
            {
                if (s == maxSecond)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, firstArray, currentLowerBoundary, f, maxFirst, m);
                }
                s++;

                goto UpperFirstLowerSecond;
            }
        }

        public static ReadOnlyArray<ContinuousInterval<T>> Intersect<T>(ReadOnlyArray<ContinuousInterval<T>> first, ReadOnlyArray<ContinuousInterval<T>> second) where T : IComparable<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new ContinuousInterval<T>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            LowerBoundary<T> currentLowerBoundary = default;

            if (maxFirst == -1 || maxSecond == -1)
            {
                return ReadOnlyArray<ContinuousInterval<T>>.Empty;
            }

        LowerFirstLowerSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].LowerBoundary))
            {
                goto UpperFirstLowerSecond;
            }
            else
            {
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].UpperBoundary, OverlapStrategies<T>.OverlapFullyClosed))
            {
                operationState++;
                if (operationState == OperationState.Highest)
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (operationState == OperationState.Middle)
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, m);
                }

                s++;
                goto LowerFirstLowerSecond;
            }

        UpperFirstLowerSecond:
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].LowerBoundary, OverlapStrategies<T>.OverlapFullyClosed))
            {
                operationState--;
                if (operationState == OperationState.Middle)
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, m);
                }

                f++;
                goto LowerFirstLowerSecond;
            }
            else
            {
                operationState++;
                if (operationState == OperationState.Highest)
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                m++;
                if (f == maxFirst)
                {
                    return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, m);
                }
                f++;

                goto LowerFirstUpperSecond;
            }
            else
            {
                mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                m++;
                if (s == maxSecond)
                {
                    return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, m);
                }
                s++;

                goto UpperFirstLowerSecond;
            }
        }

        //NOT IMPLEMENTED YET
        public static ReadOnlyArray<ContinuousInterval<T>> SymmetricDifference<T, D>(ReadOnlyArray<ContinuousInterval<T>> first, ReadOnlyArray<ContinuousInterval<T>> second, D description)
            where T : IComparable<T> where D : IAlgorithmDescription<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new ContinuousInterval<T>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            OperationDirection operationDirection = default;
            LowerBoundary<T> currentLowerBoundary = default;

            if (maxFirst == -1)
            {
                return MergeTail(mergedIntervals, secondArray, s, maxSecond, m, description);
            }
            else if (maxSecond == -1)
            {
                return MergeTail(mergedIntervals, firstArray, f, maxFirst, m, description);
            }

        LowerFirstLowerSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].LowerBoundary))
            {
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }
                goto UpperFirstLowerSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].UpperBoundary, description))
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfContinuousInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return MergeTail(mergedIntervals, firstArray, f, maxFirst, m, description);
                }

                s++;
                goto LowerFirstLowerSecond;
            }

        UpperFirstLowerSecond:
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].LowerBoundary, description))
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfContinuousInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return MergeTail(mergedIntervals, secondArray, s, maxSecond, m, description);
                }

                f++;
                goto LowerFirstLowerSecond;
            }
            else
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                if (description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, secondArray, currentLowerBoundary, s, maxSecond, m, description);
                }
                f++;

                goto LowerFirstUpperSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, firstArray, currentLowerBoundary, f, maxFirst, m, description);
                }
                s++;

                goto UpperFirstLowerSecond;
            }
        }

        public static bool Contains<T>(ReadOnlyArray<ContinuousInterval<T>> continuousIntervals, T value) where T : IComparable<T>
        {
            bool ValueIsBetweenThesePivots(int leftPivot, int rightPivot) => new ContinuousInterval<T>(continuousIntervals[leftPivot].LowerBoundary, continuousIntervals[rightPivot].UpperBoundary).Contains(value);

            if (continuousIntervals.Count == 0)
            {
                return false;
            }

            int leftPivot = 0;
            int rightPivot = continuousIntervals.Count - 1;

            int FindMiddlePivot() => (leftPivot + rightPivot) / 2;

            int middlePivot = FindMiddlePivot();

            bool ValueIsToTheLeft() => ValueIsBetweenThesePivots(leftPivot, middlePivot);
            bool ValueIsToTheRight() => ValueIsBetweenThesePivots(middlePivot, rightPivot);

            while (rightPivot > leftPivot + 1)
            {
                if (ValueIsToTheLeft())
                {
                    rightPivot = middlePivot;
                    middlePivot = FindMiddlePivot();
                }
                else if (ValueIsToTheRight())
                {
                    leftPivot = middlePivot;
                    middlePivot = FindMiddlePivot();
                }
                else
                {
                    return false;
                }
            }

            return ValueIsBetweenThesePivots(leftPivot, leftPivot) || ValueIsBetweenThesePivots(rightPivot, rightPivot);
        }

        public static ReadOnlyArray<ContinuousInterval<T>> MergeTailStartingWithUpperBoundary<T>(ContinuousInterval<T>[] mergedIntervals, ContinuousInterval<T>[] sourceIntervals, in LowerBoundary<T> lowerBoundary, int sourceIndex, int maxSourceIndex, int mergerIndex) where T : IComparable<T>
        {
            mergedIntervals[mergerIndex] = new ContinuousInterval<T>(lowerBoundary, sourceIntervals[sourceIndex].UpperBoundary);

            return MergeTail(mergedIntervals, sourceIntervals, sourceIndex + 1, maxSourceIndex, mergerIndex + 1);
        }

        public static ReadOnlyArray<ContinuousInterval<T>> MergeTail<T>(ContinuousInterval<T>[] mergedIntervals, ContinuousInterval<T>[] sourceIntervals, int sourceIndex, int maxSourceIndex, int mergerIndex) where T : IComparable<T>
        {
            var length = maxSourceIndex - sourceIndex + 1;
            Copying.Copy(sourceIntervals, sourceIndex, mergedIntervals, mergerIndex, length);

            return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, mergerIndex + length);
        }

        public static ReadOnlyArray<ContinuousInterval<T>> Merge<T, D>(ReadOnlyArray<ContinuousInterval<T>> first, ReadOnlyArray<ContinuousInterval<T>> second, D description)
            where T : IComparable<T> where D : IAlgorithmDescription<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new ContinuousInterval<T>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            OperationDirection operationDirection = default;
            LowerBoundary<T> currentLowerBoundary = default;

            if (maxFirst == -1)
            {
                return MergeTail(mergedIntervals, secondArray, s, maxSecond, m, description);
            }
            else if (maxSecond == -1)
            {
                return MergeTail(mergedIntervals, firstArray, f, maxFirst, m, description);
            }

        LowerFirstLowerSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].LowerBoundary))
            {
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }
                goto UpperFirstLowerSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan(secondArray[s].UpperBoundary, description))
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfContinuousInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return MergeTail(mergedIntervals, firstArray, f, maxFirst, m, description);
                }

                s++;
                goto LowerFirstLowerSecond;
            }

        UpperFirstLowerSecond:
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].LowerBoundary, description))
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfContinuousInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return MergeTail(mergedIntervals, secondArray, s, maxSecond, m, description);
                }

                f++;
                goto LowerFirstLowerSecond;
            }
            else
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfContinuousInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                if (description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, secondArray, currentLowerBoundary, s, maxSecond, m, description);
                }
                f++;

                goto LowerFirstUpperSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new ContinuousInterval<T>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return MergeTailStartingWithUpperBoundary(mergedIntervals, firstArray, currentLowerBoundary, f, maxFirst, m, description);
                }
                s++;

                goto UpperFirstLowerSecond;
            }
        }

        public static ReadOnlyArray<ContinuousInterval<T>> MergeTailStartingWithUpperBoundary<T, D>(
            ContinuousInterval<T>[] mergedIntervals, ContinuousInterval<T>[] sourceIntervals, in LowerBoundary<T> lowerBoundary, int sourceIndex, int maxSourceIndex, int mergerIndex, D description)
            where D : IAlgorithmDescription<T> where T : IComparable<T>
        {
            if (description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Lowest))
            {
                mergedIntervals[mergerIndex] = new ContinuousInterval<T>(lowerBoundary, sourceIntervals[sourceIndex].UpperBoundary);
                mergerIndex++;
                sourceIndex++;
            }

            return MergeTail(mergedIntervals, sourceIntervals, sourceIndex, maxSourceIndex, mergerIndex, description);
        }

        public static ReadOnlyArray<ContinuousInterval<T>> MergeTail<T, D>(
            ContinuousInterval<T>[] mergedIntervals, ContinuousInterval<T>[] sourceIntervals, int sourceIndex, int maxSourceIndex, int mergerIndex, D description)
            where D : IAlgorithmDescription<T> where T : IComparable<T>
        {
            if (description.OperationStateMatchesTheBeginningOfContinuousInterval(OperationState.Middle, OperationStatus.Up) &&
                description.OperationStateMatchesTheEndOfContinuousInterval(OperationState.Lowest))
            {
                var length = maxSourceIndex - sourceIndex + 1;
                Copying.Copy(sourceIntervals, sourceIndex, mergedIntervals, mergerIndex, length);
                mergerIndex += length;
            }

            return new ReadOnlyArray<ContinuousInterval<T>>(mergedIntervals, mergerIndex);
        }

    }
}
