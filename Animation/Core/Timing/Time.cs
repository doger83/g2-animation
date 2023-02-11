using System.Diagnostics;

namespace g2.Animation.Core.Timing;

public static class Time
{
    private static Stopwatch? watch;
    private static long previousTicks;

    public static void Delta()
    {
        DeltaTime = (double)(watch!.ElapsedTicks - previousTicks) / Stopwatch.Frequency;
        previousTicks = watch.ElapsedTicks;
    }

    private static double? TotalTicksInMilliseconds()
    {
        return watch?.ElapsedTicks / Stopwatch.Frequency * 1000.0;
    }

    public static double DeltaTime { get; private set; }

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

