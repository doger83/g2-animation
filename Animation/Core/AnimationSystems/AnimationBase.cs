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

        for (int i = 0; i < 5; i++)
        {
            particles.Add(new Particle(26, 50 + (i * 100), 25, quadrant));
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

