using System;
using System.Collections.Generic;
using System.ComponentModel;
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

public class Game
{
    Canvas canvas;
    private readonly FPSCounterViewModel fpsCounter;
    Particle particle;

    public Game(FPSCounterViewModel fpsCounter, Canvas canvas, Particle particle)
    {
        this.fpsCounter = fpsCounter;
        this.canvas = canvas;
        this.particle = particle;
    }

    public void Update()
    {
        while (true)
        {
            fpsCounter.Draw();

            particle.Move();
            particle.Boundary();

            // Todo: kann eventuell durch ein LOCK entfallen? und alle Aufrufe direkt 
            //particle.Move();
            //particle.Boundary();
            //particle.Render();
            Application.Current?.Dispatcher.InvokeAsync(() =>
            {           

                particle.Render();
            });
            Thread.Sleep(1);
        }

    }

    private void DrawCircleAtPoint(Particle p)
    {
        Ellipse circle = new()
        {
            Width = p.Radius * 2,
            Height = p.Radius * 2,
            Stroke = Brushes.Aqua,
            Fill = Brushes.Beige,
            StrokeThickness = 3,
        };
        circle.SetValue(Canvas.TopProperty, p.Y - p.Radius);
        circle.SetValue(Canvas.LeftProperty, p.X - p.Radius);

        canvas.Children.Add(circle);

    }

}

