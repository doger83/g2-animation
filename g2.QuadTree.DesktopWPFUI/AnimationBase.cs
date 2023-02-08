using System.Threading.Tasks;
using System.Windows.Controls;

namespace g2.Animation.DesktopWPFUI;

public class AnimationBase
{

    private readonly FPSCounterViewModel fpsCounter;
    private bool stopThread;

    public AnimationBase(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;

        Particle = new(25, 250, 25, canvas);
    }

    public Particle Particle { get; private set; }

    public void RunUpdate()
    {
        stopThread = false;

        _ = Task.Run(() =>
        {
            Time.Start();

            while (!stopThread)
            {
                Time.Delta();

                fpsCounter.Draw();
                Particle.Move();
                Particle.Boundary();
                //Debug.WriteLine("Running");
            }

            Time.Reset();

        });


    }
    public void StopThread()
    {
        stopThread = true;
    }
}

