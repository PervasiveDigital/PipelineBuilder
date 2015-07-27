using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using VSLangProj110;
using Microsoft.VisualStudio.CommandBars;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using VSLangProj80;

namespace PipelineBuilderPkg
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
            ResolveEventHandler handler = new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomain.CurrentDomain.AssemblyResolve += handler;
        }

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
				Commands2 commands = (Commands2)_applicationObject.Commands;
				string toolsMenuName = "Tools";

				//Place the command on the tools menu.
				//Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
				Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

				//Find the Tools command bar on the MenuBar command bar:
				CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
				CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

				//This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
				//  just make sure you also update the QueryStatus/Exec method to include the new command names.
				try
				{
					//Add a command to the Commands collection:
					Command command = commands.AddNamedCommand2(_addInInstance, "PipelineBuilderPkg", "Pipeline Builder", "Executes the command for PipelineBuilder", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported+(int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

					//Add a control for the command to the tools menu:
					if((command != null) && (toolsPopup != null))
					{
						command.AddControl(toolsPopup.CommandBar, 1);
					}
				}
				catch(System.ArgumentException)
				{
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
				}
			}
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}
		
		/// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		/// <param term='commandName'>The name of the command to determine state for.</param>
		/// <param term='neededText'>Text that is needed for the command.</param>
		/// <param term='status'>The state of the command in the user interface.</param>
		/// <param term='commandText'>Text requested by the neededText parameter.</param>
		/// <seealso class='Exec' />
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
			if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				if(commandName == "PipelineBuilderPkg.Connect.PipelineBuilderPkg")
				{
					status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported|vsCommandStatus.vsCommandStatusEnabled;
					return;
				}
			}
		}

		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				if(commandName == "PipelineBuilderPkg.Connect.PipelineBuilderPkg")
				{
                    handled = true;
                    LoadMe();
                    try
                    {
                        PipelineConfiguration config = new PipelineConfiguration();
                        config.Initialize(_applicationObject);
                        if (config.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK))
                        {
                            BuildPipeline(config.SourceProject, config.ProjectDestination, config.BuildDestination);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        DisplayError error = new DisplayError();
                        error.SetError(e);
                        error.Show();
                    }
                    return;
                }
			}
		}

        private void LoadMe()
        {
            LoadFromPromotion();
        }

        private static void LoadFromPromotion()
        {
            Assembly.Load(typeof(PipelineBuilderPkg.Connect).Assembly.FullName);
            Assembly.Load(typeof(PipelineBuilder.PipelineBuilder).Assembly.FullName);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName name = new AssemblyName(args.Name);
            if (args.Name.Equals(typeof(Connect).Assembly.FullName))
            {
                return typeof(Connect).Assembly;
            }
            String myPath = Pop(typeof(Connect).Assembly.Location);
            if (name.Name.Equals("PipelineBuilder"))
            {
                return Assembly.LoadFrom(myPath + "\\PipelineBuilder.dll");
            }
            if (name.Name.Equals("PipelineHints"))
            {
                return Assembly.LoadFrom(myPath + "\\PipelineBuilder.dll");
            }
            return null;
        }

        private void BuildPipeline(Project sourceProject, string destPath, string outputPath)
        {
            String sourceAssemblyPath = GetFullOutputPath(sourceProject);
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
                Project p = CreateAddInProject(destPath, source);
                SetOutputPath(p, source.Type, outputPath);
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
                AddProjectReference(hsa2, hav);
                AddProjectReference(asa2, aiv);
            }
            else
            {
                AddProjectReference(hsa2, view);
                AddProjectReference(asa2, view);
            }
            sourceProject.DTE.StatusBar.Progress(false, "Pipeline Generation Complete", 1, 1);
            DTE2 dte2 = (DTE2)sourceProject.DTE;

            foreach (UIHierarchyItem solution in dte2.ToolWindows.SolutionExplorer.UIHierarchyItems)
            {
                CollapseFolder(hsa2, asa2, solution);
            }

        }

        private static void AddProjectReference(VSProject2 project, Project reference)
        {
            if (project.References.Item(reference.Name) == null)
            {
                project.References.AddProject(reference).CopyLocal = false;
            }
        }


        private static void CollapseFolder(VSProject2 hsa2, VSProject2 asa2, UIHierarchyItem folder)
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



        private void SetOutputPath(Project p, PipelineBuilder.SegmentType type, String root)
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

        private List<PipelineBuilder.PipelineSegmentSource> BuildSource(String source)
        {
            IPipelineBuilderWorker worker = new PipelineBuilderWorker();
            List<PipelineBuilder.PipelineSegmentSource> sourceCode = worker.BuildPipeline(source);
            return sourceCode;
        }





        private Project CreateAddInProject(String destPath, PipelineBuilder.PipelineSegmentSource pipelineComponent)
        {
            destPath += "\\" + pipelineComponent.Name;
            String generatedDestPath = destPath + "\\Generated Files";

            Project proj = null;
            foreach (Project p in GetProjectsFromSolution(_applicationObject))
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


                _applicationObject.Solution.AddFromTemplate(Pop(this.GetType().Assembly.Location) + "\\Template.csproj", destPath, pipelineComponent.Name + ".csproj", false);

                foreach (Project p in GetProjectsFromSolution(_applicationObject))
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

        internal static string GetFullOutputPath(Project p)
        {
            String fileName;
            fileName = Pop(p.FileName) + "\\";
            fileName += p.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(); ;
            fileName += p.Properties.Item("OutputFileName").Value;

            return fileName;
        }

        internal static string Pop(string path)
        {
            if (!path.Contains("\\"))
            {
                throw new InvalidOperationException("Path does not contain '\\': " + path);
            }
            return path.Substring(0, path.LastIndexOf('\\'));
        }

        internal static List<Project> GetProjectsFromSolution(DTE2 root)
        {
            List<Project> projects = new List<Project>();
            foreach (Project proj in root.Solution.Projects)
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

		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}

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