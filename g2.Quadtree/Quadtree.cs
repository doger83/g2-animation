using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public abstract class Quadtree : IQuadtree
{
    //// Arbitrary constant to indicate how many elements can be stored in this quad tree node
    protected const int NODE_CAPACITY = 4;
    protected IAxisAlignedBoundingBox _boundary;
    protected List<Point>? _points;
    protected IQuadtree? _northWest;
    protected IQuadtree? _northEast;
    protected IQuadtree? _southWest;
    protected IQuadtree? _southEast;

    public Quadtree(IAxisAlignedBoundingBox boundary)
    {
        _boundary = boundary;
    }

    public abstract IAxisAlignedBoundingBox Boundary { get; set; }
    public abstract List<Point>? Points { get; set; }
    public abstract IQuadtree? NorthWest { get; set; }
    public abstract IQuadtree? NorthEast { get; set; }
    public abstract IQuadtree? SouthWest { get; set; }
    public abstract IQuadtree? SouthEast { get; set; }

    public abstract void QueryRange(AxisAlignedBoundingBox range);
    public abstract void Subdivide();
}
