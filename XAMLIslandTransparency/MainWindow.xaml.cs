using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vanara.PInvoke;

namespace XAMLIslandTransparency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CanvasWindow _canvasWindow;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _canvasWindow.CanClose = true;
            _canvasWindow.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_canvasWindow == null)
            {
                _canvasWindow = new CanvasWindow();
                _canvasWindow.CanClose = false;
                _canvasWindow.WindowState = WindowState.Maximized;
                _canvasWindow.Show();
            }
        }

        private void CanvasWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Close();
        }

        private void DrawMode_Click(object sender, RoutedEventArgs e)
        {
            _canvasWindow.Topmost = false;
            _canvasWindow.SetTransparentNotHitThrough();
            this.Topmost = true;
            this.Activate();
        }

        private void MouseMode_Click(object sender, RoutedEventArgs e)
        {
            _canvasWindow.Topmost = true;
            _canvasWindow.SetTransparentHitThrough();
        }
    }
}
