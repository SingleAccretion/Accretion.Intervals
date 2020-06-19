using System;
using System.Linq;
using System.Reflection;

namespace Accretion.Intervals
{
    internal class GenericSpecializer<T> 
    {
        private static T _zeroValueOfThisType;
        private static bool _zeroValueOfThisTypeIsInitialised;

        public static bool TypeIsDiscrete { get; } = typeof(T) == typeof(byte) || 
                                                     typeof(T) == typeof(sbyte) ||
                                                     typeof(T) == typeof(ushort) ||
                                                     typeof(T) == typeof(char) ||
                                                     typeof(T) == typeof(short) ||
                                                     typeof(T) == typeof(uint) ||
                                                     typeof(T) == typeof(int) ||
                                                     typeof(T) == typeof(ulong) ||
                                                     typeof(T) == typeof(long) ||
                                                     typeof(IDiscreteValue<T>).IsAssignableFrom(typeof(T));
        
        public static bool TypeIsAddable { get; } = typeof(T) == typeof(sbyte) || 
                                                    typeof(T) == typeof(byte) ||
                                                    typeof(T) == typeof(short) ||
                                                    typeof(T) == typeof(ushort) ||
                                                    typeof(T) == typeof(int) ||
                                                    typeof(T) == typeof(uint) ||
                                                    typeof(T) == typeof(long) ||
                                                    typeof(T) == typeof(ulong) ||
                                                    typeof(T) == typeof(float) ||
                                                    typeof(T) == typeof(double) ||
                                                    typeof(T) == typeof(decimal) ||
                                                    typeof(T) == typeof(TimeSpan) ||
                                                    typeof(IAddable<T>).IsAssignableFrom(typeof(T));

        public static bool TypeImplementsIDiscrete { get; } = typeof(IDiscreteValue<T>).IsAssignableFrom(typeof(T));
        public static bool TypeImplementsIAddable { get; } = typeof(IAddable<T>).IsAssignableFrom(typeof(T));
        public static bool TypeInstanceCanBeNull { get; } = !typeof(T).IsValueType || (Nullable.GetUnderlyingType(typeof(T)) != null);        
        public static bool DefaultTypeValueCannotBeIncremented { get; } = !DefaultTypeValueCanBeIncremented();
        public static bool DefaultTypeValueCannotBeDecremented { get; } = !DefaultTypeValueCanBeDecremented();
        public static T ZeroValueOfThisType 
        {
            get
            {
                if (!TypeIsAddable)
                {
                    throw new MemberAccessException($"Non-addable types do not have access to {nameof(ZeroValueOfThisType)}");
                }
                if (!_zeroValueOfThisTypeIsInitialised)
                {
                    _zeroValueOfThisType = DiscoverZeroValue();
                    _zeroValueOfThisTypeIsInitialised = true;
                }

                return _zeroValueOfThisType;
            } 
        }

        private GenericSpecializer() { }

        private static bool DefaultTypeValueCanBeIncremented()
        {
            return typeof(T) == typeof(byte) ||
                   typeof(T) == typeof(sbyte) ||
                   typeof(T) == typeof(short) ||
                   typeof(T) == typeof(ushort) ||
                   typeof(T) == typeof(char) ||
                   typeof(T) == typeof(int) ||
                   typeof(T) == typeof(uint) ||
                   typeof(T) == typeof(long) ||
                   typeof(T) == typeof(ulong) ||
                  (TypeImplementsIDiscrete && !Checker.IsNull(default(T)) && ((IDiscreteValue<T>)default(T)).IsIncrementable);
        }

        private static bool DefaultTypeValueCanBeDecremented()
        {
            return typeof(T) == typeof(sbyte) ||
                   typeof(T) == typeof(short) ||
                   typeof(T) == typeof(int) ||
                   typeof(T) == typeof(long) ||
                  (TypeImplementsIDiscrete && !Checker.IsNull(default(T)) && ((IDiscreteValue<T>)default(T)).IsDecrementable);
        }

        private static T DiscoverZeroValue()
        {
            var type = typeof(T);
            T value = default;

            var staticProperties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var staticFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            
            var zeroProperty = staticProperties.FirstOrDefault(x => Attribute.IsDefined(x, typeof(ZeroValueAttribute)));
            var zeroField = staticFields.FirstOrDefault(x => Attribute.IsDefined(x, typeof(ZeroValueAttribute)));
            
            if (zeroProperty != null)
            {
                value = (T)zeroProperty.GetValue(null);
            }
            else if (zeroField != null)
            {
                value = (T)zeroField.GetValue(null);
            }
            else if (Nullable.GetUnderlyingType(type) != null)
            {
                value = (T)Activator.CreateInstance(Nullable.GetUnderlyingType(type));
            }
            else if (Checker.IsNull(value))
            {
                throw new MissingMemberException($"There is no suitable zero value defined for {type.FullName}. Try creating a static field or property with the desired non-null zero value and applying ZeroValueAttribute to it.");
            }

            return value;
        }
    }

    internal class GenericSpecializer<T, R> 
    {
        public static bool TypeImplementsISubtractable { get; } = typeof(ISubtractable<T, R>).IsAssignableFrom(typeof(T));

        private GenericSpecializer() { }
    }
}
