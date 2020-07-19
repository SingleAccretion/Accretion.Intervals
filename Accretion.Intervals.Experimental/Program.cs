using System;
using System.IO;
using System.Text;
using Accretion.Intervals.Tests;
using FsCheck;

namespace Accretion.Intervals
{
    public static class Program
    {
        static Program() { Console.OutputEncoding = Encoding.UTF8; }

        private static unsafe void Main()
        {
            Arb.Register(typeof(Arbitrary));

            var writer = new StreamWriter("intervals.txt");

            //var i = Interval<ValueClass>.Parse("");

            writer.Dispose();
        }
    }
}


