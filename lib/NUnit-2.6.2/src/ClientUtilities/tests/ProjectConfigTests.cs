// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Collections;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for ProjectConfigTests.
	/// </summary>
	[TestFixture]
	public class ProjectConfigTests
	{
		private ProjectConfig activeConfig;
        private ProjectConfig inactiveConfig;
		private NUnitProject project;

		[SetUp]
		public void SetUp()
		{
			activeConfig = new ProjectConfig( "Debug" );
            inactiveConfig = new ProjectConfig("Release");
			project = new NUnitProject( TestPath( "/test/myproject.nunit" ) );
			project.Configs.Add( activeConfig );
            project.Configs.Add(inactiveConfig);
            project.IsDirty = false;
            project.HasChangesRequiringReload = false;
		}

        /// <summary>
        /// Take a valid Linux filePath and make a valid windows filePath out of it
        /// if we are on Windows. Change slashes to backslashes and, if the
        /// filePath starts with a slash, add C: in front of it.
        /// </summary>
        private string TestPath(string path)
        {
            if (Path.DirectorySeparatorChar != '/')
            {
                path = path.Replace('/', Path.DirectorySeparatorChar);
                if (path[0] == Path.DirectorySeparatorChar)
                    path = "C:" + path;
            }

            return path;
        }

		[Test]
		public void EmptyConfig()
		{
			Assert.AreEqual( "Debug", activeConfig.Name );
			Assert.AreEqual( 0, activeConfig.Assemblies.Count );
		}

		[Test]
		public void CanAddAssemblies()
		{
            string path1 = TestPath("/test/assembly1.dll");
            string path2 = TestPath("/test/assembly2.dll");
            activeConfig.Assemblies.Add(path1);
			activeConfig.Assemblies.Add( path2 );
			Assert.AreEqual( 2, activeConfig.Assemblies.Count );
			Assert.AreEqual( path1, activeConfig.Assemblies[0] );
			Assert.AreEqual( path2, activeConfig.Assemblies[1] );
		}

		[Test]
		public void ToArray()
		{
            string path1 = TestPath("/test/assembly1.dll");
            string path2 = TestPath("/test/assembly2.dll");
            activeConfig.Assemblies.Add( path1 );
			activeConfig.Assemblies.Add( path2 );

			string[] files = activeConfig.Assemblies.ToArray();
			Assert.AreEqual( path1, files[0] );
			Assert.AreEqual( path2, files[1] );
		}

        [Test]
        public void AddToActiveConfigMarksProjectDirty()
        {
            activeConfig.Assemblies.Add(TestPath("/test/bin/debug/assembly1.dll"));
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void AddToActiveConfigRequiresReload()
        {
            activeConfig.Assemblies.Add(TestPath("/test/bin/debug/assembly1.dll"));
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void AddToInactiveConfigMarksProjectDirty()
        {
            inactiveConfig.Assemblies.Add(TestPath("/test/bin/release/assembly1.dll"));
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void AddToInactiveConfigDoesNotRequireReload()
        {
            inactiveConfig.Assemblies.Add(TestPath("/test/bin/release/assembly1.dll"));
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
        public void AddingConfigMarksProjectDirty()
        {
            project.Configs.Add("New");
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void AddingInitialConfigRequiresReload()
        {
            NUnitProject newProj = new NUnitProject("/junk");
            newProj.HasChangesRequiringReload = false;
            newProj.Configs.Add("New");
            Assert.That(newProj.HasChangesRequiringReload);
        }

        [Test]
        public void AddingSubsequentConfigDoesNotRequireReload()
        {
            project.Configs.Add("New");
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
        public void RenameActiveConfigMarksProjectDirty()
        {
            activeConfig.Name = "Renamed";
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void RenameActiveConfigRequiresReload()
        {
            activeConfig.Name = "Renamed";
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void RenameInctiveConfigMarksProjectDirty()
        {
            inactiveConfig.Name = "Renamed";
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void RenameInactiveConfigDoesNotRequireReload()
        {
            inactiveConfig.Name = "Renamed";
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
        public void RemoveActiveConfigMarksProjectDirty()
        {
            string path1 = TestPath("/test/bin/debug/assembly1.dll");
            activeConfig.Assemblies.Add(path1);
            project.IsDirty = false;
            activeConfig.Assemblies.Remove(path1);
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void RemoveActiveConfigRequiresReload()
        {
            string path1 = TestPath("/test/bin/debug/assembly1.dll");
            activeConfig.Assemblies.Add(path1);
            project.IsDirty = false;
            activeConfig.Assemblies.Remove(path1);
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void RemoveInactiveConfigMarksProjectDirty()
        {
            string path1 = TestPath("/test/bin/debug/assembly1.dll");
            inactiveConfig.Assemblies.Add(path1);
            project.IsDirty = false;
            inactiveConfig.Assemblies.Remove(path1);
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void RemoveInactiveConfigDoesNotRequireReload()
        {
            string path1 = TestPath("/test/bin/debug/assembly1.dll");
            inactiveConfig.Assemblies.Add(path1);
            project.HasChangesRequiringReload = false;
            inactiveConfig.Assemblies.Remove(path1);
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingActiveConfigApplicationBaseMarksProjectDirty()
        {
            activeConfig.BasePath = TestPath("/junk");
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingActiveConfigApplicationBaseRequiresReload()
        {
            activeConfig.BasePath = TestPath("/junk");
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingInactiveConfigApplicationBaseMarksProjectDirty()
        {
            inactiveConfig.BasePath = TestPath("/junk");
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingInactiveConfigApplicationBaseDoesNotRequireReload()
        {
            inactiveConfig.BasePath = TestPath("/junk");
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
		public void AbsoluteBasePath()
		{
            activeConfig.BasePath = TestPath("/junk");
            string path1 = TestPath( "/junk/bin/debug/assembly1.dll" );
			activeConfig.Assemblies.Add( path1 );
			Assert.AreEqual( path1, activeConfig.Assemblies[0] );
		}

		[Test]
		public void RelativeBasePath()
		{
			activeConfig.BasePath = @"junk";
            string path1 = TestPath("/test/junk/bin/debug/assembly1.dll");
            activeConfig.Assemblies.Add( path1 );
			Assert.AreEqual( path1, activeConfig.Assemblies[0] );
		}

		[Test]
		public void NoBasePathSet()
		{
            string path1 = TestPath( "/test/bin/debug/assembly1.dll" );
			activeConfig.Assemblies.Add( path1 );
			Assert.AreEqual( path1, activeConfig.Assemblies[0] );
		}

        [Test]
        public void SettingActiveConfigConfigurationFileMarksProjectDirty()
        {
            activeConfig.ConfigurationFile = "MyProject.config";
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingActiveConfigConfigurationFileRequiresReload()
        {
            activeConfig.ConfigurationFile = "MyProject.config";
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingInactiveConfigConfigurationFileMarksProjectDirty()
        {
            inactiveConfig.ConfigurationFile = "MyProject.config";
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingInactiveConfigConfigurationFileDoesNotRequireReload()
        {
            inactiveConfig.ConfigurationFile = "MyProject.config";
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
		public void DefaultConfigurationFile()
		{
			Assert.AreEqual( "myproject.config", activeConfig.ConfigurationFile );
			Assert.AreEqual( TestPath( "/test/myproject.config" ), activeConfig.ConfigurationFilePath );
		}

		[Test]
		public void AbsoluteConfigurationFile()
		{
            string path1 = TestPath("/configs/myconfig.config");
			activeConfig.ConfigurationFile = path1;
			Assert.AreEqual( path1, activeConfig.ConfigurationFilePath );
		}

		[Test]
		public void RelativeConfigurationFile()
		{
			activeConfig.ConfigurationFile = "myconfig.config";
			Assert.AreEqual( TestPath( "/test/myconfig.config" ), activeConfig.ConfigurationFilePath );
		}

        [Test]
        public void SettingActiveConfigPrivateBinPathMarksProjectDirty()
        {
            activeConfig.PrivateBinPath = TestPath("/junk") + Path.PathSeparator + TestPath("/bin");
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingActiveConfigPrivateBinPathRequiresReload()
        {
            activeConfig.PrivateBinPath = TestPath("/junk") + Path.PathSeparator + TestPath("/bin");
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingInactiveConfigPrivateBinPathMarksProjectDirty()
        {
            inactiveConfig.PrivateBinPath = TestPath("/junk") + Path.PathSeparator + TestPath("/bin");
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingInactiveConfigPrivateBinPathDoesNotRequireReload()
        {
            inactiveConfig.PrivateBinPath = TestPath("/junk") + Path.PathSeparator + TestPath("/bin");
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingActiveConfigBinPathTypeMarksProjectDirty()
        {
            activeConfig.BinPathType = BinPathType.Manual;
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingActiveConfigBinPathTypeRequiresReload()
        {
            activeConfig.BinPathType = BinPathType.Manual;
            Assert.IsTrue(project.HasChangesRequiringReload);
        }

        [Test]
        public void SettingInactiveConfigBinPathTypeMarksProjectDirty()
        {
            inactiveConfig.BinPathType = BinPathType.Manual;
            Assert.IsTrue(project.IsDirty);
        }

        [Test]
        public void SettingInactiveConfigBinPathTypeDoesNotRequireReload()
        {
            inactiveConfig.BinPathType = BinPathType.Manual;
            Assert.IsFalse(project.HasChangesRequiringReload);
        }

		[Test]
		public void NoPrivateBinPath()
		{
			activeConfig.Assemblies.Add( TestPath( "/bin/assembly1.dll" ) );
			activeConfig.Assemblies.Add( TestPath( "/bin/assembly2.dll" ) );
			activeConfig.BinPathType = BinPathType.None;
			Assert.IsNull( activeConfig.PrivateBinPath );
		}

		[Test]
		public void ManualPrivateBinPath()
		{
			activeConfig.Assemblies.Add( TestPath( "/test/bin/assembly1.dll" ) );
			activeConfig.Assemblies.Add( TestPath( "/test/bin/assembly2.dll" ) );
			activeConfig.BinPathType = BinPathType.Manual;
			activeConfig.PrivateBinPath = TestPath( "/test" );
			Assert.AreEqual( TestPath( "/test" ), activeConfig.PrivateBinPath );
		}

// TODO: Move to DomainManagerTests
//		[Test]
//		public void AutoPrivateBinPath()
//		{
//			config.Assemblies.Add( TestPath( "/test/bin/assembly1.dll" ) );
//			config.Assemblies.Add( TestPath( "/test/bin/assembly2.dll" ) );
//			config.BinPathType = BinPathType.Auto;
//			Assert.AreEqual( "bin", config.PrivateBinPath );
//		}
	}
}
