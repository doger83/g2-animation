using g2.Datastructures.DesktopWPFUI;
using g2.Datastructures.Trees;
using g2.Quadtree;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = g2.Quadtree.Point;

namespace g2.QuadTree.DesktopWPFUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const double WIDTH = 500.0;
    private const double HEIGHT = 500.0;
    private const double X = 50.0;
    private const double Y = 50.0;
    private const int CAPACATY = 4;
    private const int GROWINGRATE = 100;
    private int totalPoints = 0;

    private readonly PointRegionQuadtree quadTree;

    private Animation? animation;
    //private List<Particle>? particles;
    private readonly FPSCounterViewModel fpsCounter;

    public MainWindow()
    {
        InitializeComponent();

        fpsCounter = new FPSCounterViewModel(myCanvas);
        DataContext = fpsCounter;


        Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        quadTree = new(boundingBox, CAPACATY);
        PointRegionQuadtree.Count = 0;

        CompositionTarget.Rendering += Render;
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

    // ToDo: Put Rendering in FixedUpdate? Or let the ui handle the frequent updates?s
    private void Render(object? sender, EventArgs e)
    {
        animation?.Particle.Shape.SetValue(Canvas.TopProperty, animation.Particle.Y - animation.Particle.Radius);
        animation?.Particle.Shape.SetValue(Canvas.LeftProperty, animation.Particle.X - animation.Particle.Radius);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        for (double x = 0; x <= myCanvas.ActualWidth; x += 50)
        {
            Line line = new()
            {


                Stroke = System.Windows.Media.Brushes.White,
                X1 = x,
                Y1 = 0,
                X2 = x,
                Y2 = myCanvas.ActualHeight,
                StrokeThickness = 0.25,
                Opacity = 0.25,

            };
            _ = myCanvas.Children.Add(line);
        }

        for (double y = 0; y <= myCanvas.ActualHeight; y += 50)
        {
            Line line = new()
            {
                Stroke = System.Windows.Media.Brushes.White,
                X1 = 0,
                Y1 = y,
                X2 = myCanvas.ActualWidth,
                Y2 = y,
                StrokeThickness = 0.25,
                Opacity = 0.25,
            };
            _ = myCanvas.Children.Add(line);
        }
    }

    private void Btn_Start_Click(object sender, RoutedEventArgs e)
    {
        animation = new(fpsCounter, myCanvas);
        _ = myCanvas.Children.Add(animation.Particle.Shape);
        _ = Task.Factory.StartNew(animation.Update);

        btn_Start.IsEnabled = false;
        //AddRandomPointsToTree(GROWINGRATE);
        //myCanvas.Children.Clear();
        //Draw(quadTree);
    }

    private void MyCanvas_LeftMouseDown(object sender, MouseButtonEventArgs e)
    {
        //DrawSearchwindowAtMousePosition(sender, e);
    }

    private void MyCanvas_RightMouseDown(object sender, MouseButtonEventArgs e)
    {
        //AddPointAtMousePositionToTree(sender, e);
    }


    private void Draw(PointRegionQuadtree quadTree)
    {
        DrawRectangleAtQuadrant(quadTree.Boundary);
        DrawCircleAtPoints(quadTree.Points);
        if (quadTree.Divided)
        {
            Draw(quadTree.NorthWest!);
            Draw(quadTree.NorthEast!);
            Draw(quadTree.SouthWest!);
            Draw(quadTree.SouthEast!);
        }
    }

    private void DrawSearchwindowAtMousePosition(object sender, MouseButtonEventArgs e, double width = 150.0, double height = 150.0)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);

        Quadrant searchWindow = new(p.X, p.Y, width / 2, height / 2);
        List<Point> points = quadTree.Query(searchWindow);

        // Todo: draw rectangle only in the canvas boudings
        DrawRectangleAtQuadrant(searchWindow, Brushes.Blue);
        DrawCircleAtPoints(points, Brushes.Blue);

        // Todo: add Bindings
        PositionText.Content = string.Format("Total Points: {0} | Found Points: {1} | Visited Points: {2}", totalPoints, points.Count, PointRegionQuadtree.Count);
    }

    private void DrawRectangleAtQuadrant(Quadrant quadrant) => DrawRectangleAtQuadrant(quadrant, Brushes.Red);

    private void DrawRectangleAtQuadrant(Quadrant quadrant, SolidColorBrush color)
    {
        double totalWidth = quadrant.Width * 2;
        double totalHeight = quadrant.Height * 2;

        Rectangle rectangle = new()
        {
            StrokeThickness = 0.5,
            Stroke = color,
            Width = totalWidth,
            Height = totalHeight
        };
        Canvas.SetLeft(rectangle, quadrant.X - quadrant.Width);
        Canvas.SetTop(rectangle, quadrant.Y - quadrant.Height);
        _ = myCanvas.Children.Add(rectangle);
    }
    private void DrawCircleAtPoints(List<Point>? points) => DrawCircleAtPoints(points, Brushes.Green);

    private void DrawCircleAtPoints(List<Point>? points, SolidColorBrush color)
    { // nullable List ? why ... now I know... its because the list of points in the tree node might be null because the points are in one of the children!
        if (points is null)
        {
            return;
        }

        foreach (Point point in points)
        {
            DrawCircleAtPoint(point, color);
        }
    }

    private void DrawCircleAtPoint(Point point) => DrawCircleAtPoint(point, Brushes.Green);

    private void DrawCircleAtPoint(Point point, SolidColorBrush color)
    {
        Ellipse circle = new()
        {
            Width = 5,
            Height = 5,
            Stroke = color,
            StrokeThickness = 3,
        };

        Canvas.SetLeft(circle, point.X - (circle.Width / 2.0));
        Canvas.SetTop(circle, point.Y - (circle.Height / 2.0));
        _ = myCanvas.Children.Add(circle);
    }


    private void AddRandomPointsToTree(int growingrate)
    {
        Random random = new();

        for (int i = 0; i < growingrate; i++)
        {
            double x = random.NextDouble() * WIDTH * 2.0;
            double y = random.NextDouble() * HEIGHT * 2.0;
            Point point = new(x, y);

            _ = quadTree.Insert(point);
        }

        totalPoints += growingrate;
    }

    private void AddPointAtMousePositionToTree(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);

        _ = quadTree.Insert(new Point(p.X, p.Y));

        myCanvas.Children.Clear();
        Draw(quadTree);

        // Todo: add Bindings
        MousPositionText.Content = string.Format("Last click at X = {0}, Y = {1}", p.X, p.Y);
    }
}
