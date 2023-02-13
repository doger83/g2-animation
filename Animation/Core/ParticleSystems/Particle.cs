using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.ParticleSystems;

public class Particle
{
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

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        x += xSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        y += ySpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

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

    public void Boundary()
    {
        bool LeftBoundary = x - radius < 0;
        bool RightBoundary = x > quadrant.Width - radius;
        bool TopBoundary = y - radius < 0;
        bool BottomBoundary = y > quadrant.Height - radius;

        if (LeftBoundary)
        {
            xSpeed *= -1;
            x = radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (RightBoundary)
        {
            xSpeed *= -1;
            x = quadrant.Width - radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (TopBoundary)
        {
            ySpeed *= -1;
            y = radius;
            return;
        }

        if (BottomBoundary)
        {
            ySpeed *= -1;
            y = quadrant.Height - radius;
            return;
        }
    }
}
