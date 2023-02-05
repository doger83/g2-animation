using System;
using System.Diagnostics;

namespace g2.Datastructures.DesktopWPFUI;

public static class Time
{
    public static readonly Stopwatch stopwatch = new Stopwatch();


    //private static long timeSpan = stopwatch.ElapsedMilliseconds;
    //private static double fps = 1000 / timeSpan;
    //private static double deltaTime = 1 / fps;

    public static DateTime Start;
    public static DateTime End;
    public static double Duration { get { return (Start - End).TotalMilliseconds; } }

    public static TimeSpan ActualRenderingDuration;


    public static double duration;

    public static double DeltaTime 
    { 
        get 
        {
            if (duration == 0)
            {
                return 0;
            }

            //ActualRenderingTime = DateTime.Now;
            //LastRenderingDuration = LastRenderingTime - ActualRenderingTime;
            //RenderingTime = LastRenderingDuration.TotalMilliseconds;

            //if (RenderingTime == 0)
            //{
            //    return 0;
            //}
            //var fps = 1000.0 / RenderingTime;
            //var deltaTime = 1.0 / fps;

            //var timeSpan = stopwatch.ElapsedMilliseconds;

            //if (Duration == 0)
            //{
            //    return 0;
            //}
            var fps = 1000.0 / duration;
            var deltaTime = 1.0 / fps;

            
            return deltaTime; 
        } 
    }

    //public static void Start()
    //{
    //    stopwatch.Start();
    //}

    //public static void Reset()
    //{
    //    stopwatch.Reset();
    //}

    //public static void Restart()
    //{
    //    stopwatch.Restart();
    //}

    //public static void Stop()
    //{
    //    stopwatch.Stop();
    //}


}

