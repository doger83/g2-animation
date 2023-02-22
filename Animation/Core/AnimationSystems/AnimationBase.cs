using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;

namespace g2.Animation.Core.AnimationSystems;
// ToDo: Add Boundary for canvas maybe move checking for boundaries in box like quadtree?  or BoundaryCheckc class?  efficiant boundary checks (k d tree?)

public class AnimationBase
{
    private const int PARTICLESCOUNT = 1000;

    private readonly FPSCounter fpsCounter;
    private readonly Quadrant quadrant;
    private readonly Particle[] particles;

    private bool stopThread;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        particles = new Particle[PARTICLESCOUNT];

        Random random = new();

        for (int i = 0; i < PARTICLESCOUNT; i++)
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

    public Particle[] Particles
    {
        get
        {
            return particles;
        }
    }

    public void FixedUpdate()
    {
        Time.StartWatch();
        Time.TimerTick += fixedUpdate;
        Time.StartTimer(20);
    }

    private void fixedUpdate(object? sender, EventArgs e)
    {
        Time.Delta();
        Debug.WriteLine($"FixedUpdate: {DateTime.Now:O} \t Detlatatime: {Time.DeltaTime}");
    }

    //private async Task fixedUpdate()
    //{
    //    Time.StartWatch();

    //    //while (await Time.timer.WaitForNextTickAsync())
    //    //{
    //    //    //Time.FixedDelta();
    //    //    Time.Delta();
    //    //    //fpsCounter.Update();
    //    //    Debug.WriteLine($"FixedUpdate: {DateTime.Now:O} \t Detlatatime: {Time.DeltaTime}");
    //    //}
    //}

    public Task Update()
    {
        return Task.Run(() =>
        {
            Time.StartWatch();

            stopThread = false;

            while (!stopThread)
            {
                Time.Delta();

                fpsCounter.Update();

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

