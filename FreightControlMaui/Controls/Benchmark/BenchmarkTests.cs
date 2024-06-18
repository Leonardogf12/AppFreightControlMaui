﻿using System.Diagnostics;

namespace FreightControlMaui.Controls.Benchmark
{
    public static class BenchmarkTests
    {
        public static void StartStopWatch(Stopwatch stopWatch) => stopWatch.Start();

        public static void StopWatchResult(Stopwatch stopWatch)
        {
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine($"*Runtime: {ts.Milliseconds}");
        }
    }

}

