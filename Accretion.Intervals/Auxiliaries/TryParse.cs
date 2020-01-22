using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public delegate bool TryParse<T>(string value, out T result);
}
