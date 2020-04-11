using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class CustomSubtractableStructWithCustomLengthSpecializationTests : GenericSpecializerTests<Day, Period>
    {
        public override bool TypeImplementsISubtractable => true;
    }

    public class CustomSubtractableStructWithPrimitiveLengthSpecializationTests : GenericSpecializerTests<Day, TimeSpan>
    {
        public override bool TypeImplementsISubtractable => true;
    }

    public class CustomSubtractableStructWithInvalidLengthSpecializationTests : GenericSpecializerTests<Day, double>
    {
        public override bool TypeImplementsISubtractable => false;
    }
}
