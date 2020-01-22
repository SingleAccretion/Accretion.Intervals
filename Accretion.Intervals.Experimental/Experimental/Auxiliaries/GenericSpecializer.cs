using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    internal class GenericSpecializer<T> where T : IComparable<T>
    {
        public static bool TypeIsDiscrete { get; } = typeof(T) == typeof(byte) || 
                                                     typeof(T) == typeof(sbyte) ||
                                                     typeof(T) == typeof(ushort) ||
                                                     typeof(T) == typeof(char) ||
                                                     typeof(T) == typeof(short) ||
                                                     typeof(T) == typeof(uint) ||
                                                     typeof(T) == typeof(int) ||
                                                     typeof(T) == typeof(ulong) ||
                                                     typeof(T) == typeof(long) ||
                                                     typeof(T).GetInterfaces().Contains(typeof(IDiscreteValue<T>));

        public static bool TypeImplementsIDiscrete { get; } = typeof(T).GetInterfaces().Contains(typeof(IDiscreteValue<T>));
        public static bool TypeInstanceCanBeNull { get; } = !typeof(T).IsValueType || (Nullable.GetUnderlyingType(typeof(T)) != null);

        private GenericSpecializer() { }
    }
}
