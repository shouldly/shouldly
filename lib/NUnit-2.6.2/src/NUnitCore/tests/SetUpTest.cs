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
            Assert.AreEqual(result.FailureSite, FailureSite.SetUp, "Test should be failed at setup site");
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
