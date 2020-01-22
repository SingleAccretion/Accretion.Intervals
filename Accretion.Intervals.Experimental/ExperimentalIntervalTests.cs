using Accretion.Intervals.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accretion.Intervals.Experimental
{
    public static class ExperimentalIntervalsTests
    {
        private const int NumberOfIntervals = IntervalsTests.NumberOfIntervals;
        private const int MaxIntervalComplexity = IntervalsTests.MaxIntervalComplexity;
        private const int InitialBoundary = IntervalsTests.InitialBoundary;
        private const int ScalingPower = IntervalsTests.ScalingPower;

        private static readonly Random _random = new Random(1);
        
        public static readonly IReadOnlyList<Interval<int>> RandomExperimentalIntervals = Enumerable.Range(1, MaxIntervalComplexity).SelectMany(x => MakeIntervals(NumberOfIntervals, -(int)Math.Pow(x * InitialBoundary, ScalingPower), (int)Math.Pow(x * InitialBoundary, ScalingPower), x)).ToList();
        
        public static void RunAll(bool runInParallel = false)
        {
            if (runInParallel)
            {
                Parallel.For(0, RandomExperimentalIntervals.Count - 1, i => 
                {
                    var interval = RandomExperimentalIntervals[i];
                    var nextInterval = RandomExperimentalIntervals[i + 1];

                    if (!TestContains(interval))
                    {
                        throw new Exception($"{interval}, {nextInterval} - contains");
                    }
                    if (!TestUnion(nextInterval, interval))
                    {
                        throw new Exception($"{interval}, {nextInterval} - union");
                    }
                    if (!TestIntersect(nextInterval, interval))
                    {
                        throw new Exception($"{interval}, {nextInterval} - intersect");
                    }

                    var (differenceResult, sampleDifference) = TestSymmetricDifference(nextInterval, interval);
                    if (!differenceResult)
                    {
                        throw new Exception($"{interval}, {nextInterval} = {sampleDifference} - symmetric difference");
                    }

                    if (i % 100 == 0)
                    {
                        Console.WriteLine(100 * i / (NumberOfIntervals * MaxIntervalComplexity) + " %");
                    }
                });
            }
            
            for (int i = 0; i < RandomExperimentalIntervals.Count - 1; i++)
            {
                var interval = RandomExperimentalIntervals[i];
                var nextInterval = RandomExperimentalIntervals[i + 1];

                if (!TestContains(interval))
                {
                    throw new Exception($"{interval}, {nextInterval} - contains");
                }
                if (!TestUnion(nextInterval, interval))
                {
                    throw new Exception($"{interval}, {nextInterval} - union");
                }
                if (!TestIntersect(nextInterval, interval))
                {
                    throw new Exception($"{interval}, {nextInterval} - intersect");
                }
                        
                var (differenceResult, sampleDifference) = TestSymmetricDifference(nextInterval, interval);
                if (!differenceResult)
                {
                    throw new Exception($"{interval}, {nextInterval} = {sampleDifference} - symmetric difference");
                }

                if (i % 100 == 0)
                {
                    Console.WriteLine(100 * i / (NumberOfIntervals * MaxIntervalComplexity) + " %");
                }
            }

            Console.WriteLine("Tests passed!");
        }

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
                intervals.Add(new Interval<int>(continiousIntervals));
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

        public static bool TestContains(Interval<int> interval)
        {
            var boundaries = (IList<Boundary<int>>)interval.Boundaries;
            var intermediateResult = true;

            for (int i = 0; i < boundaries.Count; i += 2)
            {
                var lowerBoundaryValue = boundaries[i].Value;
                var upperBoundaryValue = boundaries[i + 1].Value;
                var nextLowerBoundaryValue = i == boundaries.Count - 2 ? int.MaxValue : boundaries[i + 2].Value;

                var inclusiveAverage = (lowerBoundaryValue + upperBoundaryValue) / 2;
                var exclusiveValue = (upperBoundaryValue + nextLowerBoundaryValue) / 2;

                if (i == 0 && interval.Contains(int.MinValue))
                {
                    intermediateResult = false;
                }

                if (exclusiveValue != upperBoundaryValue && exclusiveValue != nextLowerBoundaryValue)
                {
                    if (interval.Contains(exclusiveValue))
                    {
                        intermediateResult = false;
                    }
                }

                if (inclusiveAverage != lowerBoundaryValue && inclusiveAverage != upperBoundaryValue)
                {
                    if (!interval.Contains(inclusiveAverage))
                    {
                        intermediateResult = false;
                    }
                }
            }

            return interval.Boundaries.Where(x => x.IsClosed).All(x => interval.Contains(x.Value)) &&
                   interval.Boundaries.Where(x => x.IsOpen).All(x => !interval.Contains(x.Value)) &&
                   intermediateResult;
        }

        public static bool TestUnion(Interval<int> first, Interval<int> second)
        {
            var result = first.Union(second);

            if (first.IsEmpty || second.IsEmpty)
            {
                return true;
            }

            var lowerValue = first.Boundaries.First().InComparisonWith(second.Boundaries.First(), new FullOverlap()) < 0 ? first.Boundaries.First().Value : second.Boundaries.First().Value;
            var upperValue = first.Boundaries.Last().InComparisonWith(second.Boundaries.Last(), new FullOverlap()) > 0 ? first.Boundaries.Last().Value : second.Boundaries.Last().Value;

            for (int i = lowerValue; i <= upperValue; i++)
            {
                var check = (first.Contains(i) || second.Contains(i)) == result.Contains(i);

                if (!check)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TestIntersect(Interval<int> first, Interval<int> second)
        {
            var result = first.Intersect(second);

            if (first.IsEmpty || second.IsEmpty)
            {
                return true;
            }

            var lowerValue = first.Boundaries.First().InComparisonWith(second.Boundaries.First(), new FullOverlap()) < 0 ? first.Boundaries.First().Value : second.Boundaries.First().Value;
            var upperValue = first.Boundaries.Last().InComparisonWith(second.Boundaries.Last(), new FullOverlap()) > 0 ? first.Boundaries.Last().Value : second.Boundaries.Last().Value;

            for (int i = lowerValue; i <= upperValue; i++)
            {
                var firstContains = first.Contains(i);
                var secondContains = second.Contains(i);
                var resultContains = result.Contains(i);

                var check = (firstContains && secondContains) == resultContains;

                if (!check)
                {
                    return false;
                }
            }

            return true;
        }

        public static (bool Result, Interval<int> SampleDifference) TestSymmetricDifference(Interval<int> first, Interval<int> second)
        {
            var testResult = true;
            var difference = first.SymmetricDifference(second);

            for (int i = difference.Boundaries.FirstOrDefault().Value; i <= difference.Boundaries.LastOrDefault().Value; i++)
            {
                var bothContain = first.Contains(i) && second.Contains(i);
                var oneContains = first.Contains(i) || second.Contains(i);
                var differenceContains = difference.Contains(i);

                testResult &= differenceContains == (!bothContain && oneContains);
            }

            return (testResult, difference);
        }
    }
}
