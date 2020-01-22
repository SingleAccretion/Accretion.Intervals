using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal delegate void ElementParsingAction<E>(string str, out E element, out Exception exception);
}
