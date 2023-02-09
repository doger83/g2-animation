using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;
    private readonly Particle particle;
    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, Quadrant quadrant)
    {
        this.fpsCounter = fpsCounter;

        particle = new(25, 250, 25, quadrant);
    }

    public Particle Particle
    {
        get
        {
            return particle;
        }         
    }

    public void Update()
    {
        stopThread = false;

        Time.Start();

        while (!stopThread)
        {
            Time.Delta();

            fpsCounter.Draw();
            particle.Move();
            particle.Boundary();
            for (int i = 0; i < 1_000_000; i++)
            {

            }
            //Debug.WriteLine("Running");
        }

        Time.Reset();

    }

    public void StopThread()
    {
        stopThread = true;
    }
}

