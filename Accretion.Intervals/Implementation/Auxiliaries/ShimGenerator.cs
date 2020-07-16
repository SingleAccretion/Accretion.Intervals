using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using GrEmit;

namespace Accretion.Intervals
{
    internal static class ShimGenerator
    {
        public static TTarget WithDefaultParametersPassed<TTarget>(MethodInfo sourceMethod) where TTarget : Delegate
        {
            var targetMethod = typeof(TTarget).GetMethod("Invoke");

            var sourceParameters = sourceMethod.GetParameters();

            var shim = new DynamicMethod(Guid.NewGuid().ToString(), targetMethod.ReturnType, targetMethod.GetParameters().Select(x => x.ParameterType).ToArray());
            using (var il = new GroboIL(shim))
            {
                for (int i = 0, j = 0; i < sourceParameters.Length; i++)
                {
                    var parameter = sourceParameters[i];
                    if (parameter.HasDefaultValue)
                    {
                        EmitDefaultParameterValue(il, parameter);
                    }
                    else
                    {
                        il.Ldarg(j);
                        j++;
                    }
                }

                il.Call(sourceMethod);
                il.Ret();
            }

            return (TTarget)shim.CreateDelegate(typeof(TTarget));
        }

        private static void EmitDefaultParameterValue(GroboIL il, ParameterInfo parameter)
        {
            var type = parameter.ParameterType;
            var value = parameter.DefaultValue;

            //ByRefLike types cannot be boxed which means that as of July 2020 "value" cannot be a ByRefLike and not null
            //This may change in the future with some updates to the reflection stack, but we're in the clear with objects for now
            if (value is null)
            {
                EmitDefaultTypeValue(il, type);
            }
            else
            {
                //We pretend all pointers "are" of type UIntPtr so that the numeric value can be boxed
                var castedValue = As(type, value);
                if (castedValue is null)
                {
                    throw new InvalidOperationException($"Could not convert default parameter value {value} to the parameter's type {type}");
                }

                EmitValue(il, castedValue);
            }
        }

        private static void EmitDefaultTypeValue(GroboIL generator, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Boolean:
                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    generator.Ldc_I4(0);
                    return;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    generator.Ldc_I8(0);
                    return;
                case TypeCode.Single:
                    generator.Ldc_R4(0f);
                    return;
                case TypeCode.Double:
                    generator.Ldc_R8(0d);
                    return;
            }

            if (type.IsPointer || type == typeof(UIntPtr))
            {
                generator.Ldc_I4(0);
                generator.Conv<UIntPtr>();
            }
            else if (type == typeof(IntPtr))
            {
                generator.Ldc_I4(0);
                generator.Conv<IntPtr>();
            }
            else if (type.IsValueType)
            {
                var local = generator.DeclareLocal(type);
                generator.Ldloca(local);
                generator.Initobj(type);
                generator.Ldloc(local);
            }
            else
            {
                generator.Ldnull();
            }
        }

