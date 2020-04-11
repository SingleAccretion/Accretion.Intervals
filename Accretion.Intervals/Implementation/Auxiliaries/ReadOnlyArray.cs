using System;
using System.Collections;
using System.Collections.Generic;

namespace Accretion.Intervals
{
    internal readonly struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>, IReadOnlyList<T>
    {
        private readonly T[] _array;
        private readonly int _count;

        public ReadOnlyArray(T[] array)
        {
            _array = array;
            _count = _array.Length;
        }

        public ReadOnlyArray(T[] array, int count)
        {
            _array = array;
            _count = count;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1825:Avoid zero-length array allocations.", Justification = "See ImmutableArray<T>.Empty")]
        public static readonly ReadOnlyArray<T> Empty = new ReadOnlyArray<T>(new T[0]);

        public int Count => _count;

        public T this[int index] => _array[index];

        public override bool Equals(object obj) => obj is ReadOnlyArray<T> array && Equals(array);

        public bool Equals(ReadOnlyArray<T> other) => _array == other._array && _count == other._count;

        public override int GetHashCode() => HashCode.Combine(_array, _count);

        public T[] AsArrayUnchecked() => _array;

        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        public static bool operator ==(ReadOnlyArray<T> left, ReadOnlyArray<T> right) => left.Equals(right);

        public static bool operator !=(ReadOnlyArray<T> left, ReadOnlyArray<T> right) => !left.Equals(right);

        public struct Enumerator : IEnumerator<T>
        {            
            private readonly T[] _array;
            private readonly int _count;
            private int _index;

            internal Enumerator(ReadOnlyArray<T> readOnlyArray)
            {
                _array = readOnlyArray._array;
                _count = readOnlyArray.Count;
                _index = -1;
            }

            public T Current => _array[_index];
            object IEnumerator.Current => Current;

            public bool MoveNext() => ++_index < _count;
            public void Reset() => _index = -1;
            public void Dispose() { }
        }
    }
}
