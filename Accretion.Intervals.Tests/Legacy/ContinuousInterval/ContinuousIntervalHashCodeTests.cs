using System;
using static Accretion.Intervals.Tests.StringConstants;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalHashCodeTests
    {
        public static IEnumerable<object[]> IntervalsOfDoubles { get; } = MakeIntervalsData.OfDouble(new List<(string, string, bool)>()
        {
            (Empty, Empty, true),
            ("(-5,5)", "(-5,5)", true),
            ("[-5,5)", "[-5,5)", true),
            ("(-5,5]", "(-5,5]", true),
            ("[-5,5]", "[-5,5]", true),

            ("(-4,4)", "(-5,5)", false),
            ("(-5,5)", "[-5,5)", false),
            ("(-5,5)", "(-5,5]", false),
            ("(-5,5)", "[-5,5]", false),
        });

        public static IEnumerable<object[]> IntervalsOfChar { get; } = MakeIntervalsData.OfChar(new List<(string, string, bool)>()
        {
            (Empty, Empty, true),
            ("['c','f']", "['c','f']", true),
            ("['c','f']", "('b','f']", true),
            ("['c','f']", "['c','g')", true),
            ("['c','f']", "('b','g')", true),
            ("['b','b']", "('a','c')", true),

            ("['c','f']", "['b','g']", false),
        });

        public static IEnumerable<object[]> IntervalsOfDays { get; } = MakeIntervalsData.OfDay(new List<(string, string, bool)>()
        {
            (Empty, Empty, true),
            ($"[{Tuesday},{Wednesday}]", $"[{Tuesday},{Wednesday}]", true),
            ($"[{Tuesday},{Wednesday}]", $"({Monday},{Wednesday}]", true),
            ($"[{Tuesday},{Wednesday}]", $"[{Tuesday},{Thursday})", true),
            ($"[{Tuesday},{Wednesday}]", $"({Monday},{Thursday})", true),
            ($"[{Tuesday},{Tuesday}]", $"({Monday},{Wednesday})", true),

            ($"[{Tuesday},{Wednesday}]", $"({Monday},{Monday})", false),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinates { get; } = MakeIntervalsData.OfCoordinate(new List<(string, string, bool)>()
        {
            (Empty, Empty, true),
            ("[-5,5]", "[-5,5]", true),
            ("[-5,5]", "(-6,5]", true),
            ("[-5,5]", "[-5,6)", true),
            ("[-5,5]", "(-6,6)", true),
            ("[0,0]", "(-1,1)", true),

            ("[-5,5]", "[-6,6]", false),
        });

        [Theory]
        [MemberData(nameof(IntervalsOfDoubles))]
        public void TestPrimitiveContinuousHashCode(ContinuousInterval<double> first, ContinuousInterval<double> second, bool hashCodesAreEqual) => Assert.Equal(hashCodesAreEqual, first.GetHashCode() == second.GetHashCode());

        [Theory]
        [MemberData(nameof(IntervalsOfChar))]
        public void TestPrimitiveDiscreteHashCode(ContinuousInterval<char> first, ContinuousInterval<char> second, bool hashCodesAreEqual) => Assert.Equal(hashCodesAreEqual, first.GetHashCode() == second.GetHashCode());

        [Theory]
        [MemberData(nameof(IntervalsOfDays))]
        public void TestCustomStructDiscreteHashCode(ContinuousInterval<Day> first, ContinuousInterval<Day> second, bool hashCodesAreEqual) => Assert.Equal(hashCodesAreEqual, first.GetHashCode() == second.GetHashCode());

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinates))]
        public void TestCustomClassDiscreteHashCode(ContinuousInterval<Coordinate> first, ContinuousInterval<Coordinate> second, bool hashCodesAreEqual) => Assert.Equal(hashCodesAreEqual, first.GetHashCode() == second.GetHashCode());
    }
}
