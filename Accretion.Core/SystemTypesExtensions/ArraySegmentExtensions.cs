using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Core
{
    public static class ArraySegmentExtensions
    {
        public static int MaxIndex<T>(this ArraySegment<T> segment)
        {
            return segment.Offset + segment.Count - 1;
        }
    }
}
