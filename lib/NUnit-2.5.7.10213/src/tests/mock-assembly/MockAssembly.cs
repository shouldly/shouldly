// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************
using System;
using NUnit.Framework;
using NUnit.Core;

namespace NUnit.Tests
{
	namespace Assemblies
	{
		/// <summary>
		/// Constant definitions for the mock-assembly dll.
		/// </summary>
		public class MockAssembly
		{
			public static int Fixtures = 6; 
			public static int NamespaceSuites = 6; // assembly, NUnit, Tests, Assemblies, Singletons, TestAssembly
			public static int Suites = Fixtures + NamespaceSuites;
			public static int ExplicitFixtures = 1;
			public static int SuitesRun = Suites - ExplicitFixtures;

			public static int Tests = MockTestFixture.Tests 
						+ Singletons.OneTestCase.Tests 
						+ TestAssembly.MockTestFixture.Tests 
						+ IgnoredFixture.Tests
						+ ExplicitFixture.Tests
						+ BadFixture.Tests;
			public static int Ignored = MockTestFixture.Ignored + IgnoredFixture.Tests;
			public static int Explicit = MockTestFixture.Explicit + ExplicitFixture.Tests;
			public static int NotRunnable = MockTestFixture.NotRunnable + BadFixture.Tests;
			public static int NotRun = Ignored + Explicit + NotRunnable;
		    public static int TestsRun = Tests - NotRun;
			public static int ResultCount = Tests - Explicit;

            public static int Errors = MockTestFixture.Errors;
            public static int Failures = MockTestFixture.Failures;
			public static int ErrorsAndFailures = Errors + Failures;

            public static int Nodes = MockTestFixture.Nodes 
						+ Singletons.OneTestCase.Nodes
						+ TestAssembly.MockTestFixture.Nodes 
						+ IgnoredFixture.Nodes
						+ ExplicitFixture.Nodes
						+ BadFixture.Nodes
						+ 6;  // assembly, NUnit, Tests, Assemblies, Singletons, TestAssembly 
			public static int Categories = MockTestFixture.Categories;
		}

		public class MockSuite
		{
			[Suite]
			public static TestSuite Suite
			{
				get
				{
					return new TestSuite( "MockSuite" );
				}
			}
		}

		[TestFixture(Description="Fake Test Fixture")]
		[Category("FixtureCategory")]
		public class MockTestFixture
		{
			public static readonly int Tests = 11;

			public static readonly int Ignored = 1;
			public static readonly int Explicit = 1;
			public static readonly int NotRunnable = 2;
			public static readonly int NotRun = Ignored + Explicit + NotRunnable;
		    public static readonly int TestsRun = Tests - NotRun;
			public static readonly int ResultCount = Tests - Explicit;

            public static readonly int Failures = 1;
            public static readonly int Errors = 1;
			public static readonly int ErrorsAndFailures = Errors + Failures;

			public static readonly int Nodes = Tests + 1;
			public static readonly int Categories = 5;
		    public static readonly int MockCategoryTests = 2;

			[Test(Description="Mock Test #1")]
			public void MockTest1()
			{}

			[Test]
			[Category("MockCategory")]
			[Property("Severity","Critical")]
            [Description("This is a really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really long description")]
            public void MockTest2()
			{}

			[Test]
			[Category("MockCategory")]
			[Category("AnotherCategory")]
			public void MockTest3()
            { Assert.Pass("Succeeded!"); }

            [Test]
            protected static void MockTest5()
            {}

            [Test]
            public void FailingTest()
            {
                Assert.Fail("Intentional failure");
            }

		    [Test, Property("TargetMethod", "SomeClassName"), Property("Size", 5), /*Property("TargetType", typeof( System.Threading.Thread ))*/]
			public void TestWithManyProperties()
			{}

			[Test]
			[Ignore("ignoring this test method for now")]
			[Category("Foo")]
			public void MockTest4()
			{}

			[Test, Explicit]
			[Category( "Special" )]
			public void ExplicitlyRunTest()
			{}

			[Test]
			public void NotRunnableTest( int a, int b)
			{
			}

            [Test]
            public void InconclusiveTest()
            {
                Assert.Inconclusive("No valid data");
            }

            [Test]
            public void TestWithException()
            {
                MethodThrowsException();
            }

            private void MethodThrowsException()
            {
                throw new ApplicationException("Intentional Exception");
            }
		}
	}

	namespace Singletons
	{
		[TestFixture]
		public class OneTestCase
		{
			public static readonly int Tests = 1;
			public static readonly int Nodes = Tests + 1;

			[Test]
			public virtual void TestCase() 
			{}
		}
	}

	namespace TestAssembly
	{
		[TestFixture]
		public class MockTestFixture
		{
			public static int Tests = 1;
			public static int Nodes = Tests + 1;

			[Test]
			public void MyTest()
			{
			}
		}
	}

	[TestFixture, Ignore]
	public class IgnoredFixture
	{
		public static int Tests = 3;
		public static int Nodes = Tests + 1;

		[Test]
		public void Test1() { }

		[Test]
		public void Test2() { }
		
		[Test]
		public void Test3() { }
	}

	[TestFixture,Explicit]
	public class ExplicitFixture
	{
		public static int Tests = 2;
        public static int Nodes = Tests + 1;

		[Test]
		public void Test1() { }

		[Test]
		public void Test2() { }
	}

	[TestFixture]
	public class BadFixture
	{
		public static int Tests = 1;
		public static int Nodes = Tests + 1;

		public BadFixture(int val) { }

		[Test]
		public void SomeTest() { }
	}
}
