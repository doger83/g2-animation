using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    public static readonly Stopwatch stopwatch = new();
    public static readonly Stopwatch stopwatch2 = Stopwatch.StartNew();


    public static double Duration { get; set; }

    public static double DeltaTime
    {
        get
        {
            if (Duration == 0) // should not happen. but I want to be save!
            {
                return 0;
            }
            // The expression 1 / (1000 / x) can be simplified to x / 1000.
            // double fps = 1000.0 / Duration;        
            // double deltaTime = 1.0 / fps;          
            //         1000 
            return Duration / 1000;
        }
    }

    public static TimeSpan PrevousTime { get; internal set; }
    public static double DT { get; internal set; }

    public static void Start() => stopwatch.Start();

    public static void Reset() => stopwatch.Reset();

    public static void Restart() => stopwatch.Restart();

    public static void Stop() => stopwatch.Stop();


}

