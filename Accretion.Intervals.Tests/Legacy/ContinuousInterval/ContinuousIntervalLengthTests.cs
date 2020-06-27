using System;
using System.Collections.Generic;
using static Accretion.Intervals.Tests.StringConstants;
using System.Text;
using Xunit;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalLengthTests
    {
        public static IEnumerable<object[]> IntervalsOfSByte { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<sbyte>, int)>()
        {
            (ContinuousInterval<sbyte>.EmptyInterval, 0),

            (new ContinuousInterval<sbyte>(-1, false, 1, false), 2),
            (new ContinuousInterval<sbyte>(-1, true, 1, false), 2),
            (new ContinuousInterval<sbyte>(-1, false, 1, true), 2),
            (new ContinuousInterval<sbyte>(-1, true, 1, true), 2),

            (new ContinuousInterval<sbyte>(sbyte.MinValue, false, sbyte.MaxValue, false), byte.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfByte { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<byte>, int)>()
        {
            (ContinuousInterval<byte>.EmptyInterval, 0),

			(new ContinuousInterval<byte>(0, false, 2, false), 2),
            (new ContinuousInterval<byte>(0, true, 2, false), 2),
            (new ContinuousInterval<byte>(0, false, 2, true), 2),
            (new ContinuousInterval<byte>(0, true, 2, true), 2),

            (new ContinuousInterval<byte>(byte.MinValue, false, byte.MaxValue, false), byte.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt16 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<short>, int)>()
        {
            (ContinuousInterval<short>.EmptyInterval, 0),

            (new ContinuousInterval<short>(-1, false, 1, false), 2),
            (new ContinuousInterval<short>(-1, true, 1, false), 2),
            (new ContinuousInterval<short>(-1, false, 1, true), 2),
            (new ContinuousInterval<short>(-1, true, 1, true), 2),

            (new ContinuousInterval<short>(short.MinValue, false, short.MaxValue, false), ushort.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt16 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<ushort>, int)>()
        {
            (ContinuousInterval<ushort>.EmptyInterval, 0),

			(new ContinuousInterval<ushort>(0, false, 2, false), 2),
            (new ContinuousInterval<ushort>(0, true, 2, false), 2),
            (new ContinuousInterval<ushort>(0, false, 2, true), 2),
            (new ContinuousInterval<ushort>(0, true, 2, true), 2),

            (new ContinuousInterval<ushort>(ushort.MinValue, false, ushort.MaxValue, false), ushort.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt32 { get; } = Make1ContinuousIntervalsData.OfInt(new List<(string, long)>()
        {
            (Empty, 0),
            ("[-1,1]", 2),
            ("[-1,1)", 2),
            ("(-1,1]", 2),
            ("(-1,1)", 2),

            ($"[{int.MinValue},{int.MaxValue}]", uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt32 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<uint>, long)>()
        {
            (ContinuousInterval<uint>.EmptyInterval, 0),

			(new ContinuousInterval<uint>(0, false, 2, false), 2),
            (new ContinuousInterval<uint>(0, true, 2, false), 2),
            (new ContinuousInterval<uint>(0, false, 2, true), 2),
            (new ContinuousInterval<uint>(0, true, 2, true), 2),

            (new ContinuousInterval<uint>(uint.MinValue, false, uint.MaxValue, false), uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt64 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<long>, ulong)>()
        {
            (ContinuousInterval<long>.EmptyInterval, 0),

			(new ContinuousInterval<long>(-1, false, 1, false), 2),
            (new ContinuousInterval<long>(-1, true, 1, false), 2),
            (new ContinuousInterval<long>(-1, false, 1, true), 2),
            (new ContinuousInterval<long>(-1, true, 1, true), 2),

            (new ContinuousInterval<long>(long.MinValue, false, long.MaxValue, false), ulong.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt64 { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<ulong>, ulong)>()
        {
            (ContinuousInterval<ulong>.EmptyInterval, 0),

			(new ContinuousInterval<ulong>(0, false, 2, false), 2),
            (new ContinuousInterval<ulong>(0, true, 2, false), 2),
            (new ContinuousInterval<ulong>(0, false, 2, true), 2),
            (new ContinuousInterval<ulong>(0, true, 2, true), 2),

            (new ContinuousInterval<ulong>(ulong.MinValue, false, ulong.MaxValue, false), ulong.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfSingle { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<float>, float)>()
        {
            (ContinuousInterval<float>.EmptyInterval, 0),

			(new ContinuousInterval<float>(0, false, 2, false), 2),
            (new ContinuousInterval<float>(0, true, 2, false), 2),
            (new ContinuousInterval<float>(0, false, 2, true), 2),
            (new ContinuousInterval<float>(0, true, 2, true), 2),

            (new ContinuousInterval<float>(float.MinValue, false, float.MaxValue, false), float.PositiveInfinity),
        });

        public static IEnumerable<object[]> IntervalsOfDouble { get; } = Make1ContinuousIntervalsData.OfDouble(new List<(string, double)>()
        {
            (Empty, 0),
            ("[-1,1]", 2),
            ("[-1,1)", 2),
            ("(-1,1]", 2),
            ("(-1,1)", 2),

            ("(-1,+Infinity)", double.PositiveInfinity),
            ("(-Infinity,1)", double.PositiveInfinity),
            ("(-Infinity,+Infinity)", double.PositiveInfinity),
            ($"({MinDouble},{MaxDouble})", double.PositiveInfinity),
        });

        public static IEnumerable<object[]> IntervalsOfDecimal { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<decimal>, decimal)>()
        {
            (ContinuousInterval<decimal>.EmptyInterval, 0),

			(new ContinuousInterval<decimal>(0, false, 2, false), 2),
            (new ContinuousInterval<decimal>(0, true, 2, false), 2),
            (new ContinuousInterval<decimal>(0, false, 2, true), 2),
            (new ContinuousInterval<decimal>(0, true, 2, true), 2),
        });

        public static IEnumerable<object[]> IntervalsOfDateTime { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<DateTime>, TimeSpan)>()
        {
            (ContinuousInterval<DateTime>.EmptyInterval, TimeSpan.Zero),

			(new ContinuousInterval<DateTime>(new DateTime(0), false, new DateTime(2), false), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTime>(new DateTime(0), true, new DateTime(2), false), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTime>(new DateTime(0), false, new DateTime(2), true), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTime>(new DateTime(0), true, new DateTime(2), true), TimeSpan.FromTicks(2)),

            (new ContinuousInterval<DateTime>(DateTime.MinValue, false, DateTime.MaxValue, false), TimeSpan.FromTicks(DateTime.MaxValue.Ticks)),
        });

        public static IEnumerable<object[]> IntervalsOfDateTimeOffset { get; } = MakeArbitraryData.Of(new List<(ContinuousInterval<DateTimeOffset>, TimeSpan)>()
        {
            (ContinuousInterval<DateTimeOffset>.EmptyInterval, TimeSpan.Zero),

			(new ContinuousInterval<DateTimeOffset>(new DateTimeOffset(0, TimeSpan.Zero), false, new DateTimeOffset(2, TimeSpan.Zero), false), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTimeOffset>(new DateTimeOffset(0, TimeSpan.Zero), true, new DateTimeOffset(2, TimeSpan.Zero), false), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTimeOffset>(new DateTimeOffset(0, TimeSpan.Zero), false, new DateTimeOffset(2, TimeSpan.Zero), true), TimeSpan.FromTicks(2)),
            (new ContinuousInterval<DateTimeOffset>(new DateTimeOffset(0, TimeSpan.Zero), true, new DateTimeOffset(2, TimeSpan.Zero), true), TimeSpan.FromTicks(2)),

            (new ContinuousInterval<DateTimeOffset>(DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue, false), TimeSpan.FromTicks(DateTime.MaxValue.Ticks)),
        });

        public static IEnumerable<object[]> IntervalsOfDayWithTimeSpanLength { get; } = Make1ContinuousIntervalsData.OfDay(new List<(string, TimeSpan)>()
        {
            (Empty, TimeSpan.Zero),
            ($"[{Monday},{Monday}]", TimeSpan.FromDays(1)),

            ($"[{Monday},{Wednesday}]", TimeSpan.FromDays(3)),
            ($"[{Monday},{Wednesday})", TimeSpan.FromDays(2)),
            ($"({Monday},{Wednesday}]", TimeSpan.FromDays(2)),
            ($"({Monday},{Wednesday})", TimeSpan.FromDays(1)),
            ($"[{Monday},{Sunday}]", TimeSpan.FromDays(7)),
        });

        public static IEnumerable<object[]> IntervalsOfDayWithPeriodLength { get; } = Make1ContinuousIntervalsData.OfDay(new List<(string, Period)>()
        {
            (Empty, Period.FromDays(0)),
            ($"[{Monday},{Monday}]", Period.FromDays(1)),

            ($"[{Monday},{Wednesday}]", Period.FromDays(3)),
            ($"[{Monday},{Wednesday})", Period.FromDays(2)),
            ($"({Monday},{Wednesday}]", Period.FromDays(2)),
            ($"({Monday},{Wednesday})", Period.FromDays(1)),
            ($"[{Monday},{Sunday}]", Period.FromDays(7)),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinateWithLongLength { get; } = Make1ContinuousIntervalsData.OfCoordinate(new List<(string, long)>()
        {
            (Empty, 0),
            ("[-1,1]", 2),
            ("[-1,1)", 1),
            ("(-1,1]", 1),
            ("(-1,1)", 0),

            ($"[{Coordinate.MinValue},{Coordinate.MaxValue}]", uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinateWithDistanceLength { get; } = Make1ContinuousIntervalsData.OfCoordinate(new List<(string, Distance)>()
        {
            (Empty, Distance.Zero),

            ("[-1,1]", new Distance(2)),
            ("[-1,1)", new Distance(1)),
            ("(-1,1]", new Distance(1)),
            ("(-1,1)", new Distance(0)),

            ($"[{Coordinate.MinValue},{Coordinate.MaxValue}]", new Distance(uint.MaxValue)),
        });

        [Theory]
        [MemberData(nameof(IntervalsOfSByte))]
        public void TestSByteLength(ContinuousInterval<sbyte> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfByte))]
        public void TestByteLength(ContinuousInterval<byte> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt16))]
        public void TestInt16Length(ContinuousInterval<short> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt16))]
        public void TestUInt16Length(ContinuousInterval<ushort> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt32))]
        public void TestInt32Length(ContinuousInterval<int> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt32))]
        public void TestUInt32Length(ContinuousInterval<uint> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt64))]
        public void TestInt64Length(ContinuousInterval<long> interval, ulong expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt64))]
        public void TestUInt64Length(ContinuousInterval<ulong> interval, ulong expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfSingle))]
        public void TestSingleLength(ContinuousInterval<float> interval, float expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDouble))]
        public void TestPrimitiveContinuousLength(ContinuousInterval<double> interval, double expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDecimal))]
        public void TestDecimalLength(ContinuousInterval<decimal> interval, decimal expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Fact]        
        public void DecimalLengthShouldOverflow() => Assert.Throws<OverflowException>(() => new ContinuousInterval<decimal>(decimal.MinValue, false, decimal.MaxValue, false).Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDateTime))]
        public void TestDateTimeLength(ContinuousInterval<DateTime> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDateTimeOffset))]
        public void TestDateTimeOffsetLength(ContinuousInterval<DateTimeOffset> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDayWithTimeSpanLength))]
        public void TestCustomStructDiscreteLengthWithPrimitiveResult(ContinuousInterval<Day> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length<Day, TimeSpan>());

        [Theory]
        [MemberData(nameof(IntervalsOfDayWithPeriodLength))]
        public void TestCustomStructDiscreteLengthWithCustomStructResult(ContinuousInterval<Day> interval, Period expectedResult) => Assert.Equal(expectedResult, interval.Length<Day, Period>());

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinateWithLongLength))]
        public void TestCustomClassDiscereteLengthWithPrimitiveResult(ContinuousInterval<Coordinate> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length<Coordinate, long>());

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinateWithDistanceLength))]
        public void TestCustomClassDiscereteLengthWithCustomClassResult(ContinuousInterval<Coordinate> interval, Distance expectedResult) => Assert.Equal(expectedResult, interval.Length<Coordinate, Distance>());
    }
}
