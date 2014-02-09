// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Security.Principal;
using System.Threading;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.Util;
using NUnit.TestUtilities;
using NUnit.TestData.FixtureSetUpTearDown;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class FixtureSetupTearDownTest
	{
		private TestResult RunTestOnFixture( object fixture )
		{
			TestSuite suite = TestBuilder.MakeFixture( fixture.GetType() );
			suite.Fixture = fixture;
            return suite.Run(NullListener.NULL, TestFilter.Empty);
		}

		[Test]
		public void MakeSureSetUpAndTearDownAreCalled()
		{
			SetUpAndTearDownFixture fixture = new SetUpAndTearDownFixture();
			RunTestOnFixture( fixture );

			Assert.AreEqual(1, fixture.setUpCount, "SetUp");
			Assert.AreEqual(1, fixture.tearDownCount, "TearDown");
		}

		[Test]
		public void MakeSureSetUpAndTearDownAreCalledOnExplicitFixture()
		{
			ExplicitSetUpAndTearDownFixture fixture = new ExplicitSetUpAndTearDownFixture();
			RunTestOnFixture( fixture );

			Assert.AreEqual(1, fixture.setUpCount, "SetUp");
			Assert.AreEqual(1, fixture.tearDownCount, "TearDown");
		}

		[Test]
		public void CheckInheritedSetUpAndTearDownAreCalled()
		{
			InheritSetUpAndTearDown fixture = new InheritSetUpAndTearDown();
			RunTestOnFixture( fixture );

			Assert.AreEqual(1, fixture.setUpCount);
			Assert.AreEqual(1, fixture.tearDownCount);
		}

        [Test]
        public static void StaticSetUpAndTearDownAreCalled()
        {
            StaticSetUpAndTearDownFixture.setUpCount = 0;
            StaticSetUpAndTearDownFixture.tearDownCount = 0;
            TestBuilder.RunTestFixture(typeof(StaticSetUpAndTearDownFixture));

            Assert.AreEqual(1, StaticSetUpAndTearDownFixture.setUpCount);
            Assert.AreEqual(1, StaticSetUpAndTearDownFixture.tearDownCount);
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public static void StaticClassSetUpAndTearDownAreCalled()
        {
            StaticClassSetUpAndTearDownFixture.setUpCount = 0;
            StaticClassSetUpAndTearDownFixture.tearDownCount = 0;
            TestBuilder.RunTestFixture(typeof(StaticClassSetUpAndTearDownFixture));

            Assert.AreEqual(1, StaticClassSetUpAndTearDownFixture.setUpCount);
            Assert.AreEqual(1, StaticClassSetUpAndTearDownFixture.tearDownCount);
        }
#endif

		[Test]
		public void OverriddenSetUpAndTearDownAreNotCalled()
		{
            DefineInheritSetUpAndTearDown fixture = new DefineInheritSetUpAndTearDown();
            RunTestOnFixture(fixture);

            Assert.AreEqual(0, fixture.setUpCount);
            Assert.AreEqual(0, fixture.tearDownCount);
            Assert.AreEqual(1, fixture.derivedSetUpCount);
            Assert.AreEqual(1, fixture.derivedTearDownCount);
        }

        [Test]
        public void BaseSetUpCalledFirstAndTearDownCalledLast()
        {
            DerivedSetUpAndTearDownFixture fixture = new DerivedSetUpAndTearDownFixture();
            RunTestOnFixture(fixture);

            Assert.AreEqual(1, fixture.setUpCount);
            Assert.AreEqual(1, fixture.tearDownCount);
            Assert.AreEqual(1, fixture.derivedSetUpCount);
            Assert.AreEqual(1, fixture.derivedTearDownCount);
            Assert.That(fixture.baseSetUpCalledFirst, "Base SetUp called first");
            Assert.That(fixture.baseTearDownCalledLast, "Base TearDown called last");
        }

        [Test]
        public void StaticBaseSetUpCalledFirstAndTearDownCalledLast()
        {
            StaticSetUpAndTearDownFixture.setUpCount = 0;
            StaticSetUpAndTearDownFixture.tearDownCount = 0;
            DerivedStaticSetUpAndTearDownFixture.derivedSetUpCount = 0;
            DerivedStaticSetUpAndTearDownFixture.derivedTearDownCount = 0;

            DerivedStaticSetUpAndTearDownFixture fixture = new DerivedStaticSetUpAndTearDownFixture();
            RunTestOnFixture(fixture);

            Assert.AreEqual(1, DerivedStaticSetUpAndTearDownFixture.setUpCount);
            Assert.AreEqual(1, DerivedStaticSetUpAndTearDownFixture.tearDownCount);
            Assert.AreEqual(1, DerivedStaticSetUpAndTearDownFixture.derivedSetUpCount);
            Assert.AreEqual(1, DerivedStaticSetUpAndTearDownFixture.derivedTearDownCount);
            Assert.That(DerivedStaticSetUpAndTearDownFixture.baseSetUpCalledFirst, "Base SetUp called first");
            Assert.That(DerivedStaticSetUpAndTearDownFixture.baseTearDownCalledLast, "Base TearDown called last");
        }

        [Test]
		public void HandleErrorInFixtureSetup() 
		{
			MisbehavingFixture fixture = new MisbehavingFixture();
			fixture.blowUpInSetUp = true;
			TestResult result = RunTestOnFixture( fixture );

			Assert.AreEqual( 1, fixture.setUpCount, "setUpCount" );
			Assert.AreEqual( 0, fixture.tearDownCount, "tearDownCOunt" );

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
			
			Assert.AreEqual(ResultState.Error, result.ResultState);
			Assert.AreEqual("SetUp : System.Exception : This was thrown from fixture setup", result.Message, "TestSuite Message");
			Assert.IsNotNull(result.StackTrace, "TestSuite StackTrace should not be null");

			TestResult testResult = ((TestResult)result.Results[0]);
			Assert.IsTrue(testResult.Executed, "Test should have executed");
			Assert.AreEqual("TestFixtureSetUp failed in MisbehavingFixture", testResult.Message, "TestSuite Message");
            Assert.AreEqual(FailureSite.Parent, testResult.FailureSite);
			Assert.AreEqual(testResult.StackTrace, testResult.StackTrace, "Test stackTrace should match TestSuite stackTrace" );
		}

		[Test]
		public void RerunFixtureAfterSetUpFixed() 
		{
			MisbehavingFixture fixture = new MisbehavingFixture();
			fixture.blowUpInSetUp = true;
			TestResult result = RunTestOnFixture( fixture );

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
			Assert.IsTrue(result.Executed, "Suite should have executed");

			//fix the blow up in setup
			fixture.Reinitialize();
			result = RunTestOnFixture( fixture );

			Assert.AreEqual( 1, fixture.setUpCount, "setUpCount" );
			Assert.AreEqual( 1, fixture.tearDownCount, "tearDownCOunt" );

			// should have one suite and one fixture
			summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
		}

		[Test]
		public void HandleIgnoreInFixtureSetup() 
		{
			IgnoreInFixtureSetUp fixture = new IgnoreInFixtureSetUp();
			TestResult result = RunTestOnFixture( fixture );

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(0, summ.TestsRun);
			Assert.AreEqual(1, summ.TestsNotRun);
			Assert.IsFalse(result.Executed, "Suite should not have executed");
			Assert.AreEqual("TestFixtureSetUp called Ignore", result.Message);
			Assert.IsNotNull(result.StackTrace, "StackTrace should not be null");

			TestResult testResult = ((TestResult)result.Results[0]);
			Assert.IsFalse(testResult.Executed, "Testcase should not have executed");
			Assert.AreEqual("TestFixtureSetUp called Ignore", testResult.Message );
		}

		[Test]
		public void HandleErrorInFixtureTearDown() 
		{
			MisbehavingFixture fixture = new MisbehavingFixture();
			fixture.blowUpInTearDown = true;
			TestResult result = RunTestOnFixture( fixture );
			Assert.AreEqual(1, result.Results.Count);
			Assert.IsTrue(result.Executed, "Suite should have executed");
			Assert.IsTrue(result.IsFailure, "Suite should have failed" );

			Assert.AreEqual( 1, fixture.setUpCount, "setUpCount" );
			Assert.AreEqual( 1, fixture.tearDownCount, "tearDownCOunt" );

			Assert.AreEqual("This was thrown from fixture teardown", result.Message);
			Assert.IsNotNull(result.StackTrace, "StackTrace should not be null");

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
		}

		[Test]
		public void HandleExceptionInFixtureConstructor()
		{
			TestSuite suite = TestBuilder.MakeFixture( typeof( ExceptionInConstructor ) );
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
			
			Assert.AreEqual(ResultState.Error, result.ResultState);
			Assert.AreEqual("SetUp : System.Exception : This was thrown in constructor", result.Message, "TestSuite Message");
			Assert.IsNotNull(result.StackTrace, "TestSuite StackTrace should not be null");

			TestResult testResult = ((TestResult)result.Results[0]);
			Assert.IsTrue(testResult.Executed, "Testcase should have executed");
			Assert.AreEqual("TestFixtureSetUp failed in ExceptionInConstructor", testResult.Message, "TestSuite Message");
            Assert.AreEqual(FailureSite.Parent, testResult.FailureSite);
			Assert.AreEqual(testResult.StackTrace, testResult.StackTrace, "Test stackTrace should match TestSuite stackTrace" );
		}

		[Test]
		public void RerunFixtureAfterTearDownFixed() 
		{
			MisbehavingFixture fixture = new MisbehavingFixture();
			fixture.blowUpInTearDown = true;
			TestResult result = RunTestOnFixture( fixture );
			Assert.AreEqual(1, result.Results.Count);

			// should have one suite and one fixture
			ResultSummarizer summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);

			fixture.Reinitialize();
			result = RunTestOnFixture( fixture );

			Assert.AreEqual( 1, fixture.setUpCount, "setUpCount" );
			Assert.AreEqual( 1, fixture.tearDownCount, "tearDownCOunt" );

			summ = new ResultSummarizer(result);
			Assert.AreEqual(1, summ.TestsRun);
			Assert.AreEqual(0, summ.TestsNotRun);
		}

		[Test]
		public void HandleSetUpAndTearDownWithTestInName()
		{
			SetUpAndTearDownWithTestInName fixture = new SetUpAndTearDownWithTestInName();
			RunTestOnFixture( fixture );

			Assert.AreEqual(1, fixture.setUpCount);
			Assert.AreEqual(1, fixture.tearDownCount);
		}

		[Test]
		public void RunningSingleMethodCallsSetUpAndTearDown()
		{
			SetUpAndTearDownFixture fixture = new SetUpAndTearDownFixture();
			TestSuite suite = TestBuilder.MakeFixture( fixture.GetType() );
			suite.Fixture = fixture;
			Test test = (Test)suite.Tests[0];
			
			suite.Run(NullListener.NULL, new Filters.NameFilter( test.TestName ) );

			Assert.AreEqual(1, fixture.setUpCount);
			Assert.AreEqual(1, fixture.tearDownCount);
		}

		[Test]
		public void IgnoredFixtureShouldNotCallFixtureSetUpOrTearDown()
		{
			IgnoredFixture fixture = new IgnoredFixture();
			TestSuite suite = new TestSuite("IgnoredFixtureSuite");
			TestSuite fixtureSuite = TestBuilder.MakeFixture( fixture.GetType() );
			suite.Fixture = fixture;
			Test test = (Test)fixtureSuite.Tests[0];
			suite.Add( fixtureSuite );

            fixtureSuite.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse( fixture.setupCalled, "TestFixtureSetUp called running fixture" );
			Assert.IsFalse( fixture.teardownCalled, "TestFixtureTearDown called running fixture" );

            suite.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse( fixture.setupCalled, "TestFixtureSetUp called running enclosing suite" );
			Assert.IsFalse( fixture.teardownCalled, "TestFixtureTearDown called running enclosing suite" );

            test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse( fixture.setupCalled, "TestFixtureSetUp called running a test case" );
			Assert.IsFalse( fixture.teardownCalled, "TestFixtureTearDown called running a test case" );
		}

		[Test]
		public void FixtureWithNoTestsCallsFixtureSetUpAndTearDown()
		{
            // NOTE: Beginning with NUnit 2.5.3, FixtureSetUp and TearDown
            // are called even if the fixture contains no tests.

			FixtureWithNoTests fixture = new FixtureWithNoTests();
			RunTestOnFixture( fixture );		
			Assert.IsTrue( fixture.setupCalled, "TestFixtureSetUp not called" );
			Assert.IsTrue( fixture.teardownCalled, "TestFixtureTearDown not called" );
		}

        [Test]
        public void DisposeCalledWhenFixtureImplementsIDisposable()
        {
            DisposableFixture fixture = new DisposableFixture();
            RunTestOnFixture(fixture);
            Assert.IsTrue(fixture.disposeCalled);
        }

        [TestFixture]
        class ChangesMadeInFixtureSetUp
        {
            [TestFixtureSetUp]
            public void TestFixtureSetUp()
            {
                GenericIdentity identity = new GenericIdentity("foo");
                Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[0]);

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            [Test]
            public void TestThatChangesPersistUsingSameThread()
            {
                Assert.AreEqual("foo", Thread.CurrentPrincipal.Identity.Name);
                Assert.AreEqual("en-GB", Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual("en-GB", Thread.CurrentThread.CurrentUICulture.Name);
            }

            [Test, RequiresThread]
            public void TestThatChangesPersistUsingSeparateThread()
            {
                Assert.AreEqual("foo", Thread.CurrentPrincipal.Identity.Name);
                Assert.AreEqual("en-GB", Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual("en-GB", Thread.CurrentThread.CurrentUICulture.Name);
            }
        }
    }
}
