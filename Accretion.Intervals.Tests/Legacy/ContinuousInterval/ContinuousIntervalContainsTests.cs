using System;
using System.Collections.Generic;
using System.Text;
using static Accretion.Intervals.Tests.StringConstants;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalContainsTests
    {
        public static IEnumerable<object[]> IntervalsOfDouble { get; } = MakeIntervalsData.OfDouble(new List<(string, double, bool)>()
        {
            (Empty, -1, false),
            (Empty, 0, false),
            (Empty, 1, false),
            (Empty, double.Epsilon, false),

            ("(-1,1)", 0, true),
            ("(-1,1)", double.Epsilon, true),
            ("(-1,1)", 2, false),
            ("(-1,1)", -2, false),

            ("(-1,1)", 1, false),
            ("(-1,1)", -1, false),

            ("(-1,1]", 1, true),
            ("(-1,1]", -1, false),

            ("[-1,1)", 1, false),
            ("[-1,1)", -1, true),

            ("[-1,1]", 1, true),
            ("[-1,1]", -1, true),

            ("[-1,1]", double.NaN, false),
            ("[-1,1]", double.PositiveInfinity, false),
            ("[-1,1]", double.NegativeInfinity, false),

            ("[-1,+Infinity]", double.PositiveInfinity, true),
            ("[-Infinity,1]", double.NegativeInfinity, true),            
            ("[-1,+Infinity)", double.PositiveInfinity, false),
            ("(-Infinity,1]", double.NegativeInfinity, false),

            ("[-1,+Infinity]", double.MaxValue, true),
            ("[-Infinity,1]", double.MinValue, true),

            ($"[-1,{MaxDouble}]", double.PositiveInfinity, false),
            ($"[{MinDouble},1]", double.NegativeInfinity, false),
        });

        public static IEnumerable<object[]> IntervalsOfChar { get; } = MakeIntervalsData.OfChar(new List<(string, char, bool)>()
        {
            (Empty, char.MinValue, false),
            (Empty, char.MaxValue, false),
            (Empty, 'a', false),

            ("[-c,'e']", 'd', true),
            ("['c','e']", 'e', true),
            ("['c','e']", 'c', true),
            ("['c','e']", 'f', false),
            ("['c','e']", 'b', false),

            ("['b','f')", 'e', true),
            ("['b','f')", 'f', false),

            ("('b','f']", 'c', true),
            ("('b','f']", 'b', false),

            ("('b','f')", 'e', true),
            ("('b','f')", 'c', true),
            ("('b','f')", 'f', false),
            ("('b','f')", 'b', false),
        });


        public static IEnumerable<object[]> IntervalsOfDay { get; } = MakeIntervalsData.OfDay(new List<(string, Day, bool)>()
        {
            (Empty, Day.Monday, false),
            (Empty, Day.Friday, false),

            ($"[{Tuesday},{Thursday}]", Day.Wednesday, true),
            ($"[{Tuesday},{Thursday}]", Day.Thursday, true),
            ($"[{Tuesday},{Thursday}]", Day.Tuesday, true),
            ($"[{Tuesday},{Thursday}]", Day.Friday, false),
            ($"[{Tuesday},{Thursday}]", Day.Monday, false),

            ($"[{Monday},{Friday})", Day.Thursday, true),
            ($"[{Monday},{Friday})", Day.Friday, false),

            ($"({Monday},{Friday}]", Day.Tuesday, true),
            ($"({Monday},{Friday}]", Day.Monday, false),

            ($"({Monday},{Friday})", Day.Thursday, true),
            ($"({Monday},{Friday})", Day.Tuesday, true),
            ($"({Monday},{Friday})", Day.Friday, false),
            ($"({Monday},{Friday})", Day.Monday, false),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinate { get; } = MakeIntervalsData.OfCoordinate(new List<(string, Coordinate, bool)>()
        {
            (Empty, null, false),
            (Empty, new Coordinate(0), false),
            (Empty, new Coordinate(-1), false),
            (Empty, new Coordinate(1), false),

            ("[-1,1]", new Coordinate(0), true),
            ("[-1,1]", new Coordinate(1), true),
            ("[-1,1]", new Coordinate(-1), true),
            ("[-1,1]", new Coordinate(2), false),
            ("[-1,1]", new Coordinate(-2), false),

            ("[-2,2)", new Coordinate(1), true),
            ("[-2,2)", new Coordinate(2), false),

            ("(-2,2]", new Coordinate(-1), true),
            ("(-2,2]", new Coordinate(-2), false),

            ("(-2,2)", new Coordinate(1), true),
            ("(-2,2)", new Coordinate(-1), true),
            ("(-2,2)", new Coordinate(2), false),
            ("(-2,2)", new Coordinate(-2), false),
        });

        [Theory]
        [MemberData(nameof(IntervalsOfDouble))]
        public void TestPrimitiveContinuousContains(ContinuousInterval<double> interval, double value, bool expectedResult) => Assert.Equal(expectedResult, interval.Contains(value));

        [Theory]
        [MemberData(nameof(IntervalsOfChar))]
        public void TestPrimitiveDiscreteContains(ContinuousInterval<char> interval, char value, bool expectedResult) => Assert.Equal(expectedResult, interval.Contains(value));

        [Theory]
        [MemberData(nameof(IntervalsOfDay))]
        public void TestCustomStructDiscreteContains(ContinuousInterval<Day> interval, Day value, bool expectedResult) => Assert.Equal(expectedResult, interval.Contains(value));
        
        [Theory]
        [MemberData(nameof(IntervalsOfCoordinate))]
        public void TestCustomClassDiscreteContains(ContinuousInterval<Coordinate> interval, Coordinate value, bool expectedResult) => Assert.Equal(expectedResult, interval.Contains(value));
    }
}
