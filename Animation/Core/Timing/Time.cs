using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace g2.Animation.Core.Timing;

public static class Time
{
    private static Stopwatch? watch;
    private static double deltaTime;
    private static long previousTicks;
    private static Timer? timer;

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
        get => deltaTime;
        private set => deltaTime = value;
    }

    public static void ResetWatch()
    {
        watch = null;
    }

    public static void StartWatch()
    {
        if (watch == null)
        {
            watch = new();
            watch.Start();
            previousTicks = watch.ElapsedTicks;
        }
    }


    public static event EventHandler<EventArgs>? TimerTick;

    private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        TimerTick?.Invoke(sender, e);
    }

    public static void StartTimer(double interval)
    {
        timer = new Timer(interval);
        timer.Elapsed += OnTimerElapsed!;
        timer.AutoReset = true;
        timer.Enabled = true;
    }


    public static void StopTimer()
    {
        if (timer != null)
        {
            timer.Enabled = false;
            timer = null;
        }
    }
}

