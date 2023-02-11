using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;

    private readonly Quadrant quadrant;
    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc lass?
        Particle = new(25, 250, 25, quadrant);
    }

    public Particle Particle { get; }

    public void Update()
    {
        stopThread = false;

        Time.Start();

        while (!stopThread)
        {
            Time.Delta();

            // ToDo: Make FPS Counter work
            fpsCounter.UpdateContent();
            Particle.Move();
            Particle.Boundary();
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

