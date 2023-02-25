using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;

namespace g2.Animation.Core.ParticleSystems;
// ToDo: Add Regions like WPF Samples

public class Particle
{
    private int index = -1;
    private double radius;
    private Vector2D position;
    private Vector2D speed;
    private readonly Quadrant quadrant;

    public Particle(double x, double y, double radius, Quadrant quadrant)
    {
        position = new Vector2D(x, y);
        speed = new Vector2D(0, 0);

        // ToDo: Add Z for deepth calculations
        this.radius = radius;
        this.quadrant = quadrant;
    }

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public double X
    {
        get
        {
            return position.X;
        }
    }

    public double Y
    {
        get
        {
            return position.Y;
        }
    }

    public double Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public Vector2D Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    public Vector2D Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public void Move()
    {
        position.Add(speed * Time.DeltaTime);

        //Debug.WriteLine($"Move X:\t{position.X}\tXSpeed:\t{speed.X}\tdt:\t{Time.FixedDeltaTime:G65}\tdt:\t{Time.DeltaTime:G65}");
    }

    public void Boundary()
    {
        bool crossedTopBoundary = position.Y - radius < 0;
        bool crossedRightBoundary = position.X > quadrant.Width - radius;
        bool crossedBottomBoundary = position.Y > quadrant.Height - radius;
        bool crossedLeftBoundary = position.X - radius < 0;

        if (!(crossedLeftBoundary || crossedRightBoundary || crossedTopBoundary || crossedBottomBoundary))
        {
            // No boundary conditions are met, so early
            return;
        }

        if (crossedLeftBoundary)
        {
            speed.NegateX();
            position = new Vector2D(radius, position.Y);
            return;
        }

        if (crossedRightBoundary)
        {
            speed.NegateX();
            position = new Vector2D(quadrant.Width - Radius, position.Y);
            return;
        }

        if (crossedTopBoundary)
        {
            speed.NegateY();
            position = new Vector2D(position.X, Radius);
            return;
        }

        if (crossedBottomBoundary)
        {
            speed.NegateY();
            position = new Vector2D(position.X, quadrant.Height - Radius);
            return;
        }
    }
}
