using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double XSpeed { get; set; } = 500;
    public double YSpeed { get; set; } = 500;
    public double Radius { get; set; }
    public Ellipse Shape { get; private set; }

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        Random random = new();
        X += XSpeed * Time.DT; // xspeed; //random.Next(-5, 6);
        //Y += YSpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        //Boundary();
    }

    // ToDo: Maybe move this to UI to get rid of Canvas injection dependency
    public void Boundary()
    {
        if (X - Radius < 0)
        {
            XSpeed *= -1;
            X = Radius;

            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (X > canvas.ActualWidth - Radius)
        {
            XSpeed *= -1;
            X = canvas.ActualWidth - Radius;

            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (Y - Radius < 0)
        {
            YSpeed *= -1;
            Y = Radius;

        }

        if (Y > canvas.ActualHeight - Radius)
        {
            YSpeed *= -1;
            Y = canvas.ActualHeight - Radius;
        }
    }
}
