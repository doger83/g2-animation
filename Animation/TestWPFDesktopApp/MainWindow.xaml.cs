using g2.Animation.Core.AnimationSystems;
using g2.Animation.Core.ParticleSystems;
using g2.Animation.Core.Timing;
using g2.Animation.TestWPFDesktopApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

    private AnimationBase? animation;
    private Task? update;
    private DispatcherTimer timer;


    // ToDo: Move FPSCounter calculations dependency to animationsystem an only use a viewmodel here!
    private readonly FPSCounter fpsCounter;
    private ParticleViewModel[] canvasParticles;

    private readonly MainWindowViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;

        viewModel = (MainWindowViewModel)DataContext;

        fpsCounter = viewModel.Lbl_FPSCounterUpdate;

        mainCanvas.MinWidth = WIDTH;
        mainCanvas.MinHeight = HEIGHT;

        CompositionTarget.Rendering += TimerCallback;
        //timer = new()
        //{
        //    Interval = TimeSpan.FromMilliseconds(20)
        //};
        //timer.Tick += TimerCallback;
        //timer.Start();
        //Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        //quadTree = new(boundingBox, CAPACATY);
        //PointRegionQuadtree.Count = 0;
    }

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
        // ToDo: causes this the ammount of gc ?
        //Dispatcher.Invoke(() =>
        //{
        //mainCanvas.Visibility = Visibility.Collapsed;

        for (int i = 0; i < animation?.Particles.Length; i++)
        {
            //Canvas.SetLeft(canvasParticles[i].Shape, animation!.Particles[i].Position.X - animation.Particles[i].Radius);
            //Canvas.SetTop(canvasParticles[i].Shape, animation.Particles[i].Position.Y - animation.Particles[i].Radius);

            canvasParticles[i].Shape.SetValue(Canvas.LeftProperty, animation?.Particles[i].Position.X - animation?.Particles[i].Radius);
            canvasParticles[i].Shape.SetValue(Canvas.TopProperty, animation?.Particles[i].Position.Y - animation?.Particles[i].Radius);
        }

        //mainCanvas.Visibility = Visibility.Visible;
        //});
    }

    // ToDo: Put Rendering in FixedUpdate? or in seperate animation library class?
    private bool started = false;
    private void Render(object? sender, EventArgs e)
    {




    }

    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        started = true;

        // ToDo: Put stuff here to the classes it belongs and move on/off toggle an method?
        if (animation == null)
        {
            animation = new(fpsCounter, WIDTH, HEIGHT);
            canvasParticles = new ParticleViewModel[animation.Particles.Length];
            Particle animationParticle;
            ParticleViewModel particleVM;

            for (int i = 0; i < animation.Particles.Length; i++)
            {
                animationParticle = animation.Particles[i];
                particleVM = new(animationParticle.X, animationParticle.Y, animationParticle.Radius);

                particleVM.Shape.SetValue(Canvas.LeftProperty, animationParticle.X - animationParticle.Radius);
                particleVM.Shape.SetValue(Canvas.TopProperty, animationParticle.Y - animationParticle.Radius);

                canvasParticles[i] = particleVM;
                animationParticle.Index = mainCanvas.Children.Add(particleVM.Shape);
            }
        }
        else
        {
            for (int i = 0; i < animation.Particles.Length; i++)
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

        for (int i = 0; i < animation?.Particles.Length; i++)
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
