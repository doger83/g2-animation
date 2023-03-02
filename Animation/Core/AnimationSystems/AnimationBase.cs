using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;

namespace g2.Animation.Core.AnimationSystems;
// ToDo: Add Boundary for canvas maybe move checking for boundaries in box like quadtree?  or BoundaryCheckc class?  efficiant boundary checks (k d tree?)

public class AnimationBase
{
    private const int PARTICLESCOUNT = 5000;

    private readonly FPSCounter fpsCounter;
    private readonly Quadrant quadrant;
    private readonly Particle[] particles;

    private bool updateRunning;
    private bool fixedUpdateRunning;

    public AnimationBase(FPSCounter fpsCounter, double width, double height)
    {
        this.fpsCounter = fpsCounter;
        quadrant = new(0, 0, width, height);
        particles = new Particle[PARTICLESCOUNT];

        Random random = new();

        for (int i = 0; i < PARTICLESCOUNT; i++)
        {
            double x = 275;// random.NextDouble() * width - 10;
            double y = random.NextDouble() * height - 10;

            Particle particle = new(x, y, 2, 2, quadrant)
            {
                //Speed = new Vector2D((random.NextDouble() * 150) - 75, (random.NextDouble() * 150) - 75)

                Velocity = new Vector2D(100, 0),
                Acceleration = new Vector2D(0, 0)
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

    public Task Loop()
    {
        return Task.Run(() =>
        {
            // ToDo: remove discard an only return completetd if both returned completed?
            _ = Update();
            _ = FixedUpdate();
        });
    }

    private Task Update()
    {
        updateRunning = true;

        return Task.Run(() =>
        {
            while (updateRunning)
            {
                Time.Delta();

                fpsCounter.Update();



                //Debug.WriteLine($"Update: {DateTime.Now:O} \t Detlatatime: {Time.DeltaTime:G65}");

                //_ = (UpdateComplete?.Invoke(null, EventArgs.Empty));
            }
        });
    }

    private Task FixedUpdate()
    {
        fixedUpdateRunning = true;
        Time.StarPeriodicTimer(1 / 50.0);

        return Task.Run(async () =>
        {
            while (fixedUpdateRunning && Time.PeriodicTimer is not null && await Time.PeriodicTimer.WaitForNextTickAsync())
            {
                Time.FixedDelta();
                fpsCounter.FixedUpdate();

                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].FixedUpdate();
                    particles[i].CheckBoundaries();
                }
                //Debug.WriteLine($"FixedUpdate: {DateTime.Now:O} \t FixedDetlatatime: {Time.FixedDeltaTime:G65}");

                _ = (FixedUpdateComplete?.Invoke(null, EventArgs.Empty));
                //Debug.WriteLine($"FixedUpdate: {DateTime.Now:O} \t FixedDetlatatime: {Time.FixedDeltaTime:G35}");
            }
        });
    }

    public void Pause()
    {
        updateRunning = false;
        fixedUpdateRunning = false;
    }

    public event Func<object?, EventArgs, Task>? FixedUpdateComplete;
    public event Func<object?, EventArgs, Task>? UpdateComplete;

}

