using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public  class Quadrant // : IAxisAlignedBoundingBox
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
  
    public bool Contains(Point point)
    {
        bool result = (
            point.X >= this.X - this.Width &&
            point.X <= this.X + this.Width &&
            point.Y >= this.Y - this.Height &&
            point.Y <= this.Y + this.Height
        );

        return result;
    }
}
