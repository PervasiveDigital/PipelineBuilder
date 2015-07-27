using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

[assembly: System.Security.AllowPartiallyTrustedCallers]
namespace AddInSideAdapters
{
    [AddInAdapter]
    public class CalculatorViewToContractAddInAdapter : ContractBase,Calculator.Contracts.ICalculatorContract
    {

        private AddInView.Calculator _view;

        public CalculatorViewToContractAddInAdapter(AddInView.Calculator view)
        {
            _view = view;
        }
        #region ICalculatorContract Members

        public System.AddIn.Contract.IListContract<Calculator.Contracts.IOperationContract> GetOperations()
        {
            return CollectionAdapters.ToIListContract<AddInView.Operation, Calculator.Contracts.IOperationContract>(_view.Operations, OperationViewToContractAddInAdapter.ViewToContractAdapter, OperationViewToContractAddInAdapter.ContractToViewAdapter);
            }

        public double Operate(Calculator.Contracts.IOperationContract op, double[] operands)
        {
           return _view.Operate(OperationViewToContractAddInAdapter.ContractToViewAdapter(op), operands);
        }

        public string GetName()
        {
            return _view.Name;
        }

        #endregion
    }
}
