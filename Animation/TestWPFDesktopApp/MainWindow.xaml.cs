using g2.Animation.Core.AnimationSystems;
using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Animation.TestWPFDesktopApp.ViewModels;
using g2.Animation.UI.WPF.Shapes.Library.CanvasShapes;
using g2.Datastructures.Geometry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

    //private List<Particle>? particles;

    // ToDo: Move FPSCounter calculations dependency to animationsystem an only use a viewmodel here!
    private readonly FPSCounter fpsCounter;
    private readonly ParticleViewModel particle;
    private List<Particle> particles;
    private Quadrant quadrant;

    private readonly MainWindowViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = (MainWindowViewModel)DataContext;

        fpsCounter = viewModel.Lbl_FPSCounterUpdate;
        //DataContext = new MainWindowViewModel();
        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        particle = new(25, 25, 25);

        CompositionTarget.Rendering += Render;

        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;

        particles = new();
        quadrant = new(0, 0, Width, HEIGHT);
    }

    // ToDo: Put Rendering in FixedUpdate? or in seperate animation library class?
    private bool started = false;
    private void Render(object? sender, EventArgs e)
    {
        if (started) // FPS Drop of 5 if not started? !!! ToDo: get rid of this rendering sh...t! Flackert ohne ende beim ziehen des Fensters
        {
            foreach (UIElement particle in mainCanvas.Children)
            {
                if (particle is Ellipse)
                {

                    particle.SetValue(Canvas.LeftProperty, animation?.Particle.X - animation?.Particle.Radius);
                    particle.SetValue(Canvas.TopProperty, animation?.Particle.Y - animation?.Particle.Radius);
                }
            }
        }

        viewModel.Update();
    }

    // ToDo: Move started, last -Position and -speed to Animation class
    private double lastX;
    private double lastY;
    private double lastXSpeed;
    private double lastYSpeed;
    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        // ToDo: Put stuff here to the classes it belongs and move on/off toggle an method?
        if (animation == null)
        {
            animation = new(fpsCounter, WIDTH, HEIGHT);

            foreach (Particle particle in animation.Particles)
            {
                _ = mainCanvas.Children.Add(new ParticleViewModel(particle.X, particle.Y, particle.Radius).Shape);

            }
        }
        else
        {
            foreach (Particle particle in animation.Particles)
            {
                Particle p2 = animation.Particle with { X = lastX, Y = lastY, XSpeed = lastXSpeed, YSpeed = lastYSpeed };
                animation.Particle = p2;

            }

            //animation.Particle.Y = lastY;
            //animation.Particle.XSpeed = lastXSpeed;
            //animation.Particle.YSpeed = lastYSpeed;
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
        //update?.Dispose();
        //update = null;

        //Debug.WriteLine("------------Button Start-------------");
        //Debug.WriteLine("------------Before stop thread-------------");
        //Debug.WriteLine(animation!.Particle.X);
        //Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));

        foreach (Particle particle in animation.Particles)
        {
            lastYSpeed = animation.Particle.YSpeed;
            lastXSpeed = animation.Particle.XSpeed;
            lastY = animation.Particle.Y;
            lastX = animation.Particle.X;
            Particle p2 = animation.Particle with { X = lastX, Y = lastY, XSpeed = lastXSpeed, YSpeed = lastYSpeed };
            animation.Particle = p2;

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
