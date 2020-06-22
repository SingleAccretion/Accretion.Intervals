﻿using Accretion.Intervals.StringConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal static class ParsingExceptions
    {
        public static IntervalFormatException SingletonIntervalMustEndWithSingletonEnd { get; } = new IntervalFormatException($"Singleton interval must end with '{Symbols.GetSymbol(TokenType.EndSingleton)}'.");
        public static IntervalFormatException InputMustNotHaveTrailingCharacters { get; } = new IntervalFormatException("Input must not have any extra non-whitespace characters after the interval.");
        public static IntervalFormatException IntervalMustHaveBoundaries { get; } = new IntervalFormatException("Interval must have boundaries.");
        public static IntervalFormatException InputMustStartWithValidStartCharacter { get; } = new IntervalFormatException("Input must not have any non-whitespace characters before interval.");
        public static IntervalFormatException BoundariesMustBeSeparated { get; } = new IntervalFormatException($"Boundaries must be separated by '{Symbols.GetSymbol(TokenType.Separator)}'.");
        public static IntervalFormatException IntervalMustHaveUpperBoundary { get; } = new IntervalFormatException("Interval must have an upper boundary");
        public static Exception IntervalMustEndWithEndClosedOrEndOpen { get; internal set; }
    }
}
