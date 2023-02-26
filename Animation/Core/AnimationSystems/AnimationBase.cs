using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;
using System.Timers;

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
            double x = random.NextDouble() * width - 10;
            double y = random.NextDouble() * height - 10;

            Particle particle = new(x, y, 2, 2, quadrant)
            {
                //Speed = new Vector2D((random.NextDouble() * 150) - 75, (random.NextDouble() * 150) - 75)

                Velocity = new Vector2D(100, 100),
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

    public async Task Loop()
    {
        //await Update();
        await FixedUpdate();
    }

    private async Task FixedUpdate()
    {
        await PeriodicUpdate();
    }

    private async Task PeriodicUpdate()
    {
        fixedUpdateRunning = true;
        Time.StarPeriodicTimer(1 / 50.0);

        await Task.Run((Func<Task?>)(async () =>
        {
            while (Time.PeriodicTimer is not null && fixedUpdateRunning && await Time.PeriodicTimer.WaitForNextTickAsync())
            {
                Time.FixedDelta();
                fpsCounter.Update();
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].FixedUpdate();
                    particles[i].CheckBoundaries();
                }

                FixedUpdateComplete?.Invoke((object?)null, EventArgs.Empty);
                //Debug.WriteLine($"FixedUpdate: {DateTime.Now:O} \t FixedDetlatatime: {Time.FixedDeltaTime:G35}");
            }
        }));
    }

    private async Task Update()
    {
        updateRunning = true;

        await Task.Run(() =>
        {
            while (updateRunning)
            {
                Time.Delta();

                fpsCounter.Update();

                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].Update();
                    particles[i].CheckBoundaries();
                }

                //Debug.WriteLine("---------------------------------");
#if DEBUG
                for (int i = 0; i < 1_100_100; i++)
                {
                    // ToDo: Hack for simulating work during frames to prevent updating toooo fast an deltatime isnt exactly enough
                }
                //Debug.WriteLine("Running");
#endif
                FixedUpdateComplete?.Invoke((object?)null, EventArgs.Empty);

            }
        });
    }

    public void Pause()
    {
        updateRunning = false;
        fixedUpdateRunning = false;
    }

    public event Func<object?, EventArgs, Task>? FixedUpdateComplete;

}

