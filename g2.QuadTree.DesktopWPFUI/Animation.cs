using System.Diagnostics;
using System.Windows.Controls;

namespace g2.Datastructures.DesktopWPFUI;

public class Animation
{
    private readonly Canvas canvas;
    private readonly FPSCounterViewModel fpsCounter;
    private Particle particle;

    public Animation(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        particle = new(25, 25, 25, canvas);
    }

    public Particle Particle { get => particle; private set => particle = value; }

    public void Update()
    {

        //Time.PrevousTime = Time.stopwatch2.Elapsed;
        while (true)
        {
            Time.DT = (Time.stopwatch2.Elapsed - Time.PrevousTime).TotalSeconds;
            Time.PrevousTime = Time.stopwatch2.Elapsed;
            //Time.Restart();

            fpsCounter.Draw();
            particle.Move();
            particle.Boundary();
            // ...
            //Thread.Sleep(10);
            //Debug.WriteLine($"old {Time.Duration} | {Time.DeltaTime}");
            //Debug.WriteLine($"new {Time.PrevousTime} | {Time.DT:G35}");
            //Time.Stop();
            //Time.Duration = TicksInMilliseconds();

        }
    }

    private static double TicksInMilliseconds() =>
        // ToDo: What happens if timer still runs? 
        (double)Time.stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
}

