using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;
using Vector2D = g2.Datastructures.Geometry.Vector2D;

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
        speed = new Vector2D(50, 0);

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

    private readonly Vector2D oppositeX = new(-1, 1);
    private readonly Vector2D oppositeY = new(1, -1);
    private bool LeftBoundary;
    private bool RightBoundary;
    private bool TopBoundary;
    private bool BottomBoundary;

    public void Boundary()
    {
        LeftBoundary = position.X - radius < 0;
        RightBoundary = position.X > quadrant.Width - radius;
        TopBoundary = position.Y - radius < 0;
        BottomBoundary = position.Y > quadrant.Height - radius;

        if (LeftBoundary)
        {
            speed *= oppositeX;
            position.X = Radius;
            return;
        }

        if (RightBoundary)
        {
            speed *= oppositeX;
            position.X = quadrant.Width - Radius;
            return;
        }

        if (TopBoundary)
        {
            speed *= oppositeY;
            position.Y = Radius;
            return;
        }

        if (BottomBoundary)
        {
            speed *= oppositeY;
            position.Y = quadrant.Height - Radius;
            return;
        }
    }
}
