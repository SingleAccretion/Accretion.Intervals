using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class IComparableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T>(this T that, T other) where T : IComparable<T>
        {
            return that.CompareTo(other) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this T that, T other) where T : IComparable<T>
        {
            return that.CompareTo(other) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan<T>(this T that, T other) where T : IComparable<T>
        {
            return that.CompareTo(other) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Comparison InComparisonWith<T>(this T that, T other) where T : IComparable<T>
        {            
            return new Comparison(that.CompareTo(other));
        }        
    }    
}
