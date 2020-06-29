using FsCheck;

namespace Accretion.Intervals.Tests
{
    public abstract class TestBase
    {
        static TestBase()
        {
            Arb.Register<Generators>();
        }
    }
}