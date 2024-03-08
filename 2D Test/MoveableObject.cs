using Microsoft.VisualBasic;
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
enum Direction
{
    Vertical,
    Horizontal
}
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
        public double Widht = 0;
        public double Height = 0;
        double MaxSpeed = 5;
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
            if (Velocity.Length > 0.01)
                Velocity = Vector.Multiply(Velocity, 0.999);
            else
                Velocity = new();
        }
        public void Move(Canvas GameCanvas, bool bounce)
        {
            Position += Velocity;
            if(Position.Y < 0)
            {
                Position.Y = 0;
                if(bounce)
                {
                    BunceVelocity(Direction.Vertical);
                }
                else
                {
                    Velocity.Y = 0;
                }
            } 
            else if (Position.Y > GameCanvas.ActualHeight - Height)
            {
                Position.Y = GameCanvas.ActualHeight - Height;
                if (bounce)
                {
                    BunceVelocity(Direction.Vertical);
                }
                else
                {
                    Velocity.Y = 0;
                }
            }

            if (Position.X < 0)
            {
                Position.X = 0;
                if (bounce)
                {
                    BunceVelocity(Direction.Horizontal);
                }
                else
                {
                    Velocity.X = 0;
                }
            }
            else if (Position.X > GameCanvas.ActualWidth - Widht)
            {
                Position.X = GameCanvas.ActualWidth - Widht;
                if (bounce)
                {
                    BunceVelocity(Direction.Horizontal);
                }
                else
                {
                    Velocity.X = 0;
                }
            }
            Canvas.SetTop(UIElement, Position.Y);
            Canvas.SetLeft(UIElement, Position.X);
        }
        public void BunceVelocity(Direction direction)
        {
            if(direction == Direction.Vertical)
            {
                Velocity.Y *= -1;
            }
            else
            {
                Velocity.X *= -1;
            }
        }
    }
}
