using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public class CheckerTests
    {
        public static IEnumerable<object[]> IsDefaultTestCases { get; } = MakeArbitraryData.Of(new List<(dynamic, bool)>()
        {
            (new object(), false),

            (new byte(), true),
            (new sbyte(), true),
            (new ushort(), true),
            (new char(), true),
            (new short(), true),
            (new uint(), true),
            (new int(), true),
            (new ulong(), true),
            (new long(), true),
            (new float(), true),
            (new double(), true),
            (new decimal(), true),
            (new DateTime(), true),

            (new StructOfSize1(), true),
            (new StructOfSize1(0b1), false),

            (new StructOfSize3(), true),
            (new StructOfSize3(0b_001), false),
            (new StructOfSize3(0b_010), false),
            (new StructOfSize3(0b_100), false),

            (new StructOfSize5(), true),
            (new StructOfSize5(0b_0000_1), false),
            (new StructOfSize5(0b_0001_0), false),
            (new StructOfSize5(0b_0010_0), false),
            (new StructOfSize5(0b_0100_0), false),
            (new StructOfSize5(0b_1000_0), false),

            (new StructOfSize6(), true),
            (new StructOfSize6(0b_0000_01), false),
            (new StructOfSize6(0b_0000_10), false),
            (new StructOfSize6(0b_0001_00), false),
            (new StructOfSize6(0b_0010_00), false),
            (new StructOfSize6(0b_0100_00), false),
            (new StructOfSize6(0b_1000_00), false),

            (new StructOfSize7(), true),
            (new StructOfSize7(0b_0000_001), false),
            (new StructOfSize7(0b_0000_010), false),
            (new StructOfSize7(0b_0000_100), false),
            (new StructOfSize7(0b_0001_000), false),
            (new StructOfSize7(0b_0010_000), false),
            (new StructOfSize7(0b_0100_000), false),
            (new StructOfSize7(0b_1000_000), false),

            (new StructOfSize9(), true),
            (new StructOfSize9(0b_0000_0000_1), false),
            (new StructOfSize9(0b_0000_0001_0), false),
            (new StructOfSize9(0b_0000_0010_0), false),
            (new StructOfSize9(0b_0000_0100_0), false),
            (new StructOfSize9(0b_0000_1000_0), false),
            (new StructOfSize9(0b_0001_0000_0), false),
            (new StructOfSize9(0b_0010_0000_0), false),
            (new StructOfSize9(0b_0100_0000_0), false),
            (new StructOfSize9(0b_1000_0000_0), false),

            (new StructOfSize10(), true),
            (new StructOfSize10(0b_0000_0000_10), false),
            (new StructOfSize10(0b_0000_0001_00), false),
            (new StructOfSize10(0b_0000_0010_00), false),
            (new StructOfSize10(0b_0000_0100_00), false),
            (new StructOfSize10(0b_0000_1000_00), false),
            (new StructOfSize10(0b_0001_0000_00), false),
            (new StructOfSize10(0b_0010_0000_00), false),
            (new StructOfSize10(0b_0100_0000_00), false),
            (new StructOfSize10(0b_1000_0000_00), false),
			
            (new StructOfSize11(), true),
            (new StructOfSize11(0b_0000_0000_001), false),
            (new StructOfSize11(0b_0000_0000_010), false),
            (new StructOfSize11(0b_0000_0000_100), false),
            (new StructOfSize11(0b_0000_0001_000), false),
            (new StructOfSize11(0b_0000_0010_000), false),
            (new StructOfSize11(0b_0000_0100_000), false),
            (new StructOfSize11(0b_0000_1000_000), false),
            (new StructOfSize11(0b_0001_0000_000), false),
            (new StructOfSize11(0b_0010_0000_000), false),
            (new StructOfSize11(0b_0100_0000_000), false),
            (new StructOfSize11(0b_1000_0000_000), false),

			(new StructOfSize16(), true),
            (new StructOfSize16(0b_0000_0000_0000_0001), false),
            (new StructOfSize16(0b_0000_0000_0000_0010), false),
            (new StructOfSize16(0b_0000_0000_0000_0100), false),
            (new StructOfSize16(0b_0000_0000_0000_1000), false),
            (new StructOfSize16(0b_0000_0000_0001_0000), false),
            (new StructOfSize16(0b_0000_0000_0010_0000), false),
            (new StructOfSize16(0b_0000_0000_0100_0000), false),
            (new StructOfSize16(0b_0000_0000_1000_0000), false),
            (new StructOfSize16(0b_0000_0001_0000_0000), false),
            (new StructOfSize16(0b_0000_0010_0000_0000), false),
            (new StructOfSize16(0b_0000_0100_0000_0000), false),
            (new StructOfSize16(0b_0000_1000_0000_0000), false),
            (new StructOfSize16(0b_0001_0000_0000_0000), false),
            (new StructOfSize16(0b_0010_0000_0000_0000), false),
            (new StructOfSize16(0b_0100_0000_0000_0000), false),
            (new StructOfSize16(0b_1000_0000_0000_0000), false),

			(new StructOfSize17(), true),
            (new StructOfSize17(0b_0000_0000_0000_0000_1), false),
            (new StructOfSize17(0b_0000_0000_0000_0001_0), false),
            (new StructOfSize17(0b_0000_0000_0000_0010_0), false),
            (new StructOfSize17(0b_0000_0000_0000_0100_0), false),
            (new StructOfSize17(0b_0000_0000_0000_1000_0), false),
            (new StructOfSize17(0b_0000_0000_0001_0000_0), false),
            (new StructOfSize17(0b_0000_0000_0010_0000_0), false),
            (new StructOfSize17(0b_0000_0000_0100_0000_0), false),
            (new StructOfSize17(0b_0000_0000_1000_0000_0), false),
            (new StructOfSize17(0b_0000_0001_0000_0000_0), false),
            (new StructOfSize17(0b_0000_0010_0000_0000_0), false),
            (new StructOfSize17(0b_0000_0100_0000_0000_0), false),
            (new StructOfSize17(0b_0000_1000_0000_0000_0), false),
            (new StructOfSize17(0b_0001_0000_0000_0000_0), false),
            (new StructOfSize17(0b_0010_0000_0000_0000_0), false),
            (new StructOfSize17(0b_0100_0000_0000_0000_0), false),
            (new StructOfSize17(0b_1000_0000_0000_0000_0), false),

			(new StructOfSize20(), true),
            (new StructOfSize20(0b_0000_0000_0000_0000_0001), false),
            (new StructOfSize20(0b_0000_0000_0000_0000_0010), false),
            (new StructOfSize20(0b_0000_0000_0000_0000_0100), false),
            (new StructOfSize20(0b_0000_0000_0000_0000_1000), false),
            (new StructOfSize20(0b_0000_0000_0000_0001_0000), false),
            (new StructOfSize20(0b_0000_0000_0000_0010_0000), false),
            (new StructOfSize20(0b_0000_0000_0000_0100_0000), false),
            (new StructOfSize20(0b_0000_0000_0000_1000_0000), false),
            (new StructOfSize20(0b_0000_0000_0001_0000_0000), false),
            (new StructOfSize20(0b_0000_0000_0010_0000_0000), false),
            (new StructOfSize20(0b_0000_0000_0100_0000_0000), false),
            (new StructOfSize20(0b_0000_0000_1000_0000_0000), false),
            (new StructOfSize20(0b_0000_0001_0000_0000_0000), false),
            (new StructOfSize20(0b_0000_0010_0000_0000_0000), false),
            (new StructOfSize20(0b_0000_0100_0000_0000_0000), false),
            (new StructOfSize20(0b_0000_1000_0000_0000_0000), false),
            (new StructOfSize20(0b_0001_0000_0000_0000_0000), false),
            (new StructOfSize20(0b_0010_0000_0000_0000_0000), false),
            (new StructOfSize20(0b_0100_0000_0000_0000_0000), false),
            (new StructOfSize20(0b_1000_0000_0000_0000_0000), false),

			(new StructOfSize26(), true),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_0000_01), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_0000_10), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_0001_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_0010_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_0100_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0000_1000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0001_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0010_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_0100_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0000_1000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0001_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0010_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_0100_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0000_1000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0001_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0010_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_0100_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0000_1000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0001_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0010_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_0100_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0000_1000_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0001_0000_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0010_0000_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_0100_0000_0000_0000_0000_0000_00), false),
            (new StructOfSize26(0b_1000_0000_0000_0000_0000_0000_00), false),

			(new StructOfSize32(), true),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0000_0001), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0000_0010), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0000_0100), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0000_1000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0001_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0010_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_0100_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0000_1000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0001_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0010_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_0100_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0000_1000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0001_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0010_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_0100_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0000_1000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0001_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0010_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_0100_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0000_1000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0001_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0010_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_0100_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0000_1000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0001_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0010_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_0100_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0000_1000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0001_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0010_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_0100_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize32(0b_1000_0000_0000_0000_0000_0000_0000_0000), false),

			(new StructOfSize33(), true),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0000_0000_1), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0000_0001_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0000_0010_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0000_0100_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0000_1000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0001_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0010_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_0100_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0000_1000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0001_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0010_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_0100_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0000_1000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0001_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0010_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_0100_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0000_1000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0001_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0010_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_0100_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0000_1000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0001_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0010_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_0100_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0000_1000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0001_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0010_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_0100_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0000_1000_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0001_0000_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0010_0000_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_0100_0000_0000_0000_0000_0000_0000_0000_0), false),
            (new StructOfSize33(0b_1000_0000_0000_0000_0000_0000_0000_0000_0), false),

			(new StructOfSize40(), true),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize40(0b_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),

			(new StructOfSize48(), true),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize48(0b_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),

			(new StructOfSize64(), true),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
            (new StructOfSize64(0b_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000), false),
        });

        [Fact]
        public void NullShouldBeTheDefaultForReferenceTypes() => Assert.True(Checker.IsDefault<object>(null));

        [Fact]
        public void NullShouldBeTheDefaultForNullablesTypes() => Assert.True(Checker.IsDefault<int?>(default));

        [Theory]
        [MemberData(nameof(IsDefaultTestCases))]
        public void TestIsDefault(dynamic value, bool expectedResult) => Assert.Equal(expectedResult, Checker.IsDefault(value));

        private unsafe struct StructOfSize1
        {
            public fixed byte Buffer[1];

            public StructOfSize1(ulong mask)
            {
                for (int i = 1 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 1).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize3
        {
            public fixed byte Buffer[3];

            public StructOfSize3(ulong mask)
            {
                for (int i = 3 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 3).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize5
        {
            public fixed byte Buffer[5];

            public StructOfSize5(ulong mask)
            {
                for (int i = 5 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 5).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize6
        {
            public fixed byte Buffer[6];

            public StructOfSize6(ulong mask)
            {
                for (int i = 6 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 6).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize7
        {
            public fixed byte Buffer[7];

            public StructOfSize7(ulong mask)
            {
                for (int i = 7 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 7).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize9
        {
            public fixed byte Buffer[9];

            public StructOfSize9(ulong mask)
            {
                for (int i = 9 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 9).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize10
        {
            public fixed byte Buffer[10];

            public StructOfSize10(ulong mask)
            {
                for (int i = 10 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 10).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize11
        {
            public fixed byte Buffer[11];

            public StructOfSize11(ulong mask)
            {
                for (int i = 11 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 11).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize16
        {
            public fixed byte Buffer[16];

            public StructOfSize16(ulong mask)
            {
                for (int i = 16 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 16).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize17
        {
            public fixed byte Buffer[17];

            public StructOfSize17(ulong mask)
            {
                for (int i = 17 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 17).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize20
        {
            public fixed byte Buffer[20];

            public StructOfSize20(ulong mask)
            {
                for (int i = 20 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 20).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize26
        {
            public fixed byte Buffer[26];

            public StructOfSize26(ulong mask)
            {
                for (int i = 26 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 26).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize32
        {
            public fixed byte Buffer[32];

            public StructOfSize32(ulong mask)
            {
                for (int i = 32 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 32).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize33
        {
            public fixed byte Buffer[33];

            public StructOfSize33(ulong mask)
            {
                for (int i = 33 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 33).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize40
        {
            public fixed byte Buffer[40];

            public StructOfSize40(ulong mask)
            {
                for (int i = 40 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 40).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize48
        {
            public fixed byte Buffer[48];

            public StructOfSize48(ulong mask)
            {
                for (int i = 48 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 48).ToArray());
                }
            }
        }

        private unsafe struct StructOfSize64
        {
            public fixed byte Buffer[64];

            public StructOfSize64(ulong mask)
            {
                for (int i = 64 - 1; i >= 0; i--, mask >>= 1)
                {
                    Buffer[i] = (byte)(mask % 2);
                }
            }

            public override string ToString()
            {
                fixed (byte* pointer = Buffer)
                {
                    return string.Join("", new Span<byte>(pointer, 64).ToArray());
                }
            }
        }
    }
}
