using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class CustomSubtractableClassWithCustomLengthSpecializationTests : GenericSpecializerTests<Coordinate, Distance>
    {
        public override bool TypeImplementsISubtractable => true;
    }

    public class CustomSubtractableClassWithPrimitiveLengthSpecializationTests : GenericSpecializerTests<Coordinate, long>
    {
        public override bool TypeImplementsISubtractable => true;
    }

    public class CustomSubtractableClassWithInvalidLengthSpecializationTests : GenericSpecializerTests<Coordinate, double>
    {
        public override bool TypeImplementsISubtractable => false;
    }
}
