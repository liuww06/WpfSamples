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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeometryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            AddPolyBezierSegment();
            AnimateEllipseGeometry();
            base.OnInitialized(e);
        }

        private void AddPolyBezierSegment()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(100, 100);
            PointCollection myPointCollection = new PointCollection(9);
            //每三个点时一组，构成多个3次贝塞尔曲线
            myPointCollection.Add(new Point(0, 0));
            myPointCollection.Add(new Point(200, 0));
            myPointCollection.Add(new Point(300, 100));
            myPointCollection.Add(new Point(300, 0));
            myPointCollection.Add(new Point(400, 0));
            myPointCollection.Add(new Point(500, 200));
            myPointCollection.Add(new Point(600, 0));
            myPointCollection.Add(new Point(600, 100));
            myPointCollection.Add(new Point(700, 50));
            PolyBezierSegment myBezierSegment = new PolyBezierSegment();
            myBezierSegment.Points = myPointCollection;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myBezierSegment);
            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myGeometry = new PathGeometry();
            myGeometry.Figures = myPathFigureCollection;



            Path myPath = new Path();
            myPath.Data = myGeometry;
            myPath.Stroke = Brushes.Brown;
            myPath.StrokeThickness = 5;
            //不同线段直接的连接方式，有圆弧接，斜街，斜角接
            //myPath.StrokeLineJoin = PenLineJoin.Miter;
            myPath.StrokeEndLineCap = PenLineCap.Round;
            myPath.StrokeStartLineCap = PenLineCap.Triangle;

            myPath.StrokeDashArray = new DoubleCollection(new List<double>() { 2 });
            myPath.StrokeDashCap = PenLineCap.Round;

            myPath.Fill = Brushes.DarkSalmon;

            canvas.Children.Add(myPath);
        }

        private void AnimateEllipseGeometry()
        {
            var myPath = new Path();
            myPath.Fill = Brushes.Blue;
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 5;
            var myEllipseGeometry =
                new System.Windows.Media.EllipseGeometry();
            myEllipseGeometry.Center = new System.Windows.Point(200, 200);
            myEllipseGeometry.RadiusX = 25;
            myEllipseGeometry.RadiusY = 50;

            // Register a name for the geometry so that it can
            // be targeted by animations.
            this.RegisterName("MyEllipseGeometry", myEllipseGeometry);

            myPath.Data = myEllipseGeometry;
            canvas.Children.Add(myPath);

            PointAnimation myPointAnimation = new PointAnimation();
            myPointAnimation.From = new System.Windows.Point(600, 500);
            myPointAnimation.To = new System.Windows.Point(50, 50);
            myPointAnimation.Duration =
                new Duration(TimeSpan.FromMilliseconds(5000));
            myPointAnimation.AutoReverse = true;
            myPointAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetName(myPointAnimation, "MyEllipseGeometry");
            Storyboard.SetTargetProperty(myPointAnimation,
                new PropertyPath(EllipseGeometry.CenterProperty));

            DoubleAnimation myDoubleAnimation = new DoubleAnimation(25,75,new Duration(TimeSpan.FromSeconds(5)));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetName(myDoubleAnimation, "MyEllipseGeometry");
            Storyboard.SetTargetProperty(myDoubleAnimation,
                new PropertyPath(EllipseGeometry.RadiusXProperty));




            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myPointAnimation);
            myStoryboard.Children.Add(myDoubleAnimation);

            myPath.Loaded += delegate (object sender, RoutedEventArgs args)
            {
                myStoryboard.Begin(myPath);
            };
        }


        private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(canvas);
            tbPosition.Text = $"[X:{p.X:F2},Y:{p.Y:F2}]";
        }
    }
}
