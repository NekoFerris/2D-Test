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
        public Point Position = new();
        public bool Focusable = false;
        public bool IsSelected = false;
        public double Width = 0;
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
            Velocity = new(R.Next(-5,6), R.Next(-5, 6));
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
            if (Velocity.Length > 0.1)
                Velocity = Vector.Multiply(Velocity, 0.999);
            else
                Velocity = new();
        }
        public void Move(Canvas GameCanvas, bool bounce)
        {
            if (Velocity.Length == 0)
                return;
            Position += Velocity;
            if(Position.Y < 0)
            {
                Position.Y = 0;
                if(bounce)
                {
                    BunceEdge(Direction.Vertical);
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
                    BunceEdge(Direction.Vertical);
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
                    BunceEdge(Direction.Horizontal);
                }
                else
                {
                    Velocity.X = 0;
                }
            }
            else if (Position.X > GameCanvas.ActualWidth - Width)
            {
                Position.X = GameCanvas.ActualWidth - Width;
                if (bounce)
                {
                    BunceEdge(Direction.Horizontal);
                }
                else
                {
                    Velocity.X = 0;
                }
            }
        }
        public void BunceEdge(Direction direction)
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

        public void Draw()
        {
            Canvas.SetTop(UIElement, Position.Y);
            Canvas.SetLeft(UIElement, Position.X);
        }
    }
}
