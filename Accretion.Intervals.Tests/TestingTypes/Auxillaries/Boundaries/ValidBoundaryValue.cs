using System;

namespace Accretion.Intervals.Tests
{
    public readonly struct ValidBoundaryValue<T, TComparer> : IEquatable<ValidBoundaryValue<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        private readonly T _value;

        public ValidBoundaryValue(T value)
        {
            if (InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(value))
            {
                throw new ArgumentException($"{value} is not a valid boudnary value of {typeof(T)}");
            }
            _value = value;
        }

        public T Value => !InvalidBoundaryValue.IsInvalidBoundaryValue<T, TComparer>(_value) ? _value : throw new InvalidOperationException($"{_value} is not a valid boundary value of {typeof(T)}");

        public override bool Equals(object obj) => obj is ValidBoundaryValue<T, TComparer> value && Equals(value);
        public bool Equals(ValidBoundaryValue<T, TComparer> other) => Value.IsEqualTo<T, TComparer>(other.Value);
        public override int GetHashCode() => HashCode.Combine(default(TComparer).GetHashCode(Value));
        public override string ToString() => Value.ToString();

        public static implicit operator T(ValidBoundaryValue<T, TComparer> validBoundaryValue) => validBoundaryValue.Value;

        public static bool operator ==(ValidBoundaryValue<T, TComparer> left, ValidBoundaryValue<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(ValidBoundaryValue<T, TComparer> left, ValidBoundaryValue<T, TComparer> right) => !(left == right);
    }
}
