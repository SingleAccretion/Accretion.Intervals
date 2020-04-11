using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal static class OverlapStrategies<T> where T : IComparable<T>
    {
        public static NoOverlap<T> NoOverlap => new NoOverlap<T>();
        public static FullOverlap<T> FullOverlap => new FullOverlap<T>();
        public static OverlapClosed<T> OverlapClosed => new OverlapClosed<T>();
        public static OverlapFullyClosed<T> OverlapFullyClosed => new OverlapFullyClosed<T>();
        public static Invariant Invariant => new Invariant();
    }
}
