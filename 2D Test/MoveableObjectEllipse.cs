using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pong
{
    internal class MoveableObjectEllipse : MoveableObject
    {
        public MoveableObjectEllipse(bool focusable, double possX, double possY, double diameter) : base(focusable)
        {
            Ellipse ellipse = new();
            Width = ellipse.Width = diameter;
            Height = ellipse.Height = diameter;
            ellipse.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(0, 255)));
            ellipse.StrokeThickness = 1;
            Canvas.SetLeft(ellipse, possX);
            Canvas.SetTop(ellipse, possY);
            Position.X = possX;
            Position.Y = possY;
            UIElement = ellipse;
        }
    }
}
