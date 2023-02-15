using g2.Animation.Core.AnimationSystems;
using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Animation.TestWPFDesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

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

    private AnimationBase? animation;
    private Task? update;


    // ToDo: Move FPSCounter calculations dependency to animationsystem an only use a viewmodel here!
    private readonly FPSCounter fpsCounter;
    private readonly List<ParticleViewModel> canvasParticles;

    private readonly MainWindowViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = (MainWindowViewModel)DataContext;

        fpsCounter = viewModel.Lbl_FPSCounterUpdate;
        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        canvasParticles = new();

        //CompositionTarget.Rendering += Render;
        Time.TimerTick += TimerCallback;
        Time.StartTimer(1000 / 50);
        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;

    }

    private Ellipse? callbackShape;
    private void TimerCallback(object? sender, EventArgs e)
    {
        if (Application.Current == null || Application.Current.Dispatcher == null)
        {
            return;
        }
        // ToDo: UI is a bit laggy. make this async ?
        viewModel.Update();

        if (!started)
        {
            return;
        }

        Dispatcher.Invoke(() =>
        {
            for (int i = 0; i < animation?.Particles.Count; i++)
            {
                callbackShape = canvasParticles[i].Shape;
                //Canvas.SetLeft(callbackShape, animation!.Particles[i].X - animation.Particles[i].Radius);
                //Canvas.SetTop(callbackShape, animation.Particles[i].Y - animation.Particles[i].Radius);

                callbackShape?.SetValue(Canvas.LeftProperty, animation?.Particles[i].X - animation?.Particles[i].Radius);
                callbackShape?.SetValue(Canvas.TopProperty, animation?.Particles[i].Y - animation?.Particles[i].Radius);

                callbackShape = null;
            }
        });
    }

    // ToDo: Put Rendering in FixedUpdate? or in seperate animation library class?
    private bool started = false;
    private void Render(object? sender, EventArgs e)
    {
        //if (started)
        //{
        //    for (int i = 0; i < canvasParticles.Count; i++)
        //    {
        //        canvasParticles[i].Shape.SetValue(Canvas.LeftProperty, animation!.Particles[i].X - animation.Particles[i].Radius);
        //        canvasParticles[i].Shape.SetValue(Canvas.TopProperty, animation.Particles[i].Y - animation.Particles[i].Radius);
        //    }
        //}
        viewModel.Update();

    }

    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        started = true;


        // ToDo: Put stuff here to the classes it belongs and move on/off toggle an method?
        if (animation == null)
        {
            animation = new(fpsCounter, WIDTH, HEIGHT);
            Particle animationParticle;
            ParticleViewModel particleVM;

            for (int i = 0; i < animation.Particles.Count; i++)
            {
                animationParticle = animation.Particles[i];
                particleVM = new(animationParticle.X, animationParticle.Y, animationParticle.Radius);

                particleVM.Shape.SetValue(Canvas.LeftProperty, animationParticle.X - animationParticle.Radius);
                particleVM.Shape.SetValue(Canvas.TopProperty, animationParticle.Y - animationParticle.Radius);

                canvasParticles.Add(particleVM);
                animationParticle.Index = mainCanvas.Children.Add(particleVM.Shape);
            }
        }
        else
        {
            for (int i = 0; i < animation.Particles.Count; i++)
            {
                animation.Particles[i].Reset();
            }
        }

        //Debug.WriteLine("------------Button Start-------------");
        //Debug.WriteLine(animation!.Particle.X);
        //Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));

        btnStart.Click -= BtnStart_Click;
        btnStart.Click += BtnStop_Click;
        btnStart.Content = "STOP";


        //Debug.WriteLine(animation?.Particle.X);
        update = Task.Factory.StartNew(animation.Update);  //animation.Update(); // Task.Factory.StartNew(animation.Update);

    }

    private void BtnStop_Click(object sender, RoutedEventArgs e)
    {
        started = false;
        animation?.StopThread();
        update?.Dispose();
        update = null;

        //Debug.WriteLine("------------Button Start-------------");
        //Debug.WriteLine("------------Before stop thread-------------");
        //Debug.WriteLine(animation!.Particle.X);
        //Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));

        for (int i = 0; i < animation.Particles.Count; i++)
        {
            animation.Particles[i].Pause();
        }

        //Debug.WriteLine("------------After stop thread-------------");
        //Debug.WriteLine(animation!.Particle.X);
        //Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));

        btnStart.Click -= BtnStop_Click;
        btnStart.Click += BtnStart_Click;
        btnStart.Content = "START";

        //Debug.WriteLine(animation?.Particle.X);
    }

    private void MainWIndow_Loaded(object sender, RoutedEventArgs e)
    {
        //CanvasShapes.AddGridLines(mainCanvas);
    }

    private void MainWIndow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        started = false;
        animation?.StopThread();
        Time.StopTimer();
        update?.Dispose();
        update = null;

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
