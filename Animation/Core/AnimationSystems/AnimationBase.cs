using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;
    private readonly Quadrant quadrant;
    private readonly Particle[] particles;
    private int particlesCount = 5000;

    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);

        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc class?  efficiant boundary checks (k d tree?)


        particles = new Particle[particlesCount];

        Random random = new();

        for (int i = 0; i < particlesCount; i++)
        {
            double x = random.NextDouble() * width;
            double y = random.NextDouble() * height;
            Particle particle = new(x, y, 2, quadrant)
            {
                //Speed = new Vector2D((random.NextDouble() * 150) - 75, (random.NextDouble() * 150) - 75)

                Speed = new Vector2D(50, 0)
            };

            particles[i] = particle;
        }
    }

    public Particle[] Particles => particles;

    public Task Update()
    {
        // ToDo: Make tis a thread? return?
        return Task.Run(() =>
        {
            Time.StartWatch();

            stopThread = false;

            while (!stopThread)
            {
                Time.Delta();

                fpsCounter.UpdateContent();

                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].Move();
                    particles[i].Boundary();
                }

                //Debug.WriteLine("---------------------------------");
#if DEBUG
                for (int i = 0; i < 1_100_100; i++)
                {
                    // ToDo: Hack for simulating work during frames to prevent updating toooo fast an deltatime isnt exactly enough
                }
                //Debug.WriteLine("Running");
#endif
            }

            Time.ResetWatch();
        });
    }

    public void StopThread()
    {
        stopThread = true;
    }
}

