using System;

namespace Accretion.Intervals
{
    public static class Lengths
    {
        /// <summary>
        /// Computes the length of this interval: sum of differences between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static double Length(this Interval<double> interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var totalLength = 0d;
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                var lowerBoundary = boundariesArray[i];
                var upperBoundary = boundariesArray[i + 1];
                var difference = upperBoundary.Value - lowerBoundary.Value;

                totalLength += difference;
            }

            return totalLength;
        }

        /// <summary>
        /// Computes the length of this interval: sum of differences between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static float Length(this Interval<float> interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var totalLength = 0f;
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                var lowerBoundary = boundariesArray[i];
                var upperBoundary = boundariesArray[i + 1];
                var difference = upperBoundary.Value - lowerBoundary.Value;

                totalLength += difference;
            }

            return totalLength;
        }

        /// <summary>
        /// Computes the length of this interval: sum of differences between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static decimal Length(this Interval<decimal> interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var totalLength = 0m;
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                var lowerBoundary = boundariesArray[i];
                var upperBoundary = boundariesArray[i + 1];
                var difference = upperBoundary.Value - lowerBoundary.Value;

                totalLength += difference;
            }

            return totalLength;
        }

        /// <summary>
        /// Computes the length of this interval: sum of differences between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static TimeSpan Length(this Interval<DateTime> interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var totalLength = TimeSpan.Zero;
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                var lowerBoundary = boundariesArray[i];
                var upperBoundary = boundariesArray[i + 1];
                var difference = upperBoundary.Value - lowerBoundary.Value;

                totalLength += difference;
            }

            return totalLength;
        }

        /// <summary>
        /// Computes the length of this interval: sum of differences between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public static TimeSpan Length(this Interval<DateTimeOffset> interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            var totalLength = TimeSpan.Zero;
            var maxIndex = interval.Boundaries.MaxIndex();
            var boundariesArray = interval.Boundaries.Array;

            for (int i = interval.Boundaries.Offset; i <= maxIndex; i += 2)
            {
                var lowerBoundary = boundariesArray[i];
                var upperBoundary = boundariesArray[i + 1];
                var difference = upperBoundary.Value - lowerBoundary.Value;

                totalLength += difference;
            }

            return totalLength;
        }

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static double Length(this ContinuousInterval<double> interval)
        {
            return interval.UpperBoundary.Value - interval.LowerBoundary.Value;
        }

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static float Length(this ContinuousInterval<float> interval)
        {
            return interval.UpperBoundary.Value - interval.LowerBoundary.Value;
        }
        
        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static decimal Length(this ContinuousInterval<decimal> interval)
        {
            return interval.UpperBoundary.Value - interval.LowerBoundary.Value;
        }

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static TimeSpan Length(this ContinuousInterval<DateTime> interval)
        {            
            return interval.UpperBoundary.Value - interval.LowerBoundary.Value;
        }

        /// <summary>
        /// Computes the length of this interval: difference between values of its boundaries, irrespective of them being open or closed.
        /// </summary>
        public static TimeSpan Length(this ContinuousInterval<DateTimeOffset> interval)
        {
            return interval.UpperBoundary.Value - interval.LowerBoundary.Value;
        }        
    }
}
