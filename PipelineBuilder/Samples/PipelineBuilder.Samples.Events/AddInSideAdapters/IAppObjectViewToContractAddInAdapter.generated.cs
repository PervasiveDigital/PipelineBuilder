//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PipelineBuilder.Samples.EventsAddInAdapters {
    
    
    public class IAppObjectViewToContractAddInAdapter : System.AddIn.Pipeline.ContractBase, PipelineBuilder.Samples.Events.Contracts.IAppObjectContract {
        
        private PipelineBuilder.Samples.Events.IAppObject _view;
        
        private System.Collections.Generic.Dictionary<PipelineBuilder.Samples.Events.Contracts.IDocumentOpenedHandlerContract, System.EventHandler<PipelineBuilder.Samples.Events.DocumentOpenedEventArgs>> DocumentOpened_handlers;
        
        public IAppObjectViewToContractAddInAdapter(PipelineBuilder.Samples.Events.IAppObject view) {
            _view = view;
            DocumentOpened_handlers = new System.Collections.Generic.Dictionary<PipelineBuilder.Samples.Events.Contracts.IDocumentOpenedHandlerContract, System.EventHandler<PipelineBuilder.Samples.Events.DocumentOpenedEventArgs>>();
        }
        
        public virtual void DocumentOpenedAdd(PipelineBuilder.Samples.Events.Contracts.IDocumentOpenedHandlerContract handler) {
            System.EventHandler<PipelineBuilder.Samples.Events.DocumentOpenedEventArgs> adaptedHandler = new System.EventHandler<PipelineBuilder.Samples.Events.DocumentOpenedEventArgs>(new IDocumentOpenedHandlerContractToViewAddInAdapter(handler).Handler);
            _view.DocumentOpened += adaptedHandler;
            DocumentOpened_handlers[handler] = adaptedHandler;
        }
        
        public virtual void DocumentOpenedRemove(PipelineBuilder.Samples.Events.Contracts.IDocumentOpenedHandlerContract handler) {
            System.EventHandler<PipelineBuilder.Samples.Events.DocumentOpenedEventArgs> adaptedHandler;
            if (DocumentOpened_handlers.TryGetValue(handler, out adaptedHandler)) {
                DocumentOpened_handlers.Remove(handler);
                _view.DocumentOpened -= adaptedHandler;
            }
        }
        
        internal PipelineBuilder.Samples.Events.IAppObject GetSourceView() {
            return _view;
        }
    }
}

