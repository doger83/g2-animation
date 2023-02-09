using g2.Animation.UI.WPF.ParticleShapes;
using System.Windows.Shapes;

namespace g2.Animation.DesktopWPFUI.ViewModels;
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
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public double Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
    public double Z
    {
        get
        {
            return z;
        }
        set
        {
            z = value;
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
    public double XSpeed
    {
        get
        {
            return xSpeed;
        }
        set
        {
            xSpeed = value;
        }
    }
    public double YSpeed
    {
        get
        {
            return ySpeed;
        }
        set
        {
            ySpeed = value;
        }
    }

    public Ellipse Shape
    {
        get
        {
            return shape;
        }
        set
        {
            shape = value;
        }
    }



}
