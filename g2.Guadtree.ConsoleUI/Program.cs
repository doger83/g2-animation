
int WIDTH = 200;
int HEIGHT = 200;
int X = 200;
int Y = 200;
int CAPACATY = 1;

AxisAlignedBoundingBox boundingBox = new(X, Y, WIDTH, HEIGHT);
PointRegionQuadtree qTree = new(boundingBox, CAPACATY);

Random rnd = new();

for (int i = 0; i < 50; i++)
{
    int x = rnd.Next(WIDTH);
    int y = rnd.Next(HEIGHT);
    var point = new Point(x, y);

    qTree.Insert(point);
}



Console.ReadKey();