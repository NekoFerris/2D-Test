using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
        Vector velocity = new();
        private double MaxSpeed = 5;
        Thread thread;
        Random r = new();
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
            Vector direction = new();
            System.Diagnostics.Debug.WriteLine("Start Game Loop");
            while (true)
            {
                direction = new();
                if (running)
                {
                    if (pressedKeys[Key.Up] == true)
                    {
                        velocity.Y -= 0.01;
                    }
                    else
                    {
                        if(velocity.Y < 0)
                            velocity.Y += 0.01;
                    }
                    if (pressedKeys[Key.Down] == true)
                    {
                        velocity.Y += 0.01;
                    }
                    else
                    {
                        if (velocity.Y > 0)
                            velocity.Y -= 0.01;
                    }
                    if (pressedKeys[Key.Left] == true)
                    {
                        velocity.X -= 0.01;
                    }
                    else
                    {
                        if (velocity.X < 0)
                            velocity.X += 0.01;
                    }
                    if (pressedKeys[Key.Right] == true)
                    {
                        velocity.X += 0.01;
                    }
                    else
                    {
                        if (velocity.X > 0)
                            velocity.X -= 0.01;
                    }
                    direction += velocity;
                    if (selectedElement != null)
                    {
                        Application.Current.Dispatcher.Invoke(() => Move(selectedElement, direction));
                    }
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

        public static void Move(UIElement element, Vector direction)
        {
            double currentTop = Canvas.GetTop(element);
            double currentLeft = Canvas.GetLeft(element);
            Canvas.SetTop(element, currentTop + direction.Y);
            Canvas.SetLeft(element, currentLeft + direction.X);
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