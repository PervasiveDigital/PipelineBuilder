using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace HostSideAdapters
{
    [HostAdapter]
    public class CalculatorContractToViewHostAdapter : HostView.Calculator
    {
        Calculator.Contracts.ICalculatorContract _contract;
        ContractHandle _handle;

        public CalculatorContractToViewHostAdapter(Calculator.Contracts.ICalculatorContract contract)
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

        public override double Operate(HostView.Operation op, double[] operands)
        {
            return _contract.Operate(OperationHostAdapters.ViewToContractAdapter(op), operands);
        }
    }
}
