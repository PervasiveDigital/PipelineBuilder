/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineBuilder
{
    [Serializable]
    public class PipelineSegmentSource
    {
        SegmentType _type;
        
        

        public SegmentType Type
        {
            get { return _type; }
        }
        List<SourceFile> _files;

        public List<SourceFile> Files
        {
            get { return _files; }
        }
        string _name;

        public string Name
        {
            get { return _name; }
        }

        public PipelineSegmentSource(SegmentType type, SymbolTable symbols)
        {
            _type = type;
            _name = symbols.GetAssemblyName(type);
            _files = new List<SourceFile>();
        }
    }

    [Flags]
    public enum  SegmentType
    {
        HAV=0,
        HSA =1, 
        ASA=2, 
        AIB=4,
        VIEW = 8
       
    }

    public enum SegmentDirection
    {
        ViewToContract,ContractToView,None
    }
}
