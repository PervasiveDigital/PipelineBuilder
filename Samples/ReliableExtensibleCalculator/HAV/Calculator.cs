namespace Calculators.Extensibility {
    
    
    public abstract class Calculator {
        
        public abstract string Operations {
            get;
        }
        
        public abstract double Operate(Operate operate);
    }
}

