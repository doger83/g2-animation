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
    public AxisAlignedBoundingBox Boundary; // { get; set; }
    public int Capacaty;
    public List<Point> Points;
    public bool Divided;

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

            if (this.NorthEast!.Insert(point))
            {
                return true;
            }
            else if (this.NorthWest!.Insert(point))
            {
                return true;
            }
            else if(this.SouthEast!.Insert(point))
            {
                return true;
            }
            else if(this.SouthWest!.Insert(point))
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
        var x = this.Boundary.X;
        var y = this.Boundary.Y;
        var w = this.Boundary.Width;
        var h = this.Boundary.Height;

        var ne = new AxisAlignedBoundingBox(x + w / 2, y - h / 2, w / 2, h / 2);
        NorthEast = new(ne, this.Capacaty);

        var nw = new AxisAlignedBoundingBox(x - w / 2, y - h / 2, w / 2, h / 2);
        NorthWest = new(nw, this.Capacaty);

        var se = new AxisAlignedBoundingBox(x + w / 2, y + h / 2, w / 2, h / 2);
        SouthEast = new(se, this.Capacaty);

        var sw = new AxisAlignedBoundingBox(x - w / 2, y + h / 2, w / 2, h / 2);
        SouthWest = new(sw, this.Capacaty);

        this.Divided = true;
    }
}
