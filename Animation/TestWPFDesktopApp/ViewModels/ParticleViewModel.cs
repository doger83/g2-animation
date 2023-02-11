using g2.Animation.UI.WPF.Shapes.Library.ParticleShapes;
using System.Windows.Shapes;

namespace g2.Animation.TestWPFDesktopApp.ViewModels;
internal class ParticleViewModel
{
    public ParticleViewModel(double x, double y, double radius)
    {
        X = x;
        Y = y;
        Z = 0;
        Radius = radius;
        Shape = ParticleShapes.CircleBasis(Radius);
    }

    public double X { get; set; }

    public double Y { get; set; }
    public double Z { get; set; }
    public double Radius { get; set; }
    public double XSpeed { get; set; }
    public double YSpeed { get; set; }

    public Ellipse Shape { get; set; }
}
