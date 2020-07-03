using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct InvalidBoundaryValue<T, TComparer> : IEquatable<InvalidBoundaryValue<T, TComparer>> where TComparer : struct, IBoundaryValueComparer<T>
    {
        private readonly T _value;

        public InvalidBoundaryValue(T value) =>
            _value = Spec.IsInvalidBoundaryValue<T, TComparer>(value) ? value : throw new ArgumentException($"{value} is not an invalid boudnary value.");

        public T Value => DoesExist ? _value : throw new InvalidOperationException($"No forbidden boundary values exist for {typeof(T)} with {typeof(TComparer)}.");
        public bool DoesExist => Spec.HasInvalidValues<T, TComparer>();

        public override bool Equals(object obj) => obj is InvalidBoundaryValue<T, TComparer> value && Equals(value);
        public bool Equals(InvalidBoundaryValue<T, TComparer> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(InvalidBoundaryValue<T, TComparer> left, InvalidBoundaryValue<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(InvalidBoundaryValue<T, TComparer> left, InvalidBoundaryValue<T, TComparer> right) => !(left == right);
    }
}
