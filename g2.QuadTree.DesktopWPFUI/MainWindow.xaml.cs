using g2.Animation.Core;
using g2.Animation.Core.Timing;
using g2.Animation.DesktopWPFUI.ViewModels;
using g2.Animation.UI.WPF.CanvasShapes;
using g2.Datastructures.Geometry;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace g2.QuadTree.DesktopWPFUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
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
    private readonly FPSCounter fpsCounter;
    private readonly ParticleViewModel particle;

    public MainWindow()
    {
        InitializeComponent();

        fpsCounter = new();
        DataContext = fpsCounter;
        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        particle = new(25,25, 25);


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

    // ToDo: Put Rendering in FixedUpdate? Or let the ui handle the frequent updates? Glitch: when stoping via btn one frame delay after pressing. maybe cause the rendering lags behind calculationg pos
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

    private double lastX;
    private double lastY;
    private double lastXSpeed;
    private double lastYSpeed;
    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        if (animation == null)
        {
            Quadrant canvasBoundaries = new(0,0, WIDTH, HEIGHT);
            animation = new(fpsCounter, canvasBoundaries);
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

        //mainLoop?.Dispose();
        //mainLoop = null;
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
