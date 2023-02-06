using System.Windows.Controls;

namespace g2.Datastructures.DesktopWPFUI;

public class Animation
{
    private readonly Canvas canvas;
    private readonly FPSCounterViewModel fpsCounter;

    public Animation(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        Particle = new(25, 250, 25, canvas);
    }

    public Particle Particle { get; private set; }

    public void Update()
    {
        while (true)
        {
            Time.Delta();

            fpsCounter.Draw();
            Particle.Move();
            Particle.Boundary();
        }
    }
}

