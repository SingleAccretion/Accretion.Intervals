using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class CharSpecializationTests : GenericSpecializerTests<char>
    {
        public override bool TypeIsDiscrete => true;
        public override bool TypeIsAddable => false;
        public override bool TypeImplementsIDiscrete => false;
        public override bool TypeImplementsIAddable => false;
        public override bool TypeInstanceCanBeNull => false;
        public override bool DefaultTypeValueCannotBeIncremented => false;
        public override bool DefaultTypeValueCannotBeDecremented => true;
        public override char ZeroValueOfThisType => throw new MemberAccessException();
    }
}
