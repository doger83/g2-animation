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
    int WIDTH = 200;
    int HEIGHT = 200;
    int X = 200;
    int Y = 200;
    int CAPACATY = 4;

    PointRegionQuadtree _qTree;

    public MainWindow()
    {
        InitializeComponent();

        int canvaseCenter = (int)(myCanvas.ActualWidth) / 2;
        int halfDimension = (int)(myCanvas.ActualWidth) / 2;
        AxisAlignedBoundingBox boundingBox = new(200, 200, 200, 200);
        _qTree = new(boundingBox, 4);

    }


    private void btn_Start_Click(object sender, RoutedEventArgs e)
    {
        
        Random rnd = new Random();

        for (int i = 0; i < 4; i++)
        {
            int x = rnd.Next(WIDTH * 2);
            int y = rnd.Next(HEIGHT * 2);
            var point = new Point(x, y);

            _qTree.Insert(point);
        }

        Draw(_qTree);
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
        Rectangle rectangle = new Rectangle();
        rectangle.StrokeThickness = 0.5;
        rectangle.Stroke = Brushes.Red;
        rectangle.Width = width;
        rectangle.Height = height;
        Canvas.SetLeft(rectangle, qTree.Boundary.X - qTree.Boundary.Width);
        Canvas.SetTop(rectangle, qTree.Boundary.Y - qTree.Boundary.Height);
        myCanvas.Children.Add(rectangle);

        foreach (var point in qTree.Points)
        {
            Ellipse circle = new Ellipse()
            {
                Width = 5,
                Height = 5,
                Stroke = Brushes.Green,
                StrokeThickness = 1,

            };

            myCanvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)point.X);
            circle.SetValue(Canvas.TopProperty, (double)point.Y);
        }


        if (qTree.Divided)
        {
           
                Draw(qTree.NorthWest);
          
                Draw(qTree.NorthEast);
          
            
                Draw(qTree.SouthWest);
            
            
                Draw(qTree.SouthEast);
            
        }



    }

    private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        System.Windows.Point p = e.GetPosition(myCanvas);
        PositionText.Content =  string.Format("Last click at X = {0}, Y = {1}", p.X, p.Y);

        int x = (int)p.X;
        int y = (int)p.Y;

        Point point = new Point(x, y);
        _qTree.Insert(point);
        Draw(_qTree);
    }
}
