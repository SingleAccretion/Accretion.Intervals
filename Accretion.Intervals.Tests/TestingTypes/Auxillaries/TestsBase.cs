using FsCheck.Xunit;

namespace Accretion.Intervals.Tests
{
    [Properties(Arbitrary = new[] { typeof(Arbitrary) }, StartSize = int.MaxValue, EndSize = int.MaxValue, Verbose = true)]
    public abstract class TestsBase
    {
    }
}
