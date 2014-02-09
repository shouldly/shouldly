// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Xml;
using System.Text;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class NUnitProjectTests
	{
		static readonly string xmlfile = Path.Combine(Path.GetTempPath(), "test.nunit");
        static readonly string mockDll = NUnit.Tests.Assemblies.MockAssembly.AssemblyPath;

		private NUnitProject project;
		private ProjectService projectService;

		[SetUp]
		public void SetUp()
		{
			projectService = new ProjectService();
			project = projectService.EmptyProject();
		}

		[TearDown]
		public void EraseFile()
		{
			if ( File.Exists( xmlfile ) )
				File.Delete( xmlfile );
		}

		[Test]
		public void IsProjectFile()
		{
			Assert.IsTrue( NUnitProject.IsNUnitProjectFile( @"\x\y\test.nunit" ) );
			Assert.IsFalse( NUnitProject.IsNUnitProjectFile( @"\x\y\test.junit" ) );
		}

		[Test]
		public void NewProjectIsEmpty()
		{
			Assert.AreEqual( 0, project.Configs.Count );
			Assert.IsNull( project.ActiveConfig );
		}

		[Test]
		public void NewProjectIsNotDirty()
		{
			Assert.IsFalse( project.IsDirty );
		}

		[Test] 
		public void NewProjectDefaultPath()
		{
			Assert.AreEqual( Path.GetFullPath( "Project1" ), project.ProjectPath );
			Assert.AreEqual( "Project1", project.Name );
			NUnitProject another = projectService.EmptyProject();
			Assert.AreEqual( Path.GetFullPath( "Project2" ), another.ProjectPath );
		}

		[Test]
		public void NewProjectNotLoadable()
		{
			Assert.IsFalse( project.IsLoadable );
		}

		[Test]
		public void SaveMakesProjectNotDirty()
		{
			project.Save( xmlfile );
			Assert.IsFalse( project.IsDirty );
		}

		[Test]
		public void SaveSetsProjectPath()
		{
			project.Save( xmlfile );
			Assert.AreEqual( Path.GetFullPath( xmlfile ), project.ProjectPath );
			Assert.AreEqual( "test", project.Name );
		}

		[Test]
		public void DefaultApplicationBase()
		{
			project.Save( xmlfile );
			Assert.AreEqual( Path.GetDirectoryName( project.ProjectPath ), project.BasePath );
		}

		[Test]
		public void DefaultConfigurationFile()
		{
			Assert.AreEqual( "Project1.config", project.ConfigurationFile );
			project.Save( xmlfile );
			Assert.AreEqual( "test.config", project.ConfigurationFile );
		}

		[Test]
		public void ConfigurationFileFromAssembly() 
		{
			NUnitProject project = projectService.WrapAssembly(mockDll);
			string config = Path.GetFileName( project.ConfigurationFile );
			Assert.That(config, Is.EqualTo("mock-assembly.dll.config").IgnoreCase);
		}

		[Test]
		public void ConfigurationFileFromAssemblies() 
		{
			NUnitProject project = projectService.WrapAssemblies(new string[] {mockDll});
			string config = Path.GetFileName( project.ConfigurationFile );
			Assert.That(config, Is.EqualTo("mock-assembly.dll.config").IgnoreCase);
		}

		[Test]
		public void DefaultProjectName()
		{
			project.Save( xmlfile );
			Assert.AreEqual( "test", project.Name );
		}

		[Test]
		public void LoadMakesProjectNotDirty()
		{
			project.Save( xmlfile );
			NUnitProject project2 = new ProjectService().LoadProject( xmlfile );
			Assert.IsFalse( project2.IsDirty );
		}

		[Test]
		public void CanSetAppBase()
		{
			project.BasePath = "..";
			Assert.AreEqual( Path.GetDirectoryName( Environment.CurrentDirectory ), project.BasePath  );
		}

		[Test]
		public void CanAddConfigs()
		{
			project.Configs.Add("Debug");
			project.Configs.Add("Release");
			Assert.AreEqual( 2, project.Configs.Count );
		}

		[Test]
		public void CanSetActiveConfig()
		{
			project.Configs.Add("Debug");
			project.Configs.Add("Release");
			project.SetActiveConfig( "Release" );
			Assert.AreEqual( "Release", project.ActiveConfig.Name );
		}

		[Test]
		public void CanAddAssemblies()
		{
			project.Configs.Add("Debug");
			project.Configs.Add("Release");

			project.Configs["Debug"].Assemblies.Add( Path.GetFullPath( @"bin\debug\assembly1.dll" ) );
			project.Configs["Debug"].Assemblies.Add( Path.GetFullPath( @"bin\debug\assembly2.dll" ) );
			project.Configs["Release"].Assemblies.Add( Path.GetFullPath( @"bin\debug\assembly3.dll" ) );

			Assert.AreEqual( 2, project.Configs.Count );
			Assert.AreEqual( 2, project.Configs["Debug"].Assemblies.Count );
			Assert.AreEqual( 1, project.Configs["Release"].Assemblies.Count );
		}

		[Test]
		public void AddConfigMakesProjectDirty()
		{
			project.Configs.Add("Debug");
			Assert.IsTrue( project.IsDirty );
		}

		[Test]
		public void RenameConfigMakesProjectDirty()
		{
			project.Configs.Add("Old");
			project.IsDirty = false;
			project.Configs[0].Name = "New";
			Assert.IsTrue( project.IsDirty );
		}

		[Test]
		public void DefaultActiveConfig()
		{
			project.Configs.Add("Debug");
			Assert.AreEqual( "Debug", project.ActiveConfig.Name );
		}

		[Test]
		public void RenameActiveConfig()
		{
			project.Configs.Add( "Old" );
			project.SetActiveConfig( "Old" );
			project.Configs[0].Name = "New";
			Assert.AreEqual( "New", project.ActiveConfig.Name );
		}

		[Test]
		public void RemoveConfigMakesProjectDirty()
		{
			project.Configs.Add("Debug");
			project.IsDirty = false;
			project.Configs.Remove("Debug");
			Assert.IsTrue( project.IsDirty );
		}

		[Test]
		public void RemoveActiveConfig()
		{
			project.Configs.Add("Debug");
			project.Configs.Add("Release");
			project.SetActiveConfig("Debug");
			project.Configs.Remove("Debug");
			Assert.AreEqual( "Release", project.ActiveConfig.Name );
		}

		[Test]
		public void SettingActiveConfigMakesProjectDirty()
		{
			project.Configs.Add("Debug");
			project.Configs.Add("Release");
			project.SetActiveConfig( "Debug" );
			project.IsDirty = false;
			project.SetActiveConfig( "Release" );
			Assert.IsTrue( project.IsDirty );
		}

		[Test]
		public void SaveAndLoadEmptyProject()
		{
			project.Save( xmlfile );
			Assert.IsTrue( File.Exists( xmlfile ) );

			NUnitProject project2 = projectService.LoadProject( xmlfile );

			Assert.AreEqual( 0, project2.Configs.Count );
		}

		[Test]
		public void SaveAndLoadEmptyConfigs()
		{
			project.Configs.Add( "Debug" );
			project.Configs.Add( "Release" );
			project.Save( xmlfile );

			Assert.IsTrue( File.Exists( xmlfile ) );

			NUnitProject project2 = projectService.LoadProject( xmlfile );

			Assert.AreEqual( 2, project2.Configs.Count );
			Assert.IsTrue( project2.Configs.Contains( "Debug" ) );
			Assert.IsTrue( project2.Configs.Contains( "Release" ) );
		}

		[Test]
		public void SaveAndLoadConfigsWithAssemblies()
		{
            string tempPath = Path.GetTempPath();

			ProjectConfig config1 = new ProjectConfig( "Debug" );
            config1.Assemblies.Add(Path.Combine(tempPath, @"bin\debug\assembly1.dll"));
            config1.Assemblies.Add(Path.Combine(tempPath, @"bin\debug\assembly2.dll"));

			ProjectConfig config2 = new ProjectConfig( "Release" );
            config2.Assemblies.Add(Path.Combine(tempPath, @"bin\release\assembly1.dll"));
            config2.Assemblies.Add(Path.Combine(tempPath, @"bin\release\assembly2.dll"));

			project.Configs.Add( config1 );
			project.Configs.Add( config2 );
			project.Save( xmlfile );

			Assert.IsTrue( File.Exists( xmlfile ) );

			NUnitProject project2 = projectService.LoadProject( xmlfile );

			Assert.AreEqual( 2, project2.Configs.Count );

			config1 = project2.Configs["Debug"];
			Assert.AreEqual( 2, config1.Assemblies.Count );
            Assert.AreEqual(Path.Combine(tempPath, @"bin\debug\assembly1.dll"), config1.Assemblies[0]);
            Assert.AreEqual(Path.Combine(tempPath, @"bin\debug\assembly2.dll"), config1.Assemblies[1]);

			config2 = project2.Configs["Release"];
			Assert.AreEqual( 2, config2.Assemblies.Count );
            Assert.AreEqual(Path.Combine(tempPath, @"bin\release\assembly1.dll"), config2.Assemblies[0]);
            Assert.AreEqual(Path.Combine(tempPath, @"bin\release\assembly2.dll"), config2.Assemblies[1]);
		}
	}
}
