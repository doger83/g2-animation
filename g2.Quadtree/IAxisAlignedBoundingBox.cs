namespace g2.Quadtree
{
    public interface IAxisAlignedBoundingBox
    {
        Point Center { get; }
        int HalfDimension { get; }

        bool ContainsPoint(Point point);
        bool IintersectsAABB(Point other);
    }
}