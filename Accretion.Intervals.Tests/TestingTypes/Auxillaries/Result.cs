using System;
using System.Collections.Generic;

namespace Accretion.Intervals.Tests
{
    public static class Result
    {
        public static Result<T> From<T>(Func<T> factory)
        {            
            try
            {
                var value = factory();
                return new Result<T>(value);
            }
            catch (Exception exception)
            {
                return new Result<T>(exception);
            }
        }
    }

    public struct Result<T> : IEquatable<Result<T>>
    {
        private readonly T _value;
        private readonly Exception _exception;

        public Result(T value) : this() => _value = value;
        public Result(Exception exception) : this() => _exception = exception;

        public Exception Exception => !HasValue ? _exception : throw new InvalidOperationException("Cannot access the exception of a successful result.");
        public T Value => HasValue ? _value : throw new InvalidOperationException("Cannot access the value of a faulted result.");
        public bool HasValue => _exception is null;

        public override bool Equals(object obj) => throw new InvalidOperationException("This method is not supported by the Result<T> type.");
        public bool Equals(Result<T> other) => Equals(_value, other._value) && _exception?.GetType() == other._exception?.GetType();

        public override int GetHashCode() => HashCode.Combine(_value, _exception);

        public static bool operator ==(Result<T> left, Result<T> right) => left.Equals(right);
        public static bool operator !=(Result<T> left, Result<T> right) => !(left == right);
    }
}
