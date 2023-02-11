using g2.Animation.UI.WPF.Shapes.Library.ParticleShapes;
using System.Windows.Shapes;

namespace g2.Animation.TestWPFDesktopApp.ViewModels;
internal class ParticleViewModel
{
    private double x;
    private double y;
    private double z;
    private double radius;
    private double xSpeed;
    private double ySpeed;
    private Ellipse shape;

    public ParticleViewModel(double x, double y, double radius)
    {
        X = x;
        Y = y;
        Z = 0;
        Radius = radius;
        shape = ParticleShapes.CircleBasis(Radius);
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
    public double Z
    {
        get => z;
        set => z = value;
    }
    public double Radius
    {
        get => radius;
        set => radius = value;
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

    public Ellipse Shape
    {
        get => shape;
        set => shape = value;
    }
}
