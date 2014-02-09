// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Collections;
using NUnit.Framework;

namespace NUnit.TestData
{
    [TestFixture]
    public class TestCaseSourceAttributeFixture
    {
        [TestCaseSource("source")]
        public void MethodThrowsExpectedException(int x, int y, int z)
        {
            throw new ArgumentNullException();
        }

        [TestCaseSource("source")]
        public void MethodThrowsWrongException(int x, int y, int z)
        {
            throw new ArgumentException();
        }

        [TestCaseSource("source")]
        public void MethodThrowsNoException(int x, int y, int z)
        {
        }

        [TestCaseSource("source")]
        public string MethodThrowsNoExceptionButReturnsResult(int x, int y, int z)
        {
            return "HELLO";
        }

        [TestCaseSource("source")]
        public void MethodCallsIgnore(int x, int y, int z)
        {
            Assert.Ignore("Ignore this");
        }

        internal static object[] source = new object[] {
            new TestCaseData( 2, 3, 4 ).Throws(typeof(ArgumentNullException)) };

        [TestCaseSource("exception_source")]
        public void MethodWithSourceThrowingException(string lhs, string rhs)
        {
        }

        [TestCaseSource("ignored_source")]
        public void MethodWithIgnoredTestCases(int num)
        {
        }

        [TestCaseSource("explicit_source")]
        public void MethodWithExplicitTestCases(int num)
        {
        }

        private static IEnumerable ignored_source
        {
            get
            {
                return new object[] {
                    new TestCaseData(1),
                    new TestCaseData(2).Ignore(),
                    new TestCaseData(3).Ignore("Don't Run Me!")
                };
            }
        }

        private static IEnumerable explicit_source
        {
            get
            {
                return new object[] {
                    new TestCaseData(1),
                    new TestCaseData(2).MakeExplicit(),
                    new TestCaseData(3).MakeExplicit("Connection failing")
                };
            }
        }

        private static IEnumerable exception_source
        {
            get
            {
#if CLR_2_0 || CLR_4_0
                yield return new TestCaseData("a", "a");
                yield return new TestCaseData("b", "b");
#endif

                throw new System.Exception("my message");
            }
        }
    }
}
