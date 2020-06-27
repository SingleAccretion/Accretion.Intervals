using System;
using System.Collections.Generic;
using System.Text;
using static Accretion.Intervals.Tests.StringConstants;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class ContinuousIntervalIsEmptyTests
    {
        public static IEnumerable<object[]> IntervalsOfDoubles { get; } = MakeIntervalsData.OfDouble(new List<(string, bool)>
        {
            ("[0,0]", false), 
            ("[0,5]", false),
            
            ($"[{MaxDouble},{MaxDouble}]", false),
            ($"[{MinDouble},{MinDouble}]", false),

            (Empty, true),
            ("[5,0]", true),
            ("(0,0)", true),
            ("(0,0]", true),
            ("[0,0)", true),
            
            ("(+Infinity,+Infinity)", true),
            ("(-Infinity,-Infinity)", true),
            ("[+Infinity,+Infinity]", true),
            ("[-Infinity,-Infinity]", true),

            ("(NaN,NaN)", true),
            ("[NaN,NaN]", true),
            ("(0,NaN)", true),
            ("(NaN,0)", true),
            ("(0,NaN]", true),
            ("[NaN,0)", true),
            
            ($"({MaxDouble},{MaxDouble})", true),
            ($"({MinDouble},{MinDouble})", true),
        });

        public static IEnumerable<object[]> IntervalsOfChar { get; } = MakeIntervalsData.OfChar(new List<(string, bool)>
        {
            ("['d','i']", false),
            ("['d','d']", false),
            ("('c','d']", false),
            ("['d','e')", false),
            ("('c','e')", false),

            ($"[{char.MinValue},{char.MinValue}]", false),
            ($"[{char.MaxValue},{char.MaxValue}]", false),

            (Empty, true),
            ("['i','d']", true),
            ("('d','d')", true),
            ("('d','d']", true),
            ("['d','d')", true),
            
            ("('d','e')", true),
            ("('c','d')", true),
            
            ($"('d',{char.MinValue})", true),
            ($"({char.MaxValue},'d')", true),
        });

        public static IEnumerable<object[]> IntervalsOfDays { get; } = MakeIntervalsData.OfDay(new List<(string, bool)>
        {
            ($"[{Monday},{Friday}]", false),
            ($"({Monday},{Tuesday}]", false),
            ($"[{Tuesday},{Wednesday})", false),
            ($"({Monday},{Wednesday})", false),

            (Empty, true),
            ($"[{Friday},{Monday}]", true),
            ($"({Monday},{Monday})", true),
            ($"[{Monday},{Monday})", true),
            ($"({Monday},{Monday}]", true),

            ($"({Monday},{Tuesday})", true),
            ($"({Tuesday},{Wednesday})", true),

            ($"({Wednesday},{Monday})", true),
            ($"({Friday},{Wednesday})", true),
        });

        public static IEnumerable<object[]> IntervalsOfCoordinates { get; } = MakeIntervalsData.OfCoordinate(new List<(string, bool)>
        {
            ("[0,5]", false),
            ("[0,0]", false),
            ("(-1,0]", false),
            ("[0,1)", false),
            ("(-1,1)", false),

            ($"[{Coordinate.MinValue},{Coordinate.MinValue}]", false),
            ($"[{Coordinate.MaxValue},{Coordinate.MaxValue}]", false),

            (Empty, true),
            ("[5,0]", true),
            ("(0,0)", true),
            ("(0,0]", true),
            ("[0,0)", true),

            ("(0,1)", true),
            ("(-1,0)", true),

            ($"(0,{Coordinate.MinValue})", true),
            ($"({Coordinate.MaxValue},0)", true),
        });


        [Theory]
        [MemberData(nameof(IntervalsOfDoubles))]
        public void TestPrimitiveContinuousIsEmpty(ContinuousInterval<double> interval, bool expectedValue) => Assert.Equal(expectedValue, interval.IsEmpty);
        
        [Theory]
        [MemberData(nameof(IntervalsOfChar))]
        public void TestPrimitiveDiscreteIsEmpty(ContinuousInterval<char> interval, bool expectedValue) => Assert.Equal(expectedValue, interval.IsEmpty);

        [Theory]
        [MemberData(nameof(IntervalsOfDays))]
        public void TestCustomStructDiscreteIsEmpty(ContinuousInterval<Day> interval, bool expectedValue) => Assert.Equal(expectedValue, interval.IsEmpty);

        [Theory]
        [MemberData(nameof(IntervalsOfCoordinates))]
        public void TestCustomDiscreteClassIsEmpty(ContinuousInterval<Coordinate> interval, bool expectedValue) => Assert.Equal(expectedValue, interval.IsEmpty);
    }
}
