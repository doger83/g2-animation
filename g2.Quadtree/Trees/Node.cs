using g2.Datastructures.Library.Geometry;

namespace g2.Datastructures.Library.Trees;

public struct Node
{
    private Point point;
    private readonly int value;

    public Node()
    {
        point = new Point();
        value = 0;
    }

    public Node(Point point, int value)
    {
        this.point = point;
        this.value = value;
    }

    public int GetValue()
    {
        return value;
    }

    public Point GetPoint()
    {
        return point;
    }
}
