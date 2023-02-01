using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    private static readonly Stopwatch stopwatch = new Stopwatch();
    public static double DeltaTime()
    {
        double deltaTime = stopwatch.Elapsed.TotalSeconds;
        stopwatch.Restart();
        return deltaTime;
    }
}

