// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using NUnit.Framework;
using NUnit.TestData;
using NUnit.TestData.ActionAttributeTests;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class ActionAttributeExceptionTests
    {
        private class Filter : TestFilter
        {
            public override bool Match(ITest test)
            {
                return test.TestName.FullName.StartsWith(typeof(ActionAttributeExceptionFixture).FullName);
            }
        }

        private TestSuite _Suite = null;

        [TestFixtureSetUp]
        public void Setup()
        {
            TestSuiteBuilder builder = new TestSuiteBuilder();
            TestPackage package = new TestPackage(AssemblyHelper.GetAssemblyPath(typeof(ActionAttributeExceptionFixture)));
            package.TestName = typeof(ActionAttributeExceptionFixture).Namespace;

            _Suite = builder.Build(package);
        }

        public TestResult RunTest()
        {
            return _Suite.Run(new NullListener(), new Filter());
        }

        private TestResult FindFailureTestResult(TestResult result)
        {
            while (result.FailureSite == FailureSite.Child && result.Results != null && result.Results.Count > 0)
                result = (TestResult)result.Results[0];

            return result;
        }

        [Test]
        public void BeforeTestException()
        {
            ExceptionThrowingActionAttribute.Reset();
            ExceptionThrowingActionAttribute.ThrowBeforeException = true;

            ActionAttributeExceptionFixture.Reset();

            TestResult result = FindFailureTestResult(RunTest());

            Assert.IsTrue(result.FailureSite == FailureSite.SetUp);
            Assert.IsFalse(ActionAttributeExceptionFixture.TestRun);
        }

        [Test]
        public void AfterTestException()
        {
            ExceptionThrowingActionAttribute.Reset();
            ExceptionThrowingActionAttribute.ThrowAfterException = true;

            ActionAttributeExceptionFixture.Reset();

            TestResult result = FindFailureTestResult(RunTest());

            Assert.IsTrue(result.FailureSite == FailureSite.TearDown);
            Assert.IsTrue(ActionAttributeExceptionFixture.TestRun);
        }
    }
}
#endif
