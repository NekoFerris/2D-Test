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
        public List<MoveableObject> MoveableObjects = [];
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
            Application.Current.Dispatcher.Invoke(() =>
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
                }
            });
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
                            double distance = Math.Sqrt(Math.Pow(source.Position.X - mo.Position.X, 2)
                                                    + Math.Pow(source.Position.Y - mo.Position.Y, 2));
                            double combinedRadii = (source.Width + mo.Width) / 2;
                            if (distance < combinedRadii * 1.2)
                            {
                                if(mo.Velocity.Length > 0)
                                {
                                    double angle = Vector.AngleBetween(source.Velocity, mo.Velocity);
                                    var ca = Math.Cos(angle);
                                    var sa = Math.Sin(angle);
                                    source.Velocity = new Vector(ca * source.Velocity.X - sa * source.Velocity.Y, sa * source.Velocity.X + ca * source.Velocity.Y);
                                }
                                else
                                {
                                    Vector still = new(0, 1);
                                    double angle = Vector.AngleBetween(source.Velocity, still);
                                    var ca = Math.Cos(angle);
                                    var sa = Math.Sin(angle);
                                    source.Velocity = new Vector(ca * source.Velocity.X - sa * source.Velocity.Y, sa * source.Velocity.X + ca * source.Velocity.Y);

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
