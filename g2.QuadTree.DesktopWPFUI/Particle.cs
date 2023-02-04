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
    private readonly Canvas canvas;

    public Particle(double x, double y, double radius, Canvas canvas)
    {
        X = x;
        Y = y;
        Radius = radius;
        this.canvas = canvas;
        Shape = new ()
        {
            Width = radius * 2,
            Height = radius * 2,
            Stroke = Brushes.Aqua,
            Fill = Brushes.Beige,
            StrokeThickness = 3,
        };
        canvas.Children.Add(Shape);
    }
    public double X { get; set; }
    public double Y { get; set; }
    public double Radius { get; set; }
    public Ellipse Shape { get; }

    public void Render()
    {        
        //Canvas.SetLeft(Shape, X);
        //Canvas.SetTop(Shape, Y);

        Shape.SetValue(Canvas.TopProperty, Y);
        Shape.SetValue(Canvas.LeftProperty, X);

    }

    public void Move()
    {
        Random random = new();
        X += random.Next(-5, 6);
        Y += random.Next(-5, 6);
    }

    public void Boundary()
    {
        if (X > canvas.ActualWidth - Radius * 2)
        {
            X = canvas.ActualWidth - Radius * 2;
        }
        if (X < 0)
        {
            X = 0;
        }
        if (Y > canvas.ActualHeight - Radius * 2)
        {
            Y = canvas.ActualHeight - Radius * 2;
        }
        if (Y < 0)
        {
            Y = 0;
        }
    }
}
