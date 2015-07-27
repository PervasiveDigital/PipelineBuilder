using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BasicArithmaticAddIn.cs
{
    [System.AddIn.AddIn("Basic Visual")]
    public class BasicVisual : AddInView.VisualCalculator
    {
        private IList<AddInView.Operation> _operations;

        public BasicVisual()
        {
            _operations = new List<AddInView.Operation>();
            _operations.Add(new AddInView.Operation("Graph", 5));
        }

        public override string Name
        {
            get { return "Basic Visual"; }
        }

        public override IList<AddInView.Operation> Operations
        {
            get { return _operations; }
        }

        public override System.Windows.FrameworkElement Operate(AddInView.Operation op, double[] operands)
        {
            switch (op.Name)
            {
                case "Graph":
                    return Graph(operands);
                default:
                    throw new NotSupportedException("Can not support operation: " + op.Name);
            }
        }

        private System.Windows.FrameworkElement Graph(double[] operands)
        {
            Grid g = new Grid();
            double max = operands[0];
            foreach (double d in operands)
            {
                max = Math.Max(max, d);
            }
            int rows = (int)max;
            int columns = operands.Length;
            for (int i = 0;i<columns;i++)
            {
                g.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < rows; i++)
            {
                g.RowDefinitions.Add(new RowDefinition());
            }
            for (int c = 0; c <columns; c++)
            {
                for (int r = (int)operands[c]; r >= 0; r--)
                {
                    Canvas canvas = new Canvas();
                    System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush();
                    brush.Color = System.Windows.Media.Colors.Red;
                    canvas.Background = brush;
                    Grid.SetColumn(canvas, c);
                    Grid.SetRow(canvas, rows-r);
                    g.Children.Add(canvas);
                }
            }
            g.Width = 229;
            g.Height = 229;
            return g;
        }
    }
}
