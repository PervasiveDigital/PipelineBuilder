namespace Calculators.ExtensibilityAddInAdapters {
    
    
    public class OperateContractToViewAddInAdapter : Calculators.Extensibility.Operate {
        
        private Calculators.Extensibility.Contracts.IOperateContract _contract;

        private System.AddIn.Pipeline.ContractHandle _handle;
        
        public OperateContractToViewAddInAdapter(Calculators.Extensibility.Contracts.IOperateContract contract) {
            _contract = contract;
            _handle = new System.AddIn.Pipeline.ContractHandle(contract);
        }
        
        public override string Operation {
            get {
                return _contract.GetOperation();
            }
        }
        
        public override double A {
            get {
                return _contract.GetA();
            }
        }
        
        public override double B {
            get {
                return _contract.GetB();
            }
        }
        
       
    }
}

