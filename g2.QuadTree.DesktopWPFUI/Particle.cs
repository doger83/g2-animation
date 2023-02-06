﻿using g2.Quadtree;
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
    public Ellipse Shape { get; private set; }

    public void Move()
    {
        Random random = new();
        X += XSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        Y += 0;// YSpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

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