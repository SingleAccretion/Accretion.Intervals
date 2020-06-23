using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    //Throw helpers
    internal static class Throw
    {
        public static void NotSupportedException() => throw new NotSupportedException();
        public static T NotSupportedException<T>() => throw new NotSupportedException();

        public static void ArgumentNullException(string message) => throw new ArgumentNullException(message);

        public static void InvalidOperationException(string message) => throw new InvalidOperationException(message);

        public static void Exception(Exception exception) => throw exception;
        public static T Exception<T>(Exception exception) => throw exception;
    }
}
