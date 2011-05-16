// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// FileHandler does all file opening and closing that
	/// involves interacting with the user.
	/// </summary>
	public class TestLoaderUI
	{
		private static bool VisualStudioSupport
		{
			get 
			{ 
				return Services.UserSettings.GetSetting( "Options.TestLoader.VisualStudioSupport", false ); 
			}
		}

		public static void OpenProject( Form owner )
		{
			OpenFileDialog dlg = new OpenFileDialog();
			System.ComponentModel.ISite site = owner == null ? null : owner.Site;
			if ( site != null ) dlg.Site = site;
			dlg.Title = "Open Project";
			
			if ( VisualStudioSupport )
			{
                dlg.Filter =
					"Projects & Assemblies(*.nunit,*.csproj,*.vbproj,*.vjsproj, *.vcproj,*.sln,*.dll,*.exe )|*.nunit;*.csproj;*.vjsproj;*.vbproj;*.vcproj;*.sln;*.dll;*.exe|" +
					"All Project Types (*.nunit,*.csproj,*.vbproj,*.vjsproj,*.vcproj,*.sln)|*.nunit;*.csproj;*.vjsproj;*.vbproj;*.vcproj;*.sln|" +
                    "Test Projects (*.nunit)|*.nunit|" +
                    "Solutions (*.sln)|*.sln|" +
                    "C# Projects (*.csproj)|*.csproj|" +
                    "J# Projects (*.vjsproj)|*.vjsproj|" +
                    "VB Projects (*.vbproj)|*.vbproj|" +
                    "C++ Projects (*.vcproj)|*.vcproj|" +
                    "Assemblies (*.dll,*.exe)|*.dll;*.exe";
			}
			else
			{
                dlg.Filter =
                    "Projects & Assemblies(*.nunit,*.dll,*.exe)|*.nunit;*.dll;*.exe|" +
                    "Test Projects (*.nunit)|*.nunit|" +
                    "Assemblies (*.dll,*.exe)|*.dll;*.exe";
			}

			dlg.FilterIndex = 1;
			dlg.FileName = "";

			if ( dlg.ShowDialog( owner ) == DialogResult.OK ) 
				OpenProject( owner, dlg.FileName );
		}

		public static void OpenProject( Form owner, string testFileName, string configName, string testName )
		{
			TestLoader loader = Services.TestLoader;

			if ( loader.IsProjectLoaded && SaveProjectIfDirty( owner ) == DialogResult.Cancel )
				return;

			loader.LoadProject( testFileName, configName );
			if ( loader.IsProjectLoaded )
			{	
				NUnitProject testProject = loader.TestProject;
				if ( testProject.Configs.Count == 0 )
					UserMessage.DisplayInfo( "Loaded project contains no configuration data" );
				else if ( testProject.ActiveConfig == null )
					UserMessage.DisplayInfo( "Loaded project has no active configuration" );
				else if ( testProject.ActiveConfig.Assemblies.Count == 0 )
					UserMessage.DisplayInfo( "Active configuration contains no assemblies" );
				else
					loader.LoadTest( testName );
			}
		}

		public static void OpenProject( Form owner, string testFileName )
		{
			OpenProject( owner, testFileName, null, null );
		}

//		public static void OpenResults( Form owner )
//		{
//			TestLoader loader = Services.TestLoader;
//			
//			OpenFileDialog dlg = new OpenFileDialog();
//			System.ComponentModel.ISite site = owner == null ? null : owner.Site;
//			if ( site != null ) dlg.Site = site;
//			dlg.Title = "Open Test Results";
//
//			dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
//			dlg.FilterIndex = 1;
//			dlg.FileName = "";
//
//			if ( dlg.ShowDialog( owner ) == DialogResult.OK ) 
//				OpenProject( owner, dlg.FileName );
//		}

		public static void AddToProject( Form owner )
		{
			AddToProject( owner, null );
		}

		public static void AddToProject( Form owner, string configName )
		{
			TestLoader loader = Services.TestLoader;
			ProjectConfig config = configName == null
				? loader.TestProject.ActiveConfig
				: loader.TestProject.Configs[configName];

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Add Assemblies To Project";
			dlg.InitialDirectory = config.BasePath;

			if ( VisualStudioSupport )
				dlg.Filter =
					"Projects & Assemblies(*.csproj,*.vbproj,*.vjsproj, *.vcproj,*.dll,*.exe )|*.csproj;*.vjsproj;*.vbproj;*.vcproj;*.dll;*.exe|" +
					"Visual Studio Projects (*.csproj,*.vjsproj,*.vbproj,*.vcproj)|*.csproj;*.vjsproj;*.vbproj;*.vcproj|" +
					"C# Projects (*.csproj)|*.csproj|" +
					"J# Projects (*.vjsproj)|*.vjsproj|" +
					"VB Projects (*.vbproj)|*.vbproj|" +
					"C++ Projects (*.vcproj)|*.vcproj|" +
					"Assemblies (*.dll,*.exe)|*.dll;*.exe";
			else
				dlg.Filter = "Assemblies (*.dll,*.exe)|*.dll;*.exe";

			dlg.FilterIndex = 1;
			dlg.FileName = "";

			if ( dlg.ShowDialog( owner ) != DialogResult.OK )
				return;

            if (PathUtils.IsAssemblyFileType(dlg.FileName))
            {
                config.Assemblies.Add(dlg.FileName);
                return;
            }
            else if (VSProject.IsProjectFile(dlg.FileName))
                try
                {
                    VSProject vsProject = new VSProject(dlg.FileName);
                    MessageBoxButtons buttons;
                    string msg;

                    if (configName != null && vsProject.Configs.Contains(configName))
                    {
                        msg = "The project being added may contain multiple configurations;\r\r" +
                            "Select\tYes to add all configurations found.\r" +
                            "\tNo to add only the " + configName + " configuration.\r" +
                            "\tCancel to exit without modifying the project.";
                        buttons = MessageBoxButtons.YesNoCancel;
                    }
                    else
                    {
                        msg = "The project being added may contain multiple configurations;\r\r" +
                            "Select\tOK to add all configurations found.\r" +
                            "\tCancel to exit without modifying the project.";
                        buttons = MessageBoxButtons.OKCancel;
                    }

                    DialogResult result = UserMessage.Ask(msg, buttons);
                    if (result == DialogResult.Yes || result == DialogResult.OK)
                    {
                        loader.TestProject.Add(vsProject);
                        return;
                    }
                    else if (result == DialogResult.No)
                    {
                        foreach (string assembly in vsProject.Configs[configName].Assemblies)
                            config.Assemblies.Add(assembly);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    UserMessage.DisplayFailure(ex.Message, "Invalid VS Project");
                }
        }

		public static void AddAssembly( Form owner )
		{
			AddAssembly( owner, null );
		}

		public static void AddAssembly( Form owner, string configName )
		{
			TestLoader loader = Services.TestLoader;
			ProjectConfig config = configName == null
				? loader.TestProject.ActiveConfig
				: loader.TestProject.Configs[configName];

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Add Assembly";
			dlg.InitialDirectory = config.BasePath;
            dlg.Filter = "Assemblies (*.dll,*.exe)|*.dll;*.exe";
			dlg.FilterIndex = 1;
			dlg.FileName = "";

            if (dlg.ShowDialog(owner) == DialogResult.OK)
                config.Assemblies.Add(dlg.FileName);
		}

		public static void AddVSProject( Form owner )
		{
			TestLoader loader = Services.TestLoader;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Add Visual Studio Project";

			dlg.Filter =
				"All Project Types (*.csproj,*.vjsproj,*.vbproj,*.vcproj)|*.csproj;*.vjsproj;*.vbproj;*.vcproj|" +
				"C# Projects (*.csproj)|*.csproj|" +
				"J# Projects (*.vjsproj)|*.vjsproj|" +
				"VB Projects (*.vbproj)|*.vbproj|" +
				"C++ Projects (*.vcproj)|*.vcproj|" +
				"All Files (*.*)|*.*";

			dlg.FilterIndex = 1;
			dlg.FileName = "";

			if ( dlg.ShowDialog( owner ) == DialogResult.OK ) 
			{
				try
				{
					VSProject vsProject = new VSProject( dlg.FileName );
					loader.TestProject.Add( vsProject );
				}
				catch( Exception ex )
				{
					UserMessage.DisplayFailure( ex.Message, "Invalid VS Project" );
				}
			}
        }

		private static bool CanWriteProjectFile( string path )
		{
			return !File.Exists( path ) || 
				( File.GetAttributes( path ) & FileAttributes.ReadOnly ) == 0;
		}

		public static void SaveProject( Form owner )
		{
			TestLoader loader = Services.TestLoader;

			if ( Path.IsPathRooted( loader.TestProject.ProjectPath ) &&
				 NUnitProject.IsNUnitProjectFile( loader.TestProject.ProjectPath ) &&
				 CanWriteProjectFile( loader.TestProject.ProjectPath ) )
				loader.TestProject.Save();
			else
				SaveProjectAs( owner );
		}

		public static void SaveProjectAs( Form owner )
		{
			TestLoader loader = Services.TestLoader;

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Title = "Save Test Project";
			dlg.Filter = "NUnit Test Project (*.nunit)|*.nunit|All Files (*.*)|*.*";
			string path = NUnitProject.ProjectPathFromFile( loader.TestProject.ProjectPath );
			if ( CanWriteProjectFile( path ) )
				dlg.FileName = path;
			dlg.DefaultExt = "nunit";
			dlg.ValidateNames = true;
			dlg.OverwritePrompt = true;

			while( dlg.ShowDialog( owner ) == DialogResult.OK )
			{
				if ( !CanWriteProjectFile( dlg.FileName ) )
					UserMessage.DisplayInfo( string.Format( "File {0} is write-protected. Select another file name.", dlg.FileName ) );
				else
				{
					loader.TestProject.Save( dlg.FileName );
					return;
				}
			}
		}

		public static void NewProject( Form owner )
		{
			TestLoader loader = Services.TestLoader;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Title = "New Test Project";
			dlg.Filter = "NUnit Test Project (*.nunit)|*.nunit|All Files (*.*)|*.*";
			dlg.FileName = Services.ProjectService.GenerateProjectName();
			dlg.DefaultExt = "nunit";
			dlg.ValidateNames = true;
			dlg.OverwritePrompt = true;

			if ( dlg.ShowDialog( owner ) == DialogResult.OK )
				loader.NewProject( dlg.FileName );
		}

		public static DialogResult CloseProject( Form owner )
		{
			DialogResult result = SaveProjectIfDirty( owner );

			if( result != DialogResult.Cancel )
				Services.TestLoader.UnloadProject();

			return result;
		}

		private static DialogResult SaveProjectIfDirty( Form owner )
		{
			DialogResult result = DialogResult.OK;
			NUnitProject project = Services.TestLoader.TestProject;

			if( project.IsDirty )
			{
				string msg = string.Format(
                    "Project {0} has been changed. Do you want to save changes?",project.Name);

				result = UserMessage.Ask( msg, MessageBoxButtons.YesNoCancel );
				if ( result == DialogResult.Yes )
					SaveProject( owner );
			}

			return result;
		}

		public static void SaveLastResult( Form owner )
		{
			//TODO: Save all results
			TestLoader loader = Services.TestLoader;
			
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Title = "Save Test Results as XML";
			dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
			dlg.FileName = "TestResult.xml";
			dlg.InitialDirectory = Path.GetDirectoryName( loader.TestFileName );
			dlg.DefaultExt = "xml";
			dlg.ValidateNames = true;
			dlg.OverwritePrompt = true;

			if ( dlg.ShowDialog( owner ) == DialogResult.OK )
			{
				try
				{
					string fileName = dlg.FileName;

					loader.SaveLastResult( fileName );

					string msg = String.Format( "Results saved as {0}", fileName );
					UserMessage.DisplayInfo( msg, "Save Results as XML" );
				}
				catch( Exception exception )
				{
					UserMessage.DisplayFailure( exception, "Unable to Save Results" );
				}
			}
		}
	}
}
