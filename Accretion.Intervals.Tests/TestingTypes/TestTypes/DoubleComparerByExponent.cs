using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct DoubleComparerByExponent : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            var xExp = Math.Log10(x);
            var yExp = Math.Log10(y);

            return (xExp, yExp) switch
            {
                (double.PositiveInfinity, double.NegativeInfinity) => ComparingValues.IsGreater,
                (double.PositiveInfinity, double.PositiveInfinity) => ComparingValues.IsEqual,
                (double.PositiveInfinity, double.NaN) => ComparingValues.IsGreater,
                (double.PositiveInfinity, _) => ComparingValues.IsGreater,

                (double.NegativeInfinity, double.NegativeInfinity) => ComparingValues.IsEqual,
                (double.NegativeInfinity, double.PositiveInfinity) => ComparingValues.IsLess,
                (double.NegativeInfinity, double.NaN) => ComparingValues.IsGreater,
                (double.NegativeInfinity, _) => ComparingValues.IsLess,

                (double.NaN, double.PositiveInfinity) => ComparingValues.IsLess,
                (double.NaN, double.NegativeInfinity) => ComparingValues.IsLess,
                (double.NaN, double.NaN) => ComparingValues.IsEqual,
                (double.NaN, _) => ComparingValues.IsLess,

                (_, _) => (int)(xExp - yExp)
            };
        }
    }
}
