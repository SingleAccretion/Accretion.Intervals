using System;
using System.Collections.Generic;
using System.Text;
using static Accretion.Intervals.Tests.StringConstants;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalEqualityTests
    {
        public static IEnumerable<object[]> IntervalsOfDoubles { get; } = MakeIntervalsData.OfDouble(new List<(string, string, bool)>
        {
            (Empty, Empty, true),
            ("[0,0]", Empty, false),
            ("[0,1]", "(0,1)", false),
            ("[0,1]", "(0,1]", false),
            ("[0,1]", "[0,1)", false),
        });

        public static IEnumerable<object[]> IntervalsOfChar { get; } = MakeIntervalsData.OfChar(new List<(string, string, bool)>
        {
             (Empty, Empty, true),
             ("['d','d']", "('c','e')", true),
             ("['d','d']", "('c','d']", true),
             ("['d','d']", "['d','e')", true),
             ("['d','d']", "('c','e')", true),
             
             ("['c','e']", "('c','e')", false),
        });

        public static IEnumerable<object[]> IntervalsOfDays { get; } = MakeIntervalsData.OfDay(new List<(string, string, bool)>
        {
             (Empty, Empty, true),
             ($"[{Tuesday},{Tuesday}]", $"({Monday},{Wednesday})", true),
             ($"[{Tuesday},{Tuesday}]", $"({Monday},{Tuesday}]", true),
             ($"[{Tuesday},{Tuesday}]", $"[{Tuesday},{Wednesday})", true),
             ($"[{Tuesday},{Tuesday}]", $"({Monday},{Wednesday})", true),

             ($"({Monday},{Wednesday})", $"[{Monday},{Wednesday}]", false),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinates { get; } = MakeIntervalsData.OfCoordinate(new List<(string, string, bool)>
        {
             (Empty, Empty, true),
             ("[0,0]", "(-1,1)", true),
             ("[0,0]", "(-1,0]", true),
             ("[0,0]", "[0,1)", true),
             ("[0,0]", "(-1,1)", true),

             ("[-1,1]", "(-1,1)", false),
        });

        [Theory]
        [MemberData(nameof(IntervalsOfDoubles))]
        public void TestPrimitiveContinuousEquality(ContinuousInterval<double> first, ContinuousInterval<double> second, bool expectedResult) => Assert.Equal(expectedResult, first.Equals(second));

        [Theory]
        [MemberData(nameof(IntervalsOfChar))]
        public void TestPrimitveDiscreteEquality(ContinuousInterval<char> first, ContinuousInterval<char> second, bool expectedResult) => Assert.Equal(expectedResult, first.Equals(second));
        
        [Theory]
        [MemberData(nameof(IntervalsOfDays))]
        public void TestCustomStructDiscreteEquality(ContinuousInterval<Day> first, ContinuousInterval<Day> second, bool expectedResult) => Assert.Equal(expectedResult, first.Equals(second));

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinates))]
        public void TestCustomClassDiscreteEquality(ContinuousInterval<Coordinate> first, ContinuousInterval<Coordinate> second, bool expectedResult) => Assert.Equal(expectedResult, first.Equals(second));
    }
}
