.assembly extern System.Runtime 
{
	.publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A)
	.ver 4:2:2:0
}
.assembly ShimSources 
{
	.ver 0:0:0:0
}

.class public auto ansi ILMethodsWithDefaultParameters extends [System.Runtime]System.Object
{	
	.method public hidebysig static bool BooleansEncodedProperly (
			[opt] bool nullBool,
			[opt] bool trueBool,
			[opt] bool falseBool,
			[opt] bool trueInt8Bool,
			[opt] bool falseInt8Bool,
			[opt] bool trueUInt8Bool,
			[opt] bool falseUInt8Bool,
			[opt] bool trueInt16Bool,
			[opt] bool falseInt16Bool,
			[opt] bool trueUInt16Bool,
			[opt] bool falseUInt16Bool,
			[opt] bool trueInt32Bool,
			[opt] bool falseInt32Bool,
			[opt] bool trueUInt32Bool,
			[opt] bool falseUInt32Bool,
			[opt] bool trueInt64Bool,
			[opt] bool falseInt64Bool,
			[opt] bool trueUInt64Bool,
			[opt] bool falseUInt64Bool
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = bool(true)
		.param [3] = bool(false)
		.param [4] = int8(1)
		.param [5] = int8(0)
		.param [6] = uint8(1)
		.param [7] = uint8(0)
		.param [8] = int16(1)
		.param [9] = int16(0)
		.param [10] = uint16(1)
		.param [11] = uint16(0)
		.param [12] = int32(1)
		.param [13] = int32(0)
		.param [14] = uint32(1)
		.param [15] = uint32(0)
		.param [16] = int64(1)
		.param [17] = int64(0)
		.param [18] = uint64(1)
		.param [19] = uint64(0)
		
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 1
		ceq
		and
		
		ldarg 2
		ldc.i4 0
		ceq
		and
		
		ldarg 3
		ldc.i4 1
		ceq
		and
		
		ldarg 4
		ldc.i4 0
		ceq
		and
		
		ldarg 5
		ldc.i4 1
		ceq
		and
		
		ldarg 6
		ldc.i4 0
		ceq
		and
		
		ldarg 7
		ldc.i4 1
		ceq
		and
		
		ldarg 8
		ldc.i4 0
		ceq
		and

		ldarg 9
		ldc.i4 1
		ceq
		and

		ldarg 10
		ldc.i4 0
		ceq
		and

		ldarg 11
		ldc.i4 1
		ceq
		and

		ldarg 12
		ldc.i4 0
		ceq
		and

		ldarg 13
		ldc.i4 1
		ceq
		and
		
		ldarg 14
		ldc.i4 0
		ceq
		and

		ldarg 15
		ldc.i4 1
		ceq
		and
		
		ldarg 16
		ldc.i4 0
		ceq
		and

		ldarg 17
		ldc.i4 1
		ceq
		and

		ldarg 18
		ldc.i4 0
		ceq
		and
		
        ret
    }
	
	.method public hidebysig static bool BooleanEncodedWithChar (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = char(0)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooSmallSByte (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int8(-1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigSByte (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int8(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigByte (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = uint8(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooSmallInt16 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int16(-1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigInt16 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int16(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigUInt16 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = uint16(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooSmallInt32 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int32(-1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigInt32 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int32(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigUInt32 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = uint32(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooSmallInt64 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int64(-1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigInt64 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = int64(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithTooBigUInt64 (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = uint64(2)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithSingle (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = float32(0.1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool BooleanEncodedWithDouble (
			[opt] bool parameter
		) cil managed
	{
		.param [1] = float64(0.1)
	
		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool CharsEncodedProperly (
			[opt] char nullChar,
			[opt] char int8Char,
			[opt] char uint8Char,
			[opt] char int16Char,
			[opt] char uint16Char,
			[opt] char int32Char,
			[opt] char uint32Char,
			[opt] char int64Char,
			[opt] char uint64Char,
			[opt] char charChar
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
		.param [10] = char(97)
		
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		ceq
		and

		ldarg 9
		ldc.i4 97
		ceq
		and

        ret
    }

    .method public hidebysig static bool CharEncodedWithBoolean (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooSmallSByte (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int8(-128)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooSmallInt16 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooSmallInt32 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooBigInt32 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int32(2147483647)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooBigUInt32 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooSmallInt64 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooBigInt64 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithTooBigUInt64 (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithSingle (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool CharEncodedWithDouble (
            [opt] char parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }

	.method public hidebysig static bool SinglesEncodedProperly (
            [opt] float32 nullfloat32,
			[opt] float32 float32float32,
			[opt] float32 float64float32
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = float32(32.0)
		.param [3] = float64(64.0)
		
		ldarg 0
		ldc.r4 0
		ceq
		
		ldarg 1
		ldc.r4 32.0
		ceq
		and
		
		ldarg 2
		ldc.r8 64.0
		ceq
		and
		
		ret
	}

	.method public hidebysig static bool DoublesEncodedProperly (
            [opt] float64 nullfloat32,
			[opt] float64 float64float32
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = float64(64.0)
		
		ldarg 0
		ldc.r8 0
		ceq
		
		ldarg 1
		ldc.r8 64.0
		ceq
		and
		
		ret
	}
	
    .method public hidebysig static bool SingleEncodedWithBoolean (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithChar (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = char(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithSByte (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = int8(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithByte (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = uint8(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithInt16 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = int16(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithUInt16 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = uint16(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithInt32 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = int32(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithUInt32 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = uint32(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithInt64 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = int64(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SingleEncodedWithUInt64 (
            [opt] float32 parameter
        ) cil managed
    {
        .param [1] = uint64(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithBoolean (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithChar (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = char(0)
    
        ldc.i4.0
        ret
    }
    
	.method public hidebysig static bool DoubleEncodedWithSingle (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = float32(32.0)
    
        ldc.i4.0
        ret
    }
	
    .method public hidebysig static bool DoubleEncodedWithSByte (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = int8(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithByte (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = uint8(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithInt16 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = int16(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithUInt16 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = uint16(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithInt32 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = int32(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithUInt32 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = uint32(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithInt64 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = int64(0)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool DoubleEncodedWithUInt64 (
            [opt] float64 parameter
        ) cil managed
    {
        .param [1] = uint64(0)
    
        ldc.i4.0
        ret
    }
	
    .method public hidebysig static bool SBytesEncodedProperly (
            [opt] int8 nullInt8,
			[opt] int8 positiveInt8Int8,
			[opt] int8 negativeInt8Int8,
			[opt] int8 uint8Int8,
			[opt] int8 positiveInt16Int8,
			[opt] int8 negativeInt16Int8,
			[opt] int8 uint16Int8,
			[opt] int8 positiveInt32Int8,
			[opt] int8 negativeInt32Int8,
			[opt] int8 uint32Int8,
			[opt] int8 positiveInt64Int8,
			[opt] int8 negativeInt64Int8,
			[opt] int8 uint64Int8
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = int8(-8)
		.param [4] = uint8(8)
		.param [5] = int16(16)
		.param [6] = int16(-16)
		.param [7] = uint16(16)
		.param [8] = int32(32)
		.param [9] = int32(-32)
		.param [10] = uint32(32)
		.param [11] = int64(64)
		.param [12] = int64(-64)
		.param [13] = uint64(64)

		ldarg 0
		ldc.i4 0
		ceq		
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 -8
		ceq
		and
		
		ldarg 3
		ldc.i4 8
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 -16
		ceq
		and
		
		ldarg 6
		ldc.i4 16
		ceq
		and
		
		ldarg 7
		ldc.i4 32
		ceq
		and
		
		ldarg 8
		ldc.i4 -32
		ceq
		and
		
		ldarg 9
		ldc.i4 32
		ceq
		and
		
		ldarg 10
		ldc.i4 64
		ceq
		and
		
		ldarg 11
		ldc.i4 -64
		ceq
		and
		
		ldarg 12
		ldc.i4 64
		ceq
		and

        ret
    }

	.method public hidebysig static bool BytesEncodedProperly (
			[opt] uint8 nullUInt8,
			[opt] uint8 int8UInt8,
			[opt] uint8 uint8UInt8,
			[opt] uint8 int16UInt8,
			[opt] uint8 uint16UInt8,
			[opt] uint8 int32UInt8,
			[opt] uint8 uint32UInt8,
			[opt] uint8 int64UInt8,
			[opt] uint8 uint64UInt8
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		ceq
		and

        ret
    }

    .method public hidebysig static bool Int16sEncodedProperly (
            [opt] int16 nullInt16,
			[opt] int16 positiveInt8Int16,
			[opt] int16 negativeInt8Int16,
			[opt] int16 uint8Int16,
			[opt] int16 positiveInt16Int16,
			[opt] int16 negativeInt16Int16,
			[opt] int16 uint16Int16,
			[opt] int16 positiveInt32Int16,
			[opt] int16 negativeInt32Int16,
			[opt] int16 uint32Int16,
			[opt] int16 positiveInt64Int16,
			[opt] int16 negativeInt64Int16,
			[opt] int16 uint64Int16
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = int8(-8)
		.param [4] = uint8(8)
		.param [5] = int16(16)
		.param [6] = int16(-16)
		.param [7] = uint16(16)
		.param [8] = int32(32)
		.param [9] = int32(-32)
		.param [10] = uint32(32)
		.param [11] = int64(64)
		.param [12] = int64(-64)
		.param [13] = uint64(64)

		ldarg 0
		ldc.i4 0
		ceq		
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 -8
		ceq
		and
		
		ldarg 3
		ldc.i4 8
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 -16
		ceq
		and
		
		ldarg 6
		ldc.i4 16
		ceq
		and
		
		ldarg 7
		ldc.i4 32
		ceq
		and
		
		ldarg 8
		ldc.i4 -32
		ceq
		and
		
		ldarg 9
		ldc.i4 32
		ceq
		and
		
		ldarg 10
		ldc.i4 64
		ceq
		and
		
		ldarg 11
		ldc.i4 -64
		ceq
		and
		
		ldarg 12
		ldc.i4 64
		ceq
		and

        ret
    }
	
	.method public hidebysig static bool UInt16sEncodedProperly (
			[opt] uint16 nullUInt16,
			[opt] uint16 int8UInt16,
			[opt] uint16 uint8UInt16,
			[opt] uint16 int16UInt16,
			[opt] uint16 uint16UInt16,
			[opt] uint16 int32UInt16,
			[opt] uint16 uint32UInt16,
			[opt] uint16 int64UInt16,
			[opt] uint16 uint64UInt16
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		ceq
		and

        ret
    }
	
	.method public hidebysig static bool Int32sEncodedProperly (
            [opt] int32 nullInt32,
			[opt] int32 positiveInt8Int32,
			[opt] int32 negativeInt8Int32,
			[opt] int32 uint8Int32,
			[opt] int32 positiveInt16Int32,
			[opt] int32 negativeInt16Int32,
			[opt] int32 uint16Int32,
			[opt] int32 positiveInt32Int32,
			[opt] int32 negativeInt32Int32,
			[opt] int32 uint32Int32,
			[opt] int32 positiveInt64Int32,
			[opt] int32 negativeInt64Int32,
			[opt] int32 uint64Int32
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = int8(-8)
		.param [4] = uint8(8)
		.param [5] = int16(16)
		.param [6] = int16(-16)
		.param [7] = uint16(16)
		.param [8] = int32(32)
		.param [9] = int32(-32)
		.param [10] = uint32(32)
		.param [11] = int64(64)
		.param [12] = int64(-64)
		.param [13] = uint64(64)

		ldarg 0
		ldc.i4 0
		ceq		
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 -8
		ceq
		and
		
		ldarg 3
		ldc.i4 8
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 -16
		ceq
		and
		
		ldarg 6
		ldc.i4 16
		ceq
		and
		
		ldarg 7
		ldc.i4 32
		ceq
		and
		
		ldarg 8
		ldc.i4 -32
		ceq
		and
		
		ldarg 9
		ldc.i4 32
		ceq
		and
		
		ldarg 10
		ldc.i4 64
		ceq
		and
		
		ldarg 11
		ldc.i4 -64
		ceq
		and
		
		ldarg 12
		ldc.i4 64
		ceq
		and

        ret
    }
	
	.method public hidebysig static bool UInt32sEncodedProperly (
			[opt] uint32 nullUInt32,
			[opt] uint32 int8UInt32,
			[opt] uint32 uint8UInt32,
			[opt] uint32 int16UInt32,
			[opt] uint32 uint16UInt32,
			[opt] uint32 int32UInt32,
			[opt] uint32 uint32UInt32,
			[opt] uint32 int64UInt32,
			[opt] uint32 uint64UInt32
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		ceq
		and

        ret
    }
	
	.method public hidebysig static bool Int64sEncodedProperly (
            [opt] int64 nullInt64,
			[opt] int64 positiveInt8Int64,
			[opt] int64 negativeInt8Int64,
			[opt] int64 uint8Int64,
			[opt] int64 positiveInt16Int64,
			[opt] int64 negativeInt16Int64,
			[opt] int64 uint16Int64,
			[opt] int64 positiveInt32Int64,
			[opt] int64 negativeInt32Int64,
			[opt] int64 uint32Int64,
			[opt] int64 positiveInt64Int64,
			[opt] int64 negativeInt64Int64,
			[opt] int64 uint64Int64
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = int8(-8)
		.param [4] = uint8(8)
		.param [5] = int16(16)
		.param [6] = int16(-16)
		.param [7] = uint16(16)
		.param [8] = int32(32)
		.param [9] = int32(-32)
		.param [10] = uint32(32)
		.param [11] = int64(64)
		.param [12] = int64(-64)
		.param [13] = uint64(64)

		ldarg 0
		ldc.i4 0
		ceq		
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 -8
		ceq
		and
		
		ldarg 3
		ldc.i4 8
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 -16
		ceq
		and
		
		ldarg 6
		ldc.i4 16
		ceq
		and
		
		ldarg 7
		ldc.i4 32
		ceq
		and
		
		ldarg 8
		ldc.i4 -32
		ceq
		and
		
		ldarg 9
		ldc.i4 32
		ceq
		and
		
		ldarg 10
		ldc.i4 64
		ceq
		and
		
		ldarg 11
		ldc.i4 -64
		ceq
		and
		
		ldarg 12
		ldc.i4 64
		ceq
		and

        ret
    }
	
	.method public hidebysig static bool UInt64sEncodedProperly (
			[opt] uint64 nullUInt64,
			[opt] uint64 int8UInt64,
			[opt] uint64 uint8UInt64,
			[opt] uint64 int16UInt64,
			[opt] uint64 uint16UInt64,
			[opt] uint64 int32UInt64,
			[opt] uint64 uint32UInt64,
			[opt] uint64 int64UInt64,
			[opt] uint64 uint64UInt64
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		ceq
		
		ldarg 1
		ldc.i4 8
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		ceq
		and

        ret
    }
	
    .method public hidebysig static bool SByteEncodedWithBoolean (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithChar (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigByte (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = uint8(255)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooSmallInt16 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigInt16 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int16(32767)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigUInt16 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = uint16(65535)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooSmallInt32 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigInt32 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int32(2147483647)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigUInt32 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooSmallInt64 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigInt64 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithTooBigUInt64 (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithBoolean (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithChar (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooSmallSByte (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int8(-128)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooSmallInt16 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigInt16 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int16(32767)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigUInt16 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = uint16(65535)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooSmallInt32 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigInt32 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int32(2147483647)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigUInt32 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooSmallInt64 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigInt64 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithTooBigUInt64 (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithBoolean (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithChar (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooBigUInt16 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = uint16(65535)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooSmallInt32 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooBigInt32 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = int32(2147483647)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooBigUInt32 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooSmallInt64 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooBigInt64 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithTooBigUInt64 (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithBoolean (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithChar (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooSmallSByte (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int8(-128)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooSmallInt16 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooSmallInt32 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooBigInt32 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int32(2147483647)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooBigUInt32 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooSmallInt64 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooBigInt64 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithTooBigUInt64 (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithBoolean (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithChar (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithTooBigUInt32 (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = uint32(4294967295)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithTooSmallInt64 (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithTooBigInt64 (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithTooBigUInt64 (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithBoolean (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithChar (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooSmallSByte (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = int8(-128)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooSmallInt16 (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooSmallInt32 (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooSmallInt64 (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooBigInt64 (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = int64(9223372036854775807)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithTooBigUInt64 (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int64EncodedWithBoolean (
            [opt] int64 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int64EncodedWithChar (
            [opt] int64 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int64EncodedWithTooBigUInt64 (
            [opt] int64 parameter
        ) cil managed
    {
        .param [1] = uint64(18446744073709551615)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithBoolean (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = bool(true)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithChar (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = char(97)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithTooSmallSByte (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = int8(-128)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithTooSmallInt16 (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = int16(-32768)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithTooSmallInt32 (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = int32(-2147483648)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithTooSmallInt64 (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = int64(-9223372036854775808)
    
        ldc.i4.0
        ret
    }
	
    .method public hidebysig static bool SByteEncodedWithSingle (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool SByteEncodedWithDouble (
            [opt] int8 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithSingle (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool ByteEncodedWithDouble (
            [opt] uint8 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithSingle (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int16EncodedWithDouble (
            [opt] int16 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithSingle (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt16EncodedWithDouble (
            [opt] uint16 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithSingle (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int32EncodedWithDouble (
            [opt] int32 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithSingle (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt32EncodedWithDouble (
            [opt] uint32 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int64EncodedWithSingle (
            [opt] int64 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool Int64EncodedWithDouble (
            [opt] int64 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithSingle (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = float32(0.1)
    
        ldc.i4.0
        ret
    }
    
    .method public hidebysig static bool UInt64EncodedWithDouble (
            [opt] uint64 parameter
        ) cil managed
    {
        .param [1] = float64(0.1)
    
        ldc.i4.0
        ret
    }
	
	.method public hidebysig static bool NativeIntsEncodedProperly (
            [opt] native int nullNInt,
			[opt] native int positiveInt8NInt,
			[opt] native int negativeInt8NInt,
			[opt] native int uint8NInt,
			[opt] native int positiveInt16NInt,
			[opt] native int negativeInt16NInt,
			[opt] native int uint16NInt,
			[opt] native int positiveInt32NInt,
			[opt] native int negativeInt32NInt,
			[opt] native int uint32NInt,
			[opt] native int positiveInt64NInt,
			[opt] native int negativeInt64NInt,
			[opt] native int uint64NInt,
			
			[opt] native uint nullNUInt,
			[opt] native uint int8NUInt,
			[opt] native uint uint8NUInt,
			[opt] native uint int16NUInt,
			[opt] native uint uint16NUInt,
			[opt] native uint int32NUInt,
			[opt] native uint uint32NUInt,
			[opt] native uint int64NUInt,
			[opt] native uint uint64NUInt
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = int8(-8)
		.param [4] = uint8(8)
		.param [5] = int16(16)
		.param [6] = int16(-16)
		.param [7] = uint16(16)
		.param [8] = int32(32)
		.param [9] = int32(-32)
		.param [10] = uint32(32)
		.param [11] = int64(64)
		.param [12] = int64(-64)
		.param [13] = uint64(64)
		
		.param [14] = nullref
		.param [15] = int8(8)
		.param [16] = uint8(8)
		.param [17] = int16(16)
		.param [18] = uint16(16)
		.param [19] = int32(32)
		.param [20] = uint32(32)
		.param [21] = int64(64)
		.param [22] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		conv.i
		ceq		
		
		ldarg 1
		ldc.i4 8
		conv.i
		ceq
		and
		
		ldarg 2
		ldc.i4 -8
		conv.i
		ceq
		and
		
		ldarg 3
		ldc.i4 8
		conv.i
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		conv.i
		ceq
		and
		
		ldarg 5
		ldc.i4 -16
		conv.i
		ceq
		and
		
		ldarg 6
		ldc.i4 16
		conv.i
		ceq
		and
		
		ldarg 7
		ldc.i4 32
		conv.i
		ceq
		and
		
		ldarg 8
		ldc.i4 -32
		conv.i
		ceq
		and
		
		ldarg 9
		ldc.i4 32
		conv.i
		ceq
		and
		
		ldarg 10
		ldc.i4 64
		conv.i
		ceq
		and
		
		ldarg 11
		ldc.i4 -64
		conv.i
		ceq
		and
		
		ldarg 12
		ldc.i4 64
		conv.i
		ceq
		and
		
		
		ldarg 13
		ldc.i4 0
		conv.u
		ceq
		and	
		
		ldarg 14
		ldc.i4 8
		conv.u
		ceq
		and
		
		ldarg 15
		ldc.i4 8
		conv.u
		ceq
		and
		
		ldarg 16
		ldc.i4 16
		conv.u
		ceq
		and
		
		ldarg 17
		ldc.i4 16
		conv.u
		ceq
		and
		
		ldarg 18
		ldc.i4 32
		conv.u
		ceq
		and
		
		ldarg 19
		ldc.i4 32
		conv.u
		ceq
		and
		
		ldarg 20
		ldc.i4 64
		conv.u
		ceq
		and
		
		ldarg 21
		ldc.i4 64
		conv.u
		ceq
		and

        ret
    }

	.method public hidebysig static bool NativeIntEncodedWithBoolean (
			[opt] native int nint
		) cil managed
	{
		.param [1] = bool(false)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeIntEncodedWithChar (
			[opt] native int nint
		) cil managed
	{
		.param [1] = char(0)

		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool NativeIntEncodedWithTooBigUInt32 (
			[opt] native int nint
		) cil managed
	{
		.param [1] = uint32(4294967295)

		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool NativeIntEncodedWithTooBigInt64 (
			[opt] native int nint
		) cil managed
	{
		.param [1] = int64(9223372036854775807)

		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool NativeIntEncodedWithTooSmallInt64 (
			[opt] native int nint
		) cil managed
	{
		.param [1] = int64(-9223372036854775808)

		ldc.i4.0
		ret
	}
	
	.method public hidebysig static bool NativeIntEncodedWithTooBigUInt64 (
			[opt] native int nint
		) cil managed
	{
		.param [1] = uint64(18446744073709551615)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithBoolean (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = bool(false)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithChar (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = char(0)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooSmallInt8 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = int8(-8)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooSmallInt16 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = int16(-16)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooSmallInt32 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = int32(-32)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooBigInt64 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = int64(9223372036854775807)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooSmallInt64 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = int64(-64)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool NativeUIntEncodedWithTooBigUInt64 (
			[opt] native uint nuint
		) cil managed
	{
		.param [1] = uint64(18446744073709551615)

		ldc.i4.0
		ret
	}
	
    .method public hidebysig static bool PointersEncodedProperly (
			[opt] void* nullUPtr,
			[opt] void* int8UPtr,
			[opt] void* uint8UPtr,
			[opt] void* int16UPtr,
			[opt] void* uint16UPtr,
			[opt] void* int32UPtr,
			[opt] void* uint32UPtr,
			[opt] void* int64UPtr,
			[opt] void* uint64UPtr
        ) cil managed 
    {
		.param [1] = nullref
		.param [2] = int8(8)
		.param [3] = uint8(8)
		.param [4] = int16(16)
		.param [5] = uint16(16)
		.param [6] = int32(32)
		.param [7] = uint32(32)
		.param [8] = int64(64)
		.param [9] = uint64(64)
				
		ldarg 0
		ldc.i4 0
		conv.u
		ceq
		
		ldarg 1
		ldc.i4 8
		conv.u
		ceq
		and
		
		ldarg 2
		ldc.i4 8
		conv.u
		ceq
		and
		
		ldarg 3
		ldc.i4 16
		conv.u
		ceq
		and
		
		ldarg 4
		ldc.i4 16
		conv.u
		ceq
		and
		
		ldarg 5
		ldc.i4 32
		conv.u
		ceq
		and
		
		ldarg 6
		ldc.i4 32
		conv.u
		ceq
		and
		
		ldarg 7
		ldc.i4 64
		conv.u
		ceq
		and
		
		ldarg 8
		ldc.i4 64
		conv.u
		ceq
		and

        ret
    }

	.method public hidebysig static bool PointerEncodedWithBoolean (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = bool(false)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithChar (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = char(0)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooSmallInt8 (
			[opt] void* pointer
	    ) cil managed
	{
		.param [1] = int8(-8)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooSmallInt16 (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = int16(-16)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooSmallInt32 (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = int32(-32)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooBigInt64 (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = int64(9223372036854775807)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooSmallInt64 (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = int64(-64)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool PointerEncodedWithTooBigUInt64 (
			[opt] void* pointer
		) cil managed
	{
		.param [1] = uint64(18446744073709551615)

		ldc.i4.0
		ret
	}

	.method public hidebysig static bool DateTimeEncodedAttribute (
            [opt] valuetype [System.Runtime]System.DateTime dateTime
        ) cil managed 
    {
        .param [1]
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.DateTimeConstantAttribute::.ctor(int64) = (
                01 00 FF 00 00 00 00 00 00 00
			)
		
        .locals init (
            [0] valuetype [System.Runtime]System.DateTime dateTime
        )

        ldloca 0
        ldc.i8 255
        call instance void [System.Runtime]System.DateTime::.ctor(int64)
        ldarg 0
        ldloc 0
        call bool [System.Runtime]System.DateTime::op_Equality(valuetype [System.Runtime]System.DateTime, valuetype [System.Runtime]System.DateTime)
        ret
    }
	
	.method public hidebysig static bool MethodWithNonTrailingDefaultParameters (
            [opt] int32 integer32,
			[opt] int64 integer64,
			float32 fp32,
			[opt] float64 fp64,
			int8 integer8,
			[opt] int16 integer16,
			[opt] char character
        ) cil managed 
    {
        .param [1] = int32(32)
		.param [2] = int64(64)
		.param [4] = float64(64.0)
		.param [6] = int16(16)
		.param [7] = char(97)

        ldarg 0
		ldc.i4 32
		ceq
		
		ldarg 1
		ldc.i8 64
		ceq
		and
		
		ldarg 2
		ldc.r4 32.0
		ceq
		and
		
		ldarg 3
		ldc.r8 64.0
		ceq
		and
		
		ldarg 4
		ldc.i4 8
		ceq
		and
		
		ldarg 5
		ldc.i4 16
		ceq
		and
		
		ldarg 6
		ldc.i4 97
		ceq
		and
		
        ret
    }
}