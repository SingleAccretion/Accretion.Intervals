using System;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class MergingAlgorithms
    {        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boundary<T>[] MergeSortedArrays<T, S>(Boundary<T>[] firstArray, Boundary<T>[] secondArray, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            return MergeSortedArrays(firstArray, 0, firstArray.Length, secondArray, 0, secondArray.Length, overlappingStrategy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boundary<T>[] MergeSortedArrays<T, S>(Boundary<T>[] firstArray, int firstTakeLength, Boundary<T>[] secondArray, int secondTakeLength, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            return MergeSortedArrays(firstArray, 0, firstTakeLength, secondArray, 0, secondTakeLength, overlappingStrategy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boundary<T>[] MergeSortedArrays<T, S>(Boundary<T>[] firstArray, int firstStartIndex, int firstTakeLength, Boundary<T>[] secondArray, int secondStartIndex, int secondTakeLength, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            var results = new Boundary<T>[firstTakeLength + secondTakeLength];

            switch (PerformanceConfiguration.MergingAlgorithm)
            {
                case MergingAlgorithm.Linear:
                    LinearMergeSortedArrays(results, firstArray, firstStartIndex, firstTakeLength, secondArray, secondStartIndex, secondTakeLength, overlappingStrategy);
                    break;

                case MergingAlgorithm.Gallop:
                    GallopMergeSortedArrays(results, firstArray, firstStartIndex, firstTakeLength, secondArray, secondStartIndex, secondTakeLength, overlappingStrategy);
                    break;

                default:
                    LinearMergeSortedArrays(results, firstArray, firstStartIndex, firstTakeLength, secondArray, secondStartIndex, secondTakeLength, overlappingStrategy);
                    break;
            }
            

            return results;
        }

        private static void GallopMergeSortedArrays<T, S>(Boundary<T>[] results, Boundary<T>[] firstArray, int firstStartIndex, int firstTakeLength, Boundary<T>[] secondArray, int secondStartIndex, int secondTakeLength, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            int firstIndex = firstStartIndex + firstTakeLength;
            int secondIndex = secondStartIndex + secondTakeLength;
            int write = firstTakeLength + secondTakeLength;
            int length, gallopPos;

            int c = firstArray[firstIndex - 1].InComparisonWith(secondArray[secondIndex - 1], overlappingStrategy);

            while (firstIndex > firstStartIndex && secondIndex > secondStartIndex)
            {
                switch (c)
                {
                    default:
                        gallopPos = GallopSearch(firstIndex, firstArray, secondArray[secondIndex - 1], overlappingStrategy);
                        length = firstIndex - gallopPos;
                        write -= length;
                        firstIndex = gallopPos;
                        ArrayCopy(firstArray, gallopPos--, results, write, length);
                        c = -1;
                        break;
                    case -1:
                        gallopPos = GallopSearch(secondIndex, secondArray, firstArray[firstIndex - 1], overlappingStrategy);
                        length = secondIndex - gallopPos;
                        write -= length;
                        secondIndex = gallopPos;
                        ArrayCopy(secondArray, gallopPos--, results, write, length);
                        c = 1;
                        break;
                }
            }

            if (secondIndex > 0)
            {
                if (secondArray != results)
                {
                    ArrayCopy(secondArray, 0, results, 0, secondIndex);
                }
            }
            else if (firstIndex > 0)
            {
                if (firstArray != results)
                {
                    ArrayCopy(firstArray, 0, results, 0, firstIndex);
                }
            }
        }

        private static int GallopSearch<T, S>(int current, Boundary<T>[] array, Boundary<T> value, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            int d = 1;
            int seek = current - d;
            int prevIteration = seek;

            while (seek > 0)
            {
                if (array[seek].InComparisonWith(value, overlappingStrategy) <= 0)
                {
                    break;
                }

                prevIteration = seek;
                d <<= 1;
                seek = current - d;
                if (seek < 0)
                {
                    seek = 0;
                }
            }

            if (prevIteration != seek)
            {
                seek = BinarySearch(array, seek, prevIteration, value, overlappingStrategy);
                seek = seek >= 0 ? seek : ~seek;
            }

            return seek;
        }

        private static int BinarySearch<T, S>(Boundary<T>[] array, int fromIndex, int toIndex, Boundary<T> value, S overlappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            int low = fromIndex;
            int high = toIndex - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                var midValue = array[mid];
                int midValueComparedToValue = midValue.InComparisonWith(value, overlappingStrategy);

                if (midValueComparedToValue < 0)
                {
                    low = mid + 1;
                }
                else if (midValueComparedToValue > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return -(low + 1);
        }

        private static void LinearMergeSortedArrays<T, S>(Boundary<T>[] result, Boundary<T>[] firstArray, int firstStartIndex, int firstTakeLength, Boundary<T>[] secondArray, int secondStartIndex, int secondTakeLength, S ovelappingStrategy) where T : IComparable<T> where S : IOverlappingStrategy
        {
            var firstIndex = firstStartIndex;
            var maxFirstIndex = firstStartIndex + firstTakeLength - 1;
            var secondIndex = secondStartIndex;
            var maxSecondIndex = secondStartIndex + secondTakeLength - 1;
            var k = 0;

            while (firstIndex <= maxFirstIndex && secondIndex <= maxSecondIndex)
            {
                if (firstArray[firstIndex].InComparisonWith(secondArray[secondIndex], ovelappingStrategy) < 0)
                {
                    result[k++] = firstArray[firstIndex];
                    firstIndex++;
                }
                else
                {
                    result[k++] = secondArray[secondIndex];
                    secondIndex++;
                }
            }
            
            if (secondIndex <= maxSecondIndex)
            {
                ArrayCopy(secondArray, secondIndex, result, k, maxSecondIndex - secondIndex + 1);
            }
            else if (firstIndex <= maxFirstIndex)
            {
                ArrayCopy(firstArray, firstIndex, result, k, maxFirstIndex - firstIndex + 1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ArrayCopy<T>(Boundary<T>[] source, int sourceStartIndex, Boundary<T>[] destination, int destionationStartIndex, int length) where T : IComparable<T>
        {
            source.AsSpan(sourceStartIndex, length).CopyTo(destination.AsSpan(destionationStartIndex, length));
        }
    }
}
