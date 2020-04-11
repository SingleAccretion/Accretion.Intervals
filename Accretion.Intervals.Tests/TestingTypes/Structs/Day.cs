using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public readonly struct Day : IEquatable<Day>, IComparable<Day>, IDiscreteValue<Day>, ISubtractable<Day, Period>, ISubtractable<Day, TimeSpan>
    {
        public static readonly Day Monday = new Day(0);
        public static readonly Day Tuesday = new Day(1);
        public static readonly Day Wednesday = new Day(2);
        public static readonly Day Thursday = new Day(3);
        public static readonly Day Friday = new Day(4);
        public static readonly Day Saturday = new Day(5);
        public static readonly Day Sunday = new Day(6);

        private readonly int _number;

        private Day(int number)
        {
            if (number < 0 || number > 6)
            {
                throw new ArgumentException($"{nameof(number)} is not valid: {number}");
            }

            _number = number;
        }

        public bool IsIncrementable => this < Sunday;
        public bool IsDecrementable => this > Monday;

        public static Day Parse(string s) => s switch
        {
            nameof(Monday) => Monday,
            nameof(Tuesday) => Tuesday,
            nameof(Wednesday) => Wednesday,
            nameof(Thursday) => Thursday,
            nameof(Friday) => Friday,
            nameof(Saturday) => Saturday,
            nameof(Sunday) => Sunday,
            _ => throw new ArgumentException($"The input string {s} could not be converted to an instance of Day")
        };

        public int CompareTo(Day other) => _number.CompareTo(other._number);

        public Day Increment() => new Day(_number + 1);
        public Day Decrement() => new Day(_number - 1);

        public TimeSpan Subtract(Day subtrahend) => TimeSpan.FromDays(_number - subtrahend._number + 1);
        Period ISubtractable<Day, Period>.Subtract(Day subtrahend) => Period.FromDays(_number - subtrahend._number + 1);

        public bool Equals(Day other) => _number == other._number;
        public override bool Equals(object obj) => obj is Day day && Equals(day);
        public override int GetHashCode() => HashCode.Combine(_number);

        public override string ToString() => ((DayOfWeek)(_number % 6 + 1)).ToString();

        public static bool operator ==(Day left, Day right) => left.Equals(right);
        public static bool operator !=(Day left, Day right) => !(left == right);
        public static bool operator <(Day left, Day right) => left.CompareTo(right) < 0;
        public static bool operator <=(Day left, Day right) => left.CompareTo(right) <= 0;
        public static bool operator >(Day left, Day right) => left.CompareTo(right) > 0;
        public static bool operator >=(Day left, Day right) => left.CompareTo(right) >= 0;
    }
}
