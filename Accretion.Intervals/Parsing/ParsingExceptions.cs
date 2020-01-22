using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal static class ParsingExceptions
    {
        public static FormatException StringTooSmall { get; } = new FormatException("The input string cannot be less than 3 characters long (except for empty intervals)");
        public static FormatException InvalidFirstCharacter { get; } = new FormatException($"The input string is of incorrect format. The first character must be {Interval.LeftOpenBoundarySymbol}, {Interval.LeftClosedBoundarySymbol} or {Interval.LeftSingleElementSetBrace}");
        public static FormatException InvalidLastCharacter { get; } = new FormatException($"The input string is of incorrect format. The last character must be {Interval.RightOpenBoundarySymbol}, {Interval.RightClosedBoundarySymbol} or {Interval.RightSingleElementSetBrace}");
        public static FormatException BracesUsedIncorrectly { get; } = new FormatException($"{Interval.LeftSingleElementSetBrace}0{Interval.RightSingleElementSetBrace} format can only be used for one element intervals");
        public static FormatException NoSeparator { get; } = new FormatException($"The input string must have one '{Interval.SeparatorSymbol}' if it is not a one element interval");

        public static FormatException InvalidLowerBoundary(Exception innerException)
        {
            return new FormatException("Could not parse lower boundary value", innerException);
        }

        public static FormatException InvalidUpperBoundary(Exception innerException)
        {
            return new FormatException("Could not parse upper boundary value", innerException);
        }

        public static FormatException InvalidSingleElement(Exception innerException)
        {
            return new FormatException("Could not parse value of one element interval", innerException);
        }

        public static FormatException CannotParseOneOfContinuousIntervals(FormatException exception)
        {
            return new FormatException("One of the continuous intervals in the input string could not be parsed", exception);
        }
    }
}
