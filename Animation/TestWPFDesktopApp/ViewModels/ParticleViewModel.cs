using g2.Animation.UI.WPF.Shapes.Library.ParticleShapes;
using System.Windows.Shapes;

namespace g2.Animation.TestWPFDesktopApp.ViewModels;
internal class ParticleViewModel
{
    private readonly Ellipse shape;

    public ParticleViewModel(double x, double y, double radius)
    {
        shape = ParticleShapes.CircleBasis(radius);
    }

    public Ellipse Shape => shape;
}
