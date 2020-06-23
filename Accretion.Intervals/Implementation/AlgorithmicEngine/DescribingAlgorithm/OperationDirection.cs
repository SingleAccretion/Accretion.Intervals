using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals
{
    internal enum OperationDirection
    {
        FirstToFirst = 0b00,
        FirstToSecond = 0b01,
        SecondToFirst = 0b10,
        SecondToSecond = 0b11,
    }
}
