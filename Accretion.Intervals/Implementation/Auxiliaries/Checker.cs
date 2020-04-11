using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Accretion.Intervals
{
    internal static class Checker
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(T obj) => GenericSpecializer<T>.TypeInstanceCanBeNull ? obj is null : false;

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
    }
}
