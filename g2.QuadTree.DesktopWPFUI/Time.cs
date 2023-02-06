using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    private static TimeSpan previous;
    private static readonly Stopwatch watch = Stopwatch.StartNew();
    public static double DeltaTime { get; private set; }

    public static void Delta()
    {
        DeltaTime = (watch.Elapsed - previous).TotalSeconds;
        previous = watch.Elapsed;
    }

    private static double TotalTicksInMilliseconds() =>
        watch.ElapsedTicks / Stopwatch.Frequency * 1000.0;
}

