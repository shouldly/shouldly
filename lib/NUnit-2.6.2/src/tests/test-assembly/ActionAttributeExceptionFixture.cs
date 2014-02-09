// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.TestData
{
    [ExceptionThrowingAction]
    [TestFixture]
    public class ActionAttributeExceptionFixture
    {
        public static bool SetUpRun = false;
        public static bool TestRun = false;
        public static bool TearDownRun = false;

        public static void Reset()
        {
            SetUpRun = false;
            TestRun = false;
            TearDownRun = false;
        }

        [SetUp]
        public void SetUp()
        {
            SetUpRun = true;
        }

        [TearDown]
        public void TearDown()
        {
            TearDownRun = true;
        }

        [ExceptionThrowingAction]
        [Test]
        public void SomeTest()
        {
            TestRun = true;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionThrowingActionAttribute : TestActionAttribute
    {
        private static bool _ThrowBeforeException;
        private static bool _ThrowAfterException;

        public static void Reset()
        {
            ThrowBeforeException = false;
            ThrowAfterException = false;
        }

        public override void BeforeTest(TestDetails testDetails)
        {
            if (ThrowBeforeException)
                throw new InvalidOperationException("Failure in BeforeTest.");
        }

        public override void AfterTest(TestDetails testDetails)
        {
            if (ThrowAfterException)
                throw new InvalidOperationException("Failure in AfterTest.");
        }

        public static bool ThrowBeforeException
        {
            get { return _ThrowBeforeException; }
            set { _ThrowBeforeException = value; }
        }

        public static bool ThrowAfterException
        {
            get { return _ThrowAfterException; }
            set { _ThrowAfterException = value; }
        }
    }
}
#endif