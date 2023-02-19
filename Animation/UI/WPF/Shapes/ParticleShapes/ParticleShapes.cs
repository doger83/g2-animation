using System.Windows.Media;
using System.Windows.Shapes;

namespace g2.Animation.UI.WPF.Shapes.Library.ParticleShapes;

public static class ParticleShapes
{

    private static readonly RadialGradientBrush gradientBrush = new()
    {
        GradientStops = { new GradientStop(Colors.LightGray, 0), new GradientStop(Colors.Black, 1) },

    };

    private static readonly SolidColorBrush strokeBrush = new(Color.FromArgb(255, 100, 100, 205));

    static ParticleShapes()
    {
        gradientBrush.Freeze();
        strokeBrush.Freeze();
    }

    public static Ellipse CircleBasis(double radius)
    {
        Ellipse shape = new()
        {
            Width = radius * 2,
            Height = radius * 2,
            Stroke = strokeBrush,
            StrokeThickness = 0.5,
            Fill = gradientBrush
        };
        return shape;
    }
}