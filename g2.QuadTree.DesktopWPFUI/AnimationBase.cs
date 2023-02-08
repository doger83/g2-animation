using System.Threading.Tasks;
using System.Windows.Controls;

namespace g2.Animation.DesktopWPFUI;

public class AnimationBase
{

    private readonly FPSCounterViewModel fpsCounter;
    private bool stopThread;
    private Particle particle;

    public AnimationBase(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;

        particle = new(25, 250, 25, canvas);
    }

    public Particle Particle
    {
        get
        {
            return particle;
        }
        private set
        {
            particle = value;
        }
    }

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
                particle.Move();
                particle.Boundary();
                for (int i = 0; i < 1_000_000; i++)
                {
                            
                }
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

