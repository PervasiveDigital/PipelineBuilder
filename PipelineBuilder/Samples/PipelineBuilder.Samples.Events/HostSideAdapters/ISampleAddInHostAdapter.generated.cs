//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PipelineBuilder.Samples.EventsHostAdapers {
    
    
    public class ISampleAddInHostAdapter {
        
        internal static PipelineBuilder.Samples.Events.ISampleAddIn ContractToViewAdapter(PipelineBuilder.Samples.Events.Contracts.ISampleAddInContract contract) {
            if (((System.Runtime.Remoting.RemotingServices.IsObjectOutOfAppDomain(contract) != true) 
                        && contract.GetType().Equals(typeof(ISampleAddInViewToContractHostAdapter)))) {
                return ((ISampleAddInViewToContractHostAdapter)(contract)).GetSourceView();
            }
            else {
                return new ISampleAddInContractToViewHostAdapter(contract);
            }
        }
        
        internal static PipelineBuilder.Samples.Events.Contracts.ISampleAddInContract ViewToContractAdapter(PipelineBuilder.Samples.Events.ISampleAddIn view) {
            if (view.GetType().Equals(typeof(ISampleAddInContractToViewHostAdapter))) {
                return ((ISampleAddInContractToViewHostAdapter)(view)).GetSourceContract();
            }
            else {
                return new ISampleAddInViewToContractHostAdapter(view);
            }
        }
    }
}

