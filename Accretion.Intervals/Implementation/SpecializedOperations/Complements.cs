using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Intervals
{    
    public static class Complements
    {
        public static Interval<byte> AllBytes { get; } = Interval.CreateClosed(byte.MinValue, byte.MaxValue);
        public static Interval<sbyte> AllSBytes { get; } = Interval.CreateClosed(sbyte.MinValue, sbyte.MaxValue);
        public static Interval<char> AllChars { get; } = Interval.CreateClosed(char.MinValue, char.MaxValue);
        public static Interval<decimal> AllDecimals { get; } = Interval.CreateClosed(decimal.MinValue, decimal.MaxValue);
        public static Interval<double> NumberLineOfDoubles { get; } = Interval.CreateClosed(double.NegativeInfinity, double.PositiveInfinity);
        public static Interval<float> NumberLineOfSingles { get; } = Interval.CreateClosed(float.NegativeInfinity, float.PositiveInfinity);
        public static Interval<double> AllDoubles { get; } = Interval.CreateClosed(double.MinValue, double.MaxValue);
        public static Interval<float> AllSingles { get; } = Interval.CreateClosed(float.MinValue, float.MaxValue);
        public static Interval<int> AllInt32s { get; } = Interval.CreateClosed(int.MinValue, int.MaxValue);
        public static Interval<uint> AllUInt32s { get; } = Interval.CreateClosed(uint.MinValue, uint.MaxValue);
        public static Interval<long> AllInt64s { get; } = Interval.CreateClosed(long.MinValue, long.MaxValue);
        public static Interval<ulong> AllUInt64s { get; } = Interval.CreateClosed(ulong.MinValue, ulong.MaxValue);
        public static Interval<short> AllInt16s { get; } = Interval.CreateClosed(short.MinValue, short.MaxValue);
        public static Interval<ushort> AllUInt16s { get; } = Interval.CreateClosed(ushort.MinValue, ushort.MaxValue);
        public static Interval<DateTime> AllDateTimes { get; } = Interval.CreateClosed(DateTime.MinValue, DateTime.MaxValue);
        public static Interval<DateTimeOffset> AllDateTimeOffsets { get; } = Interval.CreateClosed(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="byte"/> values not present in this interval. 
        /// </summary>  
        public static CompositeInterval<byte> Complement(this CompositeInterval<byte> interval) => interval.SymmetricDifference(AllBytes);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="sbyte"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<sbyte> Complement(this CompositeInterval<sbyte> interval) => interval.SymmetricDifference(AllSBytes);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="Char"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<char> Complement(this CompositeInterval<char> interval) => interval.SymmetricDifference(AllChars);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="decimal"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<decimal> Complement(this CompositeInterval<decimal> interval) => interval.SymmetricDifference(AllDecimals);

        /// <summary>
        /// Returns a new composite interval that contains all values on the number line not present in this interval. 
        /// </summary> 
        public static CompositeInterval<double> Complement(this CompositeInterval<double> interval) => interval.SymmetricDifference(NumberLineOfDoubles);

        /// <summary>
        /// Returns a new composite interval that contains all values on the number line not present in this interval. 
        /// </summary> 
        public static CompositeInterval<float> Complement(this CompositeInterval<float> interval) => interval.SymmetricDifference(NumberLineOfSingles);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="int"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<int> Complement(this CompositeInterval<int> interval) => interval.SymmetricDifference(AllInt32s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="uint"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<uint> Complement(this CompositeInterval<uint> interval) => interval.SymmetricDifference(AllUInt32s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="long"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<long> Complement(this CompositeInterval<long> interval) => interval.SymmetricDifference(AllInt64s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="ulong"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<ulong> Complement(this CompositeInterval<ulong> interval) => interval.SymmetricDifference(AllUInt64s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="short"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<short> Complement(this CompositeInterval<short> interval) => interval.SymmetricDifference(AllInt16s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="ushort"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<ushort> Complement(this CompositeInterval<ushort> interval) => interval.SymmetricDifference(AllUInt16s);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="DateTime"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<DateTime> Complement(this CompositeInterval<DateTime> interval) => interval.SymmetricDifference(AllDateTimes);

        /// <summary>
        /// Returns a new composite interval that contains all possible <see cref="DateTimeOffset"/> values not present in this interval. 
        /// </summary> 
        public static CompositeInterval<DateTimeOffset> Complement(this CompositeInterval<DateTimeOffset> interval) => interval.SymmetricDifference(AllDateTimeOffsets);
    }
}
