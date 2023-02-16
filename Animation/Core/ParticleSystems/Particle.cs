using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using NetTopologySuite.Mathematics;

namespace g2.Animation.Core.ParticleSystems;

public class Particle
{
    private Vector2D position;
    private Vector2D speed;

    private int index = -1;
    private readonly Quadrant quadrant;
    // ToDo: Add Regions like WPF Samples
    private double x;
    private double y;
    private double radius;
    private double xSpeed = 1000;
    private double ySpeed = 0;


    private double lastX;
    private double lastY;
    private double lastXSpeed;
    private double lastYSpeed;

    public Particle(double x, double y, double radius, Quadrant quadrant)
    {
        position = new Vector2D(x, y);

        this.x = x;
        this.y = y;
        // ToDo: Add Z for deepth calculations
        this.radius = radius;
        this.quadrant = quadrant;

        lastX = x;
        lastY = y;
        lastXSpeed = xSpeed;
        lastYSpeed = ySpeed;
    }


    public int Index
    {
        get => index;
        set => index = value;
    }


    public double X => x;
    public double Y => y;
    public double XSpeed
    {
        get => xSpeed;
        set => xSpeed = value;
    }
    public double YSpeed
    {
        get => ySpeed;
        set => ySpeed = value;
    }
    public double Radius
    {
        get => radius;
        set => radius = value;
    }

    public double LastX { get => lastX; set => lastX = value; }
    public double LastY { get => lastY; set => lastY = value; }
    public double LastXSpeed { get => lastXSpeed; set => lastXSpeed = value; }
    public double LastYSpeed { get => lastYSpeed; set => lastYSpeed = value; }
    public Vector2D Position { get => position; set => position = value; }
    public Vector2D Speed { get => speed; set => speed = value; }

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        position += speed * Time.DeltaTime; //new Vector2((float)Time.DeltaTime, (float)Time.DeltaTime); // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {x} XSpeed: {XSpeed} dt: {Time.DeltaTime:G25}");
    }

    public void Pause()
    {
        lastX = x;
        lastY = y;
        lastXSpeed = xSpeed;
        lastYSpeed = ySpeed;

    }

    public void Reset()
    {
        x = lastX;
        y = lastY;
        xSpeed = lastXSpeed;
        ySpeed = lastYSpeed;
    }

    private const double negOne = -1;
    private const double one = 1;
    private readonly Vector2D oppositeX = new(negOne, one);
    private readonly Vector2D oppositeY = new(one, negOne);

    public void Boundary()
    {
        bool LeftBoundary = position.X - radius < 0;
        bool RightBoundary = position.X > quadrant.Width - radius;
        bool TopBoundary = position.Y - radius < 0;
        bool BottomBoundary = position.Y > quadrant.Height - radius;




        if (LeftBoundary)
        {
            speed *= oppositeX;
            position = new Vector2D(Radius, position.Y);
        }

        if (RightBoundary)
        {
            speed *= oppositeX;
            position = new Vector2D(quadrant.Width - Radius, position.Y);
        }

        if (TopBoundary)
        {
            speed *= oppositeY;
            position = new Vector2D(position.X, Radius);
        }

        if (BottomBoundary)
        {
            speed *= oppositeY;
            position = new Vector2D(position.X, quadrant.Height - Radius);
        }
    }
}
