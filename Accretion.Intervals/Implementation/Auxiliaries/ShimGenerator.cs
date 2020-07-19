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
            if (targetMethod.ReturnType != sourceMethod.ReturnType)
            {
                throw new ArgumentException("Return must be the same for the source and the target.");
            }

            var sourceParameters = sourceMethod.GetParameters();
            var targetParameters = targetMethod.GetParameters();

            //Fast path for cases where we do not need to create a shim
            if (!sourceParameters.Any(x => x.HasDefaultValue))
            {
                return (TTarget)Delegate.CreateDelegate(typeof(TTarget), sourceMethod);
            }

            var shim = new DynamicMethod(Guid.NewGuid().ToString(), targetMethod.ReturnType, targetMethod.GetParameters().Select(x => x.ParameterType).ToArray());
            using (var il = new GroboIL(shim))
            {
                var j = 0;
                for (int i = 0; i < sourceParameters.Length; i++)
                {
                    var sourceParameter = sourceParameters[i];
                    if (sourceParameter.HasDefaultValue)
                    {
                        EmitDefaultParameterValue(il, sourceParameter);
                    }
                    else
                    {
                        if (targetParameters[j].ParameterType == sourceParameter.ParameterType)
                        {
                            il.Ldarg(j);
                            j++;
                        }
                        else
                        {
                            throw new ArgumentException($"The type of the required source parameter {sourceParameter} does not match that of the target {targetParameters[j]}.");
                        }
                    }
                }

                if (j != targetParameters.Length)
                {
                    throw new ArgumentException("The parameters of target must be exactly the required parameters of source.");
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
                    throw new InvalidOperationException($"Could not convert default parameter value {value} of type {value.GetType()} to the parameter's type {type}.");
                }

                EmitValue(il, castedValue);
            }
        }

        private static void EmitDefaultTypeValue(GroboIL il, Type type)
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
                    il.Ldc_I4(0);
                    return;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    il.Ldc_I8(0);
                    return;
                case TypeCode.Single:
                    il.Ldc_R4(0f);
                    return;
                case TypeCode.Double:
                    il.Ldc_R8(0d);
                    return;
            }

            if (type.IsPointer || type == typeof(UIntPtr) || type == typeof(IntPtr))
            {
                il.Ldc_IntPtr(IntPtr.Zero);
                il.Conv<UIntPtr>();
            }
            else if (type.IsEnum)
            {
                EmitDefaultTypeValue(il, Enum.GetUnderlyingType(type));
            }
            else if (type.IsValueType)
            {
                var local = il.DeclareLocal(type);
                il.Ldloca(local);
                il.Initobj(type);
                il.Ldloc(local);
            }
            else
            {
                il.Ldnull();
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
                case DateTime dateTime: 
                    var local = il.DeclareLocal(typeof(DateTime));
                    il.Ldloca(local);
                    il.Ldc_I8(dateTime.Ticks);
                    il.Ldc_I4((int)dateTime.Kind);
                    il.Call(typeof(DateTime).GetConstructor(new[] { typeof(long), typeof(DateTimeKind) }));
                    il.Ldloc(local);
                    break;
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

            //Apparantely, the type code for enums returns the type code for the underlying type, so we have to dodge that bullet
            var targetTypeCode = !targetType.IsEnum ? Type.GetTypeCode(targetType) : TypeCode.Empty;

            //We choose the "strict" approach to interpreting metadata
            switch (targetTypeCode)
            {
                case TypeCode.Boolean:
                    return value switch
                    {
                        bool boolean => boolean,
                        sbyte int8 when int8 == 0 || int8 == 1 => int8 == 1,
                        byte uint8 when uint8 == 0 || uint8 == 1 => uint8 == 1,
                        short int16 when int16 == 0 || int16 == 1 => int16 == 1,
                        ushort uint16 when uint16 == 0 || uint16 == 1 => uint16 == 1,
                        int int32 when int32 == 0 || int32 == 1 => int32 == 1,
                        uint uint32 when uint32 == 0 || uint32 == 1 => uint32 == 1,
                        long int64 when int64 == 0 || int64 == 1 => int64 == 1,
                        ulong uint64 when uint64 == 0 || uint64 == 1 => uint64 == 1,
                        _ => null
                    };
                case TypeCode.SByte:
                    return value switch
                    {
                        sbyte int8 => int8,
                        byte uint8 when uint8 <= sbyte.MaxValue => (sbyte)uint8,
                        short int16 when int16 >= sbyte.MinValue && int16 <= sbyte.MaxValue => (sbyte)int16,
                        ushort uint16 when uint16 <= sbyte.MaxValue => (sbyte)uint16,
                        int int32 when int32 >= sbyte.MinValue && int32 <= sbyte.MaxValue => (sbyte)int32,
                        uint uint32 when uint32 <= sbyte.MaxValue => (sbyte)uint32,
                        long int64 when int64 >= sbyte.MinValue && int64 <= sbyte.MaxValue => (sbyte)int64,
                        ulong uint64 when uint64 <= (long)sbyte.MaxValue => (sbyte)uint64,
                        _ => null
                    };
                case TypeCode.Byte:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (byte)int8,
                        byte uint8 => uint8,
                        short int16 when int16 >= byte.MinValue && int16 <= byte.MaxValue => (byte)int16,
                        ushort uint16 when uint16 <= byte.MaxValue => (byte)uint16,
                        int int32 when int32 >= byte.MinValue && int32 <= byte.MaxValue => (byte)int32,
                        uint uint32 when uint32 <= byte.MaxValue => (byte)uint32,
                        long int64 when int64 >= byte.MinValue && int64 <= byte.MaxValue => (byte)int64,
                        ulong uint64 when uint64 <= byte.MaxValue => (byte)uint64,
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
                        int int32 when int32 >= char.MinValue && int32 <= char.MaxValue => (char)int32,
                        uint uint32 when uint32 <= char.MaxValue => (char)uint32,
                        long int64 when int64 >= char.MinValue && int64 <= char.MaxValue => (char)int64,
                        ulong uint64 when uint64 <= char.MaxValue => (char)uint64,
                        _ => null
                    };
                case TypeCode.Int16:
                    return value switch
                    {
                        sbyte int8 => (short)int8,
                        byte uint8 => (short)uint8,
                        short int16 => int16,
                        ushort uint16 when uint16 <= short.MaxValue => (short)uint16,
                        int int32 when int32 >= short.MinValue && int32 <= short.MaxValue => (short)int32,
                        uint uint32 when uint32 <= short.MaxValue => (short)uint32,
                        long int64 when int64 >= short.MinValue && int64 <= short.MaxValue => (short)int64,
                        ulong uint64 when uint64 <= (long)short.MaxValue => (short)uint64,
                        _ => null
                    };
                case TypeCode.UInt16:
                    return value switch
                    {
                        sbyte int8 when int8 >= 0 => (ushort)int8,
                        byte uint8 => (ushort)uint8,
                        short int16 when int16 >= 0 => (ushort)int16,
                        ushort uint16 => uint16,
                        int int32 when int32 >= ushort.MinValue && int32 <= ushort.MaxValue => (ushort)int32,
                        uint uint32 when uint32 <= ushort.MaxValue => (ushort)uint32,
                        long int64 when int64 >= ushort.MinValue && int64 <= ushort.MaxValue => (ushort)int64,
                        ulong uint64 when uint64 <= ushort.MaxValue => (ushort)uint64,
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
                        long int64 when int64 >= int.MinValue && int64 <= int.MaxValue => (int)int64,
                        ulong uint64 when uint64 <= int.MaxValue => (int)uint64,
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
                        long int64 when int64 >= uint.MinValue && int64 <= uint.MaxValue => (uint)int64,
                        ulong uint64 when uint64 <= uint.MaxValue => (uint)uint64,
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
                case TypeCode.Single:
                    return value switch
                    {
                        float float32 => float32,
                        double float64 => (float)float64,
                        _ => null
                    };
                case TypeCode.Double: return value as double?;
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
