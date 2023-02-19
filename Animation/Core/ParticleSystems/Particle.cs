using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using Vector2D = g2.Datastructures.Geometry.Vector2D;

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


    public int Index { get => index; set => index = value; }

    public double X => position.X;

    public double Y => position.Y;

    public double Radius { get => radius; set => radius = value; }

    public Vector2D Position { get => position; set => position = value; }
    public Vector2D Speed { get => speed; set => speed = value; }

    public void Move()
    {
        position += speed * Time.DeltaTime; //new Vector2((float)Time.DeltaTime, (float)Time.DeltaTime); // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"Move X:\t{position.X}\tXSpeed:\t{xSpeed}\tdt:\t{Time.DeltaTime:G65}");
    }


    private bool crossendTopBoundary;
    private bool crossedRightBoundary;
    private bool crossedBottomBoundary;
    private bool crossedLeftBoundary;

    public void Boundary()
    {
        crossendTopBoundary = position.Y - radius < 0;
        crossedRightBoundary = position.X > quadrant.Width - radius;
        crossedBottomBoundary = position.Y > quadrant.Height - radius;
        crossedLeftBoundary = position.X - radius < 0;

        if (!(crossedLeftBoundary || crossedRightBoundary || crossendTopBoundary || crossedBottomBoundary))
        {
            // No boundary conditions are met, so early
            return;
        }

        if (crossedLeftBoundary)
        {
            speed.X *= -1;
            position.X = Radius;
            return;
        }

        if (crossedRightBoundary)
        {
            speed.X *= -1;
            position.X = quadrant.Width - Radius;
            return;
        }

        if (crossendTopBoundary)
        {
            speed.Y *= -1;
            position.Y = Radius;
            return;
        }

        if (crossedBottomBoundary)
        {
            speed.Y *= -1;
            position.Y = quadrant.Height - Radius;
            return;
        }
    }
}
