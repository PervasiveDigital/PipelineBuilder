using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HostView
{
    public abstract class CalculatorBase
    {
        public abstract String Name
        {
            get;
        }

        public abstract IList<Operation> Operations
        {
            get;
        }
       
    }

    public abstract class Calculator : CalculatorBase
    {
       

        public abstract double Operate(Operation op, double[] operands);
    }

    public abstract class VisualCalculator : CalculatorBase
    {

        public abstract FrameworkElement Operate(Operation op, double[] operands);
    }

    public abstract class Operation
    {
        public abstract string Name
        {
            get;            
        }

        public abstract int NumOperands
        {
            get;
        }
    }
}
