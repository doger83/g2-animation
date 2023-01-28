
const int WIDTH = 200;
const int HEIGHT = 200;
const int X = 200;
const int Y = 200;
const int CAPACATY = 1;

Quadrant boundingBox = new(X, Y, WIDTH, HEIGHT);
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