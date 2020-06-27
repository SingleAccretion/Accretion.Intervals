using System.Collections.Generic;
using static Accretion.Intervals.Tests.StringConstants;

namespace Accretion.Intervals.Tests
{
    public class IntervalUnionTests
    {
		public static IEnumerable<object[]> IntervalsOfDouble { get; } = MakeCompositeIntervalsData.OfDouble(new List<(string, string, string)>()
		{
			(Empty, Empty, Empty),
			("[-5,5]", Empty, "[-5,5]"),
			("[-5,5]", "[-15,-10]", "[-15,-10]∪[-5,5]"),
			("[-5,5]", "[-10,-3]", "[-10,5]"),
			("[-5,5]", "[-3,3]", "[-5,5]"),
			("[-5,5]", "[-3,10]", "[-5,10]"),
			("[-5,5]", "[10,15]", "[-5,5]∪[10,15]"),

			("[-5,5]", "[-5,5]", "[-5,5]"),
			("[-5,5]", "(-5,5]", "[-5,5]"),
			("[-5,5]", "[-5,5)", "[-5,5]"),
			("[-5,5]", "(-5,5)", "[-5,5]"),
			("[-5,5)", "(-5,5)", "[-5,5)"),
			("(-5,5)", "(-5,5)", "(-5,5)"),

			("[-5,5]", "[5,10]", "[-5,10]"),
			("[-5,5]", "(5,10]", "[-5,10]"),
			("[-5,5)", "[5,10]", "[-5,10]"),
			("[-5,5)", "(5,10]", "[-5,5)∪(5,10]"),
			("[-5,5)∪(5,10]", "{5}", "[-5,10]"),

			("[-5,5]∪[10,15]", "[-10,-3]∪[3,12]", "[-10,15]"),
			("[-5,5]∪[12,15]", "[7,10]∪[17,20]∪[22,25]", "[-5,5]∪[7,10]∪[12,15]∪[17,20]∪[22,25]"),
			("(-5,5)∪(10,15)∪(20,25)", "[-10,-5)∪(5,10)∪(15,20)∪[25,30]", "[-10,-5)∪(-5,5)∪(5,10)∪(10,15)∪(15,20)∪(20,30]"),
			("[-5,25]", "[0,5]∪[7,10]∪(15,20)", "[-5,25]"),
			("[-5,5]∪(10,15]∪(20,25]∪(30,35]", "(0,10)∪[17,19]", "[-5,10)∪(10,15]∪[17,19]∪(20,25]∪(30,35]"),
			("[-5,5]∪[10,15]∪[20,25)∪(30,35]", "[-10,-3)∪(-3,7]∪(12,14)∪[15,20)", "[-10,7]∪[10,25)∪(30,35]"),
			("[-5,5]∪[10,15]∪[20,25]∪[30,35]∪[40,45]", "[-15,-10]∪[45,50]", "[-15,-10]∪[-5,5]∪[10,15]∪[20,25]∪[30,35]∪[40,45]∪[45,50]"),
			("(-5,5)∪(10,15)∪[20,25]", "[-5,-2)∪(2,5]∪[10,30]", "[-5,5]∪[10,30]"),
			("[-5,5]∪[10,15]∪[20,40]", "[0,25]", "[-5,40]"),
			("[-5,5]∪(10,15]∪(20,25)∪(30,35)", "(-5,0)∪(0,5)∪(7,10)∪[13,35]∪[40,45)", "[-5,5]∪(7,10)∪(10,35]∪[40,45)"),
		});

		/*
		public static IEnumerable<object[]> IntervalsOfTwoOfDouble { get; } = IntervalsOfDouble.Select(x => new object[] { x[0], x[1] }).Concat(IntervalsOfDouble.Select(x => new object[] { x[0], x[2] })).Concat(IntervalsOfDouble.Select(x => new object[] { x[1], x[2] }));
		public static IEnumerable<object[]> IntervalsOfOneOfDouble { get; } = IntervalsOfDouble.SelectMany(x => x).Select(x => new object[] { x });

		public static IEnumerable<object[]> IntervalsOfCoordinate { get; } = MakeCompositeIntervalsData.OfCoordinate(new List<(string, string, string)>()
		{
			(Empty, Empty, Empty),
			("[-5,5]", Empty, "[-5,5]"),
			(Empty, "[-5,5]", "[-5,5]"),
		});

		[Theory]
		[MemberData(nameof(IntervalsOfOneOfDouble))]
		public void UnionWithEmptyIntervalMustResultInTheSameInterval(Interval<double> interval) =>
			Assert.Equal(interval, interval.Union(Interval<double>.EmptyInterval), Interval<double>.LinearComparerByValue);

		[Theory]
		[MemberData(nameof(IntervalsOfTwoOfDouble))]
		public void UnionLengthCannotExceedCombinedLengthOfTheSourceIntervals(Interval<double> firstInterval, Interval<double> secondInterval) =>
			Assert.True(firstInterval.Length() + secondInterval.Length() >= firstInterval.Union(secondInterval).Length());

		[Theory]
		[MemberData(nameof(IntervalsOfTwoOfDouble))]
		public void UnionMustBeCommutative(Interval<double> firstInterval, Interval<double> secondInterval) =>
			Assert.Equal(firstInterval.Union(secondInterval), secondInterval.Union(firstInterval), Interval<double>.LinearComparerByValue);

		[Theory]
		[MemberData(nameof(IntervalsOfDouble))]
		public void TestPrimitiveContinuousUnion(Interval<double> firstInterval, Interval<double> secondInterval, Interval<double> expectedResult) => 
			Assert.Equal(expectedResult, firstInterval.Union(secondInterval), Interval<double>.LinearComparerByValue);
		
		[Theory]
		[MemberData(nameof(IntervalsOfCoordinate))]
		public void TestCustomClassDiscreteUnion(Interval<Coordinate> firstInterval, Interval<Coordinate> secondInterval, Interval<Coordinate> expectedResult) => 
			Assert.Equal(expectedResult, firstInterval.Union(secondInterval), Interval<Coordinate>.LinearComparerByValue);
		*/
	}
}
