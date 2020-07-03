using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct DoubleComparerByExponent : IBoundaryValueComparer<double>
    {
        public int Compare(double x, double y)
        {
            static double Normalize(double exp) => exp switch
            {
                double.PositiveInfinity => Math.Log10(double.MaxValue),
                double.NegativeInfinity => Math.Log10(double.MinValue),
                _ => exp
            };

            var xExp = Normalize(Math.Log10(x));
            var yExp = Normalize(Math.Log10(y));

            return (xExp, yExp) switch
            {
                (double.NaN, double.NaN) => ComparingValues.IsEqual,
                (double.NaN, _) => ComparingValues.IsLess,
                (_, double.NaN) => ComparingValues.IsGreater,

                _ => (int)xExp - (int)yExp
            };
        }

        public int GetHashCode(double value) => (int)Math.Log10(value);

        public bool IsInvalidBoundaryValue(double value) => false;
        
        public string ToString(double value, string format, IFormatProvider formatProvider) => Math.Truncate(Math.Log10(value)).ToString(format, formatProvider);
    }
}
