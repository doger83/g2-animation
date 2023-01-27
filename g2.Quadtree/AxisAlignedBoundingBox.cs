using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public readonly struct AxisAlignedBoundingBox : IAxisAlignedBoundingBox
{

    private readonly Point _center;
    private readonly int _halfDimension;

    public AxisAlignedBoundingBox()
    {
        _center = new Point();
        _halfDimension = 1;
    }

    public AxisAlignedBoundingBox(Point center, int halfDimension)
    {
        _halfDimension = halfDimension;
        _center = center;
    }
    
    public int HalfDimension => _halfDimension;

    public Point Center => _center;

    public bool ContainsPoint(Point point) 
    {
        bool result = !((point.X < _center.X - _halfDimension) || (point.X > _center.X + _halfDimension) ||
                       (point.Y < _center.Y - _halfDimension) || (point.Y > _center.Y + _halfDimension));

        return result;
    }
    public bool IintersectsAABB(Point other) { throw new NotImplementedException(); }

    public override string ToString()
    {
        return @$"Point at x: {Center.X} y: {Center.Y}
HalfDimension: {HalfDimension}";
    }

}
