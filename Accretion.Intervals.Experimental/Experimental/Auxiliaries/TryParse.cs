using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    public delegate bool TryParse<T>(string value, out T result);
}
