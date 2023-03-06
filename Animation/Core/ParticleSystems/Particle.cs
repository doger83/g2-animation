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

    public void CheckBoundaries_basic()
    {
        if (!(location.X - width < 0 || location.X > boundingBox.Width - width || location.Y - height < 0 || location.Y > boundingBox.Height - height))
        {
            // No boundary conditions are met, so early
            return;
        }

        if (location.X - width < 0)
        {
            velocity.NegateX();
            location.Reset(width, location.Y);
            return;
        }

        if (location.X > boundingBox.Width - width)
        {
            velocity.NegateX();
            location.Reset(boundingBox.Width - Width, location.Y);
            return;
        }

        if (location.Y - height < 0)
        {
            velocity.NegateY();
            location.Reset(location.X, Width);
            return;
        }

        if (location.Y > boundingBox.Height - height)
        {
            velocity.NegateY();
            location.Reset(location.X, boundingBox.Height - Width);

            return;
        }
    }

    public void CheckBoundaries_cachedInMethod()
    {
        bool crossedTopBoundary = location.Y < height;
        bool crossedBottomBoundary = location.Y > boundingBox.Height - height;
        bool crossedRightBoundary = location.X > boundingBox.Width - width;
        bool crossedLeftBoundary = location.X < width;
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


    int left = 25;
    int top = 25;
    int right = 550;
    int bottom = 550;
    public void CheckBoundaries_basicWithFixedBounds()
    {

        if (!(location.X < left || location.X > right || location.Y < 0 || location.Y > bottom))
        {
            // No boundary conditions are met, so early
            return;
        }

        if (location.X < left)
        {
            velocity.NegateX();
            location.Reset(width, location.Y);
            return;
        }

        if (location.X > right)
        {
            velocity.NegateX();
            location.Reset(boundingBox.Width - Width, location.Y);
            return;
        }

        if (location.Y < top)
        {
            velocity.NegateY();
            location.Reset(location.X, Width);
            return;
        }

        if (location.Y > bottom)
        {
            velocity.NegateY();
            location.Reset(location.X, boundingBox.Height - Width);

            return;
        }
    }
    public void CheckBoundaries_cachedInMethodWithFixedBounds()
    {
        bool crossedTopBoundary = location.Y < top;
        bool crossedRightBoundary = location.X > right;
        bool crossedBottomBoundary = location.Y > bottom;
        bool crossedLeftBoundary = location.X < left;
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
    bool crossedTopBoundaryGlobal;
    bool crossedRightBoundaryGlobal;
    bool crossedBottomBoundaryGlobal;
    bool crossedLeftBoundaryGlobal;
    bool crossedNoBoundaryGlobal;

    public void CheckBoundaries_cachedGlobal()
    {
        crossedTopBoundaryGlobal = location.Y - width < 0;
        crossedRightBoundaryGlobal = location.X > boundingBox.Width - width;
        crossedBottomBoundaryGlobal = location.Y > boundingBox.Height - width;
        crossedLeftBoundaryGlobal = location.X - width < 0;
        crossedNoBoundaryGlobal = !(crossedLeftBoundaryGlobal || crossedRightBoundaryGlobal || crossedTopBoundaryGlobal || crossedBottomBoundaryGlobal);

        if (crossedNoBoundaryGlobal)
        {
            // No boundary conditions are met, so early
            return;
        }

        if (crossedLeftBoundaryGlobal)
        {
            velocity.NegateX();
            location.Reset(width, location.Y);
            return;
        }

        if (crossedRightBoundaryGlobal)
        {
            velocity.NegateX();
            location.Reset(boundingBox.Width - Width, location.Y);
            return;
        }

        if (crossedTopBoundaryGlobal)
        {
            velocity.NegateY();
            location.Reset(location.X, Width);
            return;
        }

        if (crossedBottomBoundaryGlobal)
        {
            velocity.NegateY();
            location.Reset(location.X, boundingBox.Height - Width);

            return;
        }
    }
}
