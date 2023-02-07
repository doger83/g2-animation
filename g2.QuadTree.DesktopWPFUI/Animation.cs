using System.Windows.Controls;

namespace g2.Datastructures.DesktopWPFUI;

public class Animation
{
    private readonly Canvas canvas;
    private readonly FPSCounterViewModel fpsCounter;
    private bool stopThread;

    public Animation(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        Particle = new(25, 250, 25, canvas);
    }

    public Particle Particle { get; private set; }

    public void Update()
    {
        stopThread = false;

        //await Task.Run(() =>
        //{
        Time.Start();

        while (!stopThread)
        {
            Time.Delta();

            fpsCounter.Draw();
            Particle.Move();
            Particle.Boundary();
            //Debug.WriteLine("Running");
        }

        Time.Reset();

        //});


    }
    public void StopThread() => stopThread = true;
}

