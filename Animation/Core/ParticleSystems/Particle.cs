using g2.Animation.Core.Timing;
using g2.Datastructures.Geometry;

namespace g2.Animation.Core.ParticleSystems;

public class Particle
{
    private readonly Quadrant quadrant;

    public Particle(double x, double y, double radius, Quadrant quadrant)
    {
        X = x;
        Y = y;
        // ToDo: Add Z for deepth calculations
        Radius = radius;
        this.quadrant = quadrant;
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double XSpeed { get; set; } = 1000;
    public double YSpeed { get; set; } = 0;
    public double Radius { get; set; }

    public void Move()
    {
        //Debug.WriteLine("-----------------------");
        //Debug.WriteLine($"before: X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");

        X += XSpeed * Time.DeltaTime; // xspeed; //random.Next(-5, 6);
        Y += YSpeed * Time.DeltaTime; // yspeed; //random.Next(-5, 6);

        //Debug.WriteLine($"after X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime:G25}");
    }

    public void Boundary()
    {
        bool LeftBoundary = X - Radius < 0;
        bool RightBoundary = X > quadrant.Width - Radius;
        bool TopBoundary = Y - Radius < 0;
        bool BottomBoundary = Y > quadrant.Height - Radius;

        if (LeftBoundary)
        {
            XSpeed *= -1;
            X = Radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (RightBoundary)
        {
            XSpeed *= -1;
            X = quadrant.Width - Radius;
            return;
            //Debug.WriteLine("----------Boundary-------------");
            //Debug.WriteLine($"Boundary X: {X} XSpeed: {XSpeed} dt: {Time.DeltaTime.ToString("G25")}");
        }

        if (TopBoundary)
        {
            YSpeed *= -1;
            Y = Radius;
            return;
        }

        if (BottomBoundary)
        {
            YSpeed *= -1;
            Y = quadrant.Height - Radius;
            return;
        }
    }
}
