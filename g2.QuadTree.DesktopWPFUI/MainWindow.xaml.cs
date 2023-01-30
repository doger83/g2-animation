using g2.Quadtree;
using System;
using System.Collections.Generic;
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
    private const double WIDTH = 200.0;
    private const double HEIGHT = 200.0;
    private const double X = 200.0;
    private const double Y = 200.0;
    private const int CAPACATY = 4;
    private const int GROWINGRATE = 5;
    private int totalPoints = 0;

    private readonly PointRegionQuadtree qTree;

    public MainWindow()
    {
        InitializeComponent();

        Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
        qTree = new(boundingBox, CAPACATY);
    }

    private void Btn_Start_Click(object sender, RoutedEventArgs e)
    {
        AddRandomPointsToTree(GROWINGRATE);

        myCanvas.Children.Clear();
        DrawQuadTree(qTree);
    }

    private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //AddPointAtMousePosition(sender, e);
        PointRegionQuadtree.Count = 0;
        DrawSearchwindowAtMousePosition(sender, e);
    }

    private void DrawQuadTree(PointRegionQuadtree qTree)
    {
        DrawRectangleAtQuadrant(qTree.Boundary);
        DrawCircleAtPoints(qTree.Points);
  
        if (qTree.Divided)
        {
            DrawQuadTree(qTree.NorthWest!);
            DrawQuadTree(qTree.NorthEast!);
            DrawQuadTree(qTree.SouthWest!);
            DrawQuadTree(qTree.SouthEast!);
        }
    }

    private void AddRandomPointsToTree(int growingrate)
    {
        Random rnd = new();

        for (int i = 0; i < growingrate; i++)
        {
            var x = rnd.NextDouble() * WIDTH * 2.0;
            var y = rnd.NextDouble() * HEIGHT * 2.0;
            Point point = new(x, y);

            qTree.Insert(point);
        }
        totalPoints += growingrate;
    }

    private void AddPointAtMousePositionToTree(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);
        PositionText.Content = string.Format("Last click at X = {0}, Y = {1}", p.X, p.Y);

        double x = p.X;
        double y = p.Y;

        Point point = new(x, y);
        qTree.Insert(point);

        myCanvas.Children.Clear();
        DrawQuadTree(qTree);
    }

    private void DrawSearchwindowAtMousePosition(object sender, MouseButtonEventArgs e)
    {
        double width = 150.0;
        double height = 150.0;

        System.Windows.Point mousePosition = e.GetPosition(myCanvas);
        double x = mousePosition.X;
        double y = mousePosition.Y;

        Quadrant searchWindow = new(x, y, width / 2, height / 2);
        List<Point> points = qTree.Query(searchWindow);

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
    {
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

        myCanvas.Children.Add(circle);
 
        circle.SetValue(Canvas.LeftProperty, point.X - circle.Width / 2.0);
        circle.SetValue(Canvas.TopProperty, point.Y - circle.Height / 2.0);
    }
}
