// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Collections;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.Util.Tests.resources;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class VSProjectTests
	{
		private string invalidFile = Path.Combine(Path.GetTempPath(), "invalid.csproj");

		private void WriteInvalidFile( string text )
		{
			StreamWriter writer = new StreamWriter( invalidFile );
			writer.WriteLine( text );
			writer.Close();
		}

		[TearDown]
		public void EraseInvalidFile()
		{
			if ( File.Exists( invalidFile ) )
				File.Delete( invalidFile );
		}

		[Test]
		public void SolutionExtension()
		{
			Assert.IsTrue( VSProject.IsSolutionFile( TestPath( @"/x/y/project.sln" ) ) );
			Assert.IsFalse( VSProject.IsSolutionFile( TestPath( @"/x/y/project.sol" ) ) );
		}

		[Test]
		public void ProjectExtensions()
		{
			Assert.IsTrue( VSProject.IsProjectFile( TestPath( @"/x/y/project.csproj" ) ) );
			Assert.IsTrue( VSProject.IsProjectFile( TestPath( @"/x/y/project.vbproj" ) ) );
			Assert.IsTrue( VSProject.IsProjectFile( TestPath( @"/x/y/project.vcproj" ) ) );
			Assert.IsFalse( VSProject.IsProjectFile( TestPath( @"/x/y/project.xyproj" ) ) );
		}

		[Test]
		public void NotWebProject()
		{
			Assert.IsFalse(VSProject.IsProjectFile( @"http://localhost/web.csproj") );
			Assert.IsFalse(VSProject.IsProjectFile( @"\MyProject\http://localhost/web.csproj") );
		}

		private void AssertCanLoadProject( string resourceName )
		{
			string fileName = Path.GetFileNameWithoutExtension( resourceName );

            using (TestResource file = new TestResource(resourceName))
			{
				VSProject project = new VSProject( file.Path );
				Assert.AreEqual( fileName, project.Name );
				Assert.AreEqual( Path.GetFullPath( file.Path ), project.ProjectPath );
				Assert.AreEqual( fileName.ToLower(), Path.GetFileNameWithoutExtension( project.Configs[0].Assemblies[0].ToString().ToLower() ) );
			}
		}

		[Test]
		public void LoadCsharpProject()
		{
			AssertCanLoadProject( "csharp-sample.csproj" );
		}

		[Test]
		public void LoadCsharpProjectVS2005()
		{
			AssertCanLoadProject( "csharp-sample_VS2005.csproj" );
		}

		[Test]
		public void LoadVbProject()
		{
			AssertCanLoadProject( "vb-sample.vbproj" );
		}


		[Test]
		public void LoadVbProjectVS2005()
		{
			AssertCanLoadProject( "vb-sample_VS2005.vbproj" );
		}

		[Test]
		public void LoadJsharpProject()
		{
			AssertCanLoadProject( "jsharp.vjsproj" );
		}

		[Test]
		public void LoadJsharpProjectVS2005()
		{
			AssertCanLoadProject( "jsharp_VS2005.vjsproj" );
		}

		[Test]
		public void LoadCppProject()
		{
			AssertCanLoadProject( "cpp-sample.vcproj" );
		}

		[Test]
		public void LoadCppProjectVS2005()
		{
			AssertCanLoadProject( "cpp-sample_VS2005.vcproj" );
		}

		[Test]
		public void LoadProjectWithHebrewFileIncluded()
		{
			AssertCanLoadProject( "HebrewFileProblem.csproj" );
		}

        [Test]
        public void LoadProjectWithMissingOutputPath()
        {
            AssertCanLoadProject("MissingOutputPath.csproj");
        }

		[Test]
		public void LoadCppProjectWithMacros()
		{
            using (TestResource file = new TestResource("CPPLibrary.vcproj"))
			{
				VSProject project = new VSProject(file.Path);
				Assert.AreEqual( "CPPLibrary", project.Name );
				Assert.AreEqual( Path.GetFullPath(file.Path), project.ProjectPath);
				Assert.AreEqual( 
                    Path.Combine(Path.GetTempPath(), @"debug\cpplibrary.dll" ).ToLower(), 
					project.Configs["Debug|Win32"].Assemblies[0].ToString().ToLower());
				Assert.AreEqual( 
                    Path.Combine(Path.GetTempPath(), @"release\cpplibrary.dll" ).ToLower(), 
					project.Configs["Release|Win32"].Assemblies[0].ToString().ToLower());
			}
		}

        [Test]
        public void GenerateCorrectExtensionsFromVCProjectVS2005()     
		{
            using (TestResource file = new TestResource("cpp-default-library_VS2005.vcproj"))           
			{
                VSProject project = new VSProject(file.Path);
                Assert.AreEqual("cpp-default-library_VS2005", project.Name);
                Assert.AreEqual(Path.GetFullPath(file.Path), project.ProjectPath);
                Assert.AreEqual(
                    Path.Combine(Path.GetTempPath(), TestPath( @"debug/cpp-default-library_VS2005.dll" ) ).ToLower(),
                    project.Configs["Debug|Win32"].Assemblies[0].ToString().ToLower());
                Assert.AreEqual(
                    Path.Combine(Path.GetTempPath(), TestPath( @"release/cpp-default-library_VS2005.dll" ) ).ToLower(),
                    project.Configs["Release|Win32"].Assemblies[0].ToString().ToLower());
            }
        }

		[Test, ExpectedException( typeof ( ArgumentException ) ) ]
		public void LoadInvalidFileType()
		{
			new VSProject( @"/test.junk" );
		}

		[Test, ExpectedException( typeof ( FileNotFoundException ) ) ]
		public void FileNotFoundError()
		{
			new VSProject( @"/junk.csproj" );
		}

		[Test, ExpectedException( typeof( ArgumentException ) )]
		public void InvalidXmlFormat()
		{
			WriteInvalidFile( "<VisualStudioProject><junk></VisualStudioProject>" );
			new VSProject( Path.Combine(Path.GetTempPath(), "invalid.csproj" ));
		}

		[Test, ExpectedException( typeof( ArgumentException ) )]
		public void InvalidProjectFormat()
		{
			WriteInvalidFile( "<VisualStudioProject><junk></junk></VisualStudioProject>" );
			new VSProject( Path.Combine(Path.GetTempPath(), "invalid.csproj" ));
		}

		[Test, ExpectedException( typeof( ArgumentException ) )]
		public void MissingAttributes()
		{
			WriteInvalidFile( "<VisualStudioProject><CSharp><Build><Settings></Settings></Build></CSharp></VisualStudioProject>" );
			new VSProject( Path.Combine(Path.GetTempPath(), "invalid.csproj" ));
		}

		[Test]
		public void NoConfigurations()
		{
			WriteInvalidFile( "<VisualStudioProject><CSharp><Build><Settings AssemblyName=\"invalid\" OutputType=\"Library\"></Settings></Build></CSharp></VisualStudioProject>" );
			VSProject project = new VSProject( Path.Combine(Path.GetTempPath(),"invalid.csproj" ));
			Assert.AreEqual( 0, project.Configs.Count );
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
	}
}
