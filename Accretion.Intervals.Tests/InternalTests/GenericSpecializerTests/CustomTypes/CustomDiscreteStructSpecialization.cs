using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class CustomDiscreteStructSpecialization : GenericSpecializerTests<Day>
    {
        public override bool TypeIsDiscrete => true;
        public override bool TypeIsAddable => false;
        public override bool TypeImplementsIDiscrete => true;
        public override bool TypeImplementsIAddable => false;
        public override bool TypeInstanceCanBeNull => false;
        public override bool DefaultTypeValueCannotBeIncremented => false;
        public override bool DefaultTypeValueCannotBeDecremented => true;
        public override Day ZeroValueOfThisType => throw new MemberAccessException();
    }
}
