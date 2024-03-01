using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2D_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Key, bool> pressedKeys = new();
        public MainWindow()
        {
            InitializeComponent();
            pressedKeys.Add(Key.Up, false);
            pressedKeys.Add(Key.Down, false);
            pressedKeys.Add(Key.Left, false);
            pressedKeys.Add(Key.Right, false);
        }

        public async void GameLogic()
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Transform Pos = Kreis.LayoutTransform;
            double x = Canvas.GetLeft(Kreis);
            double y = Canvas.GetLeft(Kreis);
            switch (e.Key)
            {
                case Key.Left:
                    pressedKeys[Key.Left] = true;
                    Canvas.SetLeft(Kreis, x - 5);
                    break;
                case Key.Right:
                    pressedKeys[Key.Right] = true;
                    Canvas.SetLeft(Kreis, x + 5);
                    break;
                case Key.Up:
                    pressedKeys[Key.Right] = true;
                    Canvas.SetTop(Kreis, y - 5);
                    break;
                case Key.Down:
                    pressedKeys[Key.Right] = true;
                    Canvas.SetTop(Kreis, y + 5);
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
                    break;
                case Key.Right:
                    pressedKeys[Key.Right] = false;
                    break;
                case Key.Up:
                    pressedKeys[Key.Right] = false;
                    break;
                case Key.Down:
                    pressedKeys[Key.Right] = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        public bool checkMonement(Transform oldPos, Transform newPos,Size Size)
        {
            return false;
        }
    }
}