/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PipelineHints
{

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class SegmentAssemblyNameAttribute : Attribute
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private PipelineSegment _segment;

        public PipelineSegment Segment
        {
            get { return _segment; }
            set { _segment = value; }
        }

        public SegmentAssemblyNameAttribute(PipelineSegment segment, String Name)
        {
            _name = Name;
            _segment = segment;
        }
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ShareViews : Attribute{}

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Struct)]
    public class CommentAttribute : Attribute
    {
        public CommentAttribute(String comment)
        {
            _comment = comment;
        }

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Struct, AllowMultiple = true)]
    public class NamespaceAttribute : Attribute
    {
        public NamespaceAttribute(PipelineSegment segment,String name)
        {
            _segment = segment;
            _name = name;
        }
        
        PipelineSegment _segment;

        public PipelineSegment Segment
        {
            get { return _segment; }
            set { _segment = value; }
        }

       

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface)]
    public class AbstractBaseClass : Attribute
    {
    }

    

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAddAttribute : Attribute
    {
        public EventAddAttribute(String name)
        {
            _name = name;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }


    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Method)]
    public class EventRemoveAttribute : Attribute
    {
         public EventRemoveAttribute(String name)
        {
            _name = name;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface)]
    public class EventHandlerAttribute : Attribute
    {
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface)]
    public class EventArgsAttribute : Attribute
    {
        private bool _cancelable;

        public bool Cancelable
        {
            get { return _cancelable; }
            set { _cancelable = value; }
        }
        
    }

  

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface)]
    public class BaseClassAttribute : Attribute
    {
        Type _base;

        public Type Base
        {
            get { return _base; }
        }

        public BaseClassAttribute(Type baseClass)
        {
            _base = baseClass;
        }
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Struct)]
    public class CustomPipelineAttribute : Attribute
    {
        PipelineSegment _segment;

        public PipelineSegment Segment
        {
            get { return _segment; }
            set { _segment = value; }
        }

        public CustomPipelineAttribute(PipelineSegment segment)
        {
            _segment = segment;
        }
    }
    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Interface)]
    public class AllowViewUpCasting : Attribute
    {
    }

    [Flags]
    public enum PipelineSegment
    {
        None = 0,
        HostView = 1,
        AddInView = 2,
        AddInSideAdapter = 4,
        HostSideAdapter = 8,
        Adapters=AddInSideAdapter | HostSideAdapter,
        Views =HostView | AddInView,
        All = Adapters | Views
    }

   

}
