// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System.IO;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Tests.Assemblies;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for MultipleAssembliesDomain.
	/// </summary>
	[TestFixture]
	public class TestDomainTests_Multiple
	{
		private TestDomain domain; 
		private ITest loadedSuite;

		private static string path1 = NoNamespaceTestFixture.AssemblyPath;
		private static string path2 = MockAssembly.AssemblyPath;

		private string name = "Multiple Assemblies Test";

		[TestFixtureSetUp]
		public void Init()
		{
			domain = new TestDomain();
			TestPackage package = new TestPackage( name );
			package.Assemblies.Add( path1 );
			package.Assemblies.Add( path2 );
			domain.Load( package );
			loadedSuite = domain.Test;
		}

		[TestFixtureTearDown]
		public void UnloadTestDomain()
		{
			domain.Unload();
			domain = null;
		}
			
		[Test]
		public void BuildSuite()
		{
			Assert.IsNotNull(loadedSuite);
		}

		[Test]
		public void RootNode()
		{
			Assert.AreEqual( name, loadedSuite.TestName.Name );
		}

		[Test]
		public void AssemblyNodes()
		{
			TestNode test0 = (TestNode)loadedSuite.Tests[0];
			TestNode test1 = (TestNode)loadedSuite.Tests[1];
			Assert.AreEqual( path1, test0.TestName.Name );
			Assert.AreEqual( path2, test1.TestName.Name );
		}

		[Test]
		public void TestCaseCount()
		{
			Assert.AreEqual(NoNamespaceTestFixture.Tests + MockAssembly.Tests, 
				loadedSuite.TestCount );
		}

		[Test]
		public void RunMultipleAssemblies()
		{
			TestResult result = domain.Run(NullListener.NULL, TestFilter.Empty, false, LoggingThreshold.Off);
			ResultSummarizer summary = new ResultSummarizer(result);
			Assert.AreEqual(
				NoNamespaceTestFixture.Tests + MockAssembly.TestsRun, 
				summary.TestsRun);
		}
	}

	[TestFixture]
	public class TestDomainTests_MultipleFixture
	{
		[Test]
		public void LoadFixture()
		{
			TestDomain domain = new TestDomain();
			TestPackage package = new TestPackage( "Multiple Assemblies Test" );
			package.Assemblies.Add(NoNamespaceTestFixture.AssemblyPath);
			package.Assemblies.Add(MockAssembly.AssemblyPath);
            package.TestName = "NUnit.Tests.Assemblies.MockTestFixture";
            try
            {
                domain.Load(package);
                Assert.AreEqual(MockTestFixture.Tests, domain.Test.TestCount);
            }
            finally
            {
                domain.Unload();
            }
		}
	}
}
