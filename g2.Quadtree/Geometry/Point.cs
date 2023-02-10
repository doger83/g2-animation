namespace g2.Datastructures.Library.Geometry;

public struct Point
{
    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    public double X { get; set; }
    public double Y { get; set; }
}
