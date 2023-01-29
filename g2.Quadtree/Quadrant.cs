using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public  class Quadrant // : Rectangle
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
    
        point.X >= this.X - this.Width  &&
        point.X <= this.X + this.Width  &&
        point.Y >= this.Y - this.Height &&
        point.Y <= this.Y + this.Height
    ;


    public bool Intersects(Quadrant searchWindow) => !
    (
        searchWindow.X - searchWindow.Width  > this.X + this.Width  ||
        searchWindow.X + searchWindow.Width  < this.X - this.Width  ||
        searchWindow.Y - searchWindow.Height > this.Y + this.Height ||
        searchWindow.Y + searchWindow.Height < this.Y - this.Height
    );
}
