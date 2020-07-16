using System;

namespace Accretion.Intervals
{
    internal static class Throw
    {
        public static void ArgumentNullException(string message) => throw new ArgumentNullException(message);

        public static void InvalidOperationException(string message) => throw new InvalidOperationException(message);

        public static void Exception(Exception exception) => throw exception;
        public static T Exception<T>(Exception exception) => throw exception;

        public static void ParserNotFound(Type type, string signature) => throw new InvalidOperationException($"The parser method on {type.Name} could not be found. It must have the following signature: {signature}");
    }
}
