using System;
using System.Collections.Generic;
using System.Linq;

namespace Accretion.Intervals
{
    public static class IntervalsTests
    {
        public const int NumberOfIntervals = 100000;        
        public const int MaxIntervalComplexity = 6;
        public const int InitialBoundary = 2;
        public const int ScalingPower = 2;

        private static readonly Random _random = new Random(1);
        
        public static readonly IReadOnlyList<Interval<int>> RandomIntervals = Enumerable.Range(1, MaxIntervalComplexity).SelectMany(x => MakeIntervals(NumberOfIntervals, -(int)Math.Pow(x * InitialBoundary, ScalingPower), (int)Math.Pow(x * InitialBoundary, ScalingPower), x)).ToList();

        public static IReadOnlyList<Interval<int>> MakeIntervals(int count, int minBound, int maxBound, int numberOfDesiredContinousIntervals)
        {
            var maxOffset = (maxBound - minBound) / numberOfDesiredContinousIntervals;
            var intervals = new List<Interval<int>>(count);
            var continiousIntervals = new ContinuousInterval<int>[numberOfDesiredContinousIntervals];

            int minBoundForContiniousIntervals = minBound;
            int minBoundaryValue;
            int maxBoundaryValue;
            bool minBoundaryIsOpen;
            bool maxBoundaryIsOpen;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < numberOfDesiredContinousIntervals; j++)
                {
                    minBoundaryValue = _random.Next(minBoundForContiniousIntervals, minBoundForContiniousIntervals + maxOffset / 2);
                    maxBoundaryValue = _random.Next(minBoundForContiniousIntervals + maxOffset / 2, minBoundForContiniousIntervals + maxOffset);
                    minBoundaryIsOpen = minBoundaryValue % 2 == 0;
                    maxBoundaryIsOpen = maxBoundaryValue % 2 == 0;

                    minBoundForContiniousIntervals += maxOffset;
                    continiousIntervals[j] = new ContinuousInterval<int>(minBoundaryValue, minBoundaryIsOpen, maxBoundaryValue, maxBoundaryIsOpen);
                }

                minBoundForContiniousIntervals = minBound;
                throw new NotImplementedException();
                //intervals.Add(new Interval<int>(continiousIntervals));
            }

            return intervals;
        }

        public static IReadOnlyList<Interval<double>> MakeDoubleIntervals(int count, int minBound, int maxBound, int numberOfDesiredContinousIntervals)
        {
            var maxOffset = (maxBound - minBound) / numberOfDesiredContinousIntervals;
            var intervals = new List<Interval<double>>(count);
            var continiousIntervals = new ContinuousInterval<double>[numberOfDesiredContinousIntervals];

            int minBoundForContiniousIntervals = minBound;
            int minBoundaryValue;
            int maxBoundaryValue;
            bool minBoundaryIsOpen;
            bool maxBoundaryIsOpen;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < numberOfDesiredContinousIntervals; j++)
                {
                    minBoundaryValue = _random.Next(minBoundForContiniousIntervals, minBoundForContiniousIntervals + maxOffset / 2);
                    maxBoundaryValue = _random.Next(minBoundForContiniousIntervals + maxOffset / 2, minBoundForContiniousIntervals + maxOffset);
                    minBoundaryIsOpen = minBoundaryValue % 2 == 0;
                    maxBoundaryIsOpen = maxBoundaryValue % 2 == 0;

                    minBoundForContiniousIntervals += maxOffset;
                    continiousIntervals[j] = new ContinuousInterval<double>(minBoundaryValue, minBoundaryIsOpen, maxBoundaryValue, maxBoundaryIsOpen);
                }

                minBoundForContiniousIntervals = minBound;
                intervals.Add(new Interval<double>(continiousIntervals));
            }

            return intervals;
        }

    }
}
