using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public class PointRegionQuadtree : Quadtree, IPointRegionQuadtree
{
    public bool Divided { get; set; } = false;
    public PointRegionQuadtree(IAxisAlignedBoundingBox boundary) : base(boundary)
    {
        Divided = false;
    }

    public override IAxisAlignedBoundingBox Boundary { get => _boundary; set => _boundary = value; }
    public override List<Point>? Points { get => _points; set => _points = value; }
    public override IQuadtree? NorthWest { get => (IPointRegionQuadtree)_northWest!; set => _northWest = (IPointRegionQuadtree)value!; }
    public override IQuadtree? NorthEast { get => (IPointRegionQuadtree)_northEast!; set => _northEast = (IPointRegionQuadtree)value!; }
    public override IQuadtree? SouthWest { get => (IPointRegionQuadtree)_southWest!; set => _southWest = (IPointRegionQuadtree)value!; }
    public override IQuadtree? SouthEast { get => (IPointRegionQuadtree)_southEast!; set => _southEast = (IPointRegionQuadtree)value!; }

    public void Insert(Point point)
    {
        Points ??= new List<Point>();
        // Ignore objects that do not belong in this quad tree
        if (!Boundary.ContainsPoint(point))
            return; // object cannot be added


        // If there is space in this quad tree and if doesn't have subdivisions, add the object here
        if (Points.Count < NODE_CAPACITY)
        {
            Points.Add(point);
        } // Otherwise, subdivide and then add the point to whichever node will accept it
        else
        {
            if (!Divided)
            {

                Subdivide();
                Divided = true;
            }
        // We have to add the points/data contained in this quad array to the new quads if we only want
        // the last node to hold the data

            (NorthWest as IPointRegionQuadtree)!.Insert(point);
            (NorthEast as IPointRegionQuadtree)!.Insert(point);
            (SouthWest as IPointRegionQuadtree)!.Insert(point);
            (SouthEast as IPointRegionQuadtree)!.Insert(point);
        }

        // Otherwise, the point cannot be inserted for some unknown reason (this should never happen)       
    }

    public override void QueryRange(AxisAlignedBoundingBox range)
    {
        throw new NotImplementedException();
    }

    public override void Subdivide()
    {
        int newHalfDimension = _boundary.HalfDimension / 2;
        int oldBounderyX = _boundary.Center.X;
        int oldBounderyY = _boundary.Center.Y;

        NorthWest ??= new PointRegionQuadtree(
            new AxisAlignedBoundingBox(
                new Point(oldBounderyX - newHalfDimension,
                          oldBounderyY - newHalfDimension),
                          newHalfDimension));

        NorthEast ??= new PointRegionQuadtree(
        new AxisAlignedBoundingBox(
            new Point(oldBounderyX + newHalfDimension,
                      oldBounderyY - newHalfDimension),
                      newHalfDimension));

        SouthWest ??= new PointRegionQuadtree(
        new AxisAlignedBoundingBox(
            new Point(oldBounderyX - newHalfDimension,
                      oldBounderyY + newHalfDimension),
                      newHalfDimension));

        SouthEast ??= new PointRegionQuadtree(
        new AxisAlignedBoundingBox(
            new Point(oldBounderyX + newHalfDimension,
                      oldBounderyY + newHalfDimension),
                      newHalfDimension));
    }

    public override string ToString()
    {
        return @$"----------
Boundary: {Boundary} 
Points: {(Points is null ? "null" : Points.Count.ToString())} 
NW: {(NorthWest is null ? "null" : NorthWest.ToString())} 
NE: {(NorthEast is null ? "null" : NorthEast.ToString())} 
SW: {(SouthWest is null ? "null" : SouthWest.ToString())} 
SE: {(SouthEast is null ? "null" : SouthEast.ToString())}";
    }
}
