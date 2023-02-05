using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    public static readonly Stopwatch stopwatch = new Stopwatch();


    public static double Duration { get; set; }

    public static double DeltaTime 
    { 
        get 
        {
            if (Duration == 0)
            {
                return 0;
            }
            
            var fps = 1000.0 / Duration;
            var deltaTime = 1.0 / fps;
            
            return deltaTime; 
        } 
    }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static void Reset()
    {
        stopwatch.Reset();
    }

    public static void Restart()
    {
        stopwatch.Restart();
    }

    public static void Stop()
    {
        stopwatch.Stop();
    }


}

