using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class OverlappingStrategiesTests
    {
		public static IEnumerable<object[]> InvariantOverlapTestCases { get; } = MakeBoundariesData.OfDouble(new List<(string, string, bool)>()
		{
			("[0", "(0", true),
			("(0", "[0", false),
			("0]", "0)", false),
			("0)", "0]", true),

			("[0", "[0", false),
			("(0", "(0", false),
			("0]", "0]", false),
			("0)", "0)", false)
		});

		public static IEnumerable<object[]> FullOverlapTestCases { get; } = MakeBoundariesData.OfDouble(new List<(string, string, bool)>()
		{
			("[0", "0]", true),
			("(0", "0)", true),
			("[0", "0)", true),
			("(0", "0]", true),

			("0]", "[0", false),
			("0)", "(0", false),
			("0]", "(0", false),
			("0)", "[0", false),
		});

		public static IEnumerable<object[]> NoOverlapTestCases { get; } = MakeBoundariesData.OfDouble(new List<(string, string, bool)>()
		{
			("[0", "0]", false),
			("(0", "0)", false),
			("[0", "0)", false),
			("(0", "0]", false),

			("0]", "[0", true),
			("0)", "(0", true),
			("0]", "(0", true),
			("0)", "[0", true),
		});

		public static IEnumerable<object[]> OverlapClosedTestCases { get; } = MakeBoundariesData.OfDouble(new List<(string, string, bool)>()
		{
			("[0", "0]", true),
			("(0", "0)", false),
			("[0", "0)", true),
			("(0", "0]", true),

			("0]", "[0", false),
			("0)", "(0", true),
			("0]", "(0", false),
			("0)", "[0", false),
		});

		public static IEnumerable<object[]> OverlapFullyClosedTestCases { get; } = MakeBoundariesData.OfDouble(new List<(string, string, bool)>()
		{
			("[0", "0]", true),
			("(0", "0)", false),
			("[0", "0)", false),
			("(0", "0]", false),

			("0]", "[0", false),
			("0)", "(0", true),
			("0]", "(0", true),
			("0)", "[0", true),
		});

		[Theory]
		[MemberData(nameof(InvariantOverlapTestCases))]
		public void TestInvariantOverlap(object firstBoundary, object secondBoundary, bool expectedResult)
		{
			if (firstBoundary is LowerBoundary<double>)
			{
				Assert.Equal(expectedResult, OverlapStrategies<double>.Invariant.IsLess((LowerBoundary<double>)firstBoundary, (LowerBoundary<double>)secondBoundary));
			}
			else
			{
				Assert.Equal(expectedResult,  OverlapStrategies<double>.Invariant.IsLess((UpperBoundary<double>)firstBoundary, (UpperBoundary<double>)secondBoundary));
			}
		}

		[Theory]
		[MemberData(nameof(FullOverlapTestCases))]
		public void TestFullOverlap(object firstBoundary, object secondBoundary, bool expectedResult) => TestOverlap(firstBoundary, secondBoundary, expectedResult, OverlapStrategies<double>.FullOverlap);

		[Theory]
		[MemberData(nameof(NoOverlapTestCases))]
		public void TestNoOverlap(object firstBoundary, object secondBoundary, bool expectedResult) => TestOverlap(firstBoundary, secondBoundary, expectedResult, OverlapStrategies<double>.NoOverlap);

		[Theory]
		[MemberData(nameof(OverlapClosedTestCases))]
		public void TestOverlapClosed(object firstBoundary, object secondBoundary, bool expectedResult) => TestOverlap(firstBoundary, secondBoundary, expectedResult, OverlapStrategies<double>.OverlapClosed);

		[Theory]
		[MemberData(nameof(OverlapFullyClosedTestCases))]
		public void TestOverlapFullyClosed(object firstBoundary, object secondBoundary, bool expectedResult) => TestOverlap(firstBoundary, secondBoundary, expectedResult, OverlapStrategies<double>.OverlapFullyClosed);

		private void TestOverlap(object firstBoundary, object secondBoundary, bool expectedResult, IOverlappingStrategy<double> strategy)
		{
			if (firstBoundary is LowerBoundary<double>)
			{
				Assert.Equal(expectedResult, strategy.IsLess((LowerBoundary<double>)firstBoundary, (UpperBoundary<double>)secondBoundary));
			}
			else
			{
				Assert.Equal(expectedResult, strategy.IsLess((UpperBoundary<double>)firstBoundary, (LowerBoundary<double>)secondBoundary));
			}
		}
	}
}
