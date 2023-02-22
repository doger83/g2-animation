using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace g2.Animation.Core.Timing;

public static class Time
{
    private static HighResolutionTimer? timer;
    private static Stopwatch? watch;
    private static double deltaTime;
    private static long previousTicks;

    private static double fixedDeltaTime;
    private static DateTime previousFixedUpdate;
    private static DateTime actualFixedUpdate;

    public static void Delta()
    {
        deltaTime = (double)(watch!.ElapsedTicks - previousTicks) / Stopwatch.Frequency;
        previousTicks = watch.ElapsedTicks;
    }

    internal static void FixedDelta()
    {
        actualFixedUpdate = DateTime.Now;
        fixedDeltaTime = (actualFixedUpdate - previousFixedUpdate).TotalSeconds;
        previousFixedUpdate = actualFixedUpdate;
    }

    private static double? TotalTicksInMilliseconds()
    {
        return watch?.ElapsedTicks / Stopwatch.Frequency * 1000.0;
    }
    public static double FixedDeltaTime
    {
        get
        {
            return fixedDeltaTime;
        }

        private set
        {
            fixedDeltaTime = value;
        }
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

    public static void ResetWatch()
    {
        watch = null;
    }

    public static void StartWatch()
    {
        (watch ??= new()).Start(); ;

        previousTicks = watch.ElapsedTicks;
    }

    public static event EventHandler<EventArgs>? TimerTick;

    //private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
    //{
    //    TimerTick?.Invoke(sender, e);
    //}

    public static void StartTimer(float interval)
    {
        timer ??= new(interval);
        timer.UseHighPriorityThread = false;
        timer.Elapsed += OnTimerElapsed;
        timer.Start();

        //timer.AutoReset = true;
        //timer.Enabled = true;
    }

    private static void OnTimerElapsed(object? sender, HighResolutionTimerElapsedEventArgs e)
    {
        TimerTick?.Invoke(sender, e);
    }

    //public static void StopTimer()
    //{
    //    if (Timer != null)
    //    {
    //        Timer.Elapsed -= OnTimerElapsed!;
    //        Timer.Enabled = false;
    //        Timer.Dispose();
    //        Timer = null;
    //    }
    //}
}

