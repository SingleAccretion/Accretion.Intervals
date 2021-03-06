﻿using System;
using System.Globalization;
using Accretion.Intervals.StringConversion;

namespace Accretion.Intervals
{
    public readonly struct LowerBoundary<T, TComparer> : IEquatable<LowerBoundary<T, TComparer>>, IBoundary<T> where TComparer : struct, IBoundaryValueComparer<T>
    {
        private readonly BoundaryType _type;
        private readonly T _value;

        internal LowerBoundary(T value, BoundaryType type)
        {
            _type = type;
            _value = value;
        }

        public BoundaryType Type => IsValid ? _type : Throw.Exception<BoundaryType>(BoundariesExceptions.InvalidBoundariesDoNotHaveValues);
        public T Value => IsValid ? _value : Throw.Exception<T>(BoundariesExceptions.InvalidBoundariesDoNotHaveTypes);
        public bool IsValid => !Checker.IsInvalidBoundaryValue<T, TComparer>(_value);

        internal bool IsClosed => Type == BoundaryType.Closed;
        internal bool IsOpen => Type == BoundaryType.Open;


        public bool Equals(LowerBoundary<T, TComparer> other) => IsValid && other.IsValid ? Value.IsEqualTo<T, TComparer>(other.Value) && Type == other.Type : IsValid == other.IsValid;
        public override bool Equals(object obj) => obj is LowerBoundary<T, TComparer> boundary && Equals(boundary);
        public override int GetHashCode() => IsValid ? HashCode.Combine(default(TComparer).GetHashCode(Value), Type) : 0;

        public override string ToString() => ToString(StringSerializer.GeneralFormat, CultureInfo.InvariantCulture);
        public string ToString(string format, IFormatProvider formatProvider) => StringSerializer.Serialize(this, format, formatProvider);

        public static bool operator ==(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) => left.Equals(right);
        public static bool operator !=(LowerBoundary<T, TComparer> left, LowerBoundary<T, TComparer> right) => !left.Equals(right);        
    }
}
