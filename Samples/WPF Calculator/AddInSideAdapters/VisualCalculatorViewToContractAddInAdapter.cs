using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;
using System.Windows;
using System.AddIn.Contract;


namespace AddInSideAdapters
{
    [AddInAdapter]
    public class VisualCalculatorViewToContractAddInAdapter : ContractBase, Calculator.Contracts.IVisualCalculatorContract
    {

        private AddInView.VisualCalculator _view;

        public VisualCalculatorViewToContractAddInAdapter(AddInView.VisualCalculator view)
        {
            _view = view;
        }
        #region ICalculatorContract Members

        public System.AddIn.Contract.IListContract<Calculator.Contracts.IOperationContract> GetOperations()
        {
            return CollectionAdapters.ToIListContract<AddInView.Operation, Calculator.Contracts.IOperationContract>(_view.Operations, OperationViewToContractAddInAdapter.ViewToContractAdapter, OperationViewToContractAddInAdapter.ContractToViewAdapter);
        }

        public INativeHandleContract Operate(Calculator.Contracts.IOperationContract op, double[] operands)
        {
            return FrameworkElementAdapters.ViewToContractAdapter(_view.Operate(OperationViewToContractAddInAdapter.ContractToViewAdapter(op), operands));
        }

        public string GetName()
        {
            return _view.Name;
        }

        #endregion
    }
}
