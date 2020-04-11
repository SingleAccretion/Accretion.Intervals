using System;
using System.Diagnostics.CodeAnalysis;

namespace Accretion.Intervals.Tests
{
    public readonly struct Period : IEquatable<Period>, IAddable<Period>
    {
        private readonly double _numberOfSeconds;

        private Period(double numberOfSeconds) => _numberOfSeconds = numberOfSeconds >= 0 ? numberOfSeconds : throw new ArgumentException(nameof(numberOfSeconds));

        public static Period FromSeconds(int numberOfSeconds) => new Period(numberOfSeconds);
        public static Period FromMinutes(int numberOfMinutes) => FromSeconds(60 * numberOfMinutes);
        public static Period FromHours(int numberOfHours) => FromMinutes(60 * numberOfHours);
        public static Period FromDays(int numberOfDays) => FromHours(24 * numberOfDays);

        public Period Add(Period addend) => new Period(_numberOfSeconds + addend._numberOfSeconds);
        public bool Equals(Period other) => _numberOfSeconds == other._numberOfSeconds;
        public override bool Equals(object obj) => obj is Period period && Equals(period);
        public override int GetHashCode() => HashCode.Combine(_numberOfSeconds);

        public static bool operator ==(Period left, Period right) => left.Equals(right);
        public static bool operator !=(Period left, Period right) => !(left == right);
        public static Period operator +(Period left, Period right) => left.Add(right);
    }
}