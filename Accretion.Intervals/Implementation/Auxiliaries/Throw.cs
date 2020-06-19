using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal static class Throw
    {
        public static void NotSupportedException() => throw new NotSupportedException();
        public static T NotSupportedException<T>() => throw new NotSupportedException();
    }
}
