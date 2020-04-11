using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal static class AlgorithmDescriptions<T> where T : IComparable<T>
    {
        public static Intersect<T> Intersect => new Intersect<T>();
        public static Union<T> Union => new Union<T>();
        public static SymmetricDifference<T> SymmetricDifference => new SymmetricDifference<T>();
    }
}
