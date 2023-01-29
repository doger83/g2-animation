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
        totalPoints += GROWINGRATE;

        Random rnd = new();

        for (int i = 0; i < GROWINGRATE; i++)
        {
            var x = rnd.NextDouble() * WIDTH * 2.0;
            var y = rnd.NextDouble() * HEIGHT * 2.0;
            Point point = new(x, y);

            qTree.Insert(point);
        }
        myCanvas.Children.Clear();
        Draw(qTree);
    }

    private void Draw(PointRegionQuadtree qTree)
    {

        //Line line = new Line();
        //Thickness thickness = new Thickness(0, 0, 0, 0);
        ////line.Margin = thickness;
        ////line.Visibility = Visibility.Visible;
        //line.StrokeThickness = 1;
        //line.Stroke = Brushes.White;
        //line.X1 = qTree.Boundary.X - qTree.Boundary.Width;
        //line.X2 = qTree.Boundary.X;
        //line.Y1 = qTree.Boundary.Y - qTree.Boundary.Height;
        //line.Y2 = qTree.Boundary.Y;
        //myCanvas.Children.Add(line);

        double width = qTree.Boundary.Width * 2;
        double height = qTree.Boundary.Height * 2;
        Rectangle rectangle = new()
        {
            StrokeThickness = 0.5,
            Stroke = Brushes.Red,
            Width = width,
            Height = height
        };
        Canvas.SetLeft(rectangle, qTree.Boundary.X - qTree.Boundary.Width);
        Canvas.SetTop(rectangle, qTree.Boundary.Y - qTree.Boundary.Height);
        myCanvas.Children.Add(rectangle);

        if (qTree.Points is not null)
        {
            foreach (var point in qTree.Points)
            {
                Ellipse circle = new()
                {
                    Width = 5,
                    Height = 5,
                    Stroke = Brushes.Green,
                    StrokeThickness = 1,
                };

                myCanvas.Children.Add(circle);

                circle.SetValue(Canvas.LeftProperty, point.X);
                circle.SetValue(Canvas.TopProperty, point.Y);
            } 
        }

        if (qTree.Divided)
        {
            Draw(qTree.NorthWest!);
            Draw(qTree.NorthEast!);
            Draw(qTree.SouthWest!);
            Draw(qTree.SouthEast!);
        }
    }

    private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        AddRectangle(sender, e);
    }

    private void AddRectangle(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point mousePosition = e.GetPosition(myCanvas);

        PointRegionQuadtree.Count = 0;

        double x = mousePosition.X;
        double y = mousePosition.Y;

        double width = 150.0;
        double height = 150.0;
        Quadrant searchWindow = new(x, y, width / 2, height / 2);
       
        Rectangle rect = new Rectangle();
        rect.Stroke = new SolidColorBrush(Colors.Green);
        rect.StrokeThickness = 1;
        //rect.Fill = new SolidColorBrush(Colors.Black);
        rect.Width = width;
        rect.Height = height;
        Canvas.SetLeft(rect, x - width / 2);
        Canvas.SetTop(rect, y - height / 2);
        myCanvas.Children.Add(rect);
       
        List<Point> points = qTree.Query(searchWindow);


        foreach (var point in points)
        {
            Ellipse circle = new()
            {
                Width = 5,
                Height = 5,
                Stroke = Brushes.Blue,
                StrokeThickness = 3,

            };

            myCanvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, point.X);
            circle.SetValue(Canvas.TopProperty, point.Y);
        }

        PositionText.Content = string.Format("Total Points: {0} | Found Points: {1} | Visited Points: {2}", totalPoints, points.Count, PointRegionQuadtree.Count);
    }

    private void AddPointAtMousePosition(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);
        PositionText.Content = string.Format("Last click at X = {0}, Y = {1}", p.X, p.Y);

        double x = p.X;
        double y = p.Y;

        Point point = new(x, y);
        qTree.Insert(point);
        myCanvas.Children.Clear();
        Draw(qTree);
    }
}
