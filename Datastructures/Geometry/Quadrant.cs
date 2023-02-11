namespace g2.Datastructures.Geometry;

// ToDo: Quadrant struct?
public class Quadrant /// ToDo: Quadrant : AABB
{
    // ToDo: make Quadrant resize dynamicly when resizing window
    // ToDo: use fields!
    public Quadrant(double x, double y, double width, double height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public double X { get; }
    public double Y { get; }
    public double Width { get; }
    public double Height { get; }

    public bool Contains(Point point)
    {
        return point.X >= X - Width &&
        point.X <= X + Width &&
        point.Y >= Y - Height &&
        point.Y <= Y + Height
    ;
    }

    public bool Intersects(Quadrant searchWindow)
    {
        return !
    (
        searchWindow.X - searchWindow.Width > X + Width ||
        searchWindow.X + searchWindow.Width < X - Width ||
        searchWindow.Y - searchWindow.Height > Y + Height ||
        searchWindow.Y + searchWindow.Height < Y - Height
    );
    }
}

public enum Quadrants
{
    NorthWest,
    NorthEast,
    SouthWest,
    SouthEast
}

