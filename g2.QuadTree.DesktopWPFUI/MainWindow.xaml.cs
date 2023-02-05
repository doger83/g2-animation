using g2.Datastructures.DesktopWPFUI;
using g2.Quadtree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = g2.Quadtree.Point;

namespace g2.QuadTree.DesktopWPFUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const double WIDTH = 400.0;
    private const double HEIGHT = 300.0;
    private const double X = 50.0;
    private const double Y = 50.0;
    private const int CAPACATY = 4;
    private const int GROWINGRATE = 100;
    private int totalPoints = 0;

    private readonly PointRegionQuadtree quadTree;

    private readonly Animation animation;
    private readonly FPSCounterViewModel fpsCounter;
    private readonly List<Particle> particles;

    public MainWindow()
    {
        InitializeComponent();

        fpsCounter = new FPSCounterViewModel(myCanvas);
        DataContext = fpsCounter;



        animation = new(fpsCounter, myCanvas);


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

    private void Render(object? sender, EventArgs e)
    {

        //Canvas.SetLeft(animation.Particle.Shape, animation.Particle.X - animation.Particle.Radius);
        //Canvas.SetTop(animation.Particle.Shape, animation.Particle.Y - animation.Particle.Radius);


        animation.Particle.Shape.SetValue(Canvas.TopProperty, animation.Particle.Y - animation.Particle.Radius);
        animation.Particle.Shape.SetValue(Canvas.LeftProperty, animation.Particle.X - animation.Particle.Radius);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        for (double x = 0; x <= myCanvas.ActualWidth; x += 50)
        {
            Line line = new()
            {
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = x,
                Y1 = 0,
                X2 = x,
                Y2 = myCanvas.ActualHeight,
                StrokeThickness = 1
            };
            myCanvas.Children.Add(line);
        }

        for (double y = 0; y <= myCanvas.ActualHeight; y += 50)
        {
            Line line = new()
            {
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = 0,
                Y1 = y,
                X2 = myCanvas.ActualWidth,
                Y2 = y,
                StrokeThickness = 1
            };
            myCanvas.Children.Add(line);
        }

        //animation = new(fpsCounter, myCanvas);
        //Task.Factory.StartNew(animation.Update);
    }

    private void Btn_Start_Click(object sender, RoutedEventArgs e)
    {
        Task.Factory.StartNew(animation.Update);
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

    private void DrawRectangleAtQuadrant(Quadrant quadrant)
    {
        DrawRectangleAtQuadrant(quadrant, Brushes.Red);
    }

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
        myCanvas.Children.Add(rectangle);
    }
    private void DrawCircleAtPoints(List<Point>? points)
    {
        DrawCircleAtPoints(points, Brushes.Green);
    }

    private void DrawCircleAtPoints(List<Point>? points, SolidColorBrush color)
    { // nullable List ? why ... now I know... its because the list of points in the tree node might be null because the points are in one of the children!
        if (points is null)
        {
            return;
        }
        foreach (var point in points)
        {
            DrawCircleAtPoint(point, color);
        }
    }

    private void DrawCircleAtPoint(Point point)
    {
        DrawCircleAtPoint(point, Brushes.Green);
    }

    private void DrawCircleAtPoint(Point point, SolidColorBrush color)
    {
        Ellipse circle = new()
        {
            Width = 5,
            Height = 5,
            Stroke = color,
            StrokeThickness = 3,
        };
        Canvas.SetLeft(circle, point.X - circle.Width / 2.0);
        Canvas.SetTop(circle, point.Y - circle.Height / 2.0);
        myCanvas.Children.Add(circle);

    }


    private void AddRandomPointsToTree(int growingrate)
    {
        Random random = new();

        for (int i = 0; i < growingrate; i++)
        {
            var x = random.NextDouble() * WIDTH * 2.0;
            var y = random.NextDouble() * HEIGHT * 2.0;
            Point point = new(x, y);

            quadTree.Insert(point);
        }
        totalPoints += growingrate;
    }

    private void AddPointAtMousePositionToTree(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);

        quadTree.Insert(new Point(p.X, p.Y));

        myCanvas.Children.Clear();
        Draw(quadTree);

        // Todo: add Bindings
        MousPositionText.Content = string.Format("Last click at X = {0}, Y = {1}", p.X, p.Y);
    }
}
