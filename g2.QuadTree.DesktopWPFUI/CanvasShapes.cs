using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace g2.Datastructures.DesktopWPFUI;
internal static class CanvasShapes
{
    public static Canvas GetCanvas(double width, double height)
    {
        Canvas canvas = new()
        {
            Width = width,
            Height = height,
            Margin = new Thickness(0, 0, 0, 34)
        };
        //canvas.MouseLeftButtonDown += MyCanvas_LeftMouseDown;
        //canvas.MouseRightButtonDown += MyCanvas_RightMouseDown;

        RadialGradientBrush brush = new()
        {
            MappingMode = BrushMappingMode.RelativeToBoundingBox,
            SpreadMethod = GradientSpreadMethod.Pad,
            RadiusX = 0.7,
            RadiusY = 0.7
        };
        GradientStop gradientStop1 = new(Colors.Black, 1);
        GradientStop gradientStop2 = new(Color.FromRgb(69, 69, 69), 0);
        brush.GradientStops.Add(gradientStop1);
        brush.GradientStops.Add(gradientStop2);

        canvas.Background = brush;

        AddGridLines(canvas);

        return canvas;
    }

    internal static void SetCanvas(Canvas canvas, double width, double height)
    {
        canvas.Width = width;
        canvas.Height = height;
        canvas.Margin = new Thickness(0, 0, 0, 34);

        //canvas.MouseLeftButtonDown += MyCanvas_LeftMouseDown;
        //canvas.MouseRightButtonDown += MyCanvas_RightMouseDown;

        RadialGradientBrush brush = new()
        {
            MappingMode = BrushMappingMode.RelativeToBoundingBox,
            SpreadMethod = GradientSpreadMethod.Pad,
            RadiusX = 0.7,
            RadiusY = 0.7
        };
        GradientStop gradientStop1 = new(Colors.Black, 1);
        GradientStop gradientStop2 = new(Color.FromRgb(69, 69, 69), 0);
        brush.GradientStops.Add(gradientStop1);
        brush.GradientStops.Add(gradientStop2);

        canvas.Background = brush;

        //AddGridLines(canvas);


    }

    public static void AddGridLines(Canvas canvas)
    {
        for (double x = 0; x <= canvas.ActualWidth; x += 50)
        {
            Line line = new()
            {


                Stroke = System.Windows.Media.Brushes.White,
                X1 = x,
                Y1 = 0,
                X2 = x,
                Y2 = canvas.ActualHeight,
                StrokeThickness = 0.25,
                Opacity = 0.25,

            };
            _ = canvas.Children.Add(line);
        }

        for (double y = 0; y <= canvas.ActualHeight; y += 50)
        {
            Line line = new()
            {
                Stroke = System.Windows.Media.Brushes.White,
                X1 = 0,
                Y1 = y,
                X2 = canvas.ActualWidth,
                Y2 = y,
                StrokeThickness = 0.25,
                Opacity = 0.25,
            };
            _ = canvas.Children.Add(line);
        }
    }



    #region Events
    private static void MyCanvas_LeftMouseDown(object sender, MouseButtonEventArgs e)
    {
        //DrawSearchwindowAtMousePosition(sender, e);
    }

    private static void MyCanvas_RightMouseDown(object sender, MouseButtonEventArgs e)
    {
        //AddPointAtMousePositionToTree(sender, e);
    }

    #endregion

    /*
     *  //AddRandomPointsToTree(GROWINGRATE);
        //myCanvas.Children.Clear();
        //Draw(quadTree);

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
    */


}
