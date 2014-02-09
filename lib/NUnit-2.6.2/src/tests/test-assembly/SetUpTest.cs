// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.SetUpTest
{
	[TestFixture]
	public class SetUpAndTearDownFixture
	{
        public bool wasSetUpCalled;
        public bool wasTearDownCalled;

        [SetUp]
        public virtual void Init()
        {
            wasSetUpCalled = true;
        }

        [TearDown]
        public virtual void Destroy()
        {
            wasTearDownCalled = true;
        }

        [Test]
        public void Success() { }
    }


	[TestFixture]
	public class SetUpAndTearDownCounterFixture
	{
		public int setUpCounter;
		public int tearDownCounter;

		[SetUp]
		public virtual void Init()
		{
			setUpCounter++;
		}

		[TearDown]
		public virtual void Destroy()
		{
			tearDownCounter++;
		}

		[Test]
		public void TestOne(){}

		[Test]
		public void TestTwo(){}

		[Test]
		public void TestThree(){}
	}
		
	[TestFixture]
	public class InheritSetUpAndTearDown : SetUpAndTearDownFixture
	{
		[Test]
		public void AnotherTest(){}
	}

	[TestFixture]
	public class DefineInheritSetUpAndTearDown : SetUpAndTearDownFixture
	{
		public bool derivedSetUpCalled;
		public bool derivedTearDownCalled;

		[SetUp]
		public override void Init()
		{
			derivedSetUpCalled = true;
		}

		[TearDown]
		public override void Destroy()
		{
			derivedTearDownCalled = true;
		}

		[Test]
		public void AnotherTest(){}
	}

    public class MultipleSetUpTearDownFixture
    {
        public bool wasSetUp1Called;
        public bool wasSetUp2Called;
        public bool wasSetUp3Called;
        public bool wasTearDown1Called;
        public bool wasTearDown2Called;

        [SetUp]
        public virtual void Init1()
        {
            wasSetUp1Called = true;
        }
        [SetUp]
        public virtual void Init2()
        {
            wasSetUp2Called = true;
        }
        [SetUp]
        public virtual void Init3()
        {
            wasSetUp3Called = true;
        }

        [TearDown]
        public virtual void TearDown1()
        {
            wasTearDown1Called = true;
        }
        [TearDown]
        public virtual void TearDown2()
        {
            wasTearDown2Called = true;
        }

        [Test]
        public void Success() { }
    }

    [TestFixture]
    public class DerivedClassWithSeparateSetUp : SetUpAndTearDownFixture
    {
        public bool wasDerivedSetUpCalled;
        public bool wasDerivedTearDownCalled;
        public bool wasBaseSetUpCalledFirst;
        public bool wasBaseTearDownCalledLast;

        [SetUp]
        public void DerivedInit()
        {
            wasDerivedSetUpCalled = true;
            wasBaseSetUpCalledFirst = wasSetUpCalled;
        }

        [TearDown]
        public void DerivedTearDown()
        {
            wasDerivedTearDownCalled = true;
            wasBaseTearDownCalledLast = !wasTearDownCalled;
        }
    }

    [TestFixture]
    public class SetupAndTearDownExceptionFixture
    {
        public Exception setupException;
        public Exception tearDownException;

        [SetUp] 
        public void SetUp()
        {
            if (setupException != null) throw setupException;
        }

        [TearDown]
        public void TearDown()
        {
            if (tearDownException!=null) throw tearDownException;
        }

        [Test]
        public void TestOne() {}
    }
}
