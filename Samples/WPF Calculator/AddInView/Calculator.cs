using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
using System.Windows;
[assembly:System.Security.AllowPartiallyTrustedCallers]
namespace AddInView
{
    [AddInBase]
    public abstract class Calculator
    {
        public abstract String Name
        {
            get;
        }

        public abstract IList<Operation> Operations
        {
            get;
        }

        public abstract double Operate(Operation op, double[] operands);

    }

    

    [AddInBase]
    public abstract class VisualCalculator
    {
        public abstract String Name
        {
            get;
        }

        public abstract IList<Operation> Operations
        {
            get;
        }

        public abstract FrameworkElement Operate(Operation op, double[] operands);
    }

    public class Operation
    {
        String _name;
        int _numOperands;
        public Operation(String name, int numOperands)
        {
            _name = name;
            _numOperands = numOperands;
        }

        public String Name
        {
            get
            {
                return _name;
            }
        }

        public int NumOperands
        {
            get
            {
                return _numOperands;
            }
        }
    }
}
