using System.Windows.Media;
using System.Windows.Shapes;

namespace g2.Animation.UI.WPF.Shapes.Library.ParticleShapes;

public static class ParticleShapes
{
    private static readonly RadialGradientBrush gradientBrush = new()
    {
        GradientStops = { new GradientStop(Colors.LightGray, 0), new GradientStop(Colors.Black, 1) },


    };
    public static Ellipse CircleBasis(double radius)
    {

        gradientBrush.Freeze();


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