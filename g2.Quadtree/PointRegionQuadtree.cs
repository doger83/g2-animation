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
    private readonly AxisAlignedBoundingBox boundary;
    private readonly int capacaty;
    private readonly List<Point> points;
    private bool divided;

    private PointRegionQuadtree? northEast;
    private PointRegionQuadtree? northWest;
    private PointRegionQuadtree? southEast;
    private PointRegionQuadtree? southWest;

    public PointRegionQuadtree(AxisAlignedBoundingBox boundary, int capacaty)
    {
        this.boundary = boundary;
        this.capacaty = capacaty;
        this.points = new();
        this.divided = false;
    }

    public AxisAlignedBoundingBox Boundary { get => boundary; /*set => boundary = value;*/ }
    public List<Point> Points { get => points; /*set => points = value;*/ }
    public bool Divided { get => divided; /*set => divided = value;*/ } 
    public PointRegionQuadtree? NorthWest { get => northWest; /*set => northWest = value;*/ }
    public PointRegionQuadtree? NorthEast { get => northEast; /*set => northEast = value;*/ }
    public PointRegionQuadtree? SouthEast { get => southEast; /*set => southEast = value;*/ }
    public PointRegionQuadtree? SouthWest { get => southWest; /*set => southWest = value;*/ }


    public bool Insert(Point point)
    {
        if (!boundary.Contains(point))
        {
            return false;
        }

        if (points.Count < capacaty)
        {
            points.Add(point);
            return true;
        }
        else
        {
            if (!this.divided)
            {
                Subdivide();
            }

            if (northEast!.Insert(point))
            {
                return true;
            }
            else if (northWest!.Insert(point))
            {
                return true;
            }
            else if (southEast!.Insert(point))
            {
                return true;
            }
            else if (southWest!.Insert(point))
            {
                return true;
            }
        }
        return false;
    }

    private void Subdivide()
    {
        var x = boundary.X;
        var y = boundary.Y;
        var w = boundary.Width;
        var h = boundary.Height;

        var ne = new AxisAlignedBoundingBox(x + w / 2, y - h / 2, w / 2, h / 2);
        northEast = new PointRegionQuadtree(ne, capacaty);

        var nw = new AxisAlignedBoundingBox(x - w / 2, y - h / 2, w / 2, h / 2);
        northWest = new(nw, capacaty);

        var se = new AxisAlignedBoundingBox(x + w / 2, y + h / 2, w / 2, h / 2);
        southEast = new(se, capacaty);

        var sw = new AxisAlignedBoundingBox(x - w / 2, y + h / 2, w / 2, h / 2);
        southWest = new(sw, capacaty);

        divided = true;
    }
}
