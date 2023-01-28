using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace g2.Quadtree;

public class PointRegionQuadtree //: IQuadtree
{
    public PointRegionQuadtree(AxisAlignedBoundingBox boundary, int capacaty)
    {
        Boundary = boundary;
        Capacaty = capacaty;
        Points = new();
        Divided = false;
    }

    public AxisAlignedBoundingBox Boundary { get; }
    public int Capacaty { get; }
    public List<Point> Points { get; }
    public bool Divided { get; private set; } 
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
            if (Points.Count < Capacaty)
            {
                Points.Add(point);
                return true;
            }
            Subdivide();
        }

        return (
            NorthEast!.Insert(point) ||
            NorthWest!.Insert(point) ||
            SouthEast!.Insert(point) ||
            SouthWest!.Insert(point)
            );
    }

    private void Subdivide()
    {
        var x = Boundary.X;
        var y = Boundary.Y;
        var w = Boundary.Width;
        var h = Boundary.Height;

        var ne = new AxisAlignedBoundingBox(x + w / 2, y - h / 2, w / 2, h / 2);
        NorthEast = new(ne, Capacaty);

        var nw = new AxisAlignedBoundingBox(x - w / 2, y - h / 2, w / 2, h / 2);
        NorthWest = new(nw, Capacaty);

        var se = new AxisAlignedBoundingBox(x + w / 2, y + h / 2, w / 2, h / 2);
        SouthEast = new(se, Capacaty);

        var sw = new AxisAlignedBoundingBox(x - w / 2, y + h / 2, w / 2, h / 2);
        SouthWest = new(sw, Capacaty);

        Divided = true;
    }
}
