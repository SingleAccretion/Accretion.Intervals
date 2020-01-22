using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class MathExtensions
    {        
        public static double RadiansToDegrees(double angleInRadians)
        {
            return 360 * angleInRadians / (2 * Math.PI);
        }                
    }
}
