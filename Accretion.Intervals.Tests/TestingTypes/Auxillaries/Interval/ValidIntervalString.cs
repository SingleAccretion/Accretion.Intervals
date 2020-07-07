using System;

namespace Accretion.Intervals.Tests
{
    public class ValidIntervalString<T, TComparer> : IEquatable<ValidIntervalString<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        public ValidIntervalString(
            string lowerPadding,
            string lowerBoundaryType,
            string lowerBoundaryTypePadding,
            string lowerBoundaryValue,
            string lowerBoundaryValuePadding,
            string separator,
            string separatorPadding,
            string upperBoundaryValue,
            string upperBoundaryValuePadding,
            string upperBoundaryType,
            string upperPadding)
        {
            LowerBoundaryType = lowerBoundaryType;
            LowerBoundaryValue = lowerBoundaryValue;
            UpperBoundaryType = upperBoundaryType;
            UpperBoundaryValue = upperBoundaryValue;
            Value = lowerPadding +
                    lowerBoundaryType +
                    lowerBoundaryTypePadding +
                    lowerBoundaryValue +
                    lowerBoundaryValuePadding +
                    separator +
                    separatorPadding +
                    upperBoundaryValue +
                    upperBoundaryValuePadding +
                    upperBoundaryType +
                    upperPadding;
        }

        public string Value { get; }
        public string LowerBoundaryType { get; }
        public string LowerBoundaryValue { get; }
        public string UpperBoundaryType { get; }
        public string UpperBoundaryValue { get; }

        public override bool Equals(object obj) => Equals(obj as ValidIntervalString<T, TComparer>);
        public bool Equals(ValidIntervalString<T, TComparer> other) => other != null && Value == other.Value && LowerBoundaryType == other.LowerBoundaryType && LowerBoundaryValue == other.LowerBoundaryValue && UpperBoundaryType == other.UpperBoundaryType && UpperBoundaryValue == other.UpperBoundaryValue;
        public override int GetHashCode() => HashCode.Combine(Value, LowerBoundaryType, LowerBoundaryValue, UpperBoundaryType, UpperBoundaryValue);
        public override string ToString() => Value;

        public static bool operator ==(ValidIntervalString<T, TComparer> left, ValidIntervalString<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(ValidIntervalString<T, TComparer> left, ValidIntervalString<T, TComparer> right) => !(left == right);
    }
}