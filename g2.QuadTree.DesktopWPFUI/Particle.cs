using g2.Datastructures.DesktopWPFUI;
using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace g2.Animation.DesktopWPFUI;

public class Particle
{
    // ToDo: Add Regions like WPF Samples
    private double x;
    private double y;
    private double radius;
    private double xSpeed = 1000;
    private double ySpeed = 0;
    private readonly Ellipse shape;
    private readonly Canvas canvas;

    public Particle(double x, double y, double radius, Canvas canvas)
    {
        this.x = x;
        this.y = y;
        // ToDo: Add Z for deepth calculations
        this.radius = radius;
        this.canvas = canvas;
        this.shape = ParticleShapes.CircleBasis(radius);
    }


    public double X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }
    public double Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
    public double XSpeed
    {
        get
        {
            return xSpeed;
        }
        set
        {
            xSpeed = value;
        }
    }
    public double YSpeed
    {
        get
        {
            return ySpeed;
        }
        set
        {
            ySpeed = value;
        }
    }
    public double Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
        }
    }
    public Ellipse Shape
    {
        get
        {
            return shape;
        }         
    }

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        x += xSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        y += ySpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime:G25}");
    }

    public void Boundary()
    {
        bool LeftBoundary   = x - radius < 0;
        bool RightBoundary  = x > canvas.ActualWidth - radius;
        bool TopBoundary    = y - radius < 0;
        bool BottomBoundary = y > canvas.ActualHeight - radius;
        
        if (LeftBoundary)
        {
            xSpeed *= -1;
            x = radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (RightBoundary)
        {
            xSpeed *= -1;
            x = canvas.ActualWidth - radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (TopBoundary)
        {
            ySpeed *= -1;
            y = radius;
            return;
        }

        if (BottomBoundary)
        {
            ySpeed *= -1;
            y = canvas.ActualHeight - radius;
            return;
        }
    }
}
