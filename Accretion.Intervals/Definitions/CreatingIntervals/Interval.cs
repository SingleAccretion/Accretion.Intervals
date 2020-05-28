using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Accretion.Intervals
{
    public static class Interval
    {
        public static Interval<T> Create<T>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> 
            => throw new NotImplementedException();

        public static Interval<T, TComparer> Create<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T> 
            => throw new NotImplementedException();

        public static Interval<T> CreateClosed<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T> 
            => Create(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);
        
        public static Interval<T, TComparer> CreateClosed<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => Create<T, TComparer>(BoundaryType.Closed, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Closed);

        public static Interval<T> CreateOpen<T>(T lowerBoundaryValue, T upperBoundaryValue) where T : IComparable<T>
            => Create(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);

        public static Interval<T, TComparer> CreateOpen<T, TComparer>(BoundaryType lowerBoundaryType, T lowerBoundaryValue, T upperBoundaryValue, BoundaryType upperBoundaryType) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => Create<T, TComparer>(BoundaryType.Open, lowerBoundaryValue, upperBoundaryValue, BoundaryType.Open);
        
        public static Interval<T> CreateSingleton<T>(T value) where T : IComparable<T>
            => Create(BoundaryType.Closed, value, value, BoundaryType.Closed);
        
        public static Interval<T, TComparer> CreateSingleton<T, TComparer>(T value) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => Create<T, TComparer>(BoundaryType.Closed, value, value, BoundaryType.Closed);
        
        public static CompositeInterval<T> Join<T>(ICollection<Interval<T>> intervals) where T : IComparable<T>
            => throw new NotImplementedException();
        
        public static CompositeInterval<T, TComparer> Join<T, TComparer>(ICollection<Interval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => throw new NotImplementedException();
        
        public static CompositeInterval<T> Join<T>(IReadOnlyCollection<Interval<T>> intervals) where T : IComparable<T>
            => throw new NotImplementedException();
        
        public static CompositeInterval<T, TComparer> Join<T, TComparer>(IReadOnlyCollection<Interval<T>> intervals) where T : IComparable<T> where TComparer : struct, IComparer<T>
            => throw new NotImplementedException();
    }
}