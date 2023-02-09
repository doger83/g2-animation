using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


using g2.Animation.Core.Timing;
using g2.Animation.Core.AnimationSystems;
using g2.Animation.UI.WPF.CanvasShapes;
using g2.Animation.WPFDesktopApp.ViewModels;


namespace g2.Animation.WPFDesktopApp;

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


    public MainWindow()
    {
        InitializeComponent();

        fpsCounter = new();
        DataContext = fpsCounter;
        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        particle = new(25, 25, 25);


        CompositionTarget.Rendering += Render;

        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;

        //particles = new List<Particle>();
        //Random random = new();

        //for (int i = 0; i < 15; i++)
        //{
        //    var x = random.NextDouble() * WIDTH * 2.0;
        //    var y = random.NextDouble() * HEIGHT * 2.0;
        //    Particle pa = new(x, y, 15, myCanvas);

        //    particles.Add(pa);
        //}

    }

    // ToDo: Put Rendering in FixedUpdate? or in seperate animation library class?
    private bool started = false;
    private void Render(object? sender, EventArgs e)
    {
        if (!started) { return; }

        particle.Shape.SetValue(Canvas.TopProperty, animation?.Particle.Y - animation?.Particle.Radius);
        particle.Shape.SetValue(Canvas.LeftProperty, animation?.Particle.X - animation?.Particle.Radius);
        //if (started)
        //{

        //    Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));
        //}
        //Debug.WriteLine(animation?.Particle.X);
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
            _ = mainCanvas.Children.Add(particle.Shape);
        }
        else
        {
            animation.Particle.X = lastX;
            animation.Particle.Y = lastY;
            animation.Particle.XSpeed = lastXSpeed;
            animation.Particle.YSpeed = lastYSpeed;
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
        //Debug.WriteLine("------------Button Start-------------");
        //Debug.WriteLine("------------Before stop thread-------------");
        //Debug.WriteLine(animation!.Particle.X);
        //Debug.WriteLine(Canvas.GetLeft(animation!.Particle.Shape));

        lastYSpeed = animation.Particle.YSpeed;
        lastXSpeed = animation.Particle.XSpeed;
        lastY = animation.Particle.Y;
        lastX = animation.Particle.X;

        animation!.Particle.XSpeed = 0;
        animation!.Particle.YSpeed = 0;

        
        
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
