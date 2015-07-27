namespace Calculators.ExtensibilityHostAdapers {
    
    
    public class OperateViewToContractHostAdapter : System.AddIn.Pipeline.ContractBase, Calculators.Extensibility.Contracts.IOperateContract {
        
        private Calculators.Extensibility.Operate _view;
        
        public OperateViewToContractHostAdapter(Calculators.Extensibility.Operate view) {
            _view = view;
        }
        
        public virtual string GetOperation() {
            return _view.Operation;
        }
        
        public virtual double GetA() {
            return _view.A;
        }
        
        public virtual double GetB() {
            return _view.B;
        }
        
        
    }
}

