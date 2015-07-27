using System;
using System.Collections.Generic;
using System.Text;
using AddInView;
using System.AddIn;
using System.Windows.Controls;
using System.Threading;

namespace GraphCalc
{
    [AddIn("Graphing Calculator")]
    public class GraphingCalculator : VisualCalculator
    {
        IList<Operation> _ops;
        List<byte[]> _leaks;
        Grapher _grapher;

        public GraphingCalculator()
        {
            _ops = new List<Operation>();
            _ops.Add(new Operation("2D Graph", 0));
            _ops.Add(new Operation("2D Parametric Graph", 0));
            _ops.Add(new Operation("3D Parametric Graph", 0));
            _grapher = new Grapher();
            _leaks = new List<byte[]>();
        }

        ~GraphingCalculator()
        {
        }

      
        public override string Name
        {
            get { return "Graphing Calculator"; }
        }

        public override IList<Operation> Operations
        {
            get { return _ops; }
        }


        public override System.Windows.FrameworkElement Operate(Operation op, double[] operands)
        {
           switch (op.Name)
            {
                
                case "2D Graph":
                    return new SceneInput2D();
                case "2D Parametric Graph":
                    return new SceneInput2dP();
                case "3D Parametric Graph":
                    return new SceneInput3D();
                default:
                    TextBox t = new TextBox();
                    t.Text = "Hello there";
                    return t;

            }
            
        }
    }
}
