using g2.Animation.Core._EventArgs;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace g2.Animation.Core.Timing;

public static class Time
{
    private static Stopwatch? watch;
    private static Timer? systemTimer;
    private static PeriodicTimer? periodicTimer;

    private static double deltaTime;
    private static long previousUpdateTicks;
    private static long actualUpdateTicks;

    private static double fixedDeltaTime;
    private static long previousFixedUpdateTicks;
    private static long actualFixedUpdateTicks;

    public static void Delta()
    {
        actualUpdateTicks = watch!.ElapsedTicks;
        deltaTime = (double)(actualUpdateTicks - previousUpdateTicks) / Stopwatch.Frequency;
        previousUpdateTicks = actualUpdateTicks;
    }

    internal static void FixedDelta()
    {
        actualFixedUpdateTicks = watch!.ElapsedTicks;
        fixedDeltaTime = (double)(actualFixedUpdateTicks - previousFixedUpdateTicks) / Stopwatch.Frequency;
        previousFixedUpdateTicks = actualFixedUpdateTicks;
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

    public static void Start()
    {
        StartWatch(); ;
        //StartSystemTimer(1000);
    }

    public static void Reset()
    {
        watch = null;
        periodicTimer = null;
        systemTimer = null;
    }

    private static void StartWatch()
    {
        (watch ??= new()).Start();
        previousUpdateTicks = watch!.ElapsedTicks;
        previousFixedUpdateTicks = previousUpdateTicks;
    }

    public static void StartSystemTimer(float interval)
    {
        systemTimer ??= new(interval);
        //timer.UseHighPriorityThread = false;
        systemTimer.Elapsed += OnTimerElapsed;
        systemTimer.AutoReset = true;
        systemTimer.Enabled = true;
        systemTimer.Start();
    }

    public static PeriodicTimer PeriodicTimer { get { return periodicTimer!; } }


    public static void StarPeriodicTimer(float interval)
    {
        periodicTimer ??= new(TimeSpan.FromMilliseconds(interval));
    }

    public static event EventHandler<EventArgs>? TimerTick;

    private static void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        TimerTick?.Invoke(sender, e);
    }

    private static void OnTimerElapsed(object? sender, HiResTimerElapsedEventArgs e)
    {
        TimerTick?.Invoke(sender, e);
    }

    private static void OnTimerElapsed(object? sender, HighResolutionTimerElapsedEventArgs e)
    {
        TimerTick?.Invoke(sender, e);
    }
}

