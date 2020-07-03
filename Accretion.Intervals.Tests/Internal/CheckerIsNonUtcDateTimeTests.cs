using FsCheck;
using FsCheck.Xunit;
using System;
using Xunit;

namespace Accretion.Intervals.Tests.Internal
{
    public class CheckerIsNonUtcDateTimeTests : TestsBase
    {
        [Property]
        public Property IsNonUtcDateTimeAgreesWithTheFrameworkImplementation(DateTime dateTime) =>
            (Checker.IsNonUtcDateTime(dateTime) == (dateTime.Kind != DateTimeKind.Utc)).ToProperty();

        [Fact]
        public void IsNonUtcDateTimeAlwaysReturnsFalseForTypesOtherThanDateTime()
        {
            Assert.False(Checker.IsNonUtcDateTime(new object()));
            Assert.False(Checker.IsNonUtcDateTime(string.Empty));
            Assert.False(Checker.IsNonUtcDateTime(0));
            Assert.False(Checker.IsNonUtcDateTime(0.0));
            Assert.False(Checker.IsNonUtcDateTime(new ValueClass(0)));
            Assert.False(Checker.IsNonUtcDateTime<DateTime?>(null));
            Assert.False(Checker.IsNonUtcDateTime<DateTime?>(new DateTime()));
        }
    }
}
