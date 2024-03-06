using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public void AddMoveableObject(UIElement uIElement)
        {
            MoveableObjects.Add(new MoveableObjectEllipse(true, GameCanvas.ActualWidth, GameCanvas.ActualHeight));
            GameCanvas.Children.Add(MoveableObjects.Last().UIElement);
        }
        public void RemoveMoveableObject(UIElement uIElement)
        {
            MoveableObject? elementToRemove = MoveableObjects.Find(mo => mo.UIElement == uIElement);
            if (elementToRemove != null)
            {
                GameCanvas.Children.Remove(elementToRemove.UIElement);
                MoveableObjects.Remove(elementToRemove);
            }
        }
        public void SetFocus(object sender, MouseEventArgs e)
        {

        }
    }
}
