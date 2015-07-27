using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PervasiveDigitalLLC.VsPipelineBuilderPkg
{
    internal class PipelineBuilderWorker : MarshalByRefObject, IPipelineBuilderWorker
    {
        public PipelineBuilderWorker()
        {
        }

        public List<PipelineBuilder.PipelineSegmentSource> BuildPipeline(String sourceFile)
        {
            PipelineBuilder.PipelineBuilder builder = new PipelineBuilder.PipelineBuilder(sourceFile);
            return builder.BuildPipeline();
        }
    }

}
