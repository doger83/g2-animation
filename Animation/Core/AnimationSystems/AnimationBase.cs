using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;

namespace g2.Animation.Core.AnimationSystems;

public class AnimationBase
{
    private readonly FPSCounter fpsCounter;

    private readonly Quadrant quadrant;
    private readonly List<Particle> particles;

    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);

        // ToDo: Add Boundary for canvas maybe move chicking for boundaries in box like quadtree?  or BoundaryCheckc lass?

        particles = new List<Particle>();

        Random random = new();

        for (int i = 0; i < 3000; i++)
        {
            double x = random.NextDouble() * width;
            double y = random.NextDouble() * height;
            Particle particle = new(x, y, 5, quadrant)
            {
                XSpeed = 50,// (random.NextDouble() * 1000) - 500,
                YSpeed = 0// (random.NextDouble() * 1000) - 500
            };

            particles.Add(particle);
        }
    }

    public List<Particle> Particles => particles;

    public Task Update()
    {
        return Task.Run(() =>
        {
            Time.StartWatch();
            //Time.StartTimer(); // start the timer with an interval of 100 milliseconds
            //Time.TimerTick += DebugIt();
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
                //for (int i = 0; i < 1_100_100; i++)
                //{
                //    // ToDo: Hack for simulating work during frames to prevent updating toooo fast an deltatime isnt exactly enough
                //}
                //Debug.WriteLine("Running");
#endif
            }

            Time.ResetWatch();
        });

    }

    private static EventHandler<EventArgs> DebugIt()
    {
        return (sender, e) => Debug.WriteLine(DateTime.Now.Millisecond.ToString());
    }


    public void StopThread()
    {
        stopThread = true;
    }
}

