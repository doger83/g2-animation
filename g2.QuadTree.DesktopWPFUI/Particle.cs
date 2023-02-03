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
public class Particle
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Radius { get; set; }
    Canvas canvas;


    public Particle(double x, double y, double radius, Canvas canvas)
    {
        X = x;
        Y = y;
        Radius = radius;
        this.canvas = canvas;
    }

    public void Move()
    {
        Random random = new();
        X += random.Next(-1, 2);
        Y += random.Next(-1, 2);
    }

    public void  Render()
    {
        canvas.Children.Clear();
            //canvas.Dispatcher.Invoke(() =>
            //{
            //canvas.Children.Clear();
            Ellipse circle = new()
            {
                Width = Radius * 2,
                Height = Radius * 2,
                Stroke = Brushes.Aqua,
                Fill = Brushes.Beige,
                StrokeThickness = 3,
            };
            Canvas.SetLeft(circle, X);
            Canvas.SetTop(circle, Y);

            canvas.Children.Add(circle);
        //});


    }

    private void DrawCircleAtPoint(Point point, Canvas canvas)
    {
        canvas.Dispatcher.Invoke(() =>
        {
            Ellipse circle = new()
            {
                Width = Radius * 2,
                Height = Radius * 2,
                Stroke = Brushes.Aqua,
                Fill = Brushes.Beige,
                StrokeThickness = 3,
            };
            Canvas.SetLeft(circle, X);
            Canvas.SetTop(circle, Y);

            canvas.Children.Add(circle);

        });
            Thread.Sleep(100);
      
    }

    internal void Boundary()
    {
        if (X > canvas.ActualWidth - Radius * 2)
        {
            //xSpeed = 0;
            X = canvas.ActualWidth - Radius * 2;
        }
        if (X < 0)
        {
            //xSpeed = 0;
            X = 0;
        }

        if (Y > canvas.ActualHeight - Radius *2)
        {
            //ySpeed = 0;
            Y = canvas.ActualHeight - Radius *2;
        }
        if (Y < 0)
        {
            //ySpeed = 0;
            Y = 0;
        }
    }
}
