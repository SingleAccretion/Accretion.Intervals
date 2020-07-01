using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class Checker
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(T value) => value is null;

		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static bool IsDefault<T>(T value)
		{
			var size = Unsafe.SizeOf<T>();
			if (size == 1)
			{
				return Unsafe.As<T, byte>(ref value) == 0;
			}
			if (size == 2)
			{
				return Unsafe.As<T, ushort>(ref value) == 0;
			}
			if (size == 4)
			{
				return Unsafe.As<T, int>(ref value) == 0;
			}
			if (size == 8)
			{
				return Unsafe.As<T, long>(ref value) == 0;
			}

			var pointer = (long*)Unsafe.AsPointer(ref value);
			if (size == 16)
			{
				return pointer[0] == 0 & pointer[1] == 0;
			}
			if (size == 32)
			{
				return pointer[0] == 0 & pointer[1] == 0 & pointer[2] == 0 & pointer[3] == 0;
			}

			var numberOfVectors = Vector.IsHardwareAccelerated ? size / Vector<byte>.Count : 0;
			var numberOfIntPtrs = (size - numberOfVectors * Vector<byte>.Count) / IntPtr.Size;
			var numberOfBytes = size - numberOfVectors * Vector<byte>.Count - numberOfIntPtrs * IntPtr.Size;

			var vectorPointer = (Vector<byte>*)pointer;
			for (int i = 0; i < numberOfVectors; i++, vectorPointer++)
			{
				if (*vectorPointer != Vector<byte>.Zero)
				{
					return false;
				}
			}

			var intPtrPointer = (IntPtr*)vectorPointer;			
			for (int i = 0; i < numberOfIntPtrs; i++, intPtrPointer++)
			{
				if (*intPtrPointer != IntPtr.Zero)
				{
					return false;
				}
			}

			var bytePointer = (byte*)intPtrPointer;
			for (int i = 0; i < numberOfBytes; i++, bytePointer++)
			{
				if (*bytePointer != 0)
				{
					return false;
				}
			}

			return true;
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN<T>(T value)
        {
            if (typeof(T) == typeof(double))
            {
				return double.IsNaN((double)(object)value);
			}
            if (typeof(T) == typeof(float))
            {
				return float.IsNaN((float)(object)value);
            }

			return false;
        }

		//We have to basically copy this from the BCL because otherwise the codegen is surprisingly suboptimal.
		//This will have to be independently tested against BCL for correctness.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsUtcDateTime<T>(T value)
		{
			if (typeof(T) == typeof(DateTime))
			{
				const ulong KindUtc = 0x4000000000000000;
				const ulong FlagsMask = 0xC000000000000000;

				var dateTime = (DateTime)(object)value;
				var dateTimeData = Unsafe.As<DateTime, ulong>(ref dateTime);
				return (dateTimeData & FlagsMask) == KindUtc;
			}

			return false;
		}
	}
}
