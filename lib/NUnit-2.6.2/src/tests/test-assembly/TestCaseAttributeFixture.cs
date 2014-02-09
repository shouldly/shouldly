// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Framework;

namespace NUnit.TestData
{
    [TestFixture]
    public class TestCaseAttributeFixture
    {
        [TestCase(2,3,4,Description="My Description")]
        public void MethodHasDescriptionSpecified(int x, int y, int z)
        {}

		[TestCase(2,3,4,TestName="XYZ")]
		public void MethodHasTestNameSpecified(int x, int y, int z)
		{}

        [TestCase(2, 3, 4, Category = "XYZ")]
        public void MethodHasSingleCategory(int x, int y, int z)
        { }

        [TestCase(2, 3, 4, Category = "X,Y,Z")]
        public void MethodHasMultipleCategories(int x, int y, int z)
        { }

        [TestCase(2, 2000000, Result = 4)]
		public int MethodCausesConversionOverflow(short x, short y)
		{
			return x + y;
		}

		[TestCase("12-Octobar-1942")]
		public void MethodHasInvalidDateFormat(DateTime dt)
		{}

        [TestCase(2, 3, 4, ExpectedException = typeof(ArgumentNullException))]
        public void MethodThrowsExpectedException(int x, int y, int z)
        {
            throw new ArgumentNullException();
        }

        [TestCase(2, 3, 4, ExpectedException = typeof(ArgumentNullException))]
        public void MethodThrowsWrongException(int x, int y, int z)
        {
            throw new ArgumentException();
        }

        [TestCase(2, 3, 4, ExpectedException = typeof(ArgumentNullException))]
        public void MethodThrowsNoException(int x, int y, int z)
        {
        }

        [TestCase(2, 3, 4, ExpectedException = typeof(ApplicationException),
            ExpectedMessage="Test Exception")]
        public void MethodThrowsExpectedExceptionWithWrongMessage(int x, int y, int z)
        {
            throw new ApplicationException("Wrong Test Exception");
        }

        [TestCase(2, 3, 4, ExpectedException = typeof(ArgumentNullException))]
        public void MethodCallsIgnore(int x, int y, int z)
        {
            Assert.Ignore("Ignore this");
        }

        [TestCase(1)]
        [TestCase(2, Ignore = true)]
        [TestCase(3, IgnoreReason = "Don't Run Me!")]
        public void MethodWithIgnoredTestCases(int num)
        {
        }

        [TestCase(1)]
        [TestCase(2, Explicit = true)]
        [TestCase(3, Explicit = true, Reason = "Connection failing")]
        public void MethodWithExplicitTestCases(int num)
        {
        }
    }
}
