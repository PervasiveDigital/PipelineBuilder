using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicArithmaticAddIn.cs
{
    [System.AddIn.AddIn("Basic Arithmatic")]
    public class BasicArithmatic :AddInView.Calculator
    {
        private IList<AddInView.Operation> _operations;

        public BasicArithmatic()
        {
            _operations = new List<AddInView.Operation>();
            _operations.Add(new AddInView.Operation("+", 2));
            _operations.Add(new AddInView.Operation("-", 2));
            _operations.Add(new AddInView.Operation("/", 2));
            _operations.Add(new AddInView.Operation("*", 2));
        }

        public override string Name
        {
            get { return "Basic Arithmatic"; }
        }

        public override IList<AddInView.Operation> Operations
        {
            get { return _operations; }
        }

        public override double Operate(AddInView.Operation op, double[] operands)
        {
            switch (op.Name)
            {
                case "+":
                    return operands[0] + operands[1];
                case "-":
                    return operands[0] - operands[1];
                case "*":
                    return operands[0] * operands[1];
                case "/":
                    return operands[0] / operands[1];
                default:
                    throw new InvalidOperationException("Can not perform operation: " +op.Name);
            }
        }
    }
}
