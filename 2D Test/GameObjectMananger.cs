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
    internal class GameObjectMananger
    {
        Canvas GameCanvas;
        public List<MoveableObject> MoveableObjects = new List<MoveableObject>();
        public MoveableObject? SelectedMoveableObject { get; set; } = null;
        public GameObjectMananger(Canvas canvas)
        {
            GameCanvas = canvas;
        }
        public void AddMoveableObject()
        {
            AddMoveableObject(false);
        }
        public void AddMoveableObject(bool focusable)
        {
            MoveableObjects.Add(new MoveableObjectEllipse(true, GameCanvas.ActualWidth, GameCanvas.ActualHeight));
            GameCanvas.Children.Add(MoveableObjects.Last().UIElement);
            if(focusable)
            {
                MoveableObjects.Last().UIElement.MouseLeftButtonUp += SetFocus;
            }
        }
        public void RemoveMoveableObject()
        {
            if (SelectedMoveableObject != null)
            {
                GameCanvas.Children.Remove(SelectedMoveableObject.UIElement);
                MoveableObjects.Remove(SelectedMoveableObject);
            }
        }
        public void SetFocus(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                if (SelectedMoveableObject != null)
                {
                    if (SelectedMoveableObject.UIElement is Ellipse)
                    {
                        ((Ellipse)SelectedMoveableObject.UIElement).Stroke = Brushes.Transparent;
                    }
                }
                SelectedMoveableObject = MoveableObjects.Where(mo => mo.UIElement == sender).Single();
                if (SelectedMoveableObject != null)
                {
                    if (SelectedMoveableObject.UIElement is Ellipse)
                    {
                        ((Ellipse)SelectedMoveableObject.UIElement).Stroke = Brushes.Black;
                    }
                }
            }
        }
    }
}
