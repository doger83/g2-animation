using g2.Animation.UI.WPF.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace g2.Animation.UI.WPF.Library.CanvasUpdater;
public class CanvasUpdater
{
    private bool started;
    private MainWindowViewModel viewModel;

    public CanvasUpdater()
    {

    }

    public void UpdateCanvas()
    {

        viewModel.Update();

        if (!started)
        {
            return;
        }

        for (int i = 0; i < particlesCount; i++)
        {
            //canvasParticles?[i].Shape.Arrange(new Rect(animation.Particles[i].X - animation.Particles[i].Width, animation.Particles[i].Y - animation.Particles[i].Height, canvasParticles![i].Shape.ActualWidth, canvasParticles![i].Shape.ActualHeight));

            Canvas.SetLeft(canvasParticles![i].Shape, animation!.Particles[i].X - animation.Particles[i].Width);
            Canvas.SetTop(canvasParticles![i].Shape, animation.Particles[i].Y - animation.Particles[i].Height);

            //canvasParticles?[i].Shape.SetValue(Canvas.LeftProperty, animation.Particles[i].X - animation.Particles[i].Width);
            //canvasParticles?[i].Shape.SetValue(Canvas.TopProperty, animation.Particles[i].Y - animation.Particles[i].Height);

            //Debug.WriteLine($"UI X:\t{animation?.Particles[i].Position.X}\tXSpeed:\t{animation?.Particles[i].XSpeed}\tdt:\t{Time.DeltaTime:G65}");
        }
        //mainCanvas.InvalidateVisual();
        //Debug.WriteLine($"FixedDetlatatime:\t{Time.FixedDeltaTime:G65}");
        //Debug.WriteLine($"Detlatatime:\t\t{Time.DeltaTime:G65}");
        //Debug.WriteLine($"Render:\t\t\t{Time.FixedDeltaTime:G65}");

        //return Task.CompletedTask;
    }
}
