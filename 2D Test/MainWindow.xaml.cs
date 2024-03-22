using System.Windows;
using System.Windows.Input;

namespace _2D_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Key, bool> pressedKeys = [];
        UIElement selectedElement = null;
        Thread gameLogicThread;
        static CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        GameObjectMananger gameObjectMananger;
        public MainWindow()
        {
            InitializeComponent();
            gameObjectMananger = new(MyCanvas);
            pressedKeys.Add(Key.Up, false);
            pressedKeys.Add(Key.Down, false);
            pressedKeys.Add(Key.Left, false);
            pressedKeys.Add(Key.Right, false);
            gameLogicThread = new(() => GameLogic(cancellationToken));
            gameLogicThread.Start();
        }

        public void GameLogic(CancellationToken token)
        {
            Vector direction;
            System.Diagnostics.Debug.WriteLine("Start Game Loop");
            while (!cancellationToken.IsCancellationRequested)
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
                if (gameObjectMananger.SelectedMoveableObject != null)
                {
                    if (direction.Length > 0)
                        gameObjectMananger.SelectedMoveableObject.Accelerate(direction);
                    else
                        gameObjectMananger.SelectedMoveableObject.Deccelerate();
                }
                Application.Current.Dispatcher.Invoke(() => gameObjectMananger.Move(MyCanvas));
                System.Threading.Thread.Sleep(10);
            }
            System.Diagnostics.Debug.WriteLine("Stop Game Loop");
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
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            gameObjectMananger.AddMoveableObject(Type.Ellipse, true);
        }
        private void BtnRem_Click(object sender, RoutedEventArgs e)
        {
            gameObjectMananger.RemoveMoveableObject();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}