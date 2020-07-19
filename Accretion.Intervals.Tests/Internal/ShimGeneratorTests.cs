using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;
using static ILMethodsWithDefaultParameters;

namespace Accretion.Intervals.Tests.Internal
{
    //There are two versions of the tests: in C# and in IL
    //We have to do both because C# semantics do not allow (the compiler does not emit):
    //Non-trailing default parameters
    //Non-null pointers
    //Non-default DateTimes
    //Mixed and matched primitives
    //We have to consider:
    //Classes (except string)
    //Strings (both null and literals)
    //Primitives: Boolean, Char, SByte, Byte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double
    //IntPtr, UIntPtr and pointers
    //DateTime, Decimal, Enums
    //Custom value types (including ByRefLike)
    //Custom value types encoded with a custom CustomConstantAttribute
    //Nullables
    //Optional parameters that do not have default values
    public class ShimGeneratorTests : TestsBase
    {
        private const string LiteralString = "LiteralString";
        private const double Float64Constant = 64d;
        private const int Int32Constant = 32;

        [Fact]
        public void SupportsProperlyEncodedBooleanConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(BooleansEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedBooleanConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(BooleanEncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedCharConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(CharsEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedCharConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(CharEncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedSingleConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(SinglesEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedSingleConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SingleEncodedWithUInt64)))));

        }

