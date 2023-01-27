using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public class Point
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
    public double X { get; init; }
    public double Y { get; init; }
}
