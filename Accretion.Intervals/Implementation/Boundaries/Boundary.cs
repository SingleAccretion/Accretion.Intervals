using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Accretion.Profiling")]
namespace Accretion.Intervals
{
    internal readonly struct Boundary<T> where T : IComparable<T>
    {
        private readonly T _value;
        private readonly bool _isClosed;

        public Boundary(T value, bool isOpen)
        {            
            _isClosed = !isOpen;
            _value = value;
        }

        public T Value { get => _value; }
        public bool IsOpen { get => !_isClosed; }
        public bool IsClosed { get => _isClosed; }
    }
}
