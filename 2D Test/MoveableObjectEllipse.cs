using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2D_Test
{
    internal class MoveableObjectEllipse : MoveableObject
    {
        public MoveableObjectEllipse(bool focusable, double CanvasWidth, double CanvasHeight) : base(focusable)
        {
            Ellipse ellipse = new();
            Width = ellipse.Width = R.Next(20, 81);
            Height = ellipse.Height = R.Next(20, 81);
            ellipse.Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(0, 255)));
            double x = R.Next(0, (int)(CanvasWidth - ellipse.Width));
            double y = R.Next(0, (int)(CanvasHeight - ellipse.Height));
            ellipse.StrokeThickness = 1;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            Position.X = x; 
            Position.Y = y;
            UIElement = ellipse;
        }
        public MoveableObjectEllipse(bool focusable, double CanvasWidth, double CanvasHeight, double Width, double Height) : base(focusable)
        {
            Ellipse ellipse = new()
            {
                Width = Width,
                Height = Height,
                Fill = new SolidColorBrush(Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(0, 255)))
            };
            double x = R.Next(0, (int)(CanvasWidth - ellipse.Width));
            double y = R.Next(0, (int)(CanvasHeight - ellipse.Height));
            ellipse.StrokeThickness = 1;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            Position.X = x;
            Position.Y = y;
            UIElement = ellipse;
        }
    }
}
