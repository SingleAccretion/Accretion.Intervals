using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Intervals.Experimental
{
    /*
    public static class Complements
    {
        public static Interval<byte> AllBytes { get; } = new Interval<byte>(new ContinuousInterval<byte>(byte.MinValue, false, byte.MaxValue, false));
        public static Interval<sbyte> AllSBytes { get; } = new Interval<sbyte>(new ContinuousInterval<sbyte>(sbyte.MinValue, false, sbyte.MaxValue, false));
        public static Interval<char> AllChars { get; } = new Interval<char>(new ContinuousInterval<char>(char.MinValue, false, char.MaxValue, false));
        public static Interval<decimal> AllDecimals { get; } = new Interval<decimal>(new ContinuousInterval<decimal>(decimal.MinValue, false, decimal.MaxValue, false));
        public static Interval<double> NumberLineOfDoubles { get; } = new Interval<double>(new ContinuousInterval<double>(double.NegativeInfinity, false, double.PositiveInfinity, false));
        public static Interval<float> NumberLineOfSingles { get; } = new Interval<float>(new ContinuousInterval<float>(float.NegativeInfinity, false, float.PositiveInfinity, false));
        public static Interval<double> AllDoubles { get; } = new Interval<double>(new ContinuousInterval<double>(double.MinValue, false, double.MaxValue, false));
        public static Interval<float> AllSingles { get; } = new Interval<float>(new ContinuousInterval<float>(float.MinValue, false, float.MaxValue, false));
        public static Interval<int> AllInt32s { get; } = new Interval<int>(new ContinuousInterval<int>(int.MinValue, false, int.MaxValue, false));
        public static Interval<uint> AllUInt32s { get; } = new Interval<uint>(new ContinuousInterval<uint>(uint.MinValue, false, uint.MaxValue, false));
        public static Interval<long> AllInt64s { get; } = new Interval<long>(new ContinuousInterval<long>(long.MinValue, false, long.MaxValue, false));
        public static Interval<ulong> AllUInt64s { get; } = new Interval<ulong>(new ContinuousInterval<ulong>(ulong.MinValue, false, ulong.MaxValue, false));
        public static Interval<short> AllInt16s { get; } = new Interval<short>(new ContinuousInterval<short>(short.MinValue, false, short.MaxValue, false));
        public static Interval<ushort> AllUInt16s { get; } = new Interval<ushort>(new ContinuousInterval<ushort>(ushort.MinValue, false, ushort.MaxValue, false));
        public static Interval<DateTime> AllDateTimes { get; } = new Interval<DateTime>(new ContinuousInterval<DateTime>(DateTime.MinValue, false, DateTime.MaxValue, false));
        public static Interval<DateTimeOffset> AllDateTimeOffsets { get; } = new Interval<DateTimeOffset>(new ContinuousInterval<DateTimeOffset>(DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue, false));

        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="byte"/> values not present in this interval. 
        /// </summary>  
        /// <exception cref="ArgumentNullException" />
        public static Interval<byte> Complement(this Interval<byte> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllBytes);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="sbyte"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<sbyte> Complement(this Interval<sbyte> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllSBytes);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="Char"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<char> Complement(this Interval<char> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllChars);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="decimal"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<decimal> Complement(this Interval<decimal> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllDecimals);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all values on the number line not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<double> Complement(this Interval<double> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(NumberLineOfDoubles);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all values on the number line not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<float> Complement(this Interval<float> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(NumberLineOfSingles);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="int"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<int> Complement(this Interval<int> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllInt32s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="uint"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<uint> Complement(this Interval<uint> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllUInt32s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="long"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<long> Complement(this Interval<long> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllInt64s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="ulong"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<ulong> Complement(this Interval<ulong> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllUInt64s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="short"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<short> Complement(this Interval<short> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllInt16s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="ushort"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<ushort> Complement(this Interval<ushort> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllUInt16s);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="DateTime"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<DateTime> Complement(this Interval<DateTime> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllDateTimes);
        /// <summary>
        /// Returns a new <see cref="Interval"/> that contains all possible <see cref="DateTimeOffset"/> values not present in this interval. 
        /// </summary> 
        /// <exception cref="ArgumentNullException" />
        public static Interval<DateTimeOffset> Complement(this Interval<DateTimeOffset> interval) => (interval ?? throw new ArgumentNullException(nameof(interval))).SymmetricDifference(AllDateTimeOffsets);
    }
    */
}
