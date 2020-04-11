using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Accretion.Intervals.Tests
{
    public abstract class GenericSpecializerTests<T>
    {
        public abstract bool TypeIsDiscrete { get; }
        public abstract bool TypeIsAddable { get; }
        public abstract bool TypeImplementsIDiscrete { get; }
        public abstract bool TypeImplementsIAddable { get; }
        public abstract bool TypeInstanceCanBeNull { get; }
        public abstract bool DefaultTypeValueCannotBeIncremented { get; }
        public abstract bool DefaultTypeValueCannotBeDecremented { get; }
        public abstract T ZeroValueOfThisType { get; }

        [Fact]
        public void TestTypeIsDiscrete() => Assert.Equal(TypeIsDiscrete, GenericSpecializer<T>.TypeIsDiscrete);
        [Fact]
        public void TestTypeIsAddable() => Assert.Equal(TypeIsAddable, GenericSpecializer<T>.TypeIsAddable);
        [Fact]
        public void TestImplementsIDiscrete() => Assert.Equal(TypeImplementsIDiscrete, GenericSpecializer<T>.TypeImplementsIDiscrete);
        [Fact]
        public void TestImplementsIAddable() => Assert.Equal(TypeImplementsIAddable, GenericSpecializer<T>.TypeImplementsIAddable);
        [Fact]
        public void TestTypeInstanceCanBeNull() => Assert.Equal(TypeInstanceCanBeNull, GenericSpecializer<T>.TypeInstanceCanBeNull);
        [Fact]
        public void TestDefaultTypeValueCannotBeIncremented() => Assert.Equal(DefaultTypeValueCannotBeIncremented, GenericSpecializer<T>.DefaultTypeValueCannotBeIncremented);
        [Fact]
        public void TestDefaultTypeValueCannotBeDecremented() => Assert.Equal(DefaultTypeValueCannotBeDecremented, GenericSpecializer<T>.DefaultTypeValueCannotBeDecremented);
        [Fact]
        public void TestZeroValueOfThisType()
        {
            if (TypeIsAddable)
            {
                Assert.Equal(ZeroValueOfThisType, GenericSpecializer<T>.ZeroValueOfThisType);
            }
            else
            {
                Assert.Throws<MemberAccessException>(() => GenericSpecializer<T>.ZeroValueOfThisType);
            }
        }
    }

    public abstract class GenericSpecializerTests<T, R>
    {
        public abstract bool TypeImplementsISubtractable { get; }

        [Fact]
        public void TestTypeImplementsISubtractable() => Assert.Equal(TypeImplementsISubtractable, GenericSpecializer<T, R>.TypeImplementsISubtractable);
    }
}
