// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using NUnit.TestData.TestContextData;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
    [TestFixture][Property("Question", "Why?")]
    public class TestContextTests
    {
        private TestContext fixtureContext;
        private TestContext setupContext;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            fixtureContext = TestContext.CurrentContext;
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            // TODO: We put some tests in one time teardown to verify that
            // the context is still valid. It would be better if these tests
            // were placed in a second-level test, invoked from this test class.
            TestContext context = TestContext.CurrentContext;
            Assert.That(context.Test.Name, Is.EqualTo("TestContextTests"));
            Assert.That(context.Test.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestContextTests"));
            Assert.That(context.Test.Properties["Question"], Is.EqualTo("Why?"));

            // The following assert cannot fail, but we use it to make
            // sure that the result can be accessed in TestFixtureTearDown.
            Assert.NotNull(context.Result.State);
        }

        [SetUp]
        public void SetUpMethod()
        {
            setupContext = TestContext.CurrentContext;
        }

        [TearDown]
        public void TearDownMethod()
        {
            Assert.That(
                TestContext.CurrentContext.Test.FullName,
                Is.EqualTo(setupContext.Test.FullName),
                "Context at TearDown failed to match that saved from SetUp");

            // The following assert cannot fail, but we use it to make
            // sure that the result can be accessed in TearDown.
            Assert.NotNull(TestContext.CurrentContext.Result.State);
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureName()
        {
            Assert.That(fixtureContext.Test.Name, Is.EqualTo("TestContextTests"));
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureFullName()
        {
            Assert.That(fixtureContext.Test.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestContextTests"));
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureResultState()
        {
            Assert.That(fixtureContext.Result.State, Is.EqualTo(TestState.Success));
            Assert.That(fixtureContext.Result.Status, Is.EqualTo(TestStatus.Passed));
        }

        [Test]
        [Property("Answer", 37)]
        public void FixtureSetUpCanAccessFixtureProperties()
        {
            Assert.That(fixtureContext.Test.Properties["Question"], Is.EqualTo("Why?"));
        }

        [Test]
        public void FixtureSetUpCanAccessTestDirectory()
        {
            string testDirectory = fixtureContext.TestDirectory;
            Assert.NotNull(testDirectory);
            Assert.That(Directory.Exists(testDirectory));
            Assert.That(File.Exists(Path.Combine(testDirectory, "nunit.core.tests.dll")));
        }

        [Test]
        public void FixtureSetUpCanAccessWorkDirectory()
        {
            string workDirectory = fixtureContext.WorkDirectory;
            Assert.NotNull(workDirectory);
            Assert.That(Directory.Exists(workDirectory), string.Format("Directory {0} does not exist", workDirectory));
        }

        [Test]
        public void SetUpCanAccessTestName()
        {
            Assert.That(setupContext.Test.Name, Is.EqualTo("SetUpCanAccessTestName"));
        }

        [Test]
        public void SetUpCanAccessTestFullName()
        {
            Assert.That(setupContext.Test.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestContextTests.SetUpCanAccessTestFullName"));
        }

        [Test]
        [Property("Answer", 99)]
        public void SetUpCanAccessTestProperties()
        {
            Assert.That(setupContext.Test.Properties["Answer"], Is.EqualTo(99));
        }

        [Test]
        public void SetUpCanAccessTestResultState()
        {
            Assert.That(setupContext.Result.State, Is.EqualTo(TestState.Inconclusive));
            Assert.That(setupContext.Result.Status, Is.EqualTo(TestStatus.Inconclusive));
        }

        [Test]
        public void SetUpCanAccessTestDirectory()
        {
            string testDirectory = setupContext.TestDirectory;
            Assert.NotNull(testDirectory);
            Assert.That(Directory.Exists(testDirectory));
            Assert.That(File.Exists(Path.Combine(testDirectory, "nunit.core.tests.dll")));
        }

        [Test]
        public void SetUpCanAccessWorkDirectory()
        {
            string workDirectory = setupContext.WorkDirectory;
            Assert.NotNull(workDirectory);
            Assert.That(Directory.Exists(workDirectory), string.Format("Directory {0} does not exist", workDirectory));
        }

        [Test]
        public void TestCanAccessItsOwnName()
        {
            Assert.That(TestContext.CurrentContext.Test.Name, Is.EqualTo("TestCanAccessItsOwnName"));
        }

        [Test]
        public void TestCanAccessItsOwnFullName()
        {
            Assert.That(TestContext.CurrentContext.Test.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestContextTests.TestCanAccessItsOwnFullName"));
        }

        [Test]
        [Property("Answer", 42)]
        public void TestCanAccessItsOwnProperties()
        {
            Assert.That(TestContext.CurrentContext.Test.Properties["Answer"], Is.EqualTo(42));
        }

        [Test]
        public void TestCanAccessItsOwnResultState()
        {
            Assert.That(TestContext.CurrentContext.Result.State, Is.EqualTo(TestState.Inconclusive));
            Assert.That(TestContext.CurrentContext.Result.Status, Is.EqualTo(TestStatus.Inconclusive));
        }

        [Test]
        public void TestCanAccessTestDirectory()
        {
            string testDirectory = TestContext.CurrentContext.TestDirectory;
            Assert.NotNull(testDirectory);
            Assert.That(Directory.Exists(testDirectory));
            Assert.That(File.Exists(Path.Combine(testDirectory, "nunit.core.tests.dll")));
        }

        [Test]
        public void TestCanAccessWorkDirectory()
        {
            string workDirectory = TestContext.CurrentContext.WorkDirectory;
            Assert.NotNull(workDirectory);
            Assert.That(Directory.Exists(workDirectory), string.Format("Directory {0} does not exist", workDirectory));
        }

        [Test]
        public void TestCanAccessTestState_PassingTest()
        {
            TestStateRecordingFixture fixture = new TestStateRecordingFixture();
            TestBuilder.RunTestFixture(fixture);
            Assert.That(fixture.stateList, Is.EqualTo("Inconclusive=>Inconclusive=>Success"));
            Assert.That(fixture.statusList, Is.EqualTo("Inconclusive=>Inconclusive=>Passed"));
        }

        [Test]
        public void TestCanAccessTestState_FailureInSetUp()
        {
            TestStateRecordingFixture fixture = new TestStateRecordingFixture();
            fixture.setUpFailure = true;
            TestBuilder.RunTestFixture(fixture);
            Assert.That(fixture.stateList, Is.EqualTo("Inconclusive=>=>Failure"));
            Assert.That(fixture.statusList, Is.EqualTo("Inconclusive=>=>Failed"));
        }

        [Test]
        public void TestCanAccessTestState_FailingTest()
        {
            TestStateRecordingFixture fixture = new TestStateRecordingFixture();
            fixture.testFailure = true;
            TestBuilder.RunTestFixture(fixture);
            Assert.That(fixture.stateList, Is.EqualTo("Inconclusive=>Inconclusive=>Failure"));
            Assert.That(fixture.statusList, Is.EqualTo("Inconclusive=>Inconclusive=>Failed"));
        }

        [Test]
        public void TestCanAccessTestState_IgnoredInSetUp()
        {
            TestStateRecordingFixture fixture = new TestStateRecordingFixture();
            fixture.setUpIgnore = true;
            TestBuilder.RunTestFixture(fixture);
            Assert.That(fixture.stateList, Is.EqualTo("Inconclusive=>=>Ignored"));
            Assert.That(fixture.statusList, Is.EqualTo("Inconclusive=>=>Skipped"));
        }	

	    [Test, RequiresThread]
        public void CanAccessTestContextWhenRunningTestOnSeparateThread()
        {
			Assert.That(TestContext.CurrentContext.Test.Name, Is.EqualTo("CanAccessTestContextWhenRunningTestOnSeparateThread"));
        }

        private string TestNameFromContext;

		[Test, Platform(Exclude="Net-1.0, Net-1.1, Mono-1.0")]
		public void CanAccessTestContextFromThreadSpawnedWithinTest()
		{
            Thread thread = new Thread(new ThreadStart(FillTestNameFromContext));
            thread.Start();
			thread.Join();

			Assert.That(TestNameFromContext, Is.EqualTo(TestContext.CurrentContext.Test.Name));
		}

		private void FillTestNameFromContext()
		{
            this.TestNameFromContext = TestContext.CurrentContext.Test.Name;
		}
	}
}