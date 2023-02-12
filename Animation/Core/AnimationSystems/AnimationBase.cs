using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;

    private readonly Quadrant quadrant;
    private Particle particle;

    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc lass?
        //particle = new(25, 250, 25, quadrant);
        particles = new();

        Random random = new();
        for (int i = 0; i < 1; i++)
        {
            double x = 250; // random.NextDouble() * width * 2.0;
            double y = 250; // random.NextDouble() * height * 2.0;
            Particle p = new(x, y, 15, quadrant);

            particles.Add(p);
        }
    }

    public Particle Particle
    {
        get => particle;
        set => particle = value;
    }

    private List<Particle> particles;

    public List<Particle> Particles
    {
        get => particles;
        set => particles = value;
    }


    public Task Update()
    {
        return Task.Run(() =>
        {
            stopThread = false;

            Time.Start();
            while (!stopThread)
            {
                Time.Delta();

                fpsCounter.UpdateContent();

                foreach (Particle particle in particles)
                {
                    particle.Move();
                    particle.Boundary();
                }
#if DEBUG
                //for (int i = 0; i < 1_100_100; i++)
                //{
                //    // ToDo: Hack for simulating work during frames to prevent updating toooo fast an deltatime isnt exactly enough
                //}
                //Debug.WriteLine("Running");
#endif
            }

            Time.Reset();
        });

    }

    public void StopThread()
    {
        stopThread = true;
    }
}

