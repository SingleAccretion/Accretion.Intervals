using System;
using System.Collections.Generic;
using Xunit;
using static Accretion.Intervals.Tests.StringConstants;

namespace Accretion.Intervals.Tests
{
    public class IntervalLengthTests
    {
        public static IEnumerable<object[]> IntervalsOfSByte { get; } = Make.Data(new List<(Interval<sbyte>, int)>()
        {
            (Interval<sbyte>.Empty, 0),

            (Interval.Create<sbyte>(BoundaryType.Closed, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<sbyte>(BoundaryType.Closed, -1, 1, BoundaryType.Open), 2),
            (Interval.Create<sbyte>(BoundaryType.Open, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<sbyte>(BoundaryType.Open, -1, 1, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, sbyte.MinValue, sbyte.MaxValue, BoundaryType.Closed), byte.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfByte { get; } = Make.Data(new List<(Interval<byte>, int)>()
        {
            (Interval<byte>.Empty, 0),

            (Interval.Create<byte>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<byte>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<byte>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<byte>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, byte.MinValue, byte.MaxValue, BoundaryType.Closed), byte.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt16 { get; } = Make.Data(new List<(Interval<short>, int)>()
        {
            (Interval<short>.Empty, 0),

            (Interval.Create<short>(BoundaryType.Closed, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<short>(BoundaryType.Closed, -1, 1, BoundaryType.Open), 2),
            (Interval.Create<short>(BoundaryType.Open, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<short>(BoundaryType.Open, -1, 1, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, short.MinValue, short.MaxValue, BoundaryType.Closed), ushort.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt16 { get; } = Make.Data(new List<(Interval<ushort>, int)>()
        {
            (Interval<ushort>.Empty, 0),

            (Interval.Create<ushort>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<ushort>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<ushort>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<ushort>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, ushort.MinValue, ushort.MaxValue, BoundaryType.Closed), ushort.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt32 { get; } = MakeIntervalsData.OfInt(new List<(string, long)>()
        {
            (Empty, 0),
            ("[-1,1]", 2),
            ("[-1,1)", 2),
            ("(-1,1]", 2),
            ("(-1,1)", 2),

            ($"[{int.MinValue},{int.MaxValue}]", uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt32 { get; } = Make.Data(new List<(Interval<uint>, long)>()
        {
            (Interval<uint>.Empty, 0),

            (Interval.Create<uint>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<uint>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<uint>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<uint>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, uint.MinValue, uint.MaxValue, BoundaryType.Closed), uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfInt64 { get; } = Make.Data(new List<(Interval<long>, ulong)>()
        {
            (Interval<long>.Empty, 0),

            (Interval.Create<long>(BoundaryType.Closed, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<long>(BoundaryType.Closed, -1, 1, BoundaryType.Open), 2),
            (Interval.Create<long>(BoundaryType.Open, -1, 1, BoundaryType.Closed), 2),
            (Interval.Create<long>(BoundaryType.Open, -1, 1, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, long.MinValue, long.MaxValue, BoundaryType.Closed), ulong.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfUInt64 { get; } = Make.Data(new List<(Interval<ulong>, ulong)>()
        {
            (Interval<ulong>.Empty, 0),

            (Interval.Create<ulong>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<ulong>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<ulong>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<ulong>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, ulong.MinValue, ulong.MaxValue, BoundaryType.Closed), ulong.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfSingle { get; } = Make.Data(new List<(Interval<float>, float)>()
        {
            (Interval<float>.Empty, 0),

            (Interval.Create<float>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<float>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<float>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<float>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),

            (Interval.Create(BoundaryType.Closed, float.MinValue, float.MaxValue, BoundaryType.Closed), float.PositiveInfinity),
        });

        public static IEnumerable<object[]> IntervalsOfDouble { get; } = MakeIntervalsData.OfDouble(new List<(string, double)>()
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

        public static IEnumerable<object[]> IntervalsOfDecimal { get; } = Make.Data(new List<(Interval<decimal>, decimal)>()
        {
            (Interval<decimal>.Empty, 0),

            (Interval.Create<decimal>(BoundaryType.Closed, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<decimal>(BoundaryType.Closed, 0, 2, BoundaryType.Open), 2),
            (Interval.Create<decimal>(BoundaryType.Open, 0, 2, BoundaryType.Closed), 2),
            (Interval.Create<decimal>(BoundaryType.Open, 0, 2, BoundaryType.Open), 2),
        });

        public static IEnumerable<object[]> IntervalsOfDateTime { get; } = Make.Data(new List<(Interval<DateTime>, TimeSpan)>()
        {
            (Interval<DateTime>.Empty, TimeSpan.Zero),

            (Interval.Create(BoundaryType.Closed, new DateTime(0), new DateTime(2), BoundaryType.Closed), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Closed, new DateTime(0), new DateTime(2), BoundaryType.Open), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Open, new DateTime(0), new DateTime(2), BoundaryType.Closed), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Open, new DateTime(0), new DateTime(2), BoundaryType.Open), TimeSpan.FromTicks(2)),

            (Interval.Create(BoundaryType.Closed, DateTime.MinValue, DateTime.MaxValue, BoundaryType.Closed), TimeSpan.FromTicks(DateTime.MaxValue.Ticks)),
        });

        public static IEnumerable<object[]> IntervalsOfDateTimeOffset { get; } = Make.Data(new List<(Interval<DateTimeOffset>, TimeSpan)>()
        {
            (Interval<DateTimeOffset>.Empty, TimeSpan.Zero),

            (Interval.Create(BoundaryType.Closed, new DateTimeOffset(0, TimeSpan.Zero),  new DateTimeOffset(2, TimeSpan.Zero), BoundaryType.Closed), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Open, new DateTimeOffset(0, TimeSpan.Zero), new DateTimeOffset(2, TimeSpan.Zero), BoundaryType.Closed), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Closed, new DateTimeOffset(0, TimeSpan.Zero), new DateTimeOffset(2, TimeSpan.Zero), BoundaryType.Open), TimeSpan.FromTicks(2)),
            (Interval.Create(BoundaryType.Open, new DateTimeOffset(0, TimeSpan.Zero), new DateTimeOffset(2, TimeSpan.Zero), BoundaryType.Open), TimeSpan.FromTicks(2)),

            (Interval.Create(BoundaryType.Closed, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, BoundaryType.Closed), TimeSpan.FromTicks(DateTime.MaxValue.Ticks)),
        });

        public static IEnumerable<object[]> IntervalsOfDayWithTimeSpanLength { get; } = MakeIntervalsData.OfDay(new List<(string, TimeSpan)>()
        {
            (Empty, TimeSpan.Zero),
            ($"[{Monday},{Monday}]", TimeSpan.FromDays(1)),

            ($"[{Monday},{Wednesday}]", TimeSpan.FromDays(3)),
            ($"[{Monday},{Wednesday})", TimeSpan.FromDays(2)),
            ($"({Monday},{Wednesday}]", TimeSpan.FromDays(2)),
            ($"({Monday},{Wednesday})", TimeSpan.FromDays(1)),
            ($"[{Monday},{Sunday}]", TimeSpan.FromDays(7)),
        });

        public static IEnumerable<object[]> IntervalsOfDayWithPeriodLength { get; } = MakeIntervalsData.OfDay(new List<(string, Period)>()
        {
            (Empty, Period.FromDays(0)),
            ($"[{Monday},{Monday}]", Period.FromDays(1)),

            ($"[{Monday},{Wednesday}]", Period.FromDays(3)),
            ($"[{Monday},{Wednesday})", Period.FromDays(2)),
            ($"({Monday},{Wednesday}]", Period.FromDays(2)),
            ($"({Monday},{Wednesday})", Period.FromDays(1)),
            ($"[{Monday},{Sunday}]", Period.FromDays(7)),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinateWithLongLength { get; } = MakeIntervalsData.OfCoordinate(new List<(string, long)>()
        {
            (Empty, 0),
            ("[-1,1]", 2),
            ("[-1,1)", 1),
            ("(-1,1]", 1),
            ("(-1,1)", 0),

            ($"[{Coordinate.MinValue},{Coordinate.MaxValue}]", uint.MaxValue),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinateWithDistanceLength { get; } = MakeIntervalsData.OfCoordinate(new List<(string, Distance)>()
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
        public void TestSByteLength(Interval<sbyte> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfByte))]
        public void TestByteLength(Interval<byte> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt16))]
        public void TestInt16Length(Interval<short> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt16))]
        public void TestUInt16Length(Interval<ushort> interval, int expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt32))]
        public void TestInt32Length(Interval<int> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt32))]
        public void TestUInt32Length(Interval<uint> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfInt64))]
        public void TestInt64Length(Interval<long> interval, ulong expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfUInt64))]
        public void TestUInt64Length(Interval<ulong> interval, ulong expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfSingle))]
        public void TestSingleLength(Interval<float> interval, float expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDouble))]
        public void TestPrimitiveContinuousLength(Interval<double> interval, double expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDecimal))]
        public void TestDecimalLength(Interval<decimal> interval, decimal expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Fact]
        public void DecimalLengthShouldOverflow() => Assert.Throws<OverflowException>(() => Interval.Create(BoundaryType.Closed, decimal.MinValue, decimal.MaxValue, BoundaryType.Closed).Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDateTime))]
        public void TestDateTimeLength(Interval<DateTime> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length());

        [Theory]
        [MemberData(nameof(IntervalsOfDateTimeOffset))]
        public void TestDateTimeOffsetLength(Interval<DateTimeOffset> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length());

        /*
        [Theory]
        [MemberData(nameof(IntervalsOfDayWithTimeSpanLength))]
        public void TestCustomStructDiscreteLengthWithPrimitiveResult(Interval<Day> interval, TimeSpan expectedResult) => Assert.Equal(expectedResult, interval.Length<Day, TimeSpan>());
        */

        [Theory]
        [MemberData(nameof(IntervalsOfDayWithPeriodLength))]
        public void TestCustomStructDiscreteLengthWithCustomStructResult(Interval<Day> interval, Period expectedResult) => Assert.Equal(expectedResult, interval.Length<Day, Period>());

        /*
        [Theory]
        [MemberData(nameof(IntervalsOfCoordinateWithLongLength))]
        public void TestCustomClassDiscereteLengthWithPrimitiveResult(Interval<Coordinate> interval, long expectedResult) => Assert.Equal(expectedResult, interval.Length<Coordinate, long>());
        */

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinateWithDistanceLength))]
        public void TestCustomClassDiscereteLengthWithCustomClassResult(Interval<Coordinate> interval, Distance expectedResult) => Assert.Equal(expectedResult, interval.Length<Coordinate, Distance>());
    }
}