        private static void EmitValue(GroboIL il, object value)
        {
            //value.GetType() is the "real" type, only all pointers are UIntPtr and nullables are BoxedNullable
            //It can be:
            //Boolean, Char, SByte, Byte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double, Decimal, DateTime, Enum, IntPtr, UIntPtr
            //Or their nullable equivalents
            //Or string
            switch (value)
            {
                case bool boolean: il.Ldc_I4(boolean ? 1 : 0); break;
                case char character: il.Ldc_I4(character); break;
                case sbyte int8: il.Ldc_I4(int8); break;
                case byte uint8: il.Ldc_I4(uint8); break;
                case short int16: il.Ldc_I4(int16); break;
                case ushort uint16: il.Ldc_I4(uint16); break;
                case int int32: il.Ldc_I4(int32); break;
                case uint uint32: il.Ldc_I4((int)uint32); break;
                case long int64: il.Ldc_I8(int64); break;
                case ulong uint64: il.Ldc_I8((long)uint64); break;
                case float float32: il.Ldc_R4(float32); break;
                case double float64: il.Ldc_R8(float64); break;
                case decimal decimal128: il.LdDec(decimal128); break;
                case DateTime dateTime: il.Ldc_I8(Unsafe.As<DateTime, long>(ref dateTime)); break;
                case UIntPtr unint: il.Ldc_IntPtr(Unsafe.As<UIntPtr, IntPtr>(ref unint)); break;
                case IntPtr nint: il.Ldc_IntPtr(nint); break;
                case Enum enumeration:
                    var underlyingType = Enum.GetUnderlyingType(enumeration.GetType());
                    var underlyingValue = Convert.ChangeType(enumeration, underlyingType);
                    EmitValue(il, underlyingValue);
                    break;
                case BoxedNullable boxedNullable:
                    EmitValue(il, boxedNullable.UnderlyingValue);
                    il.Newobj(boxedNullable.NullableType.GetConstructor(new[] { boxedNullable.UnderlyingType }));
                    break;
                case string str: il.Ldstr(str); break;
                default: throw new ArgumentException($"Value {value} of type {value.GetType()} is not supported by the emitter.");
            }
        }

        private static object As(Type targetType, object value)
        {
            Debug.Assert(value != null, "Value in ShimGenerator.As cannot be null.");
            Debug.Assert(targetType != null, "TargetType in ShimGenerator.As cannot be null.");

            //Value can be of type:
            //Boolean, Char, SByte, Byte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double - as per ECMA 335 II.22.9
            //Or Enum
            //Or Decimal, DateTime - as per custom attributes supported by the reflection stack
            //Please note that this code cannot be complete since the value can theoretically be anything defined by a user with some CustomConstantAttribute
            //ParameterInfo.DefaultValue will return the raw value defined in metadata/attributes unless:
            //ParameterInfo.ParameterType is DataTime or Decimal and it is decorated with the relevant attributes
            //Or ParameterInfo.ParameterInfo is enum

            var targetTypeCode = Type.GetTypeCode(targetType);

