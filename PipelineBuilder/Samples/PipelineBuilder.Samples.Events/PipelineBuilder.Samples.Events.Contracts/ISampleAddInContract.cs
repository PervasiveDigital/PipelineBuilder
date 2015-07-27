using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace PipelineBuilder.Samples.Events.Contracts
{
    [AddInContract]
    public interface ISampleAddInContract : IContract
    {
        void Initialize(IAppObjectContract appObject);
        void Shutdown();
    }
}
