using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace g2.Datastructures.DesktopWPFUI;

public class Animation
{
    private readonly Canvas canvas;
    private readonly FPSCounterViewModel fpsCounter;
    private Particle particle;

    public Animation(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        this.particle = new(25, 25, 25, canvas);
    }

    public Particle Particle { get { return particle; } private set { particle = value; } }

    public void Update()
    {


        while (true)
        {
            Time.Restart();

            fpsCounter.Draw();
            particle.Move();
            particle.Boundary();
            // ...
            //Thread.Sleep(10);
            Time.Stop();
            Time.Duration = TicksInMilliseconds();

        }
    }

    private static double TicksInMilliseconds()
    {
        // ToDo: What happens if timer still runs? 
        return (double)Time.stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
    }
}

