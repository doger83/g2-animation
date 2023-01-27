using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public struct Node
{
    private Point _point;
    private int _value;

    public Node()
    {
        _point = new Point();
        _value = 0;
    }

    public Node(Point point, int value)
    {
        _point = point;
        _value = value;
    }
}
