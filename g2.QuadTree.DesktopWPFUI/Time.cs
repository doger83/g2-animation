using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    private static TimeSpan prevousTime;
    private static readonly Stopwatch stopwatch = Stopwatch.StartNew();
    public static double DeltaTime { get; private set; }

    public static void Delta()
    {
        DeltaTime = (stopwatch.Elapsed - prevousTime).TotalSeconds;
        prevousTime = stopwatch.Elapsed;
    }

    private static double TotalTicksInMilliseconds() =>

        (double)stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
}

