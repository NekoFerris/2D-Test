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
using System.Windows.Threading;

namespace _2D_Test
{
    internal class MoveableObject
    {
        protected Random R = new();
        public UIElement UIElement { get; set; }
        public Vector Velocity = new();
        public Vector Position = new();
        public bool Focusable = false;
        public bool IsSelected = false;
        double MaxSpeed = 2.5;
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
        public void Accelerate(Vector accelerationDirection)
        {
            if(Velocity.Length < MaxSpeed || ((Velocity + accelerationDirection).Length < MaxSpeed))
            Velocity += accelerationDirection;
        }
        public void Deccelerate()
        {
            if (Velocity.X < 0)
                Velocity.X += 0.01;
            else if (Velocity.X > 0)
                Velocity.X -= 0.01;
            if (Velocity.Y < 0)
                Velocity.Y += 0.01;
            else if (Velocity.Y > 0)
                Velocity.Y -= 0.01;
        }
        public void Move()
        {
            Position += Velocity;
            Canvas.SetTop(UIElement, Position.Y);
            Canvas.SetLeft(UIElement, Position.X);
        }
    }
}
