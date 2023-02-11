using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.ParticleSystems;

public class Particle
{
    private readonly Quadrant quadrant;
    // ToDo: Add Regions like WPF Samples
    private double x;
    private double y;
    private double radius;
    private double xSpeed = 1000;
    private double ySpeed = 0;

    public Particle(double x, double y, double radius, Quadrant quadrant)
    {
        this.x = x;
        this.y = y;
        // ToDo: Add Z for deepth calculations
        this.radius = radius;
        this.quadrant = quadrant;
    }

    public double X
    {
        get => x;
        set => x = value;
    }
    public double Y
    {
        get => y;
        set => y = value;
    }
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

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        x += xSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        y += ySpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime:G25}");
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
