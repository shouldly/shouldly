// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.TestUtilities;
using NUnit.TestData.SetUpTest;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class SetUpTest
	{
		[Test]
		public void SetUpAndTearDownCounter()
		{
			SetUpAndTearDownCounterFixture fixture = new SetUpAndTearDownCounterFixture();
			TestBuilder.RunTestFixture( fixture );

			Assert.AreEqual(3, fixture.setUpCounter);
			Assert.AreEqual(3, fixture.tearDownCounter);
		}

		
		[Test]
		public void MakeSureSetUpAndTearDownAreCalled()
		{
			SetUpAndTearDownFixture fixture = new SetUpAndTearDownFixture();
			TestBuilder.RunTestFixture( fixture );

			Assert.IsTrue(fixture.wasSetUpCalled);
			Assert.IsTrue(fixture.wasTearDownCalled);
		}

		[Test]
		public void CheckInheritedSetUpAndTearDownAreCalled()
		{
			InheritSetUpAndTearDown fixture = new InheritSetUpAndTearDown();
			TestBuilder.RunTestFixture( fixture );

			Assert.IsTrue(fixture.wasSetUpCalled);
			Assert.IsTrue(fixture.wasTearDownCalled);
		}

		[Test]
		public void CheckOverriddenSetUpAndTearDownAreNotCalled()
		{
			DefineInheritSetUpAndTearDown fixture = new DefineInheritSetUpAndTearDown();
			TestBuilder.RunTestFixture( fixture );

			Assert.IsFalse(fixture.wasSetUpCalled);
			Assert.IsFalse(fixture.wasTearDownCalled);
			Assert.IsTrue(fixture.derivedSetUpCalled);
			Assert.IsTrue(fixture.derivedTearDownCalled);
		}

        [Test]
        public void MultipleSetUpAndTearDownMethodsAreCalled()
        {
            MultipleSetUpTearDownFixture fixture = new MultipleSetUpTearDownFixture();
            TestBuilder.RunTestFixture(fixture);

            Assert.IsTrue(fixture.wasSetUp1Called, "SetUp1");
            Assert.IsTrue(fixture.wasSetUp2Called, "SetUp2");
            Assert.IsTrue(fixture.wasSetUp3Called, "SetUp3");
            Assert.IsTrue(fixture.wasTearDown1Called, "TearDown1");
            Assert.IsTrue(fixture.wasTearDown2Called, "TearDown2");
        }

        [Test]
        public void BaseSetUpIsCalledFirstTearDownLast()
        {
            DerivedClassWithSeparateSetUp fixture = new DerivedClassWithSeparateSetUp();
            TestBuilder.RunTestFixture(fixture);

            Assert.IsTrue(fixture.wasSetUpCalled, "Base SetUp Called");
            Assert.IsTrue(fixture.wasTearDownCalled, "Base TearDown Called");
            Assert.IsTrue(fixture.wasDerivedSetUpCalled, "Derived SetUp Called");
            Assert.IsTrue(fixture.wasDerivedTearDownCalled, "Derived TearDown Called");
            Assert.IsTrue(fixture.wasBaseSetUpCalledFirst, "SetUp Order");
            Assert.IsTrue(fixture.wasBaseTearDownCalledLast, "TearDown Order");
        }

        [Test]
        public void SetupRecordsOriginalExceptionThownByTestCase()
        {
            Exception e = new Exception("Test message for exception thrown from setup");
            SetupAndTearDownExceptionFixture fixture = new SetupAndTearDownExceptionFixture();
            fixture.setupException = e;
            TestResult result = TestBuilder.RunTestFixture(fixture);
            Assert.IsTrue(result.HasResults, "Fixture test should have child result.");
            result = (TestResult)result.Results[0];
            Assert.AreEqual(result.ResultState, ResultState.Error, "Test should be in error state");
            //TODO: below assert fails now, a bug?
            //Assert.AreEqual(result.FailureSite, FailureSite.SetUp, "Test should be failed at setup site");
            string expected = string.Format("SetUp : {0} : {1}", e.GetType().FullName, e.Message);
            Assert.AreEqual(expected, result.Message);
        }

        [Test]
        public void TearDownRecordsOriginalExceptionThownByTestCase()
        {
            Exception e = new Exception("Test message for exception thrown from tear down");
            SetupAndTearDownExceptionFixture fixture = new SetupAndTearDownExceptionFixture();
            fixture.tearDownException = e;
            TestResult result = TestBuilder.RunTestFixture(fixture);
            Assert.IsTrue(result.HasResults, "Fixture test should have child result.");
            result = (TestResult)result.Results[0];
            Assert.AreEqual(result.ResultState, ResultState.Error, "Test should be in error state");
            Assert.AreEqual(result.FailureSite, FailureSite.TearDown, "Test should be failed at tear down site");
            string expected = string.Format("TearDown : {0} : {1}", e.GetType().FullName, e.Message);
            Assert.AreEqual(expected, result.Message);
        }

        #region TestContext Tests
        // TODO: Put these in their own file once NUnit.Core.TestContext is renamed

        [Test]
        public void TestCanAccessItsOwnName()
        {
            Assert.That(NUnit.Framework.TestContext.TestName, Is.EqualTo("TestCanAccessItsOwnName"));
        }

        [Test]
        [Property("Answer", 42)]
        public void TestCanAccessItsOwnProperties()
        {
            Assert.That(NUnit.Framework.TestContext.Properties["Answer"], Is.EqualTo(42));
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

        #endregion

        public class SetupCallBase
        {
            protected int setupCount = 0;
            public virtual void Init()
            {
                setupCount++;
            }
            public virtual void AssertCount()
            {
            }
        }

        [TestFixture]
        // Test for bug 441022
        public class SetupCallDerived : SetupCallBase
        {
            [SetUp]
            public override void Init()
            {
                setupCount++;
                base.Init();
            }
            [Test]
            public override void AssertCount()
            {
                Assert.AreEqual(2, setupCount);
            }
        }
    }
}
