using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn;
using AddInView;

namespace BasicStackOperations
{
    [AddIn("Stack Operations")]
    public class BasicStackOps : Calculator
    {
        double _current;
        List<Operation> _ops;
        Random _r;

        public BasicStackOps()
        {
            _current = 0;
            _ops = new List<Operation>();
            _r = new Random();
            _ops.Add(new Operation("Pop", 2));
            _ops.Add(new Operation("Push Next", 0));
            _ops.Add(new Operation("Push Random", 0));
            _ops.Add(new Operation("Push Random Up To", 1));


        }

        public override string Name
        {
            get { return "Stack Operations"; }
        }

        public override IList<Operation> Operations
        {
            get { return _ops; }
        }

        public override double Operate(Operation op, double[] operands)
        {
            
            switch (op.Name)
            {
                case "Pop":
                    return operands[1];
                case "Push Next":
                    _current++;
                    return _current;
                case "Push Random":
                    return _r.Next(100);
                case "Push Random Up To":
                    return _r.Next((int)operands[0]);
                default:
                    throw new NotSupportedException("Can not support operation" + op.Name);
            }
        }
    }
}