        [Fact]
        public void SupportsProperlyEncodedDoubleConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(DoublesEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedDoubleConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(DoubleEncodedWithUInt64)))));
        }

        [Fact]
        public void SupportsProperlyEncodedSByteConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(SBytesEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedSByteConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(SByteEncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedByteConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(BytesEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedByteConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(ByteEncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedInt16Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(Int16sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedInt16Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooBigUInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int16EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedUInt16Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(UInt16sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedUInt16Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooBigInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt16EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedInt32Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(Int32sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedInt32Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int32EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedUInt32Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(UInt32sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedUInt32Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt32EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedInt64Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(Int64sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedInt64Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int64EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int64EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int64EncodedWithTooBigUInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int64EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(Int64EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedUInt64Constants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(UInt64sEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedUInt64Constants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithTooSmallSByte)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithSingle)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(UInt64EncodedWithDouble)))));
        }

        [Fact]
        public void SupportsProperlyEncodedNativeIntegerConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(NativeIntsEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedNativeIntegerConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithTooBigUInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeIntEncodedWithTooBigUInt64)))));

            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooSmallInt8)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(NativeUIntEncodedWithTooBigUInt64)))));
        }

        [Fact]
        public void SupportsProperlyEncodedPointerConstants()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(PointersEncodedProperly));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void DoesNotSupportBadlyEncodedPointerConstants()
        {
            var type = typeof(ILMethodsWithDefaultParameters);
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithBoolean)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithChar)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooSmallInt8)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooSmallInt16)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooSmallInt32)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooSmallInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooBigInt64)))));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(type.GetMethod(nameof(PointerEncodedWithTooBigUInt64)))));
        }

        [Fact]
        public void SupportsDateTimeConstantAttribute()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(DateTimeEncodedAttribute));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        //Note that this support is not just a checkbox. There is a proposal on csharplang for this, and it has recently been looked at by the LDT
        public void SupportsNonTrailingDefaultParameters()
        {
            var source = typeof(ILMethodsWithDefaultParameters).GetMethod(nameof(MethodWithNonTrailingDefaultParameters));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<float, sbyte, bool>>(source);
            Assert.True(shim(32f, 8));
        }

        [Fact]
        public void SupportsValidCSharpDefaultParameters()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithAllPossibleCSharpDefaultParameters));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source);
            Assert.True(shim());
        }

        [Fact]
        public void SupportsShimsForNoDefaultParameters()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithoutDefaultParameters));
            var shim = ShimGenerator.WithDefaultParametersPassed<Func<double, string, int, object, bool>>(source);
            Assert.True(shim(Float64Constant, LiteralString, Int32Constant, null));
        }

        [Fact]
        public void DoesNotSupportOptionalParameters()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithOptionalParameters));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source)));
        }

        [Fact]
        public void DoesNotSupportInstanceMethods()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(InstanceMethod));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<bool>>(source)));
        }

        [Fact]
        public void TargetParameterMustExactlyMatchRequiredSourceParameters()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithDefaultAndRequiredParameters));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<string, double, bool>>(source)));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<long, string, bool>>(source)));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<decimal, double, bool>>(source)));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<double, string, int, object, bool>>(source)));
        }

        [Fact]
        public void DoesNotSupportReturnTypeMismatch()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithAllPossibleCSharpDefaultParameters));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<int>>(source)));
        }

        [Fact]
        public void DoesNotSupportInParameters()
        {
            var source = typeof(ShimGeneratorTests).GetMethod(nameof(MethodWithDefaultInParameters));
            Assert.NotNull(Record.Exception(() => ShimGenerator.WithDefaultParametersPassed<Func<int>>(source)));
        }

        public static bool MethodWithoutDefaultParameters(double float64, string str, int int32, object @object) => 
            float64 == Float64Constant && 
            str == LiteralString && 
            int32 == Int32Constant && 
            @object == null;

        public static bool MethodWithDefaultAndRequiredParameters(double float64, string str, int int32 = Int32Constant, object @object = null) => 
            int32 == Int32Constant && 
            @object == null;

        public static bool MethodWithDefaultInParameters(in int int32 = Int32Constant, in double float64 = Float64Constant) => 
            int32 == Int32Constant && 
            float64 == Float64Constant;

        public static bool MethodWithOptionalParameters([Optional] int int32, [Optional] decimal decimal128, [Optional] string str) =>  true;

        public static unsafe bool MethodWithAllPossibleCSharpDefaultParameters(
            object @object = null,
            string nullString = null,
            string literalString = LiteralString,
            bool boolean = true,
            char character = 'c',
            sbyte int8 = 8,
            byte uint8 = 8,
            short int16 = 16,
            ushort uint16 = 16,
            int int32 = Int32Constant,
            uint uint32 = Int32Constant,
            long int64 = 64,
            ulong uint64 = 64,
            IntPtr zeroIntPtr = default,
            UIntPtr zeroUIntPtr = default,
            void* nullPtr = null,
            DateTime defaultDateTime = default,
            decimal decimal128 = 128m,
            TypeCode enumeration = TypeCode.Int32,
            BindingFlags flagsEnumeration = BindingFlags.Instance | BindingFlags.Public,
            float float32 = 32f,
            double float64 = Float64Constant,
            Guid defaultCustomStruct = default,
            ReadOnlySpan<char> defaultCustomByRefLike = default,
            bool? nonNullNullableBoolean = true,
            char? nonNullNullableCharacter = 'c',
            sbyte? nonNullNullableInt8 = 8,
            byte? nonNullNullableUint8 = 8,
            short? nonNullNullableInt16 = 16,
            ushort? nonNullNullableUint16 = 16,
            int? nonNullNullableInt32 = Int32Constant,
            uint? nonNullNullableUint32 = Int32Constant,
            long? nonNullNullableInt64 = 64,
            ulong? nonNullNullableUint64 = 64,
            decimal? nonNullNullableDecimal128 = 128m,
            TypeCode? nonNullNullableEnumeration = TypeCode.Int32,
            BindingFlags? nonNullNullableFlagsEnumeration = BindingFlags.Instance | BindingFlags.Public,
            float? nonNullNullableFloat32 = 32f,
            double? nonNullNullableFloat64 = Float64Constant,
            bool? nullNullableBoolean = null,
            char? nullNullableCharacter = null,
            sbyte? nullNullableInt8 = null,
            byte? nullNullableUint8 = null,
            short? nullNullableInt16 = null,
            ushort? nullNullableUint16 = null,
            int? nullNullableInt32 = null,
            uint? nullNullableUint32 = null,
            long? nullNullableInt64 = null,
            ulong? nullNullableUint64 = null,
            IntPtr? nullNullableZeroIntPtr = null,
            UIntPtr? nullNullableZeroUIntPtr = null,
            DateTime? nullNullableDefaultDateTime = null,
            decimal? nullNullableDecimal128 = null,
            TypeCode? nullNullableEnumeration = null,
            BindingFlags? nullNullableFlagsEnumeration = null,
            float? nullNullableFloat32 = null,
            double? nullNullableFloat64 = null,
            Guid? nullNullableDefaultCustomStruct = null) =>
            @object == null &&
            nullString == null &&
            literalString == LiteralString &&
            boolean == true &&
            character == 'c' &&
            int8 == 8 &&
            uint8 == 8 &&
            int16 == 16 &&
            uint16 == 16 &&
            int32 == Int32Constant &&
            uint32 == Int32Constant &&
            int64 == 64 &&
            uint64 == 64 &&
            zeroIntPtr == default &&
            zeroUIntPtr == default &&
            nullPtr == null &&
            defaultDateTime == default &&
            decimal128 == 128m &&
            enumeration == TypeCode.Int32 &&
            flagsEnumeration == (BindingFlags.Instance | BindingFlags.Public) &&
            float32 == 32f &&
            float64 == Float64Constant &&
            defaultCustomStruct == default &&
            defaultCustomByRefLike == default &&
            nonNullNullableBoolean == true &&
            nonNullNullableCharacter == 'c' &&
            nonNullNullableInt8 == 8 &&
            nonNullNullableUint8 == 8 &&
            nonNullNullableInt16 == 16 &&
            nonNullNullableUint16 == 16 &&
            nonNullNullableInt32 == Int32Constant &&
            nonNullNullableUint32 == Int32Constant &&
            nonNullNullableInt64 == 64 &&
            nonNullNullableUint64 == 64 &&
            nonNullNullableDecimal128 == 128m &&
            nonNullNullableEnumeration == TypeCode.Int32 &&
            nonNullNullableFlagsEnumeration == (BindingFlags.Instance | BindingFlags.Public) &&
            nonNullNullableFloat32 == 32f &&
            nonNullNullableFloat64 == Float64Constant &&
            nullNullableBoolean == null &&
            nullNullableCharacter == null &&
            nullNullableInt8 == null &&
            nullNullableUint8 == null &&
            nullNullableInt16 == null &&
            nullNullableUint16 == null &&
            nullNullableInt32 == null &&
            nullNullableUint32 == null &&
            nullNullableInt64 == null &&
            nullNullableUint64 == null &&
            nullNullableZeroIntPtr == null &&
            nullNullableZeroUIntPtr == null &&
            nullNullableDefaultDateTime == null &&
            nullNullableDecimal128 == null &&
            nullNullableEnumeration == null &&
            nullNullableFlagsEnumeration == null &&
            nullNullableFloat32 == null &&
            nullNullableFloat64 == null &&
            nullNullableDefaultCustomStruct == null;

        public bool InstanceMethod(int int32 = Int32Constant, DateTime dateTime = default) => int32 == Int32Constant && dateTime == default;
    }
}
