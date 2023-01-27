using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Quadtree;

public struct Point
{
    private readonly int _x;
    private readonly int _y;

    public Point()
    {
        _x = 0;
        _y = 0;
    }

    public Point(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public int X => _x;

    public int Y => _y;
}
