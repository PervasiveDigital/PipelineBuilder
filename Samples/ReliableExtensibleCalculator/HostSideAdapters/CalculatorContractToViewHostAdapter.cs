namespace Calculators.ExtensibilityHostAdapers {
    
    
    [System.AddIn.Pipeline.HostAdapterAttribute()]
    public class CalculatorContractToViewHostAdapter : Calculators.Extensibility.Calculator {
        
        private Calculators.Extensibility.Contracts.ICalculatorContract _contract;

        private System.AddIn.Pipeline.ContractHandle _handle;
        
        public CalculatorContractToViewHostAdapter(Calculators.Extensibility.Contracts.ICalculatorContract contract) {
            _contract = contract;
            _handle = new System.AddIn.Pipeline.ContractHandle(contract);
        }
        
        public override string Operations {
            get {
                return _contract.GetAvailableOperations();
            }
        }
        
        public override double Operate(Calculators.Extensibility.Operate operate) {
            return _contract.Operate(new OperateViewToContractHostAdapter(operate));
        }
        
       
    }
}

