using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLangProj80;

namespace PervasiveDigitalLLC.VsPipelineBuilderPkg
{
    class Pipeline
    {
        public void BuildPipeline(DTE2 dte, Project sourceProject, string destPath, string outputPath)
        {
            String sourceAssemblyPath = ProjectHelpers.GetFullOutputPath(sourceProject);
            if (!File.Exists(sourceAssemblyPath))
            {
                throw new InvalidOperationException("Please build the contract project before attempting to generate a pipeline.");
            }

            List<PipelineBuilder.PipelineSegmentSource> sources;

            sources = BuildSource(sourceAssemblyPath);

            Project hav = null;
            Project hsa = null;
            Project asa = null;
            Project aiv = null;
            Project view = null;
            int progress = 0;
            sourceProject.DTE.StatusBar.Progress(true, "Generating Pipeline", progress, sources.Count);
            foreach (PipelineBuilder.PipelineSegmentSource source in sources)
            {
                Project p = CreateAddInProject(dte, destPath, source);
                ProjectHelpers.SetOutputPath(p, source.Type, outputPath);
                switch (source.Type)
                {
                    case PipelineBuilder.SegmentType.HAV:
                        hav = p;
                        break;
                    case PipelineBuilder.SegmentType.HSA:
                        hsa = p;
                        break;
                    case PipelineBuilder.SegmentType.ASA:
                        asa = p;
                        break;
                    case PipelineBuilder.SegmentType.AIB:
                        aiv = p;
                        break;
                    case PipelineBuilder.SegmentType.VIEW:
                        view = p;
                        break;
                }
                progress++;
                sourceProject.DTE.StatusBar.Progress(true, "Generating Pipeline", progress, sources.Count);
            }

            VSProject2 hsa2 = (VSProject2)hsa.Object;
            VSProject2 asa2 = (VSProject2)asa.Object;
            hsa2.References.AddProject(sourceProject).CopyLocal = false;
            hsa2.References.Add(typeof(System.AddIn.Contract.IContract).Assembly.Location).CopyLocal = false;
            asa2.References.AddProject(sourceProject).CopyLocal = false;
            asa2.References.Add(typeof(System.AddIn.Contract.IContract).Assembly.Location).CopyLocal = false; ;
            if (view == null)
            {
                ProjectHelpers.AddProjectReference(hsa2, hav);
                ProjectHelpers.AddProjectReference(asa2, aiv);
            }
            else
            {
                ProjectHelpers.AddProjectReference(hsa2, view);
                ProjectHelpers.AddProjectReference(asa2, view);
            }
            sourceProject.DTE.StatusBar.Progress(false, "Pipeline Generation Complete", 1, 1);
            DTE2 dte2 = (DTE2)sourceProject.DTE;

            foreach (UIHierarchyItem solution in dte2.ToolWindows.SolutionExplorer.UIHierarchyItems)
            {
                ProjectHelpers.CollapseFolder(hsa2, asa2, solution);
            }
        }

        private List<PipelineBuilder.PipelineSegmentSource> BuildSource(String source)
        {
            IPipelineBuilderWorker worker = new PipelineBuilderWorker();
            List<PipelineBuilder.PipelineSegmentSource> sourceCode = worker.BuildPipeline(source);
            return sourceCode;
        }

        private Project CreateAddInProject(DTE2 dte, String destPath, PipelineBuilder.PipelineSegmentSource pipelineComponent)
        {
            destPath += "\\" + pipelineComponent.Name;
            String generatedDestPath = destPath + "\\Generated Files";

            Project proj = null;
            foreach (Project p in ProjectHelpers.GetProjectsFromSolution(dte))
            {
                if (p.Name.Trim().Equals((pipelineComponent.Name)))
                {
                    proj = p;
                }
            }
            if (proj == null)
            {
                if (Directory.Exists(destPath))
                {
                    throw new InvalidOperationException("The directory for this project already exists but is not part of the current solution. Please either add it back to the current solution or delete it manually: " + destPath);
                }
                else
                {
                    Directory.CreateDirectory(destPath);
                }


                dte.Solution.AddFromTemplate(ProjectHelpers.Pop(this.GetType().Assembly.Location) + "\\Template.csproj", destPath, pipelineComponent.Name + ".csproj", false);

                foreach (Project p in ProjectHelpers.GetProjectsFromSolution(dte))
                {
                    if (p.Name.Trim().Equals((pipelineComponent.Name)))
                    {
                        proj = p;
                    }
                }
            }

            if (Directory.Exists(generatedDestPath))
            {
                CheckSumValidator.ValidateCheckSum(generatedDestPath);
            }
            if (proj != null)
            {
                try
                {
                    proj.ProjectItems.Item("Generated Files").Delete();
                }
                catch (ArgumentException)
                {
                    //If there is no generated files project item then we should just ignore this step.
                }
                if (Directory.Exists(generatedDestPath))
                {
                    throw new InvalidOperationException("Generated Files directory exists but is not part of project. Please save required files and delete manually: " + generatedDestPath);
                }
                Directory.CreateDirectory(generatedDestPath);
                proj.ProjectItems.AddFolder("Generated Files", null);
                foreach (PipelineBuilder.SourceFile source in pipelineComponent.Files)
                {
                    String path = generatedDestPath + "\\" + source.Name;
                    StreamWriter sw = new StreamWriter(path);
                    sw.WriteLine(source.Source);
                    sw.Close();

                }
                proj.ProjectItems.AddFromDirectory(generatedDestPath);
                CheckSumValidator.StoreCheckSum(generatedDestPath);
            }
            return proj;
        }


    }
}
