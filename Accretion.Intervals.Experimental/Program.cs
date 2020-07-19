using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Accretion.Intervals.Tests;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using FsCheck;

namespace Accretion.Intervals
{
    public static class Program
    {
        public static readonly int P = Environment.ProcessorCount;

        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        private static unsafe void Main()
        {
            void WriteMethod(StreamWriter writer, PrimitiveType type, InvalidValue value, string methodName)
            {
                writer.WriteLine($".method public hidebysig static bool {methodName} (");
                writer.WriteLine($"        [opt] {type.CLIName} parameter");
                writer.WriteLine("    ) cil managed");
                writer.WriteLine("{");
                writer.WriteLine($"    .param [1] = {value.Type.CLIName}({value.Value})");
                writer.WriteLine();
                writer.WriteLine("    ldc.i4.0");
                writer.WriteLine("    ret");
                writer.WriteLine("}");
                writer.WriteLine();
            }

            Arb.Register(typeof(Arbitrary));

            var writer = new StreamWriter("intervals.txt");

            foreach (var type in new[] { new PrimitiveType(0, 1, "bool", "Boolean") })
            {
                foreach (var value in InvalidIntegerValues(type).Concat(FloatingPointValues()))
                {
                    var methodName = $"{type.FrameworkName}EncodedWith{value.Reason}{value.Type.FrameworkName}";
                    writer.WriteLine($"Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof({methodName})))));");

                    //WriteMethod(writer, type, value, methodName);
                }
            }

            writer.Dispose();
        }

        private static IEnumerable<PrimitiveType> GetOtherPrimitiveTypes()
        {
            yield return new PrimitiveType(byte.MinValue, byte.MaxValue, "bool", "Boolean");
            yield return new PrimitiveType(char.MinValue, char.MaxValue, "char", "Char");
        }

        private static IEnumerable<PrimitiveType> GetIntegerTypes()
        {
            yield return new PrimitiveType(sbyte.MinValue, sbyte.MaxValue, "int8", "SByte");
            yield return new PrimitiveType(byte.MinValue, byte.MaxValue, "uint8", "Byte");
            yield return new PrimitiveType(short.MinValue, short.MaxValue, "int16", "Int16");
            yield return new PrimitiveType(ushort.MinValue, ushort.MaxValue, "uint16", "UInt16");
            yield return new PrimitiveType(int.MinValue, int.MaxValue, "int32", "Int32");
            yield return new PrimitiveType(uint.MinValue, uint.MaxValue, "uint32", "UInt32");
            yield return new PrimitiveType(long.MinValue, long.MaxValue, "int64", "Int64");
            yield return new PrimitiveType(ulong.MinValue, ulong.MaxValue, "uint64", "UInt64");
        }

        private static IEnumerable<PrimitiveType> GetFloatingPointTypes()
        {
            yield return new PrimitiveType(null, null, "float32", "Single");
            yield return new PrimitiveType(null, null, "float64", "Double");
        }

        private static IEnumerable<InvalidValue> InvalidIntegerValues(PrimitiveType targetType)
        {
            yield return new InvalidValue("true", new PrimitiveType(0, 0, "bool", "Boolean"), "");
            yield return new InvalidValue("97", new PrimitiveType(char.MinValue, char.MaxValue, "char", "Char"), "");
            
            foreach (var primitive in GetIntegerTypes())
            {
                if (primitive.MinValue < targetType.MinValue)
                {
                    yield return new InvalidValue(primitive.MinValue.ToString(), primitive, "TooSmall");
                }
                if (primitive.MaxValue > targetType.MaxValue)
                {
                    yield return new InvalidValue(primitive.MaxValue.ToString(), primitive, "TooBig");
                }
            }
        }

        private static IEnumerable<InvalidValue> FloatingPointValues()
        {
            yield return new InvalidValue("0.1", new PrimitiveType(null, null, "float32", "Single"), "");
            yield return new InvalidValue("0.1", new PrimitiveType(null, null, "float64", "Double"), "");
        }

        private readonly struct PrimitiveType
        {
            public PrimitiveType(BigInteger? minValue, BigInteger? maxValue, string cliName, string frameworkName)
            {
                MinValue = minValue;
                MaxValue = maxValue;
                CLIName = cliName;
                FrameworkName = frameworkName;
            }

            public BigInteger? MinValue { get; }
            public BigInteger? MaxValue { get; }
            public string CLIName { get; }
            public string FrameworkName { get; }
        }

        private readonly struct InvalidValue
        {
            public InvalidValue(string value, PrimitiveType type, string reason)
            {
                Value = value;
                Type = type;
                Reason = reason;
            }

            public string Value { get; }
            public PrimitiveType Type { get; }
            public string Reason { get; }
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


