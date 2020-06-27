using System;

namespace Accretion.Intervals
{
    public readonly struct Interval<T> : IEquatable<Interval<T>> where T : IComparable<T> 
    {
        private readonly Interval<T, DefaultValueComparer<T>> _interval;

        internal Interval(LowerBoundary<T> lowerBoundary, UpperBoundary<T> upperBoundary) : this(new Interval<T, DefaultValueComparer<T>>(lowerBoundary, upperBoundary)) { }
        private Interval(Interval<T, DefaultValueComparer<T>> interval) => _interval = interval;

        public static Interval<T> Empty { get; }

        public bool IsEmpty => _interval.IsEmpty;

        public LowerBoundary<T> LowerBoundary => _interval.LowerBoundary;
        public UpperBoundary<T> UpperBoundary => _interval.UpperBoundary;

        public static bool TryParse(string input, TryParse<T> elementParser, out Interval<T> interval)
        {
            var result = Interval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(ReadOnlySpan<char> input, TryParseSpan<T> elementParser, out Interval<T> interval)
        {
            var result = Interval<T, DefaultValueComparer<T>>.TryParse(input, elementParser, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(string input, out Interval<T> interval)
        {
            var result = Interval<T, DefaultValueComparer<T>>.TryParse(input, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static bool TryParse(ReadOnlySpan<char> input, out Interval<T> interval)
        {
            var result = Interval<T, DefaultValueComparer<T>>.TryParse(input, out var comparerInterval);
            interval = comparerInterval;
            return result;
        }

        public static Interval<T> Parse(string input, Parse<T> elementParser) => Interval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static Interval<T> Parse(ReadOnlySpan<char> input, ParseSpan<T> elementParser) => Interval<T, DefaultValueComparer<T>>.Parse(input, elementParser);

        public static Interval<T> Parse(string input) => Interval<T, DefaultValueComparer<T>>.Parse(input);

        public static Interval<T> Parse(ReadOnlySpan<char> input) => Interval<T, DefaultValueComparer<T>>.Parse(input);

        public override bool Equals(object obj) => obj is Interval<T> interval && Equals(interval);
        public bool Equals(Interval<T> other) => _interval == other._interval;
        public override int GetHashCode() => HashCode.Combine(_interval);
        
        public override string ToString() => _interval.ToString();

        public static implicit operator Interval<T>(Interval<T, DefaultValueComparer<T>> interval) => new Interval<T>(interval);
        public static implicit operator Interval<T, DefaultValueComparer<T>>(Interval<T> interval) => interval._interval;

        public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);
        public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);
    }
}
