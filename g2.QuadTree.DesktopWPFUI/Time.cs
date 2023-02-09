using System;
using System.Diagnostics;

namespace g2.Animation.DesktopWPFUI;

public static class Time
{
    private static Stopwatch? watch;
    private static double deltaTime;
    private static long previousTicks;


    public static void Delta()
    {
        deltaTime = (double)(watch!.ElapsedTicks - previousTicks) / Stopwatch.Frequency;
        previousTicks = watch.ElapsedTicks;
    }

    private static double? TotalTicksInMilliseconds()
    {
        return watch?.ElapsedTicks / Stopwatch.Frequency * 1000.0;
    }

    public static double DeltaTime
    {
        get
        {
            return deltaTime;
        }
        private set
        {
            deltaTime = value;
        }
    }

    public static void Reset()
    {
        watch = null;
    }

    public static void Start()
    {
        if (watch == null)
        {
            watch = new();
            watch.Start();
            previousTicks = watch.ElapsedTicks;
        }
    }
}

