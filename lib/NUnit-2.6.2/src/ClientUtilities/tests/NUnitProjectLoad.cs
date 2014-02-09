// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System.IO;
using NUnit.Framework;
using NUnit.TestUtilities;

namespace NUnit.Util.Tests
{
	// TODO: Some of these tests are really tests of VSProject and should be moved there.

	[TestFixture]
	public class NUnitProjectLoad
	{
		static readonly string xmlfile = Path.Combine(Path.GetTempPath(), "test.nunit");
        static readonly string mockDll = NUnit.Tests.Assemblies.MockAssembly.AssemblyPath;

		private ProjectService projectService;
		private NUnitProject project;

		[SetUp]
		public void SetUp()
		{
			projectService = new ProjectService();
			project = projectService.EmptyProject();
		}

		[TearDown]
		public void TearDown()
		{
			if ( File.Exists( xmlfile ) )
				File.Delete( xmlfile );
		}

		// Write a string out to our xml file and then load project from it
		private void LoadProject( string source )
		{
			StreamWriter writer = new StreamWriter( xmlfile );
			writer.Write( source );
			writer.Close();

			project.ProjectPath = Path.GetFullPath( xmlfile );
			project.Load();
		}

		[Test]
		public void LoadEmptyProject()
		{
			LoadProject( NUnitProjectXml.EmptyProject );
			Assert.AreEqual( 0, project.Configs.Count );
		}

		[Test]
		public void LoadEmptyConfigs()
		{
			LoadProject( NUnitProjectXml.EmptyConfigs );
			Assert.AreEqual( 2, project.Configs.Count );
			Assert.IsTrue( project.Configs.Contains( "Debug") );
			Assert.IsTrue( project.Configs.Contains( "Release") );
		}

		[Test]
		public void LoadNormalProject()
		{
			LoadProject( NUnitProjectXml.NormalProject );
			Assert.AreEqual( 2, project.Configs.Count );

            string tempPath = Path.GetTempPath();

			ProjectConfig config1 = project.Configs["Debug"];
			Assert.AreEqual( 2, config1.Assemblies.Count );
			Assert.AreEqual( Path.Combine(tempPath, @"bin" + Path.DirectorySeparatorChar + "debug" + Path.DirectorySeparatorChar + "assembly1.dll" ), config1.Assemblies[0] );
			Assert.AreEqual( Path.Combine(tempPath, @"bin" + Path.DirectorySeparatorChar + "debug" + Path.DirectorySeparatorChar + "assembly2.dll" ), config1.Assemblies[1] );

			ProjectConfig config2 = project.Configs["Release"];
			Assert.AreEqual( 2, config2.Assemblies.Count );
			Assert.AreEqual( Path.Combine(tempPath, @"bin" + Path.DirectorySeparatorChar + "release" + Path.DirectorySeparatorChar + "assembly1.dll" ), config2.Assemblies[0] );
			Assert.AreEqual( Path.Combine(tempPath, @"bin" + Path.DirectorySeparatorChar + "release" + Path.DirectorySeparatorChar + "assembly2.dll" ), config2.Assemblies[1] );
		}

		[Test]
		public void LoadProjectWithManualBinPath()
		{
			LoadProject( NUnitProjectXml.ManualBinPathProject );
			Assert.AreEqual( 1, project.Configs.Count );
			ProjectConfig config1 = project.Configs["Debug"];
			Assert.AreEqual( "bin_path_value", config1.PrivateBinPath );
		}

		[Test]
		public void FromAssembly()
		{
			NUnitProject project = projectService.WrapAssembly(mockDll);
			Assert.AreEqual( "Default", project.ActiveConfigName );
			Assert.SamePath( mockDll, project.ActiveConfig.Assemblies[0] );
			Assert.IsTrue( project.IsLoadable, "Not loadable" );
			Assert.IsTrue( project.IsAssemblyWrapper, "Not wrapper" );
			Assert.IsFalse( project.IsDirty, "Not dirty" );
		}

		[Test]
		public void SaveClearsAssemblyWrapper()
		{
			NUnitProject project = projectService.WrapAssembly(mockDll);
			project.Save( xmlfile );
			Assert.IsFalse( project.IsAssemblyWrapper,
				"Changed project should no longer be wrapper");
		}
	}
}
