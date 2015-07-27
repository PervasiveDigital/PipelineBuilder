using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;

namespace Calculator.Contracts
{
    [System.AddIn.Pipeline.AddInContract]
    public interface ICalculatorContract : IContract
    {
        IListContract<IOperationContract> GetOperations();
        [System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.LinkDemand)]
        double Operate(IOperationContract op, double[] operands);
        string GetName();
    }

    [System.AddIn.Pipeline.AddInContract]
    public interface IVisualCalculatorContract : IContract
    {
        IListContract<IOperationContract> GetOperations();
        System.AddIn.Contract.INativeHandleContract Operate(IOperationContract op, double[] operands);
        string GetName();
    }

    public interface IOperationContract : IContract
    {
        string GetName();
        int GetNumOperands();
    }
}
