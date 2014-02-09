// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;
using NUnit.TestUtilities;
using NUnit.TestData.TestFixtureData;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Tests of the NUnitTestFixture class
	/// </summary>
	[TestFixture]
	public class TestFixtureTests
	{
		[Test]
		public void ConstructFromType()
		{
			TestSuite fixture = TestBuilder.MakeFixture( typeof( NUnit.Tests.Assemblies.MockTestFixture ) );
			Assert.AreEqual( "MockTestFixture", fixture.TestName.Name );
			Assert.AreEqual( "NUnit.Tests.Assemblies.MockTestFixture", fixture.TestName.FullName );
		}

		[Test]
		public void ConstructFromTypeWithoutNamespace()
		{
			TestSuite fixture = TestBuilder.MakeFixture( typeof( NoNamespaceTestFixture ) );
			Assert.AreEqual( "NoNamespaceTestFixture", fixture.TestName.Name );
			Assert.AreEqual( "NoNamespaceTestFixture", fixture.TestName.FullName );
		}

		[Test]
		public void ConstructFromNestedType()
		{
			TestSuite fixture = TestBuilder.MakeFixture( typeof( OuterClass.NestedTestFixture ) );
			Assert.AreEqual( "OuterClass+NestedTestFixture", fixture.TestName.Name );
			Assert.AreEqual( "NUnit.TestData.TestFixtureData.OuterClass+NestedTestFixture", fixture.TestName.FullName );
		}

		[Test]
		public void ConstructFromDoublyNestedType()
		{
			TestSuite fixture = TestBuilder.MakeFixture( typeof( OuterClass.NestedTestFixture.DoublyNestedTestFixture ) );
			Assert.AreEqual( "OuterClass+NestedTestFixture+DoublyNestedTestFixture", fixture.TestName.Name );
			Assert.AreEqual( "NUnit.TestData.TestFixtureData.OuterClass+NestedTestFixture+DoublyNestedTestFixture", fixture.TestName.FullName );
		}

        [Test]
        public void ConstructFromTypeWithoutTestFixtureAttributeContainingTest()
        {
            TestSuite fixture = TestBuilder.MakeFixture(typeof(FixtureWithoutTestFixtureAttributeContainingTest));
            Assert.NotNull(fixture, "Unable to construct fixture");
            Assert.AreEqual("FixtureWithoutTestFixtureAttributeContainingTest", fixture.TestName.Name);
            Assert.AreEqual("NUnit.TestData.TestFixtureData.FixtureWithoutTestFixtureAttributeContainingTest", fixture.TestName.FullName);
        }

        [Test]
        public void ConstructFromTypeWithoutTestFixtureAttributeContainingTestCase()
        {
            TestSuite fixture = TestBuilder.MakeFixture(typeof(FixtureWithoutTestFixtureAttributeContainingTestCase));
            Assert.NotNull(fixture, "Unable to construct fixture");
            Assert.AreEqual("FixtureWithoutTestFixtureAttributeContainingTestCase", fixture.TestName.Name);
            Assert.AreEqual("NUnit.TestData.TestFixtureData.FixtureWithoutTestFixtureAttributeContainingTestCase", fixture.TestName.FullName);
        }

        [Test]
        public void ConstructFromTypeWithoutTestFixtureAttributeContainingTestCaseSource()
        {
            TestSuite fixture = TestBuilder.MakeFixture(typeof(FixtureWithoutTestFixtureAttributeContainingTestCaseSource));
            Assert.NotNull(fixture, "Unable to construct fixture");
            Assert.AreEqual("FixtureWithoutTestFixtureAttributeContainingTestCaseSource", fixture.TestName.Name);
            Assert.AreEqual("NUnit.TestData.TestFixtureData.FixtureWithoutTestFixtureAttributeContainingTestCaseSource", fixture.TestName.FullName);
        }

        [Test]
        public void ConstructFromTypeWithoutTestFixtureAttributeContainingTheory()
        {
            TestSuite fixture = TestBuilder.MakeFixture(typeof(FixtureWithoutTestFixtureAttributeContainingTheory));
            Assert.NotNull(fixture, "Unable to construct fixture");
            Assert.AreEqual("FixtureWithoutTestFixtureAttributeContainingTheory", fixture.TestName.Name);
            Assert.AreEqual("NUnit.TestData.TestFixtureData.FixtureWithoutTestFixtureAttributeContainingTheory", fixture.TestName.FullName);
        }

        [Test]
        public void CannotRunConstructorWithArgsNotSupplied()
        {
            TestAssert.IsNotRunnable(typeof(NoDefaultCtorFixture));
        }

        [Test]
        public void CanRunConstructorWithArgsSupplied()
        {
            TestAssert.IsRunnable(typeof(FixtureWithArgsSupplied));
        }

        [Test]
		public void CannotRunBadConstructor()
		{
            TestAssert.IsNotRunnable(typeof(BadCtorFixture));
		}

		[Test] 
		public void CanRunMultipleSetUp()
		{
            TestAssert.IsRunnable(typeof(MultipleSetUpAttributes));
		}

		[Test] 
		public void CanRunMultipleTearDown()
		{
            TestAssert.IsRunnable(typeof(MultipleTearDownAttributes));
		}

		[Test]
		public void CannotRunIgnoredFixture()
		{
			TestSuite suite = TestBuilder.MakeFixture( typeof( IgnoredFixture ) );
			Assert.AreEqual( RunState.Ignored, suite.RunState );
			Assert.AreEqual( "testing ignore a fixture", suite.IgnoreReason );
		}

        [Test]
        public void CanRunFixtureDerivedFromAbstractFixture()
        {
            TestAssert.IsRunnable(typeof(DerivedFromAbstractTestFixture));
        }

        [Test]
        public void CanRunFixtureDerivedFromAbstractDerivedTestFixture()
        {
            TestAssert.IsRunnable(typeof(DerivedFromAbstractDerivedTestFixture));
        }

        [Test]
        public void FixtureInheritingTwoTestFixtureAttributesIsLoadedOnlyOnce()
        {
            TestSuite suite = TestBuilder.MakeFixture(typeof(DoubleDerivedClassWithTwoInheritedAttributes));
            Assert.That(suite, Is.TypeOf(typeof(NUnitTestFixture)));
            Assert.That(suite.Tests.Count, Is.EqualTo(0));
        }

		[Test] 
		public void CanRunMultipleTestFixtureSetUp()
		{
            TestAssert.IsRunnable(typeof(MultipleFixtureSetUpAttributes));
		}

		[Test] 
		public void CanRunMultipleTestFixtureTearDown()
		{
            TestAssert.IsRunnable(typeof(MultipleFixtureTearDownAttributes));
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void ConstructFromStaticTypeWithoutTestFixtureAttribute()
        {
            TestSuite fixture = TestBuilder.MakeFixture(typeof(StaticFixtureWithoutTestFixtureAttribute));
            Assert.NotNull(fixture, "Unable to construct fixture");
            Assert.AreEqual("StaticFixtureWithoutTestFixtureAttribute", fixture.TestName.Name);
            Assert.AreEqual("NUnit.TestData.TestFixtureData.StaticFixtureWithoutTestFixtureAttribute", fixture.TestName.FullName);
        }

        [Test]
        public void CanRunStaticFixture()
        {
            TestAssert.IsRunnable(typeof(StaticFixtureWithoutTestFixtureAttribute));
        }

        [Test]
        public void CanRunGenericFixtureWithProperArgsProvided()
        {
            TestSuite suite = TestBuilder.MakeFixture(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureWithProperArgsProvided`1,test-assembly"));
            Assert.That(suite.RunState, Is.EqualTo(RunState.Runnable));
            Assert.That(suite is ParameterizedFixtureSuite);
            Assert.That(suite.Tests.Count, Is.EqualTo(2));
        }

        [Test]
        public void CannotRunGenericFixtureWithNoTestFixtureAttribute()
        {
            TestAssert.IsNotRunnable(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureWithNoTestFixtureAttribute`1,test-assembly"));
        }

        [Test]
        public void CannotRunGenericFixtureWithNoArgsProvided()
        {
            Test suite = TestBuilder.MakeFixture(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureWithNoArgsProvided`1,test-assembly"));
            TestAssert.IsNotRunnable((Test)suite.Tests[0]);
        }

        [Test]
        public void CannotRunGenericFixtureDerivedFromAbstractFixtureWithNoArgsProvided()
        {
            Test suite = TestBuilder.MakeFixture(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureDerivedFromAbstractFixtureWithNoArgsProvided`1,test-assembly"));
            TestAssert.IsNotRunnable((Test)suite.Tests[0]);
        }

        [Test]
        public void CanRunGenericFixtureDerivedFromAbstractFixtureWithArgsProvided()
        {
            Test suite = TestBuilder.MakeFixture(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureDerivedFromAbstractFixtureWithArgsProvided`1,test-assembly"));
            Assert.That(suite.RunState, Is.EqualTo(RunState.Runnable));
            Assert.That(suite is ParameterizedFixtureSuite);
            Assert.That(suite.Tests.Count, Is.EqualTo(2));
        }

        [Test]
        public void CannotRunGenericFixtureWithOpenTypeAsArgument()
        {
            Test suite = TestBuilder.MakeFixture(
                Type.GetType("NUnit.TestData.TestFixtureData.GenericFixtureWithOpenTypeAsArgument`1,test-assembly"));
            TestAssert.IsNotRunnable((Test)suite.Tests[0]);
        }
#endif

        #region SetUp Signature
		[Test] 
		public void CannotRunPrivateSetUp()
		{
            TestAssert.IsNotRunnable(typeof(PrivateSetUp));
		}

		[Test] 
		public void CanRunProtectedSetUp()
		{
            TestAssert.IsRunnable(typeof(ProtectedSetUp));
		}

        /// <summary>
        /// Determines whether this instance [can run static set up].
        /// </summary>
		[Test] 
		public void CanRunStaticSetUp()
		{
            TestAssert.IsRunnable(typeof(StaticSetUp));
		}

		[Test]
		public void CannotRunSetupWithReturnValue()
		{
            TestAssert.IsNotRunnable(typeof(SetUpWithReturnValue));
		}

		[Test]
		public void CannotRunSetupWithParameters()
		{
            TestAssert.IsNotRunnable(typeof(SetUpWithParameters));
		}
		#endregion

		#region TearDown Signature
		[Test] 
		public void CannotRunPrivateTearDown()
		{
            TestAssert.IsNotRunnable(typeof(PrivateTearDown));
		}

		[Test] 
		public void CanRunProtectedTearDown()
		{
            TestAssert.IsRunnable(typeof(ProtectedTearDown));
		}

		[Test] 
		public void CanRunStaticTearDown()
		{
            TestAssert.IsRunnable(typeof(StaticTearDown));
		}

		[Test]
		public void CannotRunTearDownWithReturnValue()
		{
            TestAssert.IsNotRunnable(typeof(TearDownWithReturnValue));
		}

		[Test]
		public void CannotRunTearDownWithParameters()
		{
            TestAssert.IsNotRunnable(typeof(TearDownWithParameters));
		}
		#endregion

		#region TestFixtureSetUp Signature
		[Test] 
		public void CannotRunPrivateFixtureSetUp()
		{
            TestAssert.IsNotRunnable(typeof(PrivateFixtureSetUp));
		}

		[Test] 
		public void CanRunProtectedFixtureSetUp()
		{
            TestAssert.IsRunnable(typeof(ProtectedFixtureSetUp));
		}

		[Test] 
		public void CanRunStaticFixtureSetUp()
		{
            TestAssert.IsRunnable(typeof(StaticFixtureSetUp));
		}

		[Test]
		public void CannotRunFixtureSetupWithReturnValue()
		{
            TestAssert.IsNotRunnable(typeof(FixtureSetUpWithReturnValue));
		}

		[Test]
		public void CannotRunFixtureSetupWithParameters()
		{
            TestAssert.IsNotRunnable(typeof(FixtureSetUpWithParameters));
		}
		#endregion

		#region TestFixtureTearDown Signature
		[Test] 
		public void CannotRunPrivateFixtureTearDown()
		{
            TestAssert.IsNotRunnable(typeof(PrivateFixtureTearDown));
		}

		[Test] 
		public void CanRunProtectedFixtureTearDown()
		{
            TestAssert.IsRunnable(typeof(ProtectedFixtureTearDown));
		}

		[Test] 
		public void CanRunStaticFixtureTearDown()
		{
            TestAssert.IsRunnable(typeof(StaticFixtureTearDown));
		}

//		[TestFixture]
//			[Category("fixture category")]
//			[Category("second")]
//			private class HasCategories 
//		{
//			[Test] public void OneTest()
//			{}
//		}
//
//		[Test]
//		public void LoadCategories() 
//		{
//			TestSuite fixture = LoadFixture("NUnit.Core.Tests.TestFixtureBuilderTests+HasCategories");
//			Assert.IsNotNull(fixture);
//			Assert.AreEqual(2, fixture.Categories.Count);
//		}

		[Test]
		public void CannotRunFixtureTearDownWithReturnValue()
		{
            TestAssert.IsNotRunnable(typeof(FixtureTearDownWithReturnValue));
		}

		[Test]
		public void CannotRunFixtureTearDownWithParameters()
		{
            TestAssert.IsNotRunnable(typeof(FixtureTearDownWithParameters));
		}
		#endregion

        #region Nested Fixtures

        [TestFixture]
        private class PrivateTestFixture
        {
            [Test]
            public void CanRunTestInPrivateTestFixture()
            {
                Assert.True(true);
            }
        }

        [TestFixture]
        protected class ProtectedTestFixture
        {
            [Test]
            public void CanRunTestInProtectedTestFixture()
            {
                Assert.True(true);
            }
        }

        [TestFixture]
        internal class InternalTestFixture
        {
            [Test]
            public void CanRunTestInInternalTestFixture()
            {
                Assert.True(true);
            }
        }

        #endregion
    }
}
