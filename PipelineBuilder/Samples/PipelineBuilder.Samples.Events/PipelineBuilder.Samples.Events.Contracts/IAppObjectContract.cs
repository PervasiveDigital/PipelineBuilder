using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;
using PipelineHints;

namespace PipelineBuilder.Samples.Events.Contracts
{
    public interface IAppObjectContract : IContract
    {
        [EventAdd("DocumentOpened")]
        void DocumentOpenedAdd(IDocumentOpenedHandlerContract handler);
        [EventRemove("DocumentOpened")]
        void DocumentOpenedRemove(IDocumentOpenedHandlerContract handler);
    }

    [EventHandler]
    public interface IDocumentOpenedHandlerContract : IContract 
    {
        void Handle(IDocumentOpenedEventArgsContract args);
    }

    [EventArgs]
    public interface IDocumentOpenedEventArgsContract : IContract
    {
    }
}
