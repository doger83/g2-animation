using g2.Animation.Core.AnimationSystems;
using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Animation.TestWPFDesktopApp.ViewModels;
using g2.Animation.UI.WPF.Shapes.Library.CanvasShapes;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace g2.Animation.TestWPFDesktopApp;

// ToDo: Add MVVM
// ToDo: Add DI
// ToDo: Add Logging
// ToDo: Write more Tests!!!
// ToDo: Make Mainwindow an animation reusable
// ToDo: Move code to seperate classes...CLean IT!!!!
// ToDo: Move Main usings to global (the main dependencies)

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // ToDo: Move to configuration (file) later make usable for templateanimations
    private const double WIDTH = 550.0;
    private const double HEIGHT = 550.0;
    //private const double X = 50.0;
    //private const double Y = 50.0;
    //private const int CAPACATY = 4;
    //private const int GROWINGRATE = 100;
    //private int totalPoints = 0;

    ////private readonly PointRegionQuadtree quadTree;

    private Task? animationLoop;
    private AnimationBase? animation;
    private ParticleViewModel[]? canvasParticles;

    // ToDo: Move FPSCounter calculations dependency to animationsystem an only use a viewmodel here!
    private readonly FPSCounter fpsCounter;
    private readonly MainWindowViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        CompositionTarget.Rendering += UpdateFPS;

        viewModel = (MainWindowViewModel)DataContext;
        fpsCounter = viewModel.Lbl_FPSCounterUpdate;

        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;
    }

    private void UpdateFPS(object? sender, EventArgs e)
    {
        viewModel.Update();
        if (started)
        {

            mainCanvas.InvalidateVisual();

        }


        //Debug.WriteLine($"Render:\t\t\t{Time.FixedDeltaTime:G65}");
        //Debug.WriteLine($"Detlatatime:\t\t{Time.DeltaTime:G65}");
        //Debug.WriteLine("-------------------------------------");
    }

    private bool started;
    int dispatchcount = 0;
    private async Task FixedUpdateAsync(object? sender, EventArgs e)
    {
        //viewModel.Update();

        if (!started)
        {
            return;
        }

        await Dispatcher.InvokeAsync(async () =>
         {
             await UpdateCanvas();
             dispatchcount++;
             Debug.WriteLine($"Dispatch\t{dispatchcount}: {(string)sender}\t{Time.FixedDeltaTime:G65}");

         });

    }
    private Task UpdateCanvas()
    {

        for (int i = 0; i < animation!.Particles.Length; i++)
        {

            Canvas.SetLeft(canvasParticles?[i].Shape, animation.Particles[i].X - animation.Particles[i].Width);
            Canvas.SetTop(canvasParticles?[i].Shape, animation.Particles[i].Y - animation.Particles[i].Height);

            //canvasParticles![i].Shape.SetValue(Canvas.LeftProperty, animation.Particles[i].X - animation.Particles[i].Width);
            //canvasParticles![i].Shape.SetValue(Canvas.TopProperty, animation.Particles[i].Y - animation.Particles[i].Height);

            //Debug.WriteLine($"UI X:\t{animation?.Particles[i].Position.X}\tXSpeed:\t{animation?.Particles[i].XSpeed}\tdt:\t{Time.DeltaTime:G65}");
        }

        return Task.CompletedTask;
    }

    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        // ToDo: Put stuff here to the classes it belongs and move on/off toggle to method or command?
        if (animation == null)
        {
            animation = new(fpsCounter, WIDTH, HEIGHT);
            animation.FixedUpdateComplete += FixedUpdateAsync;

            canvasParticles = new ParticleViewModel[animation.Particles.Length];
            Particle animationParticle;
            ParticleViewModel particleVM;

            for (int i = 0; i < animation.Particles.Length; i++)
            {
                animationParticle = animation.Particles[i];
                particleVM = new(animationParticle.X, animationParticle.Y, animationParticle.Width);

                particleVM.Shape.Measure(new Size(particleVM.Shape.ActualWidth, particleVM.Shape.ActualHeight));
                particleVM.Shape.Arrange(new Rect(animationParticle.X - animationParticle.Width, animationParticle.Y - animationParticle.Width, particleVM.Shape.ActualWidth, particleVM.Shape.ActualHeight));

                particleVM.Shape.SetValue(Canvas.LeftProperty, animationParticle.X - animationParticle.Width);
                particleVM.Shape.SetValue(Canvas.TopProperty, animationParticle.Y - animationParticle.Width);

                canvasParticles[i] = particleVM;
                animationParticle.Index = mainCanvas.Children.Add(particleVM.Shape);
            }
        }

        Time.Start();
        started = true;

        animationLoop = Task.Factory.StartNew(animation.Loop);

        btnStart.Click -= BtnStart_Click;
        btnStart.Click += BtnStop_Click;
        btnStart.Content = "STOP";
    }

    private void BtnStop_Click(object sender, RoutedEventArgs e)
    {
        started = false;
        animation?.Pause();
        animationLoop?.Dispose();
        animationLoop = null;
        Time.Reset();

        btnStart.Click -= BtnStop_Click;
        btnStart.Click += BtnStart_Click;
        btnStart.Content = "START";
    }

    private void MainWIndow_Loaded(object sender, RoutedEventArgs e)
    {
        CanvasShapes.AddGridLines(mainCanvas);
    }

    private void MainWIndow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        //started = false;
        //animation?.Pause();
        //animationLoop?.Dispose();
        //animationLoop = null;
        //Time.Reset();
    }

    //private void MainWIndow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    //{

    //    switch (e.Key)
    //    {
    //        case System.Windows.Input.Key.Right:

    //            if (started)
    //            {
    //                for (int i = 0; i < animation?.Particles.Count; i++)
    //                {
    //                    animation.Particles[i].X += 50;
    //                }
    //            }

    //            break;
    //        case System.Windows.Input.Key.Left:

    //            if (started)
    //            {
    //                for (int i = 0; i < animation?.Particles.Count; i++)
    //                {
    //                    animation.Particles[i].X -= 50;
    //                }
    //            }

    //            break;
    //        default: break;
    //    }
    //}
}