            //We choose the "strict" approach to interpreting metadata
            switch (targetTypeCode)
            {
                case TypeCode.Boolean:
                    return value switch
                    {
                        bool boolean => boolean,
                        sbyte int8 => int8 > 0,
                        byte uint8 => uint8 > 0,
                        short int16 => int16 > 0,
                        ushort uint16 => uint16 > 0,
                        int int32 => int32 > 0,
                        uint uint32 => uint32 > 0,
                        long int64 => int64 > 0,
                        ulong uint64 => uint64 > 0,
                        _ => null
                    };
                case TypeCode.SByte:
                    return value switch
                    {
                        sbyte int8 => int8,
                        byte uint8 when uint8 <= sbyte.MaxValue => (sbyte)uint8,
                        _ => null
                    };
                case TypeCode.Byte:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (byte)int8,
                        byte uint8 => uint8,
                        _ => null
                    };
                case TypeCode.Char:
                    return value switch
                    {
                        char character => character,
                        sbyte int8 when int8 >= 0 => (char)int8,
                        byte uint8 => (char)uint8,
                        short int16 when int16 >= 0 => (char)int16,
                        ushort uint16 => (char)uint16,
                        _ => null
                    };
                case TypeCode.Int16:
                    return value switch
                    {
                        sbyte int8 => (short)int8,
                        byte uint8 => (short)uint8,
                        short int16 => int16,
                        ushort uint16 when uint16 <= short.MaxValue => (short)uint16,
                        _ => null
                    };
                case TypeCode.UInt16:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (ushort)int8,
                        byte uint8 => (ushort)uint8,
                        short int16 when int16 >= 0 => (ushort)int16,
                        ushort uint16 => uint16,
                        _ => null
                    };
                case TypeCode.Int32:
                    return value switch
                    {
                        sbyte int8 => (int)int8,
                        byte uint8 => (int)uint8,
                        short int16 => (int)int16,
                        ushort uint16 => (int)uint16,
                        int int32 => int32,
                        uint uint32 when uint32 <= int.MaxValue => (int)uint32,
                        _ => null
                    };
                case TypeCode.UInt32:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (uint)int8,
                        byte uint8 => (uint)uint8,
                        short int16 when int16 >= 0 => (uint)int16,
                        ushort uint16 => (uint)uint16,
                        int int32 when int32 >= 0 => (uint)int32,
                        uint uint32 => uint32,
                        _ => null
                    };
                case TypeCode.Int64:
                    return value switch
                    {
                        sbyte int8 => (long)int8,
                        byte uint8 => (long)uint8,
                        short int16 => (long)int16,
                        ushort uint16 => (long)uint16,
                        int int32 => (long)int32,
                        uint uint32 => (long)uint32,
                        long int64 => int64,
                        ulong uint64 when uint64 <= long.MaxValue => uint64,
                        _ => null
                    };
                case TypeCode.UInt64:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (ulong)int8,
                        byte uint8 => (ulong)uint8,
                        short int16 when int16 >= 0 => (ulong)int16,
                        ushort uint16 => (ulong)uint16,
                        int int32 when int32 >= 0 => (ulong)int32,
                        uint uint32 => (ulong)uint32,
                        long int64 when int64 >= 0 => (ulong)int64,
                        ulong uint64 => uint64,
                        _ => null
                    };
                case TypeCode.Single: return value as float?;
                case TypeCode.Double: return value is float float32 ? float32 : value as double?;
            }

            //Strictly speaking, if targetTypes is Enum-derived, value will always be strongly typed
            //This code supports the case of Nullable<Enum>, which is reported as the underlying integral type
            if (targetType.IsEnum && targetType != value.GetType())
            {
                var underlyingType = Enum.GetUnderlyingType(targetType);
                var underlyingValue = As(underlyingType, value);

                return underlyingValue is null ? null : Enum.ToObject(targetType, underlyingValue);
            }
            else if (targetType.IsPointer || targetType == typeof(UIntPtr))
            {
                //Native unsigned integer constants can only be represented as integers not wider than UInt32 in portable metadata
                var literal = As(typeof(uint), value) as uint?;
                return literal is null ? default(UIntPtr?) : new UIntPtr(literal.Value);
            }
            else if (targetType == typeof(IntPtr))
            {
                var literal = As(typeof(int), value) as int?;
                return literal is null ? default(IntPtr?) : new IntPtr(literal.Value);
            }
            else if (Nullable.GetUnderlyingType(targetType) != null)
            {
                var underlyingType = Nullable.GetUnderlyingType(targetType);
                var underlyingValue = As(underlyingType, value);
                //This is a workaround required to preserve the type information of T?, as it would ordinarily be boxed to T
                return underlyingValue is null ? null : new BoxedNullable(targetType, underlyingValue);
            }
            else
            {
                //This takes care of Decimal, DateTime, Enum and String
                if (targetType == value.GetType())
                {
                    return value;
                }

                return null;
            }
        }

        private class BoxedNullable
        {
            public BoxedNullable(Type type, object value)
            {
                Debug.Assert(value != null, "Value passed to the BoxedNullable constructor cannot be null.");
                Debug.Assert(type != null, "Type passed to the BoxedNullable constructor cannot be null.");
                Debug.Assert(Nullable.GetUnderlyingType(type) == value.GetType(), "Value passed to the BoxedNullable constructor must be of the underlying type of the nullable.");

                NullableType = type;
                UnderlyingType = Nullable.GetUnderlyingType(type);
                UnderlyingValue = value;
            }

            public Type NullableType { get; }
            public Type UnderlyingType { get; }
            public object UnderlyingValue { get; }
        }
    }
}
