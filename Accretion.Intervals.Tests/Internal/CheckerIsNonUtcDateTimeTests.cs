using FsCheck;
using FsCheck.Xunit;
using System;
using Xunit;

namespace Accretion.Intervals.Tests.Internal
{
    public class CheckerIsNonUtcDateTimeTests : TestsBase
    {
        [Property]
        public Property IsUtcDateTimeAgreesWithTheFrameworkImplementation(DateTime dateTime) =>
            (Checker.IsNonUtcDateTime(dateTime) == (dateTime.Kind != DateTimeKind.Utc)).ToProperty();

        [Fact]
        public void IsUtcDateTimeAlwaysReturnsFalseForTypesOtherThanDateTime()
        {
            Assert.True(Checker.IsNonUtcDateTime(new object()));
            Assert.True(Checker.IsNonUtcDateTime(string.Empty));
            Assert.True(Checker.IsNonUtcDateTime(0));
            Assert.True(Checker.IsNonUtcDateTime(0.0));
            Assert.True(Checker.IsNonUtcDateTime(new ValueClass(0)));
            Assert.True(Checker.IsNonUtcDateTime<DateTime?>(null));
            Assert.True(Checker.IsNonUtcDateTime<DateTime?>(new DateTime()));
        }
    }
}
