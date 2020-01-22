using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Experimental
{
    public static class PerformanceConfiguration
    {
        /// <summary>
        /// Intervals use arrays of boundaries to represent their state. When creating new intervals via operations like Union or Intersect, those arrays are merged into a new one. This configuration setting determines what algorithm is used for that.
        /// </summary>
        public static MergingAlgorithm MergingAlgorithm { get; set; } = MergingAlgorithm.Linear;
    }
}
