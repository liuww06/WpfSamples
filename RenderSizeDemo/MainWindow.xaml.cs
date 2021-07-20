using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RenderSizeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            canvas.MouseMove += CanvasOnMouseMove;
        }

        private void CanvasOnMouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(canvas);
            tbPosition.Text = $"X:{p.X},Y:{p.Y}";
        }

        protected override void OnInitialized(EventArgs e)
        {
            canvas.RenderTransform = new MatrixTransform(2,0,0,-2,0,200);
            rect.RenderTransform = new MatrixTransform(1, 0, 0, -1, -231, 78);
            base.OnInitialized(e);
        }

        private double _scale = 1.0d;
        private void ScaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            _scale += 0.1;
            rect.RenderTransform = new ScaleTransform(_scale,_scale);
        }
    }
}
