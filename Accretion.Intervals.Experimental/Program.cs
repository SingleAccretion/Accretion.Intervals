﻿using System;
using System.Globalization;
using System.Text;
using Accretion.Intervals.Tests;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace Accretion.Intervals
{
    public static class Program
    {
        public static readonly int P = Environment.ProcessorCount;

        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        private static void Main()
        {
            //((5.41667E-07, (5.2853085E-07, F, )
            var b1 = new LowerBoundary<float>(5.41667E-07f, BoundaryType.Open);
            var b2 = new LowerBoundary<float>(5.2853085E-07f, BoundaryType.Open);


            Console.WriteLine(b1.Equals(b2));
            Console.WriteLine(default(SingleComparerByExponent).Compare(b1.Value, b2.Value));
            Console.WriteLine(default(SingleComparerByExponent).ToString(b1.Value, "G", null));
            Console.WriteLine(default(SingleComparerByExponent).ToString(b2.Value, "G", null));
            Console.WriteLine(b1.ToString("F50", null));
            Console.WriteLine(b2.ToString("F50", null));
            //BenchmarkRunner.Run<Profiled>();
            //Console.ReadLine();
        }
    }

    //[HardwareCounters(HardwareCounter.BranchInstructions, HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses, HardwareCounter.InstructionRetired)]
    [InliningDiagnoser(logFailuresOnly: true, allowedNamespaces: new[] { "Accretion.Intervals" })]
    public class Profiled
    {
        public int N = 1000;
        [Params(10)]
        //[Params(1, 10, 100, 1000, 10_000)]
        public int IntervalsComplexity;

        //public Experimental.Interval<int>[] ExperimentalIntIntervals;
        public Interval<int>[] IntIntervals;

        //public Experimental.Interval<double>[] ExperimentalDoubleIntervals;
        public Interval<double>[] DoubleIntervals;

        //internal ExperimentalBoundary<int>[] Boundaries1;
        //internal ExperimentalBoundary<int>[] Boundaries2;

        [GlobalSetup]
        public void Setup()
        {
            /*
            ExperimentalIntIntervals = ExperimentalIntervalsTests.MakeIntervals(N, -200 * IntervalsComplexity, 200 * IntervalsComplexity, IntervalsComplexity).ToArray();
            IntIntervals = IntervalsTests.MakeIntervals(N, -200 * IntervalsComplexity, 200 * IntervalsComplexity, IntervalsComplexity).ToArray();

            ExperimentalDoubleIntervals = ExperimentalIntervalsTests.MakeDoubleIntervals(N, -200 * IntervalsComplexity, 200 * IntervalsComplexity, IntervalsComplexity).ToArray();
            DoubleIntervals = IntervalsTests.MakeDoubleIntervals(N, -200 * IntervalsComplexity, 200 * IntervalsComplexity, IntervalsComplexity).ToArray();
            */
            //Boundaries1 = Boundaries.MakeExperimentalBoundaries(IntervalsComplexity);
            //Boundaries2 = Boundaries.MakeExperimentalBoundaries(IntervalsComplexity);
        }

        [Benchmark]
        public void TestNormal()
        {
            TestNormalIntIntersect();
            //TestNormalIntSymmetricDifference();
            //TestNormalIntUnion();

            //TestNormalIntReduce();
        }

        //[Benchmark(Baseline = true)]
        public void TestExperimental()
        {
            //TestExperimentalIntIntersect();
            
            //TestExperimentalIntSymmetricDifference();
            //TestExperimentalIntUnion();

            //TestExperimentalIntReduce();
        }

        private void TestNormalIntIntersect()
        {
            var intervals = IntIntervals;
            
            for (int i = 0; i < intervals.Length - 1; i++)
            {
                throw new NotImplementedException();
                //intervals[i].Intersect(intervals[i + 1]);
            }
        }

        private void TestNormalIntSymmetricDifference()
        {
            var intervals = IntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                throw new NotImplementedException();
                //intervals[i].SymmetricDifference(intervals[i + 1]);
            }
        }

        private void TestNormalIntUnion()
        {
            var intervals = IntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                throw new NotImplementedException();
                //intervals[i].Union(intervals[i + 1]);
            }
        }

        private void TestNormalIntReduce()
        {
            var intervals = IntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                throw new NotImplementedException();
                //intervals[i].Reduce();
            }
        }
        /*
        private void TestExperimentalIntIntersect()
        {
            var intervals = ExperimentalIntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                intervals[i].Intersect(intervals[i + 1]);
            }
        }

        private void TestExperimentalIntSymmetricDifference()
        {
            var intervals = ExperimentalIntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                intervals[i].SymmetricDifference(intervals[i + 1]);
            }
        }

        private void TestExperimentalIntUnion()
        {
            var intervals = ExperimentalIntIntervals;

            for (int i = 0; i < intervals.Length - 1; i++)
            {
                intervals[i].Union(intervals[i + 1]);
            }
        }
        */
    }
}


