using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class Sorting
    {
        public static T[] MergeSortedArrays<T>(T[] firstArray, int firstTakeLength, T[] secondArray, int secondTakeLength) where T : IComparable<T>
        {
            var results = new T[firstTakeLength + secondTakeLength];
            MergeSortedArrays(results, firstArray, 0, firstTakeLength, secondArray, 0, secondTakeLength);
            return results;
        }

        public static T[] MergeSortedArrays<T>(T[] firstArray, int firstStartIndex, int firstTakeLength, T[] secondArray, int secondStartIndex, int secondTakeLength) where T : IComparable<T>
        {
            var results = new T[firstTakeLength + secondTakeLength];
            MergeSortedArrays(results, firstArray, firstStartIndex, firstTakeLength, secondArray, secondStartIndex, secondTakeLength);
            return results;
        }

        public static T[] MergeSortedArrays<T>(T[] firstArray, T[] secondArray) where T : IComparable<T>
        {
            var results = new T[firstArray.Length + secondArray.Length];
            MergeSortedArrays(results, firstArray, 0, firstArray.Length, secondArray, 0, secondArray.Length);
            return results;
        }

        private static void MergeSortedArrays<T>(T[] results, T[] firstArray, int firstStartIndex, int firstTakeLength, T[] secondArray, int secondStartIndex, int secondTakeLength) where T : IComparable<T>
        {
            int firstIndex = firstStartIndex + firstTakeLength;
            int secondIndex = secondStartIndex + secondTakeLength;
            int write = firstTakeLength + secondTakeLength;
            int length, gallopPos;

            if (results is null)
            {
                throw new ArgumentNullException(nameof(results));
            }
            if (results.Length < write)
            {
                throw new ArgumentException(nameof(results));
            }
            if (firstArray is null)
            {
                throw new ArgumentNullException(nameof(firstArray));
            }
            if (firstStartIndex < 0 || firstStartIndex > (firstArray.Length == 0 ? 0 : firstArray.Length - 1))
            {
                throw new ArgumentException(nameof(firstStartIndex));
            }
            if (firstTakeLength < 0 || firstTakeLength > firstArray.Length)
            {
                throw new ArgumentException(nameof(firstTakeLength));
            }
            if (secondArray is null)
            {
                throw new ArgumentNullException(nameof(secondArray));
            }
            if (secondStartIndex < 0 || secondStartIndex > (secondArray.Length == 0 ? 0 : secondArray.Length - 1))
            {
                throw new ArgumentException(nameof(secondStartIndex));
            }
            if (secondTakeLength < 0 || secondArray.Length > secondArray.Length)
            {
                throw new ArgumentException(nameof(secondTakeLength));
            }

            if (firstTakeLength > 0 && secondTakeLength > 0)
            {
                int c = firstArray[firstIndex - 1].CompareTo(secondArray[secondIndex - 1]);

                while (firstIndex > firstStartIndex && secondIndex > secondStartIndex)
                {
                    switch (c)
                    {
                        default:
                            gallopPos = GallopSearch(firstIndex, firstArray, secondArray[secondIndex - 1]);
                            length = firstIndex - gallopPos;
                            write -= length;
                            firstIndex = gallopPos;
                            Array.Copy(firstArray, gallopPos--, results, write, length);
                            c = -1;
                            break;
                        case -1:
                            gallopPos = GallopSearch(secondIndex, secondArray, firstArray[firstIndex - 1]);
                            length = secondIndex - gallopPos;
                            write -= length;
                            secondIndex = gallopPos;
                            Array.Copy(secondArray, gallopPos--, results, write, length);
                            c = 1;
                            break;
                    }
                }
            }

            if (secondIndex > 0)
            {
                if (secondArray != results)
                {
                    Array.Copy(secondArray, 0, results, 0, secondIndex);
                }
            }
            else if (firstIndex > 0)
            {
                if (firstArray != results)
                {
                    Array.Copy(firstArray, 0, results, 0, firstIndex);
                }
            }
        }

        private static int GallopSearch<T>(int current, T[] array, T value) where T : IComparable<T>
        {
            int d = 1;
            int seek = current - d;
            int prevIteration = seek;

            while (seek > 0)
            {                
                if (array[seek].CompareTo(value) <= 0)
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
                seek = BinarySearch(array, seek, prevIteration, value);
                seek = seek >= 0 ? seek : ~seek;
            }
            return seek;
        }

        private static int BinarySearch<T>(T[] array, int fromIndex, int toIndex, T value) where T : IComparable<T>
        {
            int low = fromIndex;
            int high = toIndex - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                var midValue = array[mid];
                int midValueComparedToValue = midValue.CompareTo(value);

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

    }
}
