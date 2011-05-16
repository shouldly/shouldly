// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class RemoteTestResultTest
	{
        private TestDomain domain;

        [SetUp]
        public void CreateRunner()
        {
            domain = new TestDomain();
        }

        [TearDown]
        public void UnloadRunner()
        {
            if ( domain != null )
                domain.Unload();
        }

		[Test]
		public void ResultStillValidAfterDomainUnload() 
		{
			TestPackage package = new TestPackage( "mock-assembly.dll" );
			Assert.IsTrue( domain.Load( package ) );
			TestResult result = domain.Run( new NullListener() );
			TestResult caseResult = findCaseResult(result);
			Assert.IsNotNull(caseResult);
			TestResultItem item = new TestResultItem(caseResult);
			string message = item.GetMessage();
			Assert.IsNotNull(message);
		}

        [Test, Explicit("Fails intermittently")]
        public void AppDomainUnloadedBug()
        {
            TestDomain domain = new TestDomain();
            domain.Load( new TestPackage( "mock-assembly.dll" ) );
            domain.Run(new NullListener());
            domain.Unload();
        }

		private TestResult findCaseResult(TestResult suite) 
		{
			foreach (TestResult r in suite.Results) 
			{
				if (!r.Test.IsSuite)
				{
					return r;
				}
				else 
				{
					TestResult result = findCaseResult(r);
					if (result != null)
						return result;
				}

			}

			return null;
		}
	}
}
