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
        Random R;
        public List<MoveableObject> MoveableObjects = [];
        public MoveableObject? SelectedMoveableObject { get; set; } = null;
        public GameObjectMananger(Canvas canvas)
        {
            GameCanvas = canvas;
            R = new();
        }
        public void AddMoveableObject(Type type)
        {
            AddMoveableObject(Type.Ellipse, false);
        }
        public void AddMoveableObject(Type type, bool focusable)
        {
            Double width = R.Next(10, 61);
            Double height = R.Next(10, 61);
            AddMoveableObject(type, focusable, width, height);
        }
        public void AddMoveableObject(Type type, bool focusable, double width, double height)
        {
            double possX = R.Next(0, (int)(GameCanvas.ActualWidth - width));
            double possY = R.Next(0, (int)(GameCanvas.ActualHeight - height));
            AddMoveableObject(type, focusable, width, height, possX, possY);
        }
        public void AddMoveableObject(Type type, bool focusable, double width, double height, double possX, double possY)
        {
            switch (type)
            {
                case Type.Ellipse:
                    MoveableObjects.Add(new MoveableObjectEllipse(focusable, possX, possY, width));
                    break;
                case Type.Rectangle:
                    break;
                default: 
                    throw new NotImplementedException();
            }
            GameCanvas.Children.Add(MoveableObjects.Last().UIElement);
            if (focusable)
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
                    if (SelectedMoveableObject.UIElement is Ellipse ellipse)
                    {
                        ellipse.Stroke = Brushes.Transparent;
                    }
                    SelectedMoveableObject.IsSelected = false;
                }
                SelectedMoveableObject = MoveableObjects.Where(mo => mo.UIElement == sender).Single();
                if (SelectedMoveableObject != null && SelectedMoveableObject.Focusable == true)
                {
                    if (SelectedMoveableObject.UIElement is Ellipse ellipse)
                    {
                        ellipse.Stroke = Brushes.Black;
                    }
                    SelectedMoveableObject.IsSelected = true;
                }
            }
        }

        public void Move(Canvas canvas)
        {
            foreach (MoveableObjectEllipse moveableObject in MoveableObjects)
            {
                if (moveableObject.IsSelected == false)
                {
                    moveableObject.Deccelerate();
                    CheckCollision(moveableObject);
                    moveableObject.Move(canvas, true);
                }
                else
                {
                    moveableObject.Move(canvas, false);
                }
                moveableObject.Draw();
            }
        }

        public bool CheckCollision(MoveableObject source)
        {
            if (source.UIElement is Ellipse && source.Velocity.Length > 0)
            {
                bool collission = false;
                Ellipse ellipse1 = (Ellipse)source.UIElement;
                Parallel.ForEach(MoveableObjects, (mo, state) =>
                {
                    if (MoveableObjects.Count > 1)
                        if (source == mo)
                            return;
                        else
                        {
                            double r1 = ((Ellipse)source.UIElement).ActualWidth / 2;
                            double x1 = source.Position.X + r1;
                            double y1 = source.Position.Y + r1;
                            double r2 = ((Ellipse)mo.UIElement).ActualWidth / 2;
                            double x2 = mo.Position.X + r2;
                            double y2 = mo.Position.Y + r2;
                            Vector d = new Vector(x2 - x1, y2 - y1);
                            if (d.Length <= r1 + r2)
                            {
                                if (mo.Velocity.Length > 0)
                                {
                                    double angle = Vector.AngleBetween(source.Velocity, mo.Velocity);
                                    var ca = Math.Cos(angle);
                                    var sa = Math.Sin(angle);
                                    source.Velocity = new Vector(ca * source.Velocity.X - sa * source.Velocity.Y, sa * source.Velocity.X + ca * source.Velocity.Y);
                                    mo.Velocity = new Vector(ca * source.Velocity.Y - sa * source.Velocity.X, sa * source.Velocity.Y + ca * source.Velocity.X);

                                    //while (d.Length <= r1 + r2)
                                    //{
                                    //    x1 = source.Position.X + r1;
                                    //    y1 = source.Position.Y + r1;
                                    //    x2 = mo.Position.X + r2;
                                    //    y2 = mo.Position.Y + r2;
                                    //    d = new Vector(x2 - x1, y2 - y1);
                                    //    source.Move(GameCanvas, true);
                                    //    mo.Move(GameCanvas, true);
                                    //}
                                }
                                else
                                {
                                    Vector still = new(0, 1);
                                    double angle = Vector.AngleBetween(source.Velocity, still);
                                    var ca = Math.Cos(angle);
                                    var sa = Math.Sin(angle);
                                    source.Velocity = new Vector(ca * source.Velocity.X - sa * source.Velocity.Y, sa * source.Velocity.X + ca * source.Velocity.Y);
                                    //while (d.Length <= r1 + r2)
                                    //{
                                    //    x1 = source.Position.X + r1;
                                    //    y1 = source.Position.Y + r1;
                                    //    x2 = mo.Position.X + r2;
                                    //    y2 = mo.Position.Y + r2;
                                    //    d = new Vector(x2 - x1, y2 - y1);
                                    //    source.Move(GameCanvas, true);
                                    //}
                                }
                                collission = true;
                                state.Break();
                            }
                        }
                });
                return collission;
            }
            return false;
        }
    }
}
