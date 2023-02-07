using System.Windows.Media;
using System.Windows.Shapes;

namespace g2.Datastructures.DesktopWPFUI;

internal static class ParticleShapes
{
    internal static Ellipse CircleBasis(double radius)
    {
        RadialGradientBrush gradientBrush = new();
        gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
        gradientBrush.GradientStops.Add(new GradientStop(Colors.Black, 1));
        return new()
        {
            Width = radius * 2,
            Height = radius * 2,
            Stroke = new SolidColorBrush(Color.FromArgb(255, 100, 100, 205)),
            StrokeThickness = 1,
            Fill = gradientBrush,
        };
    }
}