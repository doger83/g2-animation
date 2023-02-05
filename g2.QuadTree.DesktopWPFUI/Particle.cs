using g2.Quadtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace g2.Datastructures.DesktopWPFUI;

public struct Particle
{
    // ToDo: Add Regions like WPF Samples
    private readonly Canvas canvas;

    public Particle(double x, double y, double radius, Canvas canvas)
    {
        X = x;
        Y = y;
        Radius = radius;
        this.canvas = canvas;
        Shape = new()
        {
            Width = radius * 2,
            Height = radius * 2,
            Stroke = Brushes.Black,
            Fill = Brushes.DarkOrchid,
            StrokeThickness = 3,
        };
        Shape.SetValue(Canvas.TopProperty, Y - Radius);
        Shape.SetValue(Canvas.LeftProperty, X - Radius);
        canvas.Children.Add(Shape);
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double XSpeed { get; set; } = 50;
    public double YSpeed { get; set; } = 50;
    public double Radius { get; set; }

    //private readonly object shapelock = new object();
    //private Ellipse shape;
    public Ellipse Shape 
    {
        get;
        //{

        //    lock (shapelock)
        //    {
        //        return shape;
        //    }
            
        //} 
    }

    // ToDo: Maybe move this to UI to get rid of Canvas injection dependency
    public void Render()
    {
        //Canvas.SetLeft(Shape, X);
        //Canvas.SetTop(Shape, Y);


        Shape.SetValue(Canvas.TopProperty, Y - Radius);
        Shape.SetValue(Canvas.LeftProperty, X - Radius);

    }

    public void Move()
    {
        Random random = new();
        var de = Time.DeltaTime;
        X += XSpeed * de; // xspeed; //random.Next(-5, 6);
        Y += 0;// YSpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

    }
    public void Move(bool foo)
    {
        Random random = new();
        X += XSpeed * 0.01; // xspeed; //random.Next(-5, 6);
        Y += 0;// YSpeed * deltaTime; // yspeed; //random.Next(-5, 6);
    }

    // ToDo: put DeltaTime in a static class
    private double deltaTime = 0;
    private TimeSpan lastRenderingTimeSpan = TimeSpan.Zero;
    private DateTime lastRenderingTime = DateTime.Now;
    // ToDo: only count the time between frames without targetframerate to make pc speed differences no thing
    private const double TARGET_RENDERING_TIME = 1.0 / 100;

    public void Move(double delta)
    {

        TimeSpan renderingTimeSpan = DateTime.Now - lastRenderingTime;

        // Accumulate the delta time
        deltaTime += renderingTimeSpan.Subtract(lastRenderingTimeSpan).TotalMilliseconds;
        // Update the previous time
        lastRenderingTimeSpan = renderingTimeSpan;
        lastRenderingTime = DateTime.Now;

        // DoDO get rit of loop. Only mulstiply by deltatime in animations (like unity)
        // While there's enougha accumulated time to process a fixed step
        Random random = new();
        while (deltaTime >= TARGET_RENDERING_TIME)
        {
            X += XSpeed * TARGET_RENDERING_TIME; // xspeed; //random.Next(-5, 6);
            Y += 0;// YSpeed * deltaTime; // yspeed; //random.Next(-5, 6);
          
            deltaTime -= TARGET_RENDERING_TIME;
        }
        deltaTime = 0;
    }

    // ToDo: Maybe move this to UI to get rid of Canvas injection dependency
    public void Boundary()
    {
        if (X > canvas.ActualWidth - Radius || X - Radius < 0)
        {
            XSpeed *= -1;
        }

        if (Y > canvas.ActualHeight - Radius || Y - Radius < 0)
        {
            YSpeed *= -1;
        }

    }
}
