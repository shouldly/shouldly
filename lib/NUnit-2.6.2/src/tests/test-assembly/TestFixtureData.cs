// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.TestFixtureData
{
	/// <summary>
	/// Classes used for testing NUnit
	/// </summary>

    [TestFixture]
    public class NoDefaultCtorFixture
    {
        public NoDefaultCtorFixture(int index) { }

        [Test]
        public void OneTest() { }
    }

    [TestFixture(7,3)]
    public class FixtureWithArgsSupplied
    {
        public FixtureWithArgsSupplied(int x, int y) { }

        [Test]
        public void OneTest() { }
    }

    [TestFixture]
	public class BadCtorFixture
	{
		BadCtorFixture()
		{
			throw new Exception();
		}

		[Test] public void OneTest()
		{}
	}

    public class FixtureWithoutTestFixtureAttributeContainingTest
    {
        [Test]
        public void SomeTest() { }
    }

    public class FixtureWithoutTestFixtureAttributeContainingTestCase
    {
        [TestCase(42)]
        public void SomeTest(int x) { }
    }

    public class FixtureWithoutTestFixtureAttributeContainingTestCaseSource
    {
        [TestCaseSource("data")]
        public void SomeTest(int x) { }
    }

    public class FixtureWithoutTestFixtureAttributeContainingTheory
    {
        [Theory]
        public void SomeTest(int x) { }
    }

#if CLR_2_0 || CLR_4_0
    public static class StaticFixtureWithoutTestFixtureAttribute
    {
        [Test]
        public static void StaticTest() { }
    }
#endif

    [TestFixture]
	public class MultipleSetUpAttributes
	{
		[SetUp]
		public void Init1()
		{}

		[SetUp]
		public void Init2()
		{}

		[Test] public void OneTest()
		{}
	}

	[TestFixture]
	public class MultipleTearDownAttributes
	{
		[TearDown]
		public void Destroy1()
		{}

		[TearDown]
		public void Destroy2()
		{}

		[Test] public void OneTest()
		{}
	}

	[TestFixture]
	[Ignore("testing ignore a fixture")]
	public class IgnoredFixture
	{
		[Test]
		public void Success()
		{}
	}

	[TestFixture]
	public class OuterClass
	{
		[TestFixture]
		public class NestedTestFixture
		{
			[TestFixture]
				public class DoublyNestedTestFixture
			{
				[Test]
				public void Test()
				{
				}
			}
		}
	}

	[TestFixture]
	public abstract class AbstractTestFixture
	{
		[TearDown]
		public void Destroy1()
		{}

        [Test]
        public void SomeTest()
        {}
	}

    public class DerivedFromAbstractTestFixture : AbstractTestFixture
    {
    }

	[TestFixture]
	public class BaseClassTestFixture
	{
		[Test]
		public void Success() { }
	}
	
	public abstract class AbstractDerivedTestFixture : BaseClassTestFixture
	{
	}

    public class DerivedFromAbstractDerivedTestFixture : AbstractDerivedTestFixture
    {
    }

    [TestFixture]
    public abstract class AbstractBaseFixtureWithAttribute
    {
    }

    [TestFixture]
    public abstract class AbstractDerivedFixtureWithSecondAttribute
        : AbstractBaseFixtureWithAttribute
    {
    }

    public class DoubleDerivedClassWithTwoInheritedAttributes
        : AbstractDerivedFixtureWithSecondAttribute
    {
    }

	[TestFixture]
	public class MultipleFixtureSetUpAttributes
	{
		[TestFixtureSetUp]
		public void Init1()
		{}

		[TestFixtureSetUp]
		public void Init2()
		{}

		[Test] public void OneTest()
		{}
	}

	[TestFixture]
	public class MultipleFixtureTearDownAttributes
	{
		[TestFixtureTearDown]
		public void Destroy1()
		{}

		[TestFixtureTearDown]
		public void Destroy2()
		{}

		[Test] public void OneTest()
		{}
	}

	// Base class used to ensure following classes
	// all have at least one test
	public class OneTestBase
	{
		[Test] public void OneTest() { }
	}

	[TestFixture]
	public class PrivateSetUp : OneTestBase
	{
		[SetUp]
		private void Setup()	{}
	}

	[TestFixture]
	public class ProtectedSetUp : OneTestBase
	{
		[SetUp]
		protected void Setup()	{}
	}

	[TestFixture]
	public class StaticSetUp : OneTestBase
	{
		[SetUp]
		public static void Setup() {}
	}

	[TestFixture]
	public class SetUpWithReturnValue : OneTestBase
	{
		[SetUp]
		public int Setup() { return 0; }
	}

	[TestFixture]
	public class SetUpWithParameters : OneTestBase
	{
		[SetUp]
		public void Setup(int j) { }
	}

	[TestFixture]
	public class PrivateTearDown : OneTestBase
	{
		[TearDown]
		private void Teardown()	{}
	}

	[TestFixture]
	public class ProtectedTearDown : OneTestBase
	{
		[TearDown]
		protected void Teardown()	{}
	}

	[TestFixture]
	public class StaticTearDown : OneTestBase
	{
		[SetUp]
		public static void TearDown() {}
	}

	[TestFixture]
	public class TearDownWithReturnValue : OneTestBase
	{
		[TearDown]
		public int Teardown() { return 0; }
	}

	[TestFixture]
	public class TearDownWithParameters : OneTestBase
	{
		[TearDown]
		public void Teardown(int j) { }
	}

	[TestFixture]
	public class PrivateFixtureSetUp : OneTestBase
	{
		[TestFixtureSetUp]
		private void Setup()	{}
	}

	[TestFixture]
	public class ProtectedFixtureSetUp : OneTestBase
	{
		[TestFixtureSetUp]
		protected void Setup()	{}
	}

	[TestFixture]
	public class StaticFixtureSetUp : OneTestBase
	{
		[TestFixtureSetUp]
		public static void Setup() {}
	}

	[TestFixture]
	public class FixtureSetUpWithReturnValue : OneTestBase
	{
		[TestFixtureSetUp]
		public int Setup() { return 0; }
	}

	[TestFixture]
	public class FixtureSetUpWithParameters : OneTestBase
	{
		[SetUp]
		public void Setup(int j) { }
	}

	[TestFixture]
	public class PrivateFixtureTearDown : OneTestBase
	{
		[TestFixtureTearDown]
		private void Teardown()	{}
	}

	[TestFixture]
	public class ProtectedFixtureTearDown : OneTestBase
	{
		[TestFixtureTearDown]
		protected void Teardown()	{}
	}

	[TestFixture]
	public class StaticFixtureTearDown : OneTestBase
	{
		[TestFixtureTearDown]
		public static void Teardown() {}
	}

	[TestFixture]
	public class FixtureTearDownWithReturnValue : OneTestBase
	{
		[TestFixtureTearDown]
		public int Teardown() { return 0; }
	}

	[TestFixture]
	public class FixtureTearDownWithParameters : OneTestBase
	{
		[TestFixtureTearDown]
		public void Teardown(int j) { }
    }

#if CLR_2_0 || CLR_4_0
    [TestFixture(typeof(int))]
    [TestFixture(typeof(string))]
    public class GenericFixtureWithProperArgsProvided<T>
    {
        [Test]
        public void SomeTest() { }
    }

    public class GenericFixtureWithNoTestFixtureAttribute<T>
    {
        [Test]
        public void SomeTest() { }
    }

    [TestFixture]
    public class GenericFixtureWithNoArgsProvided<T>
    {
        [Test]
        public void SomeTest() { }
    }

    [TestFixture]
    public abstract class AbstractFixtureBase
    {
        [Test]
        public void SomeTest() { }
    }

    public class GenericFixtureDerivedFromAbstractFixtureWithNoArgsProvided<T> : AbstractFixtureBase
    {
    }

    [TestFixture(typeof(int))]
    [TestFixture(typeof(string))]
    public class GenericFixtureDerivedFromAbstractFixtureWithArgsProvided<T> : AbstractFixtureBase
    {
    }

    [TestFixture(typeof(System.Collections.Generic.List<>))]
    public class GenericFixtureWithOpenTypeAsArgument<T>
    {
    }
#endif
}
