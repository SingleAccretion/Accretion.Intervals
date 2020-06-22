using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Accretion.Intervals
{
    [Serializable]
    public class IntervalFormatException : Exception
    {
        public IntervalFormatException() { }
        public IntervalFormatException(string message) : base(message) { }
        public IntervalFormatException(string message, Exception inner) : base(message, inner) { }
        protected IntervalFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
