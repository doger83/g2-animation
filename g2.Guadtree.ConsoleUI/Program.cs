
AxisAlignedBoundingBox boundingBox = new(new Point(25, 25), 25);
PointRegionQuadtree qTree = new(boundingBox);
qTree.Insert(new Point(5, 5));
//qTree.Insert(new Point(10, 10));
//qTree.Insert(new Point(15, 15));
//qTree.Insert(new Point(25, 25));
//qTree.Insert(new Point(40, 40));
//qTree.Insert(new Point(12, 12));
//qTree.Insert(new Point(40, 40));
//qTree.Insert(new Point(40, 40));
//qTree.Insert(new Point(40, 40));
//qTree.Insert(new Point(40, 40));
//qTree.Insert(new Point(40, 40));
Console.WriteLine(qTree);

Console.ReadKey();