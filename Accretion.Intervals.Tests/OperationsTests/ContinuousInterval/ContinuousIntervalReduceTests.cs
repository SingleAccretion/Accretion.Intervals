using System;
using System.Collections.Generic;
using static Accretion.Intervals.Tests.StringConstants;
using System.Text;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalReduceTests 
    {
        public static IEnumerable<object[]> IntervalsOfSByte { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<sbyte>, ContinuousInterval<sbyte>)>()
        {
            (ContinuousInterval<sbyte>.EmptyInterval,  ContinuousInterval<sbyte>.EmptyInterval),
            (new ContinuousInterval<sbyte>(0, false, 2, false), new ContinuousInterval<sbyte>(0, false, 2, false)),
            (new ContinuousInterval<sbyte>(0, false, 2, true), new ContinuousInterval<sbyte>(0, false, 1, false)),
            (new ContinuousInterval<sbyte>(0, true, 2, false), new ContinuousInterval<sbyte>(1, false, 2, false)),
            (new ContinuousInterval<sbyte>(0, true, 2, true), new ContinuousInterval<sbyte>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfByte { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<byte>, ContinuousInterval<byte>)>()
        {
            (ContinuousInterval<byte>.EmptyInterval,  ContinuousInterval<byte>.EmptyInterval),
            (new ContinuousInterval<byte>(0, false, 2, false), new ContinuousInterval<byte>(0, false, 2, false)),
            (new ContinuousInterval<byte>(0, false, 2, true), new ContinuousInterval<byte>(0, false, 1, false)),
            (new ContinuousInterval<byte>(0, true, 2, false), new ContinuousInterval<byte>(1, false, 2, false)),
            (new ContinuousInterval<byte>(0, true, 2, true), new ContinuousInterval<byte>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfInt16 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<short>, ContinuousInterval<short>)>()
        {
            (ContinuousInterval<short>.EmptyInterval,  ContinuousInterval<short>.EmptyInterval),
            (new ContinuousInterval<short>(0, false, 2, false), new ContinuousInterval<short>(0, false, 2, false)),
            (new ContinuousInterval<short>(0, false, 2, true), new ContinuousInterval<short>(0, false, 1, false)),
            (new ContinuousInterval<short>(0, true, 2, false), new ContinuousInterval<short>(1, false, 2, false)),
            (new ContinuousInterval<short>(0, true, 2, true), new ContinuousInterval<short>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfUInt16 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<ushort>, ContinuousInterval<ushort>)>()
        {
            (ContinuousInterval<ushort>.EmptyInterval,  ContinuousInterval<ushort>.EmptyInterval),
            (new ContinuousInterval<ushort>(0, false, 2, false), new ContinuousInterval<ushort>(0, false, 2, false)),
            (new ContinuousInterval<ushort>(0, false, 2, true), new ContinuousInterval<ushort>(0, false, 1, false)),
            (new ContinuousInterval<ushort>(0, true, 2, false), new ContinuousInterval<ushort>(1, false, 2, false)),
            (new ContinuousInterval<ushort>(0, true, 2, true), new ContinuousInterval<ushort>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfChar { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<char>, ContinuousInterval<char>)>()
        {
            (ContinuousInterval<char>.EmptyInterval,  ContinuousInterval<char>.EmptyInterval),
            (new ContinuousInterval<char>((char)0, false, (char)2, false), new ContinuousInterval<char>((char)0, false, (char)2, false)),
            (new ContinuousInterval<char>((char)0, false, (char)2, true), new ContinuousInterval<char>((char)0, false, (char)1, false)),
            (new ContinuousInterval<char>((char)0, true, (char)2, false), new ContinuousInterval<char>((char)1, false, (char)2, false)),
            (new ContinuousInterval<char>((char)0, true, (char)2, true), new ContinuousInterval<char>((char)1, false, (char)1, false)),
        });
        
        public static IEnumerable<object[]> IntervalsOfInt32 { get; } = MakeContinuousIntervalsData.OfInt(new List<(string, string)>()
        {
            (Empty, Empty),
            ("[-2,2]", "[-2,2]"),
            ("[-2,2)", "[-2,1]"),
            ("(-2,2]", "[-1,2]"),
            ("(-2,2)", "[-1,1]"),
        });

        public static IEnumerable<object[]> IntervalsOfUInt32 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<uint>, ContinuousInterval<uint>)>()
        {
            (ContinuousInterval<uint>.EmptyInterval,  ContinuousInterval<uint>.EmptyInterval),
            (new ContinuousInterval<uint>(0, false, 2, false), new ContinuousInterval<uint>(0, false, 2, false)),
            (new ContinuousInterval<uint>(0, false, 2, true), new ContinuousInterval<uint>(0, false, 1, false)),
            (new ContinuousInterval<uint>(0, true, 2, false), new ContinuousInterval<uint>(1, false, 2, false)),
            (new ContinuousInterval<uint>(0, true, 2, true), new ContinuousInterval<uint>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfInt64 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<long>, ContinuousInterval<long>)>()
        {
            (ContinuousInterval<long>.EmptyInterval,  ContinuousInterval<long>.EmptyInterval),
            (new ContinuousInterval<long>(0, false, 2, false), new ContinuousInterval<long>(0, false, 2, false)),
            (new ContinuousInterval<long>(0, false, 2, true), new ContinuousInterval<long>(0, false, 1, false)),
            (new ContinuousInterval<long>(0, true, 2, false), new ContinuousInterval<long>(1, false, 2, false)),
            (new ContinuousInterval<long>(0, true, 2, true), new ContinuousInterval<long>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfUInt64 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<ulong>, ContinuousInterval<ulong>)>()
        {
            (ContinuousInterval<ulong>.EmptyInterval,  ContinuousInterval<ulong>.EmptyInterval),
            (new ContinuousInterval<ulong>(0, false, 2, false), new ContinuousInterval<ulong>(0, false, 2, false)),
            (new ContinuousInterval<ulong>(0, false, 2, true), new ContinuousInterval<ulong>(0, false, 1, false)),
            (new ContinuousInterval<ulong>(0, true, 2, false), new ContinuousInterval<ulong>(1, false, 2, false)),
            (new ContinuousInterval<ulong>(0, true, 2, true), new ContinuousInterval<ulong>(1, false, 1, false)),
        });

        public static IEnumerable<object[]> IntervalsOfDay { get; } = MakeContinuousIntervalsData.OfDay(new List<(string, string)>()
        {
            (Empty, Empty),
            ($"[{Monday},{Friday}]", $"[{Monday},{Friday}]"),
            ($"[{Monday},{Friday})", $"[{Monday},{Thursday}]"),
            ($"({Monday},{Friday}]", $"[{Tuesday},{Friday}]"),
            ($"({Monday},{Friday})", $"[{Tuesday},{Thursday}]"),
        });
        
        public static IEnumerable<object[]> IntervalsOfCoordinate { get; } = MakeContinuousIntervalsData.OfCoordinate(new List<(string, string)>()
        {
            (Empty, Empty),
            ("[-2,2]", "[-2,2]"),
            ("[-2,2)", "[-2,1]"),
            ("(-2,2]", "[-1,2]"),
            ("(-2,2)", "[-1,1]"),
        });

        [Theory]
        [MemberData(nameof(IntervalsOfSByte))]
        public void TestSByteReduce(ContinuousInterval<sbyte> interval, ContinuousInterval<sbyte> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfByte))]
        public void TestByteReduce(ContinuousInterval<byte> interval, ContinuousInterval<byte> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfInt16))]
        public void TestInt16Reduce(ContinuousInterval<short> interval, ContinuousInterval<short> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt16))]
        public void TestUInt16Reduce(ContinuousInterval<ushort> interval, ContinuousInterval<ushort> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfChar))]
        public void TestCharReduce(ContinuousInterval<char> interval, ContinuousInterval<char> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfInt32))]
        public void TestInt32Reduce(ContinuousInterval<int> interval, ContinuousInterval<int> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt32))]
        public void TestUInt32Reduce(ContinuousInterval<uint> interval, ContinuousInterval<uint> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfInt64))]
        public void TestInt64Reduce(ContinuousInterval<long> interval, ContinuousInterval<long> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt64))]
        public void TestUInt64Reduce(ContinuousInterval<ulong> interval, ContinuousInterval<ulong> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());
        
        [Theory]
        [MemberData(nameof(IntervalsOfDay))]
        public void TestCustomStructDiscreteReduce(ContinuousInterval<Day> interval, ContinuousInterval<Day> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinate))]
        public void TestCustomClassDiscreteReduce(ContinuousInterval<Coordinate> interval, ContinuousInterval<Coordinate> expectedInterval) => Assert.Equal(expectedInterval, interval.Reduce());
    }
}
