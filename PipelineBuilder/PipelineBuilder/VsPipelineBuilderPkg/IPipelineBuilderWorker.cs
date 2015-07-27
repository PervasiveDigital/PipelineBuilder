/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;

namespace PervasiveDigitalLLC.VsPipelineBuilderPkg
{
    interface IPipelineBuilderWorker
    {
        System.Collections.Generic.List<PipelineBuilder.PipelineSegmentSource> BuildPipeline(string sourceFile);
    }
}
