using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Tests;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphCalc
{
    class Grapher
    {
        
        double CanvasWidth = 224;
        double CanvasHeight = 249;
        double XMin = -10;
        double XMax = 10;
        double YMin = -10;
        double YMax = 10;
        double XMin2D = -10;
        double XMax2D = 10;
        double YMin2D = -10;
        double YMax2D = 10;
        double TMin2D = 0;
        double TMax2D = 31.4;
        double TStep2D = .1962;
        double UMin = -3.15;
        double UMax = 3.15;
        double VMin = 0;
        double VMax = 6.29;
        int UGrid = 96;
        int VGrid = 48;
        Trackball _tb;


        internal Canvas Show2D(String input)
        {
            Canvas c = new DrawingCanvas();
            Show2D(c, input);
            return c;
        }

        internal Canvas Show2DP(String inputX, String inputY)
        {
            DrawingCanvas c = new DrawingCanvas();
            GraphScene2DP(c, inputX, inputY);
            return c;
        }

        internal Canvas Show3D(String inputX, string inputY, string inputZ)
        {
            DrawingCanvas c = new DrawingCanvas();
            GraphScene3D(c, inputX, inputY, inputZ);
            return c;
        }

        private void Show2D(Canvas c, String input)
        {
            IExpression exp = FunctionParser.Parse(input);

            double width = CanvasWidth;
            double height = CanvasHeight;
            double offsetX = -XMin;
            double offsetY = YMax;
            double graphToCanvasX = width / (XMax - XMin);
            double graphToCanvasY = height / (YMax - YMin);

            PointCollection points = new PointCollection();
            for (double x = XMin; x < XMax; x += 1 / graphToCanvasX)
            {
                VariableExpression.Define("x", x);

                // Translate the origin based on the max/min parameters (y axis is flipped), then scale to canvas.
                double xCanvas = (x + offsetX) * graphToCanvasX;
                double yCanvas = (offsetY - exp.Evaluate()) * graphToCanvasY;

                points.Add(ClampedPoint(xCanvas, yCanvas));
            }
            VariableExpression.Undefine("x");

            c.Children.Clear();
            DrawAxisHelper axisHelper = new DrawAxisHelper(c, new Size(c.Width,c.Height));
            axisHelper.DrawAxes(XMin, XMax, YMin, YMax);


            Polyline graphLine = new Polyline();
            graphLine.Stroke = Brushes.Black;
            graphLine.StrokeThickness = 1;
            graphLine.Points = points;

            c.Children.Add(graphLine);

           
        }

        private void GraphScene2DP(Canvas c, String inputX, String inputY)
        {
            IExpression xExp = FunctionParser.Parse(inputX);
            IExpression yExp = FunctionParser.Parse(inputY);

            double width = CanvasWidth;
            double height = CanvasHeight;
            double graphToCanvasX = width / (XMax2D - XMin2D);
            double graphToCanvasY = height / (YMax2D - YMin2D);

            // distance from origin of graph to origin of canvas
            double offsetX = -XMin2D;
            double offsetY = YMax2D;

            PointCollection points = new PointCollection();
            for (double t = TMin2D; t <= TMax2D + 0.000001; t += TStep2D)
            {
                VariableExpression.Define("t", t);
                double xGraph = xExp.Evaluate();
                double yGraph = yExp.Evaluate();

                // Translate the origin based on the max/min parameters (y axis is flipped), then scale to canvas.
                double xCanvas = (xGraph + offsetX) * graphToCanvasX;
                double yCanvas = (offsetY - yGraph) * graphToCanvasY;

                points.Add(ClampedPoint(xCanvas, yCanvas));
            }
            VariableExpression.Undefine("t");

            c.Children.Clear();
            DrawAxisHelper axisHelper = new DrawAxisHelper(c, new Size(width, height));
            axisHelper.DrawAxes(XMin2D, XMax2D, YMin2D, YMax2D);


            Polyline polyLine = new Polyline();
            polyLine.Stroke = Brushes.Black;
            polyLine.StrokeThickness = 1;
            polyLine.Points = points;
            c.Children.Add(polyLine);            
        }

        private void GraphScene3D(Canvas screenCanvas,String eqX, String eqY, String eqZ)
        {
            // We do this so we can get good error information.
            // The values return by these calls are ignored.
            FunctionParser.Parse(eqX);
            FunctionParser.Parse(eqY);
            FunctionParser.Parse(eqZ);
            

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(0, 0, 5);
            camera.LookDirection = new Vector3D(0, 0, -2);
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.NearPlaneDistance = 1;
            camera.FarPlaneDistance = 100;
            camera.FieldOfView = 45;

            Model3DGroup group = null;

            
            group = new Model3DGroup();
            FunctionMesh mesh = new FunctionMesh(eqX, eqY, eqZ, UMin, UMax, VMin, VMax);
            group.Children.Add(new GeometryModel3D(mesh.CreateMesh(UGrid + 1, VGrid + 1), new DiffuseMaterial(Brushes.Blue)));
            group.Children.Add(new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1)));


            Viewport3D viewport = new Viewport3D();

            //<newcode>
            ModelVisual3D sceneVisual = new ModelVisual3D();
            sceneVisual.Content = group;
            viewport.Children.Clear();
            viewport.Children.Add(sceneVisual);
            //</newcode>


            //viewport.Models = group;
            viewport.Camera = camera;
            viewport.Width = CanvasWidth;
            viewport.Height = CanvasHeight;
            viewport.ClipToBounds = true;

            screenCanvas.Children.Clear();
            screenCanvas.Children.Add(viewport);
            _tb = new Trackball();
            _tb.Attach(screenCanvas);
            _tb.Enabled = true;
            _tb.Servants.Add(viewport);
            screenCanvas.IsVisibleChanged += new DependencyPropertyChangedEventHandler(screenCanvas_IsVisibleChanged);
        }

        void screenCanvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                _tb.Enabled = false;
                _tb.Detach((FrameworkElement)sender);
            }
        }

        private Point ClampedPoint(double x, double y)
        {
            if (double.IsPositiveInfinity(x) || x > CanvasWidth * 2)
            {
                x = CanvasWidth * 2;
            }
            else if (double.IsNegativeInfinity(x) || x < -CanvasWidth)
            {
                x = -CanvasWidth;
            }
            else if (double.IsNaN(x))
            {
                x = -CanvasWidth;
            }
            if (double.IsPositiveInfinity(y) || y > CanvasHeight * 2)
            {
                y = CanvasHeight * 2;
            }
            else if (double.IsNegativeInfinity(y) || y < -CanvasHeight)
            {
                y = -CanvasHeight;
            }
            else if (double.IsNaN(x))
            {
                y = -CanvasHeight;
            }
            return new Point(x, y);
        }
    }
}
