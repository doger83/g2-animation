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
    private readonly AxisAlignedBoundingBox Boundary; // { get; set; }
    private readonly int Capacaty;
    private readonly List<Point> Points;
    private bool Divided;

    public PointRegionQuadtree(AxisAlignedBoundingBox boundary, int capacaty)
    {
        Boundary = boundary;
        Capacaty = capacaty;
        Points = new();
        Divided = false;
    }

    public bool Insert(Point point)
    {
        if (!Boundary.Contains(point))
        {
            return false;
        }

        if (Points.Count < Capacaty)
        {
            Points.Add(point);
            return true;
        }
        else
        {
            if (!this.Divided)
            {
                Subdivide();
            }

            if (NorthEast!.Insert(point))
            {
                return true;
            }
            else if (NorthWest!.Insert(point))
            {
                return true;
            }
            else if(SouthEast!.Insert(point))
            {
                return true;
            }
            else if(SouthWest!.Insert(point))
            {
                return true;
            }
        }
        return false;
    }

    public PointRegionQuadtree? NorthEast;
    public PointRegionQuadtree? NorthWest;
    public PointRegionQuadtree? SouthEast;
    public PointRegionQuadtree? SouthWest;
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
