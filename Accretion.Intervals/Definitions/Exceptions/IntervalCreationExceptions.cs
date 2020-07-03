using System;

namespace Accretion.Intervals
{
    internal static class IntervalCreationExceptions
    {
        public static ArgumentNullException BoundariesCannotBeNull { get; } = new ArgumentNullException("Boundaries cannot be null.");
        public static ArgumentException BoundariesCannotBeNaN { get; } = new ArgumentException("Boundaries cannot be NaN.");
        public static ArgumentException DateTimeBoundariesMustBeUtc { get; } = new ArgumentException("DateTime boundaries must have the Kind of Utc.");
        public static ArgumentException BoundariesMustBeValid { get; } = new ArgumentException("Boundaires must be valid.");
        public static ArgumentException BoundariesMustProduceNonEmptyInterval { get; } = new ArgumentException("Boundaires must produce a non-empty interval.");
    }
}
