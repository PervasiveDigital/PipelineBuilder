using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Pipeline;

namespace AddInSideAdapters
{
    public class OperationViewToContractAddInAdapter : ContractBase,Calculator.Contracts.IOperationContract
    {
        AddInView.Operation _view;

        public OperationViewToContractAddInAdapter(AddInView.Operation view)
        {
            _view = view;
        }

        #region IOperationContract Members

        public string GetName()
        {
            return _view.Name;
        }

        public int GetNumOperands()
        {
            return _view.NumOperands;
        }

        #endregion

        public static Calculator.Contracts.IOperationContract ViewToContractAdapter(AddInView.Operation view)
        {
            return new OperationViewToContractAddInAdapter(view);
        }

        public static AddInView.Operation ContractToViewAdapter(Calculator.Contracts.IOperationContract contract)
        {
            return ((OperationViewToContractAddInAdapter)contract)._view;
        }
    }
}
