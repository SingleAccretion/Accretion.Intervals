using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public readonly struct ForbiddenBoundaryValue<T> : IEquatable<ForbiddenBoundaryValue<T>>
    {
        public ForbiddenBoundaryValue(T value) => Value = value;

        public T Value { get; }
        public bool DoesExist => Facts.HasForbiddenValues<T>();

        public override bool Equals(object obj) => obj is ForbiddenBoundaryValue<T> value && Equals(value);
        public bool Equals(ForbiddenBoundaryValue<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(ForbiddenBoundaryValue<T> left, ForbiddenBoundaryValue<T> right) => left.Equals(right);
        public static bool operator !=(ForbiddenBoundaryValue<T> left, ForbiddenBoundaryValue<T> right) => !(left == right);
    }
}
