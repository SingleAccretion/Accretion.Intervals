using FsCheck;
using FsCheck.Xunit;
using System;
using Xunit;

namespace Accretion.Intervals.Tests.Internal
{
    public class CheckerIsUtcDateTimeTests
    {
        [Property]
        public Property IsUtcDateTimeAgreesWithTheFrameworkImplementation(DateTime dateTime) =>
            (Checker.IsUtcDateTime(dateTime) == (dateTime.Kind == DateTimeKind.Utc)).ToProperty();

        [Fact]
        public void IsUtcDateTimeAlwaysReturnsFalseForTypesOtherThanDateTime()
        {
            Assert.False(Checker.IsUtcDateTime(new object()));
            Assert.False(Checker.IsUtcDateTime(string.Empty));
            Assert.False(Checker.IsUtcDateTime(0));
            Assert.False(Checker.IsUtcDateTime(0.0));
            Assert.False(Checker.IsUtcDateTime(new ValueClass(0)));
            Assert.False(Checker.IsUtcDateTime<DateTime?>(null));
            Assert.False(Checker.IsUtcDateTime<DateTime?>(new DateTime()));
        }
    }
}
