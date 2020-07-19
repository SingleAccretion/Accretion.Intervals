using System;

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

        public override bool Equals(object obj) => throw new NotSupportedException("Equals method is not supported by the Result<T> type.");
        
        public bool Equals(Result<T> other) => Equals(_value, other._value) && ExcecptionsAreEquals(_exception, other._exception);

        public bool Equals<TComparer>(Result<T> other) where TComparer : struct, IBoundaryValueComparer<T> =>
            _value.IsEqualTo<T, TComparer>(other._value) && ExcecptionsAreEquals(_exception, other._exception);

        public override int GetHashCode() => throw new NotSupportedException("GetHashCode method is not supported by the Result<T> type.");

        public override string ToString() => HasValue ? Value.ToString() : Exception.ToString();

        private static bool ExcecptionsAreEquals(Exception left, Exception right) => left?.GetType() == right?.GetType();
    }
}
