using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    public enum MergingAlgorithm
    {
        /// <summary>
        /// A simple algorithm for merging sorted arrays, great for similarly sized arrays with similar elements.
        /// </summary>
        Linear,
        /// <summary>
        /// Algorithm for merging sorted arrays of different sizes with dissimilar elements.
        /// </summary>
        Gallop
    }
}
