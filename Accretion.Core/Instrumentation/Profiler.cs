using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Accretion.Core
{
    public static class Profiler
    {
        private static readonly Stack<(Stopwatch Watch, string ActionName, BenchmarkingMode BenchmarkingMode)> _stopwatches = new Stack<(Stopwatch, string, BenchmarkingMode)>();
        private static readonly Dictionary<string, (int NumberOfRuns, double AverageTimeInTicks)> _multipleRunsResults = new Dictionary<string, (int NumberOfIterations, double AverageTimeInTicks)>();

        static Profiler()
        {
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        public static void StartProfiling(string actionName, BenchmarkingMode benchmarkingMode = BenchmarkingMode.MultipleRuns)
        {
            var watch = new Stopwatch();
            _stopwatches.Push((watch, actionName, benchmarkingMode));

            watch.Start();
        }

        public static void EndProfiling(UnitOfTime unitOfTime)
        {
            _stopwatches.Peek().Watch.Stop();
            var (watch, actionName, benchmarkingMode) = _stopwatches.Pop();

            if (benchmarkingMode == BenchmarkingMode.MultipleRuns)
            {
                if (!_multipleRunsResults.ContainsKey(actionName))
                {
                    _multipleRunsResults.Add(actionName, (1, watch.ElapsedTicks));
                }
                else
                {
                    var (numberOfRuns, averageTimeInTicks) = _multipleRunsResults[actionName];
                    var newAverage = (averageTimeInTicks * numberOfRuns + watch.ElapsedTicks) / (numberOfRuns + 1);

                    _multipleRunsResults[actionName] = (numberOfRuns + 1, newAverage);
                }

                Console.WriteLine($"On average, it took {ConvertTicksToUnitsOfTime(_multipleRunsResults[actionName].AverageTimeInTicks, unitOfTime)} {unitOfTime.ToString().ToLower()}s to complete {actionName}");
            }
            else if (benchmarkingMode == BenchmarkingMode.SingleRun)
            {
                Console.WriteLine($"It took {ConvertTicksToUnitsOfTime(watch.ElapsedTicks, unitOfTime)} {unitOfTime.ToString().ToLower()}s to complete {actionName}");
            }
        }

        private static long ConvertTicksToUnitsOfTime(double ticks, UnitOfTime unitOfTime)
        {
            return (long)((long)unitOfTime * ticks / Stopwatch.Frequency);
        }
    }
}
