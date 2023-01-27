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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = g2.Quadtree.Point;

namespace g2.QuadTree.DesktopWPFUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    PointRegionQuadtree qTree;

    public MainWindow()
    {
        InitializeComponent();



    }


    private void btn_Start_Click(object sender, RoutedEventArgs e)
    {
        int canvaseCenter = (int)myCanvas.ActualWidth / 2;
        int halfDimension = (int)myCanvas.ActualWidth / 2;
        AxisAlignedBoundingBox boundingBox = new(new Point(canvaseCenter, canvaseCenter), halfDimension);
        qTree = new(boundingBox);
        qTree.Insert(new Point(5, 5));
        qTree.Insert(new Point(10, 10));
        qTree.Insert(new Point(15, 15));
        qTree.Insert(new Point(25, 25));
        qTree.Insert(new Point(45, 45));
        //qTree.Insert(new Point(450, 450));
        //qTree.Insert(new Point(455, 455));
        //qTree.Insert(new Point(460, 460));
        //qTree.Insert(new Point(465, 465));
        //qTree.Insert(new Point(475, 475));

        Draw(qTree);
    }

    private void Draw(PointRegionQuadtree qTree)
    {
        Line line = new Line();
        Thickness thickness = new Thickness(0, 0, 0, 0);
        //line.Margin = thickness;
        //line.Visibility = Visibility.Visible;
        line.StrokeThickness = 1;
        line.Stroke = Brushes.White;
        line.X1 = qTree.Boundary.Center.X - qTree.Boundary.HalfDimension;
        line.X2 = qTree.Boundary.Center.X;
        line.Y1 = qTree.Boundary.Center.Y - qTree.Boundary.HalfDimension;
        line.Y2 = qTree.Boundary.Center.Y;
        myCanvas.Children.Add(line);
        if (qTree.Points != null)
        {
            foreach (var point in qTree.Points)
            {
                Ellipse circle = new Ellipse()
                {
                    Width = 6,
                    Height = 6,
                    Stroke = Brushes.Green,
                    StrokeThickness = 6,
                    
                };

                myCanvas.Children.Add(circle);

                circle.SetValue(Canvas.LeftProperty, (double)point.X);
                circle.SetValue(Canvas.TopProperty, (double)point.Y);
            }
        }

        double width = qTree.Boundary.HalfDimension * 2;
        double height = qTree.Boundary.HalfDimension * 2;
        Rectangle rectangle = new Rectangle();
        rectangle.StrokeThickness = 1;
        rectangle.Stroke = Brushes.Red; 
        rectangle.Width = width;
        rectangle.Height = height;
        Canvas.SetLeft(rectangle, qTree.Boundary.Center.X - qTree.Boundary.HalfDimension);
        Canvas.SetTop(rectangle, qTree.Boundary.Center.Y - qTree.Boundary.HalfDimension);
        myCanvas.Children.Add(rectangle);

        if (qTree.Divided)
        {
            if (qTree.NorthWest != null)
            {
                Draw((PointRegionQuadtree)qTree.NorthWest);
            }
            if (qTree.NorthEast != null)
            {
                Draw((PointRegionQuadtree)qTree.NorthEast);
            }
            if (qTree.SouthWest != null)
            {
                Draw((PointRegionQuadtree)qTree.SouthWest);
            }
            if (qTree.SouthEast != null)
            {
                Draw((PointRegionQuadtree)qTree.SouthEast);
            }
        }
     
    }
}
