using System;
using System.Collections.Generic;
using System.Text;
using static Accretion.Intervals.Tests.StringConstants;
using Xunit;
using System.Linq;

namespace Accretion.Intervals.Tests
{
    public class IntervalIntersectTests
    {
		public static IEnumerable<object[]> IntervalsOfDouble { get; } = MakeIntervalsData.OfDouble(new List<(string, string, string)>()
		{
			(Empty, Empty, Empty),
			("[-5,0]", "[5,10]", Empty),
			("[-5,7]", "[5,10]", "[5,7]"),
			("[7,9]", "[5,10]", "[7,9]"),
			("[7,15]", "[5,10]", "[7,10]"),
			("[15,20]", "[5,10]", Empty),
			("[2,12]", "[5,10]", "[5,10]"),

			("[-5,0]", "[0,5]", "{0}"),
			("[-5,0]", "(0,5]", Empty),
			("[-5,0)", "[0,5]", Empty),
			("[-5,0)", "(0,5]", Empty),
			("[-5,5)∪(5,10]", "{5}", Empty),

			("[-20,20]", "(-15,-10]∪[-5,0]∪[5,10)∪(10,15]∪[17,25)", "(-15,-10]∪[-5,0]∪[5,10)∪(10,15]∪[17,20]"),
			("[-20,-15]∪[-10,-5]∪[0,5]", "[-15,-10]∪[-5,0]∪(5,10]", "{-15}∪{-10}∪{-5}∪{0}"),
			("[-20,-15)∪(-10,-5]∪[0,5]∪(10,15]∪[20,25]", "[-17,-10)∪[-9,-8]∪(-7,-5)∪(3,13)∪(27,30)", "[-17,-15)∪[-9,-8]∪(-7,-5)∪(3,5]∪(10,13)"),
			("(-20,-10]∪(-5,5]∪[15,20]", "[-25,-18)∪[-17,-15]∪[-12,-8]∪(-7,-3)∪[0,5]", "(-20,-18)∪[-17,-15]∪[-12,-10]∪(-5,-3)∪[0,5]"),
			("{-5}∪[0,5]∪{10}", "[-10,-5)∪(-5,2)∪(5,10]", "[0,2)∪{10}"),
			("[-5,0]∪[10,23]", "[-10,0]∪(5,10]∪(15,20)∪[25,30]∪[35,40]", "[-5,0]∪{10}∪(15,20)"),
			("(-5,5)∪(10,20]∪(25,35]", "(-5,-2]∪[2,7]∪[8,27]∪[28,29]∪[30,31]∪[32,33]∪(35,40]", "(-5,-2]∪[2,5)∪(10,20]∪(25,27]∪[28,29]∪[30,31]∪[32,33]"),
			("[-5,0]∪[5,10]", "[-10,-7]∪[2,4]∪[12,15]", Empty),
			("(-5,0)∪(0,5)∪(5,10)", "{0}∪{5}∪[7,12]", "[7,10)"),
			("[-5,0]", "[-20,-28]∪[-25,-23]∪[-20,-18]∪[-15,-13]∪[-10,-7]∪[2,4]∪[12,15]", Empty),
		});

		public static IEnumerable<object[]> IntervalsOfTwoOfDouble { get; } = IntervalsOfDouble.Select(x => new object[] { x[0], x[1] }).Concat(IntervalsOfDouble.Select(x => new object[] { x[0], x[2] })).Concat(IntervalsOfDouble.Select(x => new object[] { x[1], x[2] }));
		public static IEnumerable<object[]> IntervalsOfOneOfDouble { get; } = IntervalsOfDouble.SelectMany(x => x).Select(x => new object[] { x });

		[Theory]
		[MemberData(nameof(IntervalsOfOneOfDouble))]
		public void IntersectWithEmptyIntervalMustResultInAnEmptyInterval(Interval<double> interval) =>
			Assert.Equal(Interval<double>.EmptyInterval, interval.Intersect(Interval<double>.EmptyInterval), Interval<double>.LinearComparerByValue);

		[Theory]
		[MemberData(nameof(IntervalsOfTwoOfDouble))]
		public void IntersectLengthCannotExceedLengthOfTheSourceIntervals(Interval<double> firstInterval, Interval<double> secondInterval) =>
			Assert.True(Math.Min(firstInterval.Length(), secondInterval.Length()) >= firstInterval.Intersect(secondInterval).Length());

		[Theory]
		[MemberData(nameof(IntervalsOfTwoOfDouble))]
		public void IntersectMustBeCommutative(Interval<double> firstInterval, Interval<double> secondInterval) =>
			Assert.Equal(firstInterval.Intersect(secondInterval), secondInterval.Intersect(firstInterval), Interval<double>.LinearComparerByValue);

		[Theory]
		[MemberData(nameof(IntervalsOfDouble))]
		public void TestPrimitiveContinuousIntersect(Interval<double> firstInterval, Interval<double> secondInterval, Interval<double> expectedResult) => 
			Assert.Equal(expectedResult, firstInterval.Intersect(secondInterval), Interval<double>.LinearComparerByValue);
	}
}
