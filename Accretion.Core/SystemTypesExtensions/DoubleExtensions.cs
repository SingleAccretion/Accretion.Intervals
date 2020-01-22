using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class DoubleExtensions
    {
        public static (double Mantissa, int Exponent) GetMantissaAndExponent(this double self)
        {            
            switch (self)
            {
                case double.NaN:
                    return (double.NaN, 0);

                case double.PositiveInfinity:
                    return (double.PositiveInfinity, 0);

                case double.NegativeInfinity:
                    return (double.NegativeInfinity, 0);
            }

            double mantissa = Math.Abs(self);
            int exponent = 0;
            int sign = Math.Sign(self);

            if (mantissa == 0)
            {
                return (mantissa, 0);
            }

            if (!(mantissa >= 1 && mantissa < 10))
            {
                while (mantissa < 1)
                {
                    mantissa *= 10;
                    exponent -= 1;
                }

                while (mantissa >= 10)
                {
                    mantissa /= 10;
                    exponent += 1;
                }
            }            

            return (sign * mantissa, exponent);
        }        
    }
}
