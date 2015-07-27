/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PipelineBuilder
{
    public class SymbolTable
    {
        String _rootName;
        Assembly _rootAssembly;
        bool _sharedView = false;
        Dictionary<SegmentType, String> _assemblyNameMapping;
       
        internal SymbolTable(Assembly root)
        {
            _rootAssembly = root;
            _assemblyNameMapping = new Dictionary<SegmentType, string>();
            _sharedView = root.GetCustomAttributes(typeof(PipelineHints.ShareViews), false).Length > 0;
            _rootName = root.GetName().Name;
            InitAssemblyNames(root);
        }

    
        internal string GetNameSpace(SegmentType component, Type contractType)
        {
            if (contractType.IsArray)
            {
                contractType = contractType.GetElementType();
            }
            object[] namespaceAttributes = contractType.GetCustomAttributes(typeof(PipelineHints.NamespaceAttribute), false);
            foreach (PipelineHints.NamespaceAttribute attr in namespaceAttributes)
            {
                if (ComponentsAreEquivalent(component, attr.Segment))
                {
                    return attr.Name;
                }
            }
            String contractNamespace = contractType.FullName.Remove(contractType.FullName.LastIndexOf("."));
            if (contractNamespace.EndsWith(".Contracts") || contractNamespace.EndsWith(".Contract"))
            {
                string viewNamespace = contractNamespace.Remove(contractNamespace.LastIndexOf("."));
                if (!(component.Equals(SegmentType.ASA) || component.Equals(SegmentType.HSA)))
                {
                    return viewNamespace;
                }
                else if (component.Equals(SegmentType.ASA))
                {
                    return viewNamespace + ".AddInSideAdapters";
                }
                else if (component.Equals(SegmentType.HSA))
                {
                    return viewNamespace + ".HostSideAdapters";
                }
            }
            else
            {
                switch (component)
                {
                    case SegmentType.AIB:
                        return contractNamespace + ".AddInViews";
                    case SegmentType.ASA:
                        return contractNamespace + ".AddInSideAdapters";
                    case SegmentType.HAV:
                        return contractNamespace + ".HostViews";
                    case SegmentType.HSA:
                        return contractNamespace + ".HostSideAdapters";
                    case SegmentType.VIEW:
                        return contractNamespace + ".Views";
                    default:
                        throw new InvalidOperationException("Component is not a valid type: " + component + "/" + contractType.FullName);
                }
            }
            throw new InvalidOperationException("Component is not a valid type: " + component + "/" + contractType.FullName);
            
        }

        internal static bool ComponentsAreEquivalent(SegmentType component, PipelineHints.PipelineSegment pipelineHints)
        {
            if (component.Equals(SegmentType.ASA) && pipelineHints.Equals(PipelineHints.PipelineSegment.AddInSideAdapter))
            {
                return true;
            }
            if (component.Equals(SegmentType.HSA) && pipelineHints.Equals(PipelineHints.PipelineSegment.HostSideAdapter))
            {
                return true;
            }
            if (component.Equals(SegmentType.HAV) && pipelineHints.Equals(PipelineHints.PipelineSegment.HostView))
            {
                return true;
            }
            if (component.Equals(SegmentType.AIB) && pipelineHints.Equals(PipelineHints.PipelineSegment.AddInView))
            {
                return true;
            }
            if ((pipelineHints.Equals(PipelineHints.PipelineSegment.Views) &&
                    (component.Equals(SegmentType.HAV) || component.Equals(SegmentType.AIB) || component.Equals(SegmentType.VIEW))))
            {
                return true;
            }
            return false;
           
        }

        internal void InitAssemblyNames(Assembly asm)
        {
            _assemblyNameMapping[SegmentType.HAV] = "HostView";
            _assemblyNameMapping[SegmentType.HSA] = "HostSideAdapters";
            _assemblyNameMapping[SegmentType.ASA] = "AddInSideAdapters";
            _assemblyNameMapping[SegmentType.AIB] = "AddInView";
            _assemblyNameMapping[SegmentType.VIEW] = "View";
            foreach (PipelineHints.SegmentAssemblyNameAttribute attr in asm.GetCustomAttributes(typeof(PipelineHints.SegmentAssemblyNameAttribute), false))
            {
                if (attr.Segment.Equals(PipelineHints.PipelineSegment.HostView))
                {
                    _assemblyNameMapping[SegmentType.HAV] = attr.Name;
                }
                if (attr.Segment.Equals(PipelineHints.PipelineSegment.AddInView))
                {
                    _assemblyNameMapping[SegmentType.AIB] = attr.Name;
                }
                if (attr.Segment.Equals(PipelineHints.PipelineSegment.HostSideAdapter))
                {
                    _assemblyNameMapping[SegmentType.HSA] = attr.Name;
                }
                if (attr.Segment.Equals(PipelineHints.PipelineSegment.AddInSideAdapter))
                {
                    _assemblyNameMapping[SegmentType.ASA] = attr.Name;
                }
                if (attr.Segment.Equals(PipelineHints.PipelineSegment.Views))
                {
                    _assemblyNameMapping[SegmentType.VIEW] = attr.Name;
                }
            }
        }

        internal String GetAssemblyName(SegmentType component)
        {
            return _assemblyNameMapping[component];
        }

      

        private string NormalizeContractName(Type contract)
        {
            String name = contract.Name;
            String result = contract.Name;
            result = result.Replace("[]", "");
            if (name.Equals("IContract"))
            {
                return name;
            }
            if (result.StartsWith("I") && contract.IsInterface)
            {
                result = result.Substring(1);
            }
            if (result.EndsWith("Contract"))
            {
                result = result.Remove(result.LastIndexOf("Contract"));
            }
            return result;
        }

        internal string GetStaticAdapterMethodNameName(Type type, SegmentType component, SegmentDirection direction)
        {
           switch (direction)
            {
                case SegmentDirection.ContractToView:
                    return "ContractToViewAdapter";
                case SegmentDirection.ViewToContract:
                    return "ViewToContractAdapter";
                default: 
                    throw new InvalidOperationException("Must be either incoming our outgoing");
            }
        }

        internal string GetNameFromType(Type type, SegmentType component)
        {
            return GetNameFromType(type, component, SegmentDirection.None);
        }
        
        internal string GetNameFromType(Type type, SegmentType component,SegmentDirection direction)
        {
            return GetNameFromType(type,component,direction,true);
        }

        internal string GetNameFromType(Type type, SegmentType component, SegmentDirection direction, Type referenceType)
        {
            if (direction.Equals(SegmentDirection.None))
            {
                return GetNameFromType(type, component, direction,
                    !GetNameSpace(component, type).Equals(GetNameSpace(component, referenceType)));
            }
            else 
            {
                return GetNameFromType(type, component, direction, true);
            }
        }

        internal string GetNameFromType(Type type, SegmentType segmentType,SegmentDirection direction, bool prefix)
        {
            Type underlyingType;
            if (type.IsArray)
            {
                underlyingType = type.GetElementType();
            }
            else
            {
                underlyingType = type;
            }
            if (type.Equals(typeof(System.AddIn.Contract.INativeHandleContract)))
            {
                if (segmentType.Equals(SegmentType.VIEW) || segmentType.Equals(SegmentType.AIB) || segmentType.Equals(SegmentType.HAV))
                {
                    return typeof(System.Windows.FrameworkElement).FullName;
                }
                else
                {
                    return typeof(System.AddIn.Pipeline.FrameworkElementAdapters).FullName;
                }
            }
            if (!type.Assembly.Equals(this._rootAssembly)) 
            {
                return type.FullName;
            }
            
            String refPrefix = "";
            String refSuffix = "";
            if (direction == SegmentDirection.ContractToView)
            {
                refSuffix = "ContractToView";
            }
            else if (direction == SegmentDirection.ViewToContract)
            {
                refSuffix = "ViewToContract";
            }
            String typeName = NormalizeContractName(type);
            if (PipelineBuilder.IsViewInterface(type))
            {
                typeName = "I" + typeName;
            }
            if (prefix)
            {
                if (_sharedView && (segmentType.Equals(SegmentType.HAV) || segmentType.Equals(SegmentType.AIB) || segmentType.Equals(SegmentType.VIEW)))
                {
                    refPrefix = GetNameSpace(SegmentType.VIEW,underlyingType) + ".";
                }
                else
                {
                    refPrefix = GetNameSpace(segmentType, underlyingType) + ".";
                }
            }
            if (type.IsArray)
            {
                if (segmentType.Equals(SegmentType.ASA) || segmentType.Equals(SegmentType.HSA))
                {
                    typeName += "Array";
                }
                else
                {
                    typeName += "[]";
                }
            }
            switch (segmentType) 
            {
                case SegmentType.HAV:
                    return refPrefix + typeName;
                case SegmentType.HSA:
                    return refPrefix + typeName + refSuffix + "HostAdapter";
                case SegmentType.ASA:
                    return refPrefix + typeName + refSuffix + "AddInAdapter";
                case SegmentType.AIB:
                    return refPrefix + typeName;
                case SegmentType.VIEW:
                    return refPrefix + typeName;
                default:
                    throw new InvalidOperationException("No segment type specified: " + segmentType);
            }
        }

    }
}
