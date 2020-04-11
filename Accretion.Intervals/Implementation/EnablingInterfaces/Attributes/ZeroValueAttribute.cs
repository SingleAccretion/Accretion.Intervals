using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ZeroValueAttribute : Attribute
    {
        public ZeroValueAttribute() { }
    }
}
