/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineBuilder
{
     internal class PropertyMethodInfo
    {
        PropertyType _type;

        public PropertyType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public PropertyMethodInfo(PropertyType type, String name)
        {
            _type = type;
            _name = name;
        }
    }

    internal enum PropertyType
    {
        get, set
    }
}
