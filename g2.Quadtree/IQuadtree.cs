//namespace g2.Quadtree;

//public interface IQuadtree
//{
//    // Axis-aligned bounding box stored as a center with half-dimensions
//    // to represent the boundaries of this quad tree
//    IAxisAlignedBoundingBox Boundary { get; set; }

//    // Points in this quad tree node
//    //Array of XY[size = QT_NODE_CAPACITY] points;
//    List<Point>? Points { get; set; }

//    // Children
//    IQuadtree? NorthWest { get; set; }
//    IQuadtree? NorthEast { get; set; }
//    IQuadtree? SouthWest { get; set; }
//    IQuadtree? SouthEast { get; set; }

//    // Methods

//    void QueryRange(AxisAlignedBoundingBox range);
//    void Subdivide();
//}