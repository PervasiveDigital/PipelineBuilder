using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
namespace HostSideAdapters
{
    internal static class OperationHostAdapters 
    {
        internal static Calculator.Contracts.IOperationContract ViewToContractAdapter(HostView.Operation view)
        {
            return ((OperationContractToViewAdapter)view).Contract;
        }

        internal static HostView.Operation ContractToViewAdapter(Calculator.Contracts.IOperationContract contract)
        {
            return new OperationContractToViewAdapter(contract);
        }
    }

    public class OperationContractToViewAdapter : HostView.Operation
    {

        Calculator.Contracts.IOperationContract _contract;
        ContractHandle _handle;
        public OperationContractToViewAdapter(Calculator.Contracts.IOperationContract contract)
        {
            _contract = contract;
            _handle = new ContractHandle(_contract);
        }

        public override string Name
        {
            get { return _contract.GetName(); }
        }

        public override int NumOperands
        {
            get { return _contract.GetNumOperands(); }
        }

        internal Calculator.Contracts.IOperationContract Contract
        {
            get
            {
                return _contract;
            }
        }
    }

}
