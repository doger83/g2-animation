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
        while (true)
        {
            Time.Delta();

            fpsCounter.Draw();
            particle.Move();
            particle.Boundary();


        }
    }
}

