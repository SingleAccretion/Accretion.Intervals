using System;

namespace Accretion.Intervals
{
    internal static class BoundariesExceptions
    {
        public static InvalidOperationException InvalidBoundariesDoNotHaveValues { get; } = new InvalidOperationException("Invalid boundaries do not have values.");
        public static InvalidOperationException InvalidBoundariesDoNotHaveTypes { get; } = new InvalidOperationException("Invalid boundaries do not have types.");
    }
}
