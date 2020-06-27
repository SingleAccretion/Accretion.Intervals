using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class CustomDiscreteClassSpecializationTests : GenericSpecializerTests<Coordinate>
    {
        public override bool TypeIsDiscrete => true;
        public override bool TypeIsAddable => false;
        public override bool TypeImplementsIDiscrete => true;
        public override bool TypeImplementsIAddable => false;
        public override bool TypeInstanceCanBeNull => true;
        public override bool DefaultTypeValueCannotBeIncremented => true;
        public override bool DefaultTypeValueCannotBeDecremented => true;
        public override Coordinate ZeroValueOfThisType => throw new MemberAccessException();
    }
}
