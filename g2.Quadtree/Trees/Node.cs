using g2.Datastructures.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Datastructures.Trees;

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
