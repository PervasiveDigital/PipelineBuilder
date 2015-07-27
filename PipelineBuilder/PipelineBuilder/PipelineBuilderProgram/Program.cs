/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PipelineBuilder;

namespace PipelineBuilderProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                String source = args[0];
                String dest = args[1];
                BuildPipeline(Assembly.LoadFrom(source), dest, true);
            }
            else
            {
                Console.WriteLine("Invalid arguments: source dest");
            }
        }


        private static void BuildPipeline(Assembly source, String root, bool print)
        {
            PipelineBuilder.PipelineBuilder tpb = new PipelineBuilder.PipelineBuilder(source.Location);
            List<PipelineSegmentSource> pipelines = tpb.BuildPipeline();
            Console.BufferHeight = 999;
            foreach (PipelineSegmentSource pipline in pipelines)
            {
                String compRoot = root + "\\" + pipline.Name;
                if (!Directory.Exists(compRoot))
                {
                    Directory.CreateDirectory(compRoot);
                }
                foreach (SourceFile file in pipline.Files)
                {
                    StreamWriter sw = new StreamWriter(compRoot + "\\" + file.Name);
                    sw.WriteLine(file.Source);
                    sw.Close();
                    if (print)
                    {
                        Console.WriteLine("Name: " + file.Name);
                        Console.WriteLine(file.Source);
                        Console.WriteLine();
                    }
                }
            }
            Console.ReadLine();
        }

        
    }
}
