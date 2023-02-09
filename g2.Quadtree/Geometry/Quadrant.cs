using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Datastructures.Geometry;

public class Quadrant // : Rectangle
{
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

    public bool Contains(Point point) =>

        point.X >= X - Width &&
        point.X <= X + Width &&
        point.Y >= Y - Height &&
        point.Y <= Y + Height
    ;


    public bool Intersects(Quadrant searchWindow) => !
    (
        searchWindow.X - searchWindow.Width > X + Width ||
        searchWindow.X + searchWindow.Width < X - Width ||
        searchWindow.Y - searchWindow.Height > Y + Height ||
        searchWindow.Y + searchWindow.Height < Y - Height
    );
}

public enum Quadrants
{
    NorthWest,
    NorthEast,
    SouthWest,
    SouthEast
}

