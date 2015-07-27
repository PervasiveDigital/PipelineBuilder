namespace Calculators.Extensibility {
    
    
    [System.AddIn.Pipeline.AddInBaseAttribute()]
    public abstract class Calculator {
        
        public abstract string Operations {
            get;
        }
        
        public abstract double Operate(Operate operate);
    }
}

