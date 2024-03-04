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
        Thread thread;
        Random r = new();
        public MainWindow()
        {
            InitializeComponent();
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
                        direction.Y -= 1;
                    }
                    if (pressedKeys[Key.Down] == true)
                    {
                        direction.Y += 1;
                    }
                    if (pressedKeys[Key.Left] == true)
                    {
                        direction.X -= 1;
                    }
                    if (pressedKeys[Key.Right] == true)
                    {
                        direction.X += 1;
                    }
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
            Ellipse ellipse = new Ellipse();
            ellipse.Width = r.Next(20, 81);
            ellipse.Height = r.Next(20, 81);
            ellipse.Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(0,255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
            double x = r.Next(0, (int)(MyCanvas.ActualWidth - ellipse.Width));
            double y = r.Next(0, (int)(MyCanvas.ActualHeight - ellipse.Height));
            ellipse.MouseLeftButtonUp += EllipseFocused;
            MyCanvas.Children.Add(ellipse);
            ellipse.StrokeThickness = 1;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
        }
        private void btnRem_Click(object sender, RoutedEventArgs e)
        {
            if(selectedElement != null)
            {
                selectedElement.MouseLeftButtonUp -= EllipseFocused;
                MyCanvas.Children.Remove(selectedElement);
            }
        }

        public void EllipseFocused(object sender, MouseEventArgs e)
        {
            if(selectedElement != null)
            {
                Canvas.SetZIndex(selectedElement, 0);
                ((Ellipse)selectedElement).Stroke = Brushes.Transparent;
            }
            selectedElement = null;
            if (sender != null)
            {
                selectedElement = (Ellipse)sender;
                Canvas.SetZIndex(selectedElement, 1);
                ((Ellipse)sender).Stroke = Brushes.Black;
                ((Ellipse)sender).StrokeThickness = 1;
                ((Ellipse)sender).Focus();
            }
        }
    }
}