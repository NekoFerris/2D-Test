using System.Windows;
using System.Windows.Input;

namespace _2D_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Key, bool> pressedKeys = new();
        UIElement selectedElement = null;
        private bool running = true;
        Thread thread;
        GameObjectMananger GameObjectMananger;
        public MainWindow()
        {
            InitializeComponent();
            GameObjectMananger = new(MyCanvas);
            pressedKeys.Add(Key.Up, false);
            pressedKeys.Add(Key.Down, false);
            pressedKeys.Add(Key.Left, false);
            pressedKeys.Add(Key.Right, false);
            thread = new(GameLogic);
            thread.Start();
        }

        public void GameLogic()
        {
            Vector direction;
            System.Diagnostics.Debug.WriteLine("Start Game Loop");
            while (true)
            {
                if (running)
                {
                    direction = new();
                    if (pressedKeys[Key.Up] == true)
                    {
                        direction.Y -= 0.01;
                    }
                    else
                    {
                        direction.Y += 0.01;
                    }
                    if (pressedKeys[Key.Down] == true)
                    {
                        direction.Y += 0.01;
                    }
                    else
                    {
                        direction.Y -= 0.01;
                    }
                    if (pressedKeys[Key.Left] == true)
                    {
                        direction.X -= 0.01;
                    }
                    else
                    {
                        direction.X += 0.01;
                    }
                    if (pressedKeys[Key.Right] == true)
                    {
                        direction.X += 0.01;
                    }
                    else
                    {
                        direction.X -= 0.01;
                    }
                    if (GameObjectMananger.SelectedMoveableObject != null)
                    {
                        if (direction.Length > 0)
                            GameObjectMananger.SelectedMoveableObject.Accelerate(direction);
                        else
                            GameObjectMananger.SelectedMoveableObject.Deccelerate();
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        foreach (MoveableObjectEllipse moveableObject in GameObjectMananger.MoveableObjects)
                        {
                            if (moveableObject.IsSelected == false)
                            {
                                moveableObject.Deccelerate();
                                moveableObject.Move(MyCanvas, true);
                            }
                            else
                            {
                                moveableObject.Move(MyCanvas, false);
                            }
                        }
                    });
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    pressedKeys[Key.Left] = true;
                    e.Handled = true;
                    break;
                case Key.Right:
                    pressedKeys[Key.Right] = true;
                    e.Handled = true;
                    break;
                case Key.Up:
                    pressedKeys[Key.Up] = true;
                    e.Handled = true;
                    break;
                case Key.Down:
                    pressedKeys[Key.Down] = true;
                    e.Handled = true;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    pressedKeys[Key.Left] = false;
                    e.Handled = true;
                    break;
                case Key.Right:
                    pressedKeys[Key.Right] = false;
                    e.Handled = true;
                    break;
                case Key.Up:
                    pressedKeys[Key.Up] = false;
                    e.Handled = true;
                    break;
                case Key.Down:
                    pressedKeys[Key.Down] = false;
                    e.Handled = true;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            GameObjectMananger.AddMoveableObject(true);
        }
        private void btnRem_Click(object sender, RoutedEventArgs e)
        {
            GameObjectMananger.RemoveMoveableObject();
        }
    }
}