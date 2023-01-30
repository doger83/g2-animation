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
            (NorthEast!.Insert(point)) ||
            (NorthWest!.Insert(point)) ||
            (SouthEast!.Insert(point)) ||
            (SouthWest!.Insert(point)) ;
    }
    public List<Point> Query(Quadrant searchWindow, List<Point>? foundPoints = null)
    {  
        if (foundPoints == null)
        {
            foundPoints = new List<Point>();
        }
        if (!searchWindow.Intersects(Boundary))
        {
            return foundPoints;
        }
        if (Divided)
        {
            NorthEast!.Query(searchWindow, foundPoints);
            NorthWest!.Query(searchWindow, foundPoints);
            SouthEast!.Query(searchWindow, foundPoints);
            SouthWest!.Query(searchWindow, foundPoints);

        }
        if (Points is not null)
        {
            // Todo: Points null?
            foreach (var point in Points)
            {
                Count++;
                if (searchWindow.Contains(point))
                {
                    foundPoints.Add(point);
                }
            }
        }
        return foundPoints;
    }

    private void Subdivide()
    {
        InitializeSubQuadrants();
        MovePointsToSubQuadrants();              
        
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
        Divided = true;
    }

    private void MovePointsToSubQuadrants()
    {
        if (!Divided)
        {
            throw new InvalidOperationException($"Children must be initialized before you can move points into them! Hint: maybe you called {nameof(MovePointsToSubQuadrants)} before {nameof(InitializeSubQuadrants)} ?");
        }
        // This improves performance by placing points in the smallest available rectangle.
        for (int i = 0; i < Points?.Count; i++)
        {
            Point? p = Points[i];
            var inserted =
                NorthWest!.Insert(p) ||
                NorthEast!.Insert(p) ||
                SouthEast!.Insert(p) ||
                SouthWest!.Insert(p);

            if (!inserted)
            {
                // Todo: add more Argument Validation!
                throw new ArgumentOutOfRangeException("capacity must be greater than 0");
            }
        }
        Points = null;
    }
}
