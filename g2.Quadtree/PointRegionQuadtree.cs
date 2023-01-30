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
    public static int Count;
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

    public List<Point>? Points { get; private set; }
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
            if ((Points ??= new()).Count < Capacaty)
            {
                Points.Add(point);
                return true;
            }
            Subdivide();
        }

        return // Todo: add Testcase in case this happens to throw Ex
            (NorthEast.Insert(point)) ||
            (NorthWest!.Insert(point)) ||
            (SouthEast?.Insert(point) ?? false) ||
            (SouthWest?.Insert(point) ?? throw new NullReferenceException($"{nameof(SouthWest)} cannot be null"))
            ;
    }
    public List<Point> Query(Quadrant searchWindow, List<Point>? foundPoints = null)
    {  
        if (!Boundary.Intersects(searchWindow))
        {
            return foundPoints ?? new();
        }
        else
        {
            if (Points is not null)
            {
                // Todo: Points null?
                foreach (var point in Points)
                {
                    Count++;
                    if (searchWindow.Contains(point))
                    {
                        (foundPoints ??= new()).Add(point);
                    }
                }
            }
            if (Divided)
            {
                (foundPoints ??= new()).AddRange(NorthEast!.Query(searchWindow));
                (foundPoints ??= new()).AddRange(NorthWest!.Query(searchWindow));
                (foundPoints ??= new()).AddRange(SouthEast!.Query(searchWindow));
                (foundPoints ??= new()).AddRange(SouthWest!.Query(searchWindow));

            }
            return foundPoints ?? new();
        }
    }    

    private void Subdivide()
    {
        InitializeSubQuadrants();
        MovePointsToSubQuadrants();              
        Divided = true;
    }

    private void InitializeSubQuadrants()
    {
        var x = Boundary.X;
        var y = Boundary.Y;
        var w = Boundary.Width;
        var h = Boundary.Height;

        var ne = new Quadrant(x + w / 2.0, y - h / 2.0, w / 2.0, h / 2.0);
        NorthEast = new(ne, Capacaty);

        var nw = new Quadrant(x - w / 2.0, y - h / 2.0, w / 2.0, h / 2.0);
        NorthWest = new(nw, Capacaty);

        var se = new Quadrant(x + w / 2.0, y + h / 2.0, w / 2.0, h / 2.0);
        SouthEast = new(se, Capacaty);

        var sw = new Quadrant(x - w / 2.0, y + h / 2.0, w / 2.0, h / 2.0);
        SouthWest = new(sw, Capacaty);
    }

    private void MovePointsToSubQuadrants()
    {
        if (NorthWest is null || NorthEast is null || SouthEast is null || SouthWest is null)
        {
            throw new NullReferenceException("Children must be initialized before you can move points into them! Hint: maybe you changed order in methodcalls?");
        }
        // This improves performance by placing points in the smallest available rectangle.
        for (int i = 0; i < Points?.Count; i++)
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
        Points = null;
    }
}
