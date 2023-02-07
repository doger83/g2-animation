using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    private static TimeSpan previous;
    private static Stopwatch? watch;
    public static double DeltaTime { get; private set; }

    public static void Delta()
    {


        //DeltaTime = ((watch ??= Stopwatch.StartNew()).Elapsed - previous).TotalSeconds;
        DeltaTime = (watch!.Elapsed - previous).TotalSeconds;
        previous = watch.Elapsed;
    }

    private static double? TotalTicksInMilliseconds() =>
        watch?.ElapsedTicks / Stopwatch.Frequency * 1000.0;

    internal static void Reset() => watch = null; // (watch ??= new()).Reset();
    internal static void Start()
    {
        if (watch == null)
        {
            watch = new();
            previous = watch.Elapsed;
            watch.Start();
        }
    }
}

