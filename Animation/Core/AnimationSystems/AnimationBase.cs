using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;

    private readonly Quadrant quadrant;
    //private Particle particle;
    private List<Particle> particles;

    private bool stopThread;

    public List<Particle> Particles => particles;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc lass?
        //particle = new(25, 250, 25, quadrant);

        particles = new List<Particle>();

        Random random = new();

        for (int i = 0; i < 1000; i++)
        {
            double x = random.NextDouble() * width;
            double y = random.NextDouble() * height;
            Particle particle = new(x, y, 25, quadrant)
            {
                XSpeed = 1000,//(random.NextDouble() * 1000) - 500,
                YSpeed = 0//(random.NextDouble() * 1000) - 500
            };

            particles.Add(particle);
        }
    }

    //public Particle Particle
    //{
    //    get => particle;
    //    set => particle = value;
    //}


    public Task Update()
    {
        return Task.Run(() =>
        {
            Time.Start();

            stopThread = false;

            while (!stopThread)
            {
                Time.Delta();

                fpsCounter.UpdateContent();

                for (int i = 0; i < particles.Count; i++)
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

            Time.Reset();
        });

    }

    public void StopThread()
    {
        stopThread = true;
    }
}

