using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Accretion.Intervals.Definitions
{
    public static class Interval
    {
        public static AtomicInterval<T> CreateAtomic<T>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> 
            => throw new NotImplementedException();

        public static AtomicInterval<T, TComparer> CreateAtomic<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T> 
            => throw new NotImplementedException();

        public static AtomicInterval<T> CreateClosedAtomic<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T> 
            => CreateAtomic(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);
        
        public static AtomicInterval<T, TComparer> CreateClosedAtomic<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => CreateAtomic<T, TComparer>(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);

        public static AtomicInterval<T> CreateOpenAtomic<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T>
            => CreateAtomic(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);

        public static AtomicInterval<T, TComparer> CreateOpenAtomic<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => CreateAtomic<T, TComparer>(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);
        
        public static AtomicInterval<T> CreateSingletonAtomic<T>(T value) where T : IComparable<T>
            => CreateAtomic(BoundaryType.Closed, value, value, BoundaryType.Closed);
        
        public static AtomicInterval<T, TComparer> CreateSingletonAtomic<T, TComparer>(T value) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => CreateAtomic<T, TComparer>(BoundaryType.Closed, value, value, BoundaryType.Closed);
        
        public static FullInterval<T> CreateFull<T>(IReadOnlyList<AtomicInterval<T>> intervals) where T : IComparable<T> 
            => throw new NotImplementedException();
        
        public static FullInterval<T, TComparer> CreateFull<T, TComparer>(IReadOnlyList<AtomicInterval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T> 
            => throw new NotImplementedException();
        
        public static FullInterval<T> CreateFull<T>(IList<AtomicInterval<T>> intervals) where T : IComparable<T> 
            => throw new NotImplementedException();
        
        public static FullInterval<T, TComparer> CreateFull<T, TComparer>(IList<AtomicInterval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T> 
            => throw new NotImplementedException();
    }

    public static class T
    {
        public class PolyInterval { }
        public class MonoInterval { }
        
        public static void TestMethod()
        {
            var a = Interval.CreateAtomic(BoundaryType.Closed, 1, 3, BoundaryType.Closed);
            a  = Interval.CreateClosedAtomic(1, 3);
            a = Interval.CreateOpenAtomic(1, 3);
            a = Interval.CreateSingletonAtomic(1);

            var c = true;
            var d = true;
            var r = !(c && d);
        }
    }
}