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
        Capacity = capacaty;
        Points = new();
        Divided = false;
    }

    public Quadrant Boundary { get; }
    public int Capacity { get; }
    public bool Divided { get; private set; } 

    public List<Point>? Points { get; private set; }
    public PointRegionQuadtree? NorthWest { get; private set; }
    public PointRegionQuadtree? NorthEast { get; private set; }
    public PointRegionQuadtree? SouthEast { get; private set; }
    public PointRegionQuadtree? SouthWest { get; private set; }


    public bool InsertChatgpt(Point point)
    {
        if (!Boundary.Contains(point))
        {
            return false;
        }

        var currentNode = this;
        while (currentNode.Divided)
        {
            var quadrant = currentNode.GetQuadrant(point);
            currentNode = quadrant switch
            {
                Quadrants.NorthEast => currentNode.NorthEast,
                Quadrants.NorthWest => currentNode.NorthWest,
                Quadrants.SouthEast => currentNode.SouthEast,
                Quadrants.SouthWest => currentNode.SouthWest,
                _ => throw new Exception("Unexpected quadrant value"),
            };
        }

        if ((currentNode.Points ??= new List<Point>()).Count < Capacity)
        {
            currentNode.Points.Add(point);
            return true;
        }
        currentNode.Subdivide();
        return currentNode.InsertChatgpt(point);
    }

    public Quadrants GetQuadrant(Point point)
    {
        var xMidpoint = (Boundary.X - (Boundary.Width/2) + Boundary.X + (Boundary.Width/2)) / 2;
        var yMidpoint = (Boundary.Y - (Boundary.Height/2) + Boundary.Y + (Boundary.Height / 2)) / 2;

        var inNorth = point.Y >= xMidpoint;
        var inWest = point.X < yMidpoint;

        if (inNorth)
        {
            return inWest ? Quadrants.NorthWest : Quadrants.NorthEast;
        }
        else
        {
            return inWest ? Quadrants.SouthWest : Quadrants.SouthEast;
        }
    }

    public bool Insert(Point point)
    {
        if (!Boundary.Contains(point))
        {
            return false;
        }

        if (!Divided)
        {
            // Todo: Points may be null here! 
            if ((Points ??= new()).Count < Capacity)
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
        NorthEast = new(ne, Capacity);

        var nw = new Quadrant(x - w / 2.0, y - h / 2.0, w / 2.0, h / 2.0);
        NorthWest = new(nw, Capacity);

        var se = new Quadrant(x + w / 2.0, y + h / 2.0, w / 2.0, h / 2.0);
        SouthEast = new(se, Capacity);

        var sw = new Quadrant(x - w / 2.0, y + h / 2.0, w / 2.0, h / 2.0);
        SouthWest = new(sw, Capacity);
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
                // Todo: add more Argument Validation! this can go to setter of Capacaty
                throw new ArgumentOutOfRangeException("capacity must be greater than 0");
            }
        }
        Points = null;
    }
}
