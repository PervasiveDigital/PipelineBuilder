using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLangProj80;

namespace PervasiveDigitalLLC.VsPipelineBuilderPkg
{
    class ProjectHelpers
    {
        internal static List<Project> GetProjectsFromSolution(DTE2 root)
        {

            List<Project> projects = new List<Project>();
            foreach (Project proj in root.Solution.Projects)
            {
                if (proj != null)
                {
                    if (!proj.Kind.Equals(EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder))
                    {
                        projects.Add(proj);
                    }
                    else
                    {
                        projects.AddRange(GetProjectsFromSolutionFolder(proj));
                    }
                }
            }
            return projects;
        }

        internal static List<Project> GetProjectsFromSolutionFolder(Project slnFolder)
        {
            List<Project> projects = new List<Project>();
            foreach (ProjectItem projItem in slnFolder.ProjectItems)
            {
                Project proj = projItem.SubProject;
                if (proj != null)
                {
                    if (!proj.Kind.Equals(EnvDTE80.ProjectKinds.vsProjectKindSolutionFolder))
                    {
                        projects.Add(proj);
                    }
                    else
                    {
                        projects.AddRange(GetProjectsFromSolutionFolder(proj));
                    }
                }
            }
            return projects;
        }

        internal static string GetFullOutputPath(Project p)
        {
            String fileName;
            fileName = Pop(p.FileName) + "\\";
            fileName += p.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(); ;
            fileName += p.Properties.Item("OutputFileName").Value;

            return fileName;
        }

        internal static void SetOutputPath(Project p, PipelineBuilder.SegmentType type, String root)
        {
            foreach (Configuration c in p.ConfigurationManager)
            {
                Property prop = c.Properties.Item("OutputPath");
                prop.Value = root;
                switch (type)
                {
                    case PipelineBuilder.SegmentType.AIB:
                        prop.Value += "\\AddInViews";
                        break;
                    case PipelineBuilder.SegmentType.ASA:
                        prop.Value += "\\AddInSideAdapters";
                        break;
                    case PipelineBuilder.SegmentType.HSA:
                        prop.Value += "\\HostSideAdapters";
                        break;
                    case PipelineBuilder.SegmentType.HAV:
                        break;
                    case PipelineBuilder.SegmentType.VIEW:
                        prop.Value += "\\AddInViews";
                        break;
                    default:
                        throw new InvalidOperationException("Not a valid pipeline component: " + p.Name);
                }
            }
        }

        internal static void CollapseFolder(VSProject2 hsa2, VSProject2 asa2, UIHierarchyItem folder)
        {
            foreach (UIHierarchyItem project in folder.UIHierarchyItems)
            {
                foreach (UIHierarchyItem projectItem in project.UIHierarchyItems)
                {
                    if (projectItem.Name.Equals("References"))
                    {
                        projectItem.UIHierarchyItems.Expanded = false;
                    }
                    else if (projectItem.Name.Equals("Generated Files") && (project.Name.Equals(hsa2.Project.Name) || project.Name.Equals(asa2.Project.Name)))
                    {
                        projectItem.UIHierarchyItems.Expanded = false;
                    }
                    CollapseFolder(hsa2, asa2, projectItem);
                }
                if (project.Name.Equals(hsa2.Project.Name) || project.Name.Equals(asa2.Project.Name))
                {
                    project.UIHierarchyItems.Expanded = false;
                }
            }
        }

        internal static void AddProjectReference(VSProject2 project, Project reference)
        {
            if (project.References.Item(reference.Name) == null)
            {
                project.References.AddProject(reference).CopyLocal = false;
            }
        }

        internal static string Pop(string path)
        {
            if (!path.Contains("\\"))
            {
                throw new InvalidOperationException("Path does not contain '\\': " + path);
            }
            return path.Substring(0, path.LastIndexOf('\\'));
        }

        internal static string GetOutputAssembly(Project p)
        {
            String fileName;
            if (p.FileName.Contains(":"))
            {
                fileName = "";
            }
            else
            {
                fileName = Pop(p.FileName) + "\\";
            }
            fileName += p.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(); ;
            fileName += p.Properties.Item("OutputFileName").Value;

            return fileName;
        }
    }
}
