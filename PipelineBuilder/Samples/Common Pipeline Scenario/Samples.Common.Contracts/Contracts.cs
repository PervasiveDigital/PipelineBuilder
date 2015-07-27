/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

//This attribute indicates to the tool that we want to use the same view assembly for both hosts and add-ins
[assembly: PipelineHints.ShareViews]

//These attributes indicate the assembly name we want to use for each of our components
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.Views, "Samples.Common")]
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.HostSideAdapter, "Samples.Common.HostSideAdapters")]
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.AddInSideAdapter, "Samples.Common.AddInSideAdapters")]


namespace Samples.Common.Contracts
{
    //The tool will see that this contract has the System.AddIn.Pipeline.AddInContract attribute and 
    //will apply the HostAdapter,AddInAdapter,AddInBase attributes to the appropriate corresponding types.
    [AddInContract]
    public interface IStandardAddInContract : IContract
    {
        void Initialize(IAppObjectContract appObj);
    }

    public interface IAppObjectContract : IContract
    {
        IDocContract OpenDocument(string path);
        DetailedDocInfo GetDetailedInfo(IDocContract doc);

        //By passing a group of structs across in an array instead of an IListContract you can increase performance by
        //reducing the number of AppDomain transitions. The majority of the cost of crossing the boundary is fixed and you 
        //only pay a very small cost for each additional byte you include in that transtion.
        DetailedDocInfo[] AvailableDocuments
        {
            get;
        }


        //This will get translated to an IList<IDoc> in the views
        IListContract<IDocContract> OpenDocuments
        {
            get;
        }

        //This pair of methods represent an Event on the view. 
        //Since events can't be marshaled well across isolation boundaries we represent them as 
        //two seperate methods: one for adding a handler and one for removing it. 
        //The attributes are used to link these two methods together and to determine the 
        //name for the event on the view.
        [PipelineHints.EventAdd("DocumentOpened")]
        void DocumentOpenedEventAdd(IDocumentEventHandlerContract handler);
        [PipelineHints.EventRemove("DocumentOpened")]
        void DocumentOpenedEventRemove(IDocumentEventHandlerContract handler);

        //The same event handler contract type can be used in multiple events as long as they share the
        //same event args type.
        [PipelineHints.EventAdd("DocumentClosed")]
        void DocumentClosedEventAdd(IDocumentEventHandlerContract handler);
        [PipelineHints.EventRemove("DocumentClosed")]
        void DocumentClosedEventRemove(IDocumentEventHandlerContract handler);
    }


    //This attribute indicates that this type represents an events handler type. 
    //This type will not get represented in the view as it is simply representing a delegate for
    //crossing an isolation boundary.
    //The source object will automatically get filled in by the adapters to be the view this event
    //was defined one. 
    [PipelineHints.EventHandler]
    public interface IDocumentEventHandlerContract : IContract
    {
        //Types representing event handlers must have exacly one method, and that method must take
        //exactly one parameter, which must be of a contract type representing the event args.
        void EventHandler(IDocumentEventArgsContract args);
    }

    //This attribute indicates to the tool that this type should be used as an event args type on the view
    //The view type will derive from System.Event args rather than object.
    //There are no additonal restrictions on the members for event args types.
    [PipelineHints.EventArgs]
    public interface IDocumentEventArgsContract : IContract
    {
        IDocContract Document
        {
            get;
        }
    }

    public interface IDocContract : IContract
    {
        string Name
        {
            get;
            set;
        }

        DocPriorityContract Priority
        {
            get;
        }
    }

    //A corresponding enum type will be created in the view. If there is a flags attribute one will be 
    //applied to the view, and the values in the view will match the values in the contract. The adapters
    //will simply perform cast operations to adapt between contracts and views. 
    [Flags]
    public enum DocPriorityContract
    {
        High = 0, Medium = 1, Low = 2
    }

    //By using structs to pass across large amounts of data you can increase performance and decrease the
    //number of AppDomain transitions you make. All serialized data in the struct will be serialized at once.
    [Serializable]
    public struct DetailedDocInfo
    {
        string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        byte[] _contents;

        public byte[] Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        //The names of the input parameters to the constructor must match the names of the corresponding properties. 
        public DetailedDocInfo(String path, byte[] contents)
        {
            _path = path;
            _contents = contents;
        }
    }
}
