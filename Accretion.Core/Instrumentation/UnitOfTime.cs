using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Core
{
    public enum UnitOfTime : long
    {
        Millisecond = 1000L,
        Microsecond = 1000L * 1000L,
        Nanonsecond = 1000L * 1000L * 1000L,
    }
}
