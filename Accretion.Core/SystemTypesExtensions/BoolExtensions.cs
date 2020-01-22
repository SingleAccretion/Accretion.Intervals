using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Core
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool boolean)
        {
            return boolean ? 1 : 0;
        }
    }
}
