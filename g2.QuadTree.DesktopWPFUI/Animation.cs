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
    public Particle particle;

    public Animation(FPSCounterViewModel fpsCounter, Canvas canvas)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        this.particle = new(50, 50, 30, canvas);
    }


    // ToDo: put DeltaTime in a static class
    private double deltaTime = 0;
    private TimeSpan lastRenderingTimeSpan = TimeSpan.Zero;
    private DateTime lastRenderingTime = DateTime.Now;
    // ToDo: only count the time between frames without targetframerate to make pc speed differences no thing
    private const double TARGET_RENDERING_TIME = 1.0 / 100;
    public void Update()
    {
        while (true)
        {

            Time.stopwatch.Restart();
          
            fpsCounter.Draw();
            particle.Move();
            particle.Boundary();

            //Thread.Sleep(1);


            Time.stopwatch.Stop();
            //Time.duration = Time.stopwatch.ElapsedMilliseconds;
            long ticks = Time.stopwatch.ElapsedTicks;
            Time.duration = (double)ticks / (double)Stopwatch.Frequency * 1000.0;                 
            
        }
    }
}

