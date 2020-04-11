﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Intervals.Tests
{
    public class SByteSpecializationTests : GenericSpecializerTests<sbyte>
    {
        public override bool TypeIsDiscrete => false;
        public override bool TypeIsAddable => true;
        public override bool TypeImplementsIDiscrete => false;
        public override bool TypeImplementsIAddable => false;
        public override bool TypeInstanceCanBeNull => false;
        public override bool DefaultTypeValueCannotBeIncremented => true;
        public override bool DefaultTypeValueCannotBeDecremented => true;
        public override sbyte ZeroValueOfThisType => 0;
    }
}