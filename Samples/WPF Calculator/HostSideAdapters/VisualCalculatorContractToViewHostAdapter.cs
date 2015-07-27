using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
using System.Windows;

namespace HostSideAdapters
{
    [HostAdapter]
    public class VisualCalculatorContractToViewHostAdapter : HostView.VisualCalculator
    {
        Calculator.Contracts.IVisualCalculatorContract _contract;
        ContractHandle _handle;

        public VisualCalculatorContractToViewHostAdapter(Calculator.Contracts.IVisualCalculatorContract contract)
        {
            _contract = contract;
            _handle = new ContractHandle(_contract);
        }

        public override string Name
        {
            get { return _contract.GetName(); }
        }

        public override IList<HostView.Operation> Operations
        {
            get { return CollectionAdapters.ToIList<Calculator.Contracts.IOperationContract, HostView.Operation>(_contract.GetOperations(), OperationHostAdapters.ContractToViewAdapter, OperationHostAdapters.ViewToContractAdapter); }
        }

        public override FrameworkElement Operate(HostView.Operation op, double[] operands)
        {
            return FrameworkElementAdapters.ContractToViewAdapter(_contract.Operate(OperationHostAdapters.ViewToContractAdapter(op), operands));
        }
    }
}
