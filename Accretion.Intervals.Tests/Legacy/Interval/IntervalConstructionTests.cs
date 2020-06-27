using System.Collections.Generic;
using static Accretion.Intervals.Tests.StringConstants;

namespace Accretion.Intervals.Tests
{
    public class IntervalConstructionTests
    {
		public static IEnumerable<object[]> IntervalsOfDouble { get; } = MakeCompositeIntervalsData.OfDouble<string>(new List<(string, string)>()
		{
			("[-5,5]∪[0,10]", "[-5,10]"),
			("[-5,5]∪[-10,0]", "[-10,5]"),
			("[-5,5]∪[-1,1]", "[-5,5]"),
			("[-5,5]∪[-6,6]", "[-6,6]"),

			("[-5,5]∪[5,10]", "[-5,10]"),
			("[-5,5]∪(5,10]", "[-5,10]"),
			("[-5,5]∪(5,10]", "[-5,10]"),
			("[-5,5)∪(5,10]", "[-5,5)∪(5,10]"),

			("[-5,5]∪[-10,-5]", "[-10,5]"),
			("[-5,5]∪[-10,-5)", "[-10,5]"),
			("(-5,5]∪[-10,-5]", "[-10,5]"),
			("(-5,5]∪[-10,-5)", "[-10,-5)∪(-5,5]"),

			("[-5,5]∪[-5,5]", "[-5,5]"),
			("[-5,5]∪[-5,5)", "[-5,5]"),
			("[-5,5]∪(-5,5]", "[-5,5]"),
			("[-5,5]∪(-5,5)", "[-5,5]"),
			("(-5,5]∪(-5,5]", "(-5,5]"),
			("[-5,5)∪[-5,5)", "[-5,5)"),
			("(-5,5)∪(-5,5)", "(-5,5)"),			
			("[-5,5]∪[10,15]", "[-5,5]∪[10,15]"),
			("[-5,5]∪[-15,-10]", "[-15,-10]∪[-5,5]"),
			($"[-5,5]∪{Empty}", "[-5,5]"),

			(Empty, Empty),
			($"{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}∪{Empty}∪{Empty}", Empty),

			("[-5,5]", "[-5,5]"),
			("(-5,5)∪(5,10)∪{5}", "(-5,10)"),
			("[-5,5]∪(-23,-2]", "(-23,5]"),
			($"(0,2)∪[-5,5]∪{Empty}∪(-23,-2]", "(-23,5]"),
			($"{Empty}∪[10,15]∪(0,2)∪[-5,5]∪(-23,-2]", "(-23,5]∪[10,15]"),
			($"[10,15)∪(0,2)∪{Empty}∪[-5,5]∪(-23,-2]∪(15,16)", "(-23,5]∪[10,15)∪(15,16)"),
		});

		public static IEnumerable<object[]> IntervalsOfInt { get; } = MakeCompositeIntervalsData.OfInt<string>(new List<(string, string)>()
		{
			("[-5,5]∪[0,10]", "[-5,10]"),
			("[-5,5]∪[-10,0]", "[-10,5]"),
			("[-5,5]∪[-1,1]", "[-5,5]"),
			("[-5,5]∪[-6,6]", "[-6,6]"),
			("[-5,5]∪(-6,6)", "(-6,6)"),

			("[-5,5]∪[5,10]", "[-5,10]"),
			("[-5,5]∪(5,10]", "[-5,10]"),
			("[-5,5]∪(5,10]", "[-5,10]"),
			("[-5,5)∪(5,10]", "[-5,5)∪(5,10]"),

			("[-5,5]∪[-10,-5]", "[-10,5]"),
			("[-5,5]∪[-10,-5)", "[-10,5]"),
			("(-5,5]∪[-10,-5]", "[-10,5]"),
			("(-5,5]∪[-10,-5)", "[-10,-5)∪(-5,5]"),

			("[-5,5]∪[-5,5]", "[-5,5]"),
			("[-5,5]∪[-5,5)", "[-5,5]"),
			("[-5,5]∪(-5,5]", "[-5,5]"),
			("[-5,5]∪(-5,5)", "[-5,5]"),
			("(-5,5]∪(-5,5]", "(-5,5]"),
			("[-5,5)∪[-5,5)", "[-5,5)"),
			("(-5,5)∪(-5,5)", "(-5,5)"),
			("[-5,5]∪[10,15]", "[-5,5]∪[10,15]"),
			("[-5,5]∪[-15,-10]", "[-15,-10]∪[-5,5]"),
			($"[-5,5]∪{Empty}", "[-5,5]"),

			(Empty, Empty),
			($"{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪[5,10]", "[5,10]"),
			($"{Empty}∪{Empty}∪[5,10]", "[5,10]"),
			($"{Empty}∪{Empty}∪{Empty}∪[5,10]", "[5,10]"),

			("[-5,5]", "[-5,5]"),
			("[-5,5]∪(-23,-2]", "(-23,5]"),
			("(0,2)∪[-5,5]∪(-23,-2]", "(-23,5]"),
			("[10,15]∪(0,2)∪[-5,5]∪(-23,-2]", "(-23,5]∪[10,15]"),
			("[10,15)∪(0,2)∪[-5,5]∪(-23,-2]∪(15,16)", "(-23,5]∪[10,15)"),
			("[10,15)∪(0,2)∪(-100,-98)∪[-5,5]∪(-23,-2]∪(15,16)", "(-100,-98)∪(-23,5]∪[10,15)"),
		});

		public static IEnumerable<object[]> IntervalsOfCoordinate { get; } = MakeCompositeIntervalsData.OfCoordinate<string>(new List<(string, string)>()
		{
			(Empty, Empty),
			($"[-5,5]∪{Empty}", "[-5,5]"),
			($"[-5,5]∪{Empty}∪{Empty}", "[-5,5]"),

			($"{Empty}∪{Empty}", Empty),
			($"{Empty}∪{Empty}∪{Empty}", Empty),
			($"{Empty}∪[5,10]", "[5,10]"),
			($"{Empty}∪{Empty}∪[5,10]", "[5,10]"),
			($"{Empty}∪{Empty}∪{Empty}∪[5,10]", "[5,10]"),
		});

		/*
		[Fact]
		public void IntervalConstructedWithEmptyCollectionShouldBeEmpty() => Assert.True(new Interval<double>(Array.Empty<ContinuousInterval<double>>()).IsEmpty);

		[Theory]
		[MemberData(nameof(IntervalsOfDouble))]
		public void TestPrimitiveContinuousConstruction(Interval<double> interval, string expectedResult) => Assert.Equal(expectedResult, interval.ToString());

		[Theory]
		[MemberData(nameof(IntervalsOfInt))]
		public void TestPrimitiveDiscreteConstruction(Interval<int> interval, string expectedResult) => Assert.Equal(expectedResult, interval.ToString());
		
		[Theory]
		[MemberData(nameof(IntervalsOfCoordinate))]
		public void TestCustomClassDiscereteConstruction(Interval<Coordinate> interval, string expectedResult) => Assert.Equal(expectedResult, interval.ToString());
		*/
	}
}
