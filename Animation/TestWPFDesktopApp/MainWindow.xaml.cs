using g2.Animation.Core.AnimationSystems;
using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Animation.TestWPFDesktopApp.ViewModels;
using g2.Animation.UI.WPF.Shapes.Library.CanvasShapes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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


        CompositionTarget.Rendering += Render;

        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;

        canvasParticles = new();
    }

    // ToDo: Put Rendering in FixedUpdate? or in seperate animation library class?
    private bool started = false;
    private void Render(object? sender, EventArgs e)
    {
        if (started)
        {
            for (int i = 0; i < canvasParticles.Count; i++)
            {
                canvasParticles[i].Shape.SetValue(Canvas.LeftProperty, animation!.Particles[i].X - animation.Particles[i].Radius);
                canvasParticles[i].Shape.SetValue(Canvas.TopProperty, animation.Particles[i].Y - animation.Particles[i].Radius);
            }
        }

        viewModel.Update();
    }


    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        // ToDo: Put stuff here to the classes it belongs and move on/off toggle an method?
        if (animation == null)
        {
            animation = new(fpsCounter, WIDTH, HEIGHT);

            for (int i = 0; i < animation.Particles.Count; i++)
            {
                Particle animationParticle = animation.Particles[i];

                ParticleViewModel particleVM = new(animationParticle.X, animationParticle.Y, animationParticle.Radius);


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
        started = true;
    }

    private void BtnStop_Click(object sender, RoutedEventArgs e)
    {
        animation!.StopThread();
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
        started = false;
    }
    private void MainWIndow_Loaded(object sender, RoutedEventArgs e)
    {
        CanvasShapes.AddGridLines(mainCanvas);
    }
}
