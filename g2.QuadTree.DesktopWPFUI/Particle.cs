using g2.Datastructures.DesktopWPFUI;
using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace g2.Animation.DesktopWPFUI;

public class Particle
{
    // ToDo: Add Regions like WPF Samples
    private readonly Canvas canvas;
    private double x;
    private double y;
    private double xSpeed = 50;
    private double ySpeed = 0;
    private double radius;
    private Ellipse shape;

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
        set
        {
            shape = value;
        }
    }

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        Random random = new();
        x += xSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        y += ySpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime:G25}");
    }

    public void Boundary()
    {
        if (x - radius < 0)
        {
            xSpeed *= -1;
            x = radius;

            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (x > canvas.ActualWidth - radius)
        {
            xSpeed *= -1;
            x = canvas.ActualWidth - radius;

            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (y - radius < 0)
        {
            ySpeed *= -1;
            y = radius;

        }

        if (y > canvas.ActualHeight - radius)
        {
            ySpeed *= -1;
            y = canvas.ActualHeight - radius;
        }
    }
}
