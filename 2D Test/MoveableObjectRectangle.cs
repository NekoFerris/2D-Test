using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace _2D_Test
{
    internal class MoveableObjectRectangle : MoveableObject
    {
        MoveableObjectRectangle(bool focusable, double PossX, double PossY) : base(focusable)
        {
            Rectangle rectangle = new();
            Width = rectangle.Width = 10;
            Height = rectangle.Height = 60;
            Position.X = PossX;
            Position.Y = PossY;
            Canvas.SetLeft(rectangle, Position.X);
            Canvas.SetTop(rectangle, Position.Y);
        }
    }
}
