using System;
using System.Collections.Generic;
using System.Linq;

namespace Accretion.Intervals
{
    internal static class IntervalOperations
    {
        public static ReadOnlyArray<Interval<T, TComparer>> Merge<T, TComparer>(IEnumerable<Interval<T, TComparer>> continuousIntervals) where TComparer : struct, IComparer<T>
        {
            var sortedIntervals = continuousIntervals.ToArray();
            if (sortedIntervals.Length == 0)
            {
                return ReadOnlyArray<Interval<T, TComparer>>.Empty;
            }

            Array.Sort(sortedIntervals, (x, y) =>x.LowerBoundary.IsLessThan(y.LowerBoundary) ? ComparingValues.IsLess : ComparingValues.IsGreater);

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
                        if (!nextInterval.LowerBoundary.IsLessThan<T, TComparer, OverlapClosed>(currentInterval.UpperBoundary))
                        {
                            length++;
                            j++;
                            sortedIntervals[j] = nextInterval;
                        }
                        else if (!nextInterval.UpperBoundary.IsLessThan(currentInterval.UpperBoundary))
                        {
                            sortedIntervals[j] = new Interval<T, TComparer>(currentInterval.LowerBoundary, nextInterval.UpperBoundary);
                        }
                    }
                }
            }

            return new ReadOnlyArray<Interval<T, TComparer>>(sortedIntervals, length);
        }

        public static ReadOnlyArray<Interval<T, TComparer>> Union<T, TComparer>(ReadOnlyArray<Interval<T, TComparer>> first, ReadOnlyArray<Interval<T, TComparer>> second) where TComparer : struct, IComparer<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new Interval<T, TComparer>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            LowerBoundary<T, TComparer> currentLowerBoundary;

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
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer>(secondArray[s].LowerBoundary))
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
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer, OverlapClosed>(secondArray[s].UpperBoundary))
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
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
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
            if (firstArray[f].UpperBoundary.IsLessThan<T, TComparer, OverlapClosed>(secondArray[s].LowerBoundary))
            {
                operationState--;
                if (operationState == OperationState.Lowest)
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
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

        public static ReadOnlyArray<Interval<T, TComparer>> Intersect<T, TComparer>(ReadOnlyArray<Interval<T, TComparer>> first, ReadOnlyArray<Interval<T, TComparer>> second) where TComparer : struct, IComparer<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new Interval<T, TComparer>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            LowerBoundary<T, TComparer> currentLowerBoundary = default;

            if (maxFirst == -1 || maxSecond == -1)
            {
                return ReadOnlyArray<Interval<T, TComparer>>.Empty;
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
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer, OverlapFullyClosed>(secondArray[s].UpperBoundary))
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
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
                    m++;
                }
                if (s == maxSecond)
                {
                    return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, m);
                }

                s++;
                goto LowerFirstLowerSecond;
            }

        UpperFirstLowerSecond:
            if (firstArray[f].UpperBoundary.IsLessThan<T, TComparer, OverlapFullyClosed>(secondArray[s].LowerBoundary))
            {
                operationState--;
                if (operationState == OperationState.Middle)
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
                    m++;
                }
                if (f == maxFirst)
                {
                    return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, m);
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
                mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
                m++;
                if (f == maxFirst)
                {
                    return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, m);
                }
                f++;

                goto LowerFirstUpperSecond;
            }
            else
            {
                mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
                m++;
                if (s == maxSecond)
                {
                    return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, m);
                }
                s++;

                goto UpperFirstLowerSecond;
            }
        }

        //NOT IMPLEMENTED
        /*
        public static ReadOnlyArray<Interval<T, TComparer>> SymmetricDifference<T, TComparer>(ReadOnlyArray<Interval<T, TComparer>> first, ReadOnlyArray<Interval<T, TComparer>> second)
            where TComparer : struct, IComparer<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = new Interval<T, TComparer>[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            OperationDirection operationDirection = default;
            LowerBoundary<T, TComparer> currentLowerBoundary = default;

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
                if (description.OperationStateMatchesTheBeginningOfInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }
                goto UpperFirstLowerSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheBeginningOfInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer>(secondArray[s].UpperBoundary, description))
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
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
            if (firstArray[f].UpperBoundary.IsLessThan<T, TComparer>(secondArray[s].LowerBoundary, description))
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
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
                if (description.OperationStateMatchesTheBeginningOfInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                if (description.OperationStateMatchesTheEndOfInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
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
                if (description.OperationStateMatchesTheEndOfInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
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
        */

        public static bool Contains<T, TComparer>(ReadOnlyArray<Interval<T, TComparer>> continuousIntervals, T value) where TComparer : struct, IComparer<T>
        {
            bool ValueIsBetweenThesePivots(int leftPivot, int rightPivot) => 
                new Interval<T, TComparer>(continuousIntervals[leftPivot].LowerBoundary, continuousIntervals[rightPivot].UpperBoundary).Contains(value);

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

        public static ReadOnlyArray<Interval<T, TComparer>> MergeTailStartingWithUpperBoundary<T, TComparer>(Interval<T, TComparer>[] mergedIntervals, Interval<T, TComparer>[] sourceIntervals, in LowerBoundary<T, TComparer> lowerBoundary, int sourceIndex, int maxSourceIndex, int mergerIndex) where TComparer : struct, IComparer<T>
        {
            mergedIntervals[mergerIndex] = new Interval<T, TComparer>(lowerBoundary, sourceIntervals[sourceIndex].UpperBoundary);

            return MergeTail(mergedIntervals, sourceIntervals, sourceIndex + 1, maxSourceIndex, mergerIndex + 1);
        }

        public static ReadOnlyArray<Interval<T, TComparer>> MergeTail<T, TComparer>(Interval<T, TComparer>[] mergedIntervals, Interval<T, TComparer>[] sourceIntervals, int sourceIndex, int maxSourceIndex, int mergerIndex) where TComparer : struct, IComparer<T>
        {
            var length = maxSourceIndex - sourceIndex + 1;
            Copying.Copy(sourceIntervals, sourceIndex, mergedIntervals, mergerIndex, length);

            return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, mergerIndex + length);
        }

        /*
        public static ReadOnlyArray<Interval<T, TComparer>> Merge<T, TComparer>(ReadOnlyArray<Interval<T, TComparer>> first, ReadOnlyArray<Interval<T, TComparer>> second)
            where TComparer : struct, IComparer<T>
        {
            var firstArray = first.AsArrayUnchecked();
            var secondArray = second.AsArrayUnchecked();
            var maxFirst = first.Count - 1;
            var maxSecond = second.Count - 1;
            var mergedIntervals = Interval<T, TComparer>.CreateUnchecked[maxFirst + maxSecond + 2];

            int f = 0;
            int s = 0;
            int m = 0;
            OperationState operationState;
            OperationDirection operationDirection = default;
            LowerBoundary<T, TComparer> currentLowerBoundary = default;

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
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer>(secondArray[s].LowerBoundary))
            {
                if (description.OperationStateMatchesTheBeginningOfInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }
                goto UpperFirstLowerSecond;
            }
            else
            {
                if (description.OperationStateMatchesTheBeginningOfInterval(OperationState.Middle, OperationStatus.Up))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }
                goto LowerFirstUpperSecond;
            }

        LowerFirstUpperSecond:
            if (firstArray[f].LowerBoundary.IsLessThan<T, TComparer>(secondArray[s].UpperBoundary, description))
            {
                operationState++;
                if (description.OperationStateMatchesTheBeginningOfInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = firstArray[f].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }
            else
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
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
            if (firstArray[f].UpperBoundary.IsLessThan<T, TComparer>(secondArray[s].LowerBoundary, description))
            {
                operationState--;
                if (description.OperationStateMatchesTheEndOfInterval(operationState, OperationStatus.Down, operationDirection))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
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
                if (description.OperationStateMatchesTheBeginningOfInterval(operationState, OperationStatus.Up, operationDirection))
                {
                    currentLowerBoundary = secondArray[s].LowerBoundary;
                }

                goto UpperFirstUpperSecond;
            }

        UpperFirstUpperSecond:
            operationState = OperationState.Middle;
            if (firstArray[f].UpperBoundary.IsLessThan(secondArray[s].UpperBoundary))
            {
                if (description.OperationStateMatchesTheEndOfInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, firstArray[f].UpperBoundary);
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
                if (description.OperationStateMatchesTheEndOfInterval(OperationState.Middle, OperationStatus.Down))
                {
                    mergedIntervals[m] = new Interval<T, TComparer>(currentLowerBoundary, secondArray[s].UpperBoundary);
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

        public static ReadOnlyArray<Interval<T, TComparer>> MergeTailStartingWithUpperBoundary<T, TComparer>(
            Interval<T, TComparer>[] mergedIntervals, Interval<T, TComparer>[] sourceIntervals, in LowerBoundary<T, TComparer> lowerBoundary, int sourceIndex, int maxSourceIndex, int mergerIndex)
            where TComparer : struct, IComparer<T>
        {
            if (description.OperationStateMatchesTheEndOfInterval(OperationState.Lowest))
            {
                mergedIntervals[mergerIndex] = new Interval<T, TComparer>(lowerBoundary, sourceIntervals[sourceIndex].UpperBoundary);
                mergerIndex++;
                sourceIndex++;
            }

            return MergeTail(mergedIntervals, sourceIntervals, sourceIndex, maxSourceIndex, mergerIndex, description);
        }

        public static ReadOnlyArray<Interval<T, TComparer>> MergeTail<T, TComparer>(
            Interval<T, TComparer>[] mergedIntervals, Interval<T, TComparer>[] sourceIntervals, int sourceIndex, int maxSourceIndex, int mergerIndex)
            where TComparer : struct, IComparer<T>
        {
            if (description.OperationStateMatchesTheBeginningOfInterval(OperationState.Middle, OperationStatus.Up) &&
                description.OperationStateMatchesTheEndOfInterval(OperationState.Lowest))
            {
                var length = maxSourceIndex - sourceIndex + 1;
                Copying.Copy(sourceIntervals, sourceIndex, mergedIntervals, mergerIndex, length);
                mergerIndex += length;
            }

            return new ReadOnlyArray<Interval<T, TComparer>>(mergedIntervals, mergerIndex);
        }
        */
    }
}
