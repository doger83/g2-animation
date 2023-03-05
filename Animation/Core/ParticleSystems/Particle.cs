using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using System.Diagnostics;
using System.Timers;

namespace g2.Animation.Core.ParticleSystems;
// ToDo: Add Regions like WPF Samples

public class Particle
{
    private int index = -1;
    private readonly double width;
    private readonly double height;
    private readonly Quadrant boundingBox;

    private Vector2D location;
    private Vector2D velocity;
    private Vector2D acceleration;
    private Vector2D rotation;
    private Vector2D position;

    public Particle(double x, double y, double width, double height, Quadrant quadrant)
    {
        location = new Vector2D(x, y);
        velocity = new Vector2D(0, 0);
        acceleration = new Vector2D(0, 0);
        // ToDo: Add Z for deepth calculations
        this.width = width;
        this.height = height;
        boundingBox = quadrant;
    }

    public int Index
    {
        get => index;
        set => index = value;
    }

    public double X
    {
        get => location.X;
    }

    public double Y
    {
        get => location.Y;
    }

    public double Width
    {
        get => width;
    }

    public double Height
    {
        get => height;
    }

    public Vector2D Location
    {
        get => location;
        init => location = value;
    }

    public Vector2D Velocity
    {
        get => velocity;
        init => velocity = value;
    }

    public Vector2D Acceleration
    {
        get => acceleration;
        init => acceleration = value;
    }

    public void Update()
    {
        velocity.Add(Time.DeltaTime * acceleration);
        location.Add(Time.DeltaTime * velocity);

        //Debug.WriteLine($"Move X:\t{position.X}\tXSpeed:\t{speed.X}\tdt:\t{Time.DeltaTime:G65}");
    }

    public void FixedUpdate()
    {
        velocity += Time.FixedDeltaTime * acceleration;
        location += Time.FixedDeltaTime * velocity;

        //Debug.WriteLine($"Move X:\t{position.X}\tXSpeed:\t{speed.X}\tdt:\t{Time.FixedDeltaTime:G65}");
    }

    public void CheckBoundaries()
    {
        bool crossedTopBoundary = location.Y - width < 0;
        bool crossedRightBoundary = location.X > boundingBox.Width - width;
        bool crossedBottomBoundary = location.Y > boundingBox.Height - width;
        bool crossedLeftBoundary = location.X - width < 0;
        bool crossedNoBoundary = !(crossedLeftBoundary || crossedRightBoundary || crossedTopBoundary || crossedBottomBoundary);

        if (crossedNoBoundary)
        {
            // No boundary conditions are met, so early
            return;
        }

        if (crossedLeftBoundary)
        {
            velocity.NegateX();
            location.Reset(width, location.Y);
            return;
        }

        if (crossedRightBoundary)
        {
            velocity.NegateX();
            location.Reset(boundingBox.Width - Width, location.Y);
            return;
        }

        if (crossedTopBoundary)
        {
            velocity.NegateY();
            location.Reset(location.X, Width);
            return;
        }

        if (crossedBottomBoundary)
        {
            velocity.NegateY();
            location.Reset(location.X, boundingBox.Height - Width);

            return;
        }
    }
}
