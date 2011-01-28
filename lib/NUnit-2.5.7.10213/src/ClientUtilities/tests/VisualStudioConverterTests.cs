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

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class VisualStudioConverterTests
	{
		static readonly string resourceDir = "resources";

		private VisualStudioConverter converter;
		
		private void AssertCanLoadVsProject( string resourceName )
		{
			string fileName = Path.GetFileNameWithoutExtension( resourceName );
			using( TempResourceFile file = new TempResourceFile( this.GetType(), resourceDir + "." + resourceName, resourceName ) )
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
			using(new TempResourceFile(this.GetType(), "resources.csharp-sample.csproj", @"csharp\csharp-sample.csproj"))
			using(new TempResourceFile(this.GetType(), "resources.jsharp.vjsproj", @"jsharp\jsharp.vjsproj"))
			using(new TempResourceFile(this.GetType(), "resources.vb-sample.vbproj", @"vb\vb-sample.vbproj"))
			using(new TempResourceFile(this.GetType(), "resources.cpp-sample.vcproj", @"cpp-sample\cpp-sample.vcproj"))
			using(TempResourceFile file = new TempResourceFile(this.GetType(), "resources.samples.sln", "samples.sln" ))
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( 4, project.Configs.Count );
				Assert.AreEqual( 3, project.Configs["Debug"].Assemblies.Count );
				Assert.AreEqual( 3, project.Configs["Release"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Debug|Win32"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release|Win32"].Assemblies.Count );
				Assert.IsTrue( project.IsLoadable, "Not loadable" );
				Assert.IsFalse( project.IsDirty, "Project should not be dirty" );
			}
		}

		[Test]
		public void FromVSSolution2005()
		{
			using(new TempResourceFile(this.GetType(), "resources.csharp-sample_VS2005.csproj", @"csharp\csharp-sample_VS2005.csproj"))
			using(new TempResourceFile(this.GetType(), "resources.jsharp_VS2005.vjsproj", @"jsharp\jsharp_VS2005.vjsproj"))
			using(new TempResourceFile(this.GetType(), "resources.vb-sample_VS2005.vbproj", @"vb\vb-sample_VS2005.vbproj"))
			using(new TempResourceFile(this.GetType(), "resources.cpp-sample_VS2005.vcproj", @"cpp-sample\cpp-sample_VS2005.vcproj"))
			using(TempResourceFile file = new TempResourceFile(this.GetType(), "resources.samples_VS2005.sln", "samples_VS2005.sln"))
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( 4, project.Configs.Count );
				Assert.AreEqual( 3, project.Configs["Debug"].Assemblies.Count );
				Assert.AreEqual( 3, project.Configs["Release"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Debug|Win32"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release|Win32"].Assemblies.Count );
				Assert.IsTrue( project.IsLoadable, "Not loadable" );
				Assert.IsFalse( project.IsDirty, "Project should not be dirty" );
			}
		}

		[Test]
		public void FromWebApplication()
		{
			using( new TempResourceFile(this.GetType(), "resources.ClassLibrary1.csproj", @"ClassLibrary1\ClassLibrary1.csproj" ) )
			using( TempResourceFile file = new TempResourceFile( this.GetType(), "resources.WebApplication1.sln", "WebApplication1.sln" ) )
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
			using( new TempResourceFile( this.GetType(), "resources.ClassLibrary1.csproj", @"ClassLibrary1\ClassLibrary1.csproj" ) )
			using( new TempResourceFile( this.GetType(), "resources.Unmanaged.vcproj", @"Unmanaged\Unmanaged.vcproj" ) )
			using( TempResourceFile file = new TempResourceFile( this.GetType(), "resources.Solution1.sln", "Solution1.sln" ) ) 
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( 4, project.Configs.Count );
				Assert.AreEqual( 1, project.Configs["Debug"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Debug|Win32"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release|Win32"].Assemblies.Count );
			}
		}

		[Test]
		public void FromMakefileProject()
		{
			using( TempResourceFile file = new TempResourceFile( this.GetType(), "resources.MakeFileProject.vcproj", "MakeFileProject.vcproj" ) )
			{
				NUnitProject project = converter.ConvertFrom( file.Path );
				Assert.AreEqual( 2, project.Configs.Count );
				Assert.AreEqual( 1, project.Configs["Debug|Win32"].Assemblies.Count );
				Assert.AreEqual( 1, project.Configs["Release|Win32"].Assemblies.Count );
			}
		}
	}
}
