using System;
using System.Collections.Generic;
using System.Text;
using Calculators.Extensibility;

namespace CalculatorV3AddIn
{
    [System.AddIn.AddIn("CalculatorV3AddIn")]
    public class CalulatorV3AddIn : Calculator
    {
        public override string Operations
        {
            get { return "+ - * / ** throw"; }
        }
        public override double Operate(Operate operation)
        {
            switch (operation.Operation)
            {
                case "+":
                    return operation.A + operation.B;
                case "-":
                    return operation.A - operation.B;
                case "*":
                    return operation.A * operation.B;
                case "/":
                    return operation.A / operation.B;
                case "**":
                    return Math.Pow(operation.A, operation.B);
                case "throw":
                    ThrowOnChildThread();
                    return 0;
                default:
                    throw new InvalidOperationException("This add-in does not support: " + operation.Operation);
            }
        }

        internal void ThrowOnChildThread()
        {
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart( delegate(){throw new Exception();});
            new System.Threading.Thread(ts).Start();
        }

    }
}
