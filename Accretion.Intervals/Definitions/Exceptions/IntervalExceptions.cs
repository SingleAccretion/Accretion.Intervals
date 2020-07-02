using System;

namespace Accretion.Intervals
{
    internal static class IntervalExceptions
    {
        public static InvalidOperationException EmptyIntervalsDoNotHaveBoundaries { get; } = new InvalidOperationException("Empty interval does not have boundaries.");
    }
}
