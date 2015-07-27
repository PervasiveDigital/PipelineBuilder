using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace Calculators.Extensibility.Contracts
{
    [AddInContract]
    public interface ICalculatorContract: IContract
    {
        string GetAvailableOperations();
        double Operate(IOperateContract operate);
    }

    public interface IOperateContract : IContract
    {
        string GetOperation();
        double GetA();
        double GetB();
    }
}
