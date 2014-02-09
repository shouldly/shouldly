// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.Util.ProjectConverters;
using NUnit.Util.Tests.resources;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class VisualStudioConverterTests
	{
		private VisualStudioConverter converter;
        static readonly bool useSolutionConfigs = 
            Services.UserSettings.GetSetting("Options.TestLoader.VisualStudio.UseSolutionConfigs", true);
		
		private void AssertCanLoadVsProject( string resourceName )
		{
			string fileName = Path.GetFileNameWithoutExtension( resourceName );

			using( TestResource file = new TestResource(resourceName) ) 
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( fileName, project.Name );
				Assert.AreEqual( project.Configs[0].Name, project.ActiveConfigName );
				Assert.AreEqual( fileName.ToLower(), Path.GetFileNameWithoutExtension( project.Configs[0].Assemblies[0].ToLower() ) );
				Assert.IsTrue( project.IsLoadable, "Not loadable" );
				Assert.IsFalse( project.IsDirty, "Project should not be dirty" );
			}
		}

		[SetUp]
		public void CreateImporter()
		{
			converter = new VisualStudioConverter();
		}

		[Test]
		public void FromCSharpProject()
		{
			AssertCanLoadVsProject( "csharp-sample.csproj" );
		}

		[Test]
		public void FromVBProject()
		{
			AssertCanLoadVsProject( "vb-sample.vbproj" );
		}

		[Test]
		public void FromJsharpProject()
		{
			AssertCanLoadVsProject( "jsharp.vjsproj" );
		}

		[Test]
		public void FromCppProject()
		{
			AssertCanLoadVsProject( "cpp-sample.vcproj" );
		}

		[Test]
		public void FromProjectWithHebrewFileIncluded()
		{
			AssertCanLoadVsProject( "HebrewFileProblem.csproj" );
		}

		[Test]
		public void FromVSSolution2003()
		{
			using(new TestResource("csharp-sample.csproj", @"csharp\csharp-sample.csproj"))
			using(new TestResource("jsharp.vjsproj", @"jsharp\jsharp.vjsproj"))
			using(new TestResource("vb-sample.vbproj", @"vb\vb-sample.vbproj"))
			using(new TestResource("cpp-sample.vcproj", @"cpp-sample\cpp-sample.vcproj"))
			using(TestResource file = new TestResource("samples.sln"))
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
                if (useSolutionConfigs)
                {
                    Assert.AreEqual(2, project.Configs.Count);
                    Assert.AreEqual(4, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(4, project.Configs["Release"].Assemblies.Count);
                }
                else
                {
                    Assert.AreEqual(4, project.Configs.Count);
                    Assert.AreEqual(3, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(3, project.Configs["Release"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Debug|Win32"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Release|Win32"].Assemblies.Count);
                }
				Assert.IsTrue( project.IsLoadable, "Not loadable" );
				Assert.IsFalse( project.IsDirty, "Project should not be dirty" );
			}
		}

		[Test]
		public void FromVSSolution2005()
		{
            using (new TestResource("csharp-sample_VS2005.csproj", @"csharp\csharp-sample_VS2005.csproj"))
            using (new TestResource("jsharp_VS2005.vjsproj", @"jsharp\jsharp_VS2005.vjsproj"))
            using (new TestResource("vb-sample_VS2005.vbproj", @"vb\vb-sample_VS2005.vbproj"))
            using (new TestResource("cpp-sample_VS2005.vcproj", @"cpp-sample\cpp-sample_VS2005.vcproj"))
            using (TestResource file = new TestResource("samples_VS2005.sln"))
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
                if (useSolutionConfigs)
                {
                    Assert.AreEqual(2, project.Configs.Count);
                    Assert.AreEqual(4, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(4, project.Configs["Release"].Assemblies.Count);
                }
                else
                {
                    Assert.AreEqual(4, project.Configs.Count);
                    Assert.AreEqual(3, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(3, project.Configs["Release"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Debug|Win32"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Release|Win32"].Assemblies.Count);
                }
				Assert.IsTrue( project.IsLoadable, "Not loadable" );
				Assert.IsFalse( project.IsDirty, "Project should not be dirty" );
			}
		}

		[Test]
		public void FromWebApplication()
		{
            using (new TestResource("ClassLibrary1.csproj", @"ClassLibrary1\ClassLibrary1.csproj"))
            using (TestResource file = new TestResource("WebApplication1.sln"))
			{
				NUnitProject project = converter.ConvertFrom( Path.GetFullPath( file.Path ) );
				Assert.AreEqual( 2, project.Configs.Count );
				Assert.AreEqual( 1, project.Configs["Debug"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release"].Assemblies.Count );
			}
		}

		[Test]
		public void WithUnmanagedCpp()
		{
            using (new TestResource("ClassLibrary1.csproj", @"ClassLibrary1\ClassLibrary1.csproj"))
            using (new TestResource("Unmanaged.vcproj", @"Unmanaged\Unmanaged.vcproj"))
            using (TestResource file = new TestResource("Solution1.sln")) 
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
                if (useSolutionConfigs)
                {
                    Assert.AreEqual(2, project.Configs.Count);
                    Assert.AreEqual(2, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(2, project.Configs["Release"].Assemblies.Count);
                }
                else
                {
                    Assert.AreEqual(4, project.Configs.Count);
                    Assert.AreEqual(1, project.Configs["Debug"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Release"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Debug|Win32"].Assemblies.Count);
                    Assert.AreEqual(1, project.Configs["Release|Win32"].Assemblies.Count);
                }
			}
		}

		[Test]
		public void FromMakefileProject()
		{
            using (TestResource file = new TestResource("MakeFileProject.vcproj"))
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( 2, project.Configs.Count );
                Assert.AreEqual(1, project.Configs["Debug|Win32"].Assemblies.Count);
                Assert.AreEqual(1, project.Configs["Release|Win32"].Assemblies.Count);
            }
		}

        [Test]
        public void FromSolutionWithDisabledProject()
        {
            using (new TestResource("DisabledProject.csproj", @"DisabledProject\DisabledProject.csproj"))
            using (new TestResource("DebugOnly.csproj", @"DebugOnly\DebugOnly.csproj"))
            using (TestResource file = new TestResource("DisabledProject.sln"))
            {
                NUnitProject project = converter.ConvertFrom(file.Path);
                Assert.AreEqual(2, project.Configs.Count);
                Assert.AreEqual(2, project.Configs["Release"].Assemblies.Count, "Release should have 2 assemblies");
                if (useSolutionConfigs)
                    Assert.AreEqual(1, project.Configs["Debug"].Assemblies.Count, "Debug should have 1 assembly");
                else
                    Assert.AreEqual(2, project.Configs["Debug"].Assemblies.Count, "Debug should have 2 assemblies");
            }
        }
	}
}
