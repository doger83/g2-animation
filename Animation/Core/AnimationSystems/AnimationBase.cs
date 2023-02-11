using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;

    private readonly Quadrant quadrant;
    private readonly Particle particle;

    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc lass?
        particle = new(25, 250, 25, quadrant);
    }

    public Particle Particle => particle;

    public void Update()
    {
        stopThread = false;

        Time.Start();

        while (!stopThread)
        {
            Time.Delta();

            fpsCounter.UpdateContent();
            particle.Move();
            particle.Boundary();
            for (int i = 0; i < 1_100_100; i++)
            {
                // ToDo: Hack for simulating work during frames to prevent updating toooo fast an deltatime isnt exactly enough
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

