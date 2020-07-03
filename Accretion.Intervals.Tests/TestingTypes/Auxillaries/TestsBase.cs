using FsCheck.Xunit;

namespace Accretion.Intervals.Tests
{
    [Properties(Arbitrary = new[] { typeof(Arbitrary) }, StartSize = int.MaxValue, EndSize = int.MaxValue, MaxFail = 1200, Verbose = true)]
    public abstract class TestsBase
    {
    }
}
