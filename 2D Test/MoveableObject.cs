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
    internal class MoveableObject
    {
        protected Random R = new();
        public UIElement UIElement { get; set; }
        public Vector Velocity = new();
        public Vector Direction = new();
        public bool Focusable = false;
        public MoveableObject()
        {
            UIElement = new UIElement();
        }
        public MoveableObject(bool focusable)
        {
            UIElement = new UIElement();
            Focusable = focusable;
        }
        public MoveableObject(bool focusable, double CanvasWidth, double CanvasHeight)
        {
            UIElement = new UIElement();
            Focusable = focusable;
        }
        public void accelerate(Vector accelerationDirection)
        {

        }
        public void move()
        {

        }
    }
}
