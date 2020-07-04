using FsCheck;
using FsCheck.Xunit;

namespace Accretion.Intervals.Tests
{
    [Properties(Arbitrary = new[] { typeof(Arbitrary) }, MaxFail = 1200, Verbose = true)]
    public abstract class TestsBase { }
}
