﻿using System;
using System.Collections.Generic;
using Accretion.Intervals.StringConversion;

namespace Accretion.Intervals
{
    public readonly struct Interval<T, TComparer> : IEquatable<Interval<T, TComparer>> where TComparer : struct, IComparer<T>
    {
        private readonly LowerBoundary<T, TComparer> _lowerBoundary;
        private readonly UpperBoundary<T, TComparer> _upperBoundary;

        internal Interval(LowerBoundary<T, TComparer> lowerBoundary, UpperBoundary<T, TComparer> upperBoundary)
        {
            _lowerBoundary = lowerBoundary;
            _upperBoundary = upperBoundary;
        }

        public static Interval<T, TComparer> Empty { get; }

        public bool IsEmpty => Checker.IsDefault(this);

        public LowerBoundary<T, TComparer> LowerBoundary => !IsEmpty ? _lowerBoundary : Throw.Exception<LowerBoundary<T, TComparer>>(IntervalExceptions.EmptyIntervalsDoNotHaveBoundaries);
        public UpperBoundary<T, TComparer> UpperBoundary => !IsEmpty ? _upperBoundary : Throw.Exception<UpperBoundary<T, TComparer>>(IntervalExceptions.EmptyIntervalsDoNotHaveBoundaries);

        public static bool TryParse(string input, TryParse<T> elementParser, out Interval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.TryParseInterval(input, elementParser, out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out Interval<T, TComparer> interval)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.TryParseInterval(input, elementParser, out interval);
        }

        public static bool TryParse(string input, out Interval<T, TComparer> interval)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }

            return Parser.TryParseInterval(input, ElementParsers.GetTryElementParser<T>(), out interval);
        }

        public static bool TryParse(ReadOnlySpan<char> input, out Interval<T, TComparer> interval) => Parser.TryParseInterval(input, ElementParsers.GetTrySpanElementParser<T>(), out interval);

        public static Interval<T, TComparer> Parse(string input, Parse<T> elementParser)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseInterval<T, TComparer>(input, elementParser);
        }

        public static Interval<T, TComparer> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser)
        {
            if (elementParser is null)
            {
                Throw.ArgumentNullException(nameof(elementParser));
            }

            return Parser.ParseInterval<T, TComparer>(input, elementParser);
        }

        public static Interval<T, TComparer> Parse(string input)
        {
            if (input is null)
            {
                Throw.ArgumentNullException(nameof(input));
            }

            return Parser.ParseInterval<T, TComparer>(input, ElementParsers.GetElementParser<T>());
        }

        public static Interval<T, TComparer> Parse(ReadOnlySpan<char> input) => Parser.ParseInterval<T, TComparer>(input, ElementParsers.GetSpanElementParser<T>());

        public bool Contains(T value)
        {
            if (IsEmpty)
            {
                return false;
            }

            return (value.IsGreaterThan<T, TComparer>(LowerBoundary.Value) && value.IsLessThan<T, TComparer>(UpperBoundary.Value)) ||
                   (value.IsEqualTo<T, TComparer>(LowerBoundary.Value) && LowerBoundary.IsClosed) ||
                   (value.IsEqualTo<T, TComparer>(UpperBoundary.Value) && UpperBoundary.IsClosed);
        }

        public override bool Equals(object obj) => obj is Interval<T, TComparer> interval && Equals(interval);
        public bool Equals(Interval<T, TComparer> other) => _lowerBoundary == other._lowerBoundary && _upperBoundary == other._upperBoundary;
        public override int GetHashCode() => HashCode.Combine(_lowerBoundary, _upperBoundary);

        public override string ToString() => StringSerializer.Serialize(this);

        public static bool operator ==(Interval<T, TComparer> left, Interval<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(Interval<T, TComparer> left, Interval<T, TComparer> right) => !(left == right);
    }
}