using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Accretion.Profiling
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            Inli("s");
            Ninli("h");
        }

        public static int Inli(string f)
        {
            return int.Parse(f);
        }

        public static int Ninli(string f)
        {
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            GC.Collect();
            return int.Parse(f);
        }
    }
}
