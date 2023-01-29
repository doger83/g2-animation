using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace g2.Quadtree;

public class PointRegionQuadtree //: IQuadtree
{
    public PointRegionQuadtree(Quadrant boundary, int capacaty)
    {
        Boundary = boundary;
        Capacaty = capacaty;
        Points = new();
        Divided = false;
    }

    public Quadrant Boundary { get; }
    public int Capacaty { get; }
    public bool Divided { get; private set; } 
    public List<Point> Points { get; private set; }
    public PointRegionQuadtree? NorthWest { get; private set; }
    public PointRegionQuadtree? NorthEast { get; private set; }
    public PointRegionQuadtree? SouthEast { get; private set; }
    public PointRegionQuadtree? SouthWest { get; private set; }


    public bool Insert(Point point)
    {
        if (!Boundary.Contains(point))
        {
            return false;
        }

        if (!Divided)
        {
            // Todo: Points may be null here! 
            if (Points.Count < Capacaty)
            {
                Points.Add(point);
                return true;
            }
            Subdivide();
        }

        return // Todo: add Testcase in case this happens to throw Ex
            (NorthEast?.Insert(point) ?? throw new NullReferenceException($"{nameof(NorthEast)} cannot be null")) ||
            (NorthWest?.Insert(point) ?? throw new NullReferenceException($"{nameof(NorthWest)} cannot be null")) ||
            (SouthEast?.Insert(point) ?? throw new NullReferenceException($"{nameof(SouthEast)} cannot be null")) ||
            (SouthWest?.Insert(point) ?? throw new NullReferenceException($"{nameof(SouthWest)} cannot be null"))
            ;
    }

    private void Subdivide()
    {
        InitializeSubQuadrants();
        MovePointsToChildren();              
        Divided = true;
    }

    private void InitializeSubQuadrants()
    {
        var x = Boundary.X;
        var y = Boundary.Y;
        var w = Boundary.Width;
        var h = Boundary.Height;

        var ne = new Quadrant(x + w / 2, y - h / 2, w / 2, h / 2);
        NorthEast = new(ne, Capacaty);

        var nw = new Quadrant(x - w / 2, y - h / 2, w / 2, h / 2);
        NorthWest = new(nw, Capacaty);

        var se = new Quadrant(x + w / 2, y + h / 2, w / 2, h / 2);
        SouthEast = new(se, Capacaty);

        var sw = new Quadrant(x - w / 2, y + h / 2, w / 2, h / 2);
        SouthWest = new(sw, Capacaty);
    }

    private void MovePointsToChildren()
    {
        if (NorthWest is null || NorthEast is null || SouthEast is null || SouthWest is null)
        {
            throw new NullReferenceException("Children must be initialized before you can move points into them! Hint: maybe you changed order in methodcalls?");
        }
        // This improves performance by placing points in the smallest available rectangle.
        for (int i = 0; i < Points.Count; i++)
        {
            Point? p = Points[i];
            var inserted =
                NorthWest.Insert(p) ||
                NorthEast.Insert(p) ||
                SouthEast.Insert(p) ||
                SouthWest.Insert(p);

            if (!inserted)
            {
                // Todo: add more Argument Validation!
                throw new ArgumentOutOfRangeException("capacity must be greater than 0");
            }
        }

        Points = new();
    }
}
