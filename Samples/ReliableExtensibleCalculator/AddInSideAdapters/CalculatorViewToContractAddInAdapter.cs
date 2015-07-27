namespace Calculators.ExtensibilityAddInAdapters {
    
    
    [System.AddIn.Pipeline.AddInAdapterAttribute()]
    public class CalculatorViewToContractAddInAdapter : System.AddIn.Pipeline.ContractBase, Calculators.Extensibility.Contracts.ICalculatorContract {
        
        private Calculators.Extensibility.Calculator _view;
        
        public CalculatorViewToContractAddInAdapter(Calculators.Extensibility.Calculator view) {
            _view = view;
        }
        
        public virtual string GetAvailableOperations() {
            return _view.Operations;
        }
        
        public virtual double Operate(Calculators.Extensibility.Contracts.IOperateContract operate) {
            return _view.Operate(new OperateContractToViewAddInAdapter(operate));
        }
        
       
    }
}

