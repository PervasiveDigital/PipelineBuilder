/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Text;

namespace PipelineBuilder
{
    [Serializable]
    public class SourceFile
    {
        //Determines whether files get the .generated addition to their filename and if the standard CodeDOM comments are added
        private static bool _generated = true;

        public static bool Generated
        {
            get { return SourceFile._generated; }
            set { SourceFile._generated = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
        }
        private String _source;

        public String Source
        {
            get { return _source; }
        }

        public SourceFile(String name, String source)
        {
            _name = name;
            _source = source;
        }

        public SourceFile(String name, CodeCompileUnit dom)
        {
            if (Generated)
            {
                _name = name + ".g.cs";
            }
            else
            {
                _name = name + ".cs";
            }

            _source = GetSourceFromCCU(dom);
        }

        private string GetSourceFromCCU(CodeCompileUnit CCU)
        {            
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            options.BlankLinesBetweenMembers = false;
            if (Generated)
            {
                cscp.GenerateCodeFromCompileUnit(CCU, sw, options);
            }
            else
            {
                cscp.GenerateCodeFromNamespace(CCU.Namespaces[0], sw, options);
            }
            return sb.ToString();
        }
    }
}
