// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using NUnit.Framework;
using NUnit.Util;
using NUnit.TestData;
using NUnit.TestUtilities;
using System.Collections;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class TestCaseSourceTests
    {
        [Test, TestCaseSource("StaticProperty")]
        public void SourceCanBeStaticProperty(string source)
        {
            Assert.AreEqual("StaticProperty", source);
        }

        static IEnumerable StaticProperty
        {
            get { return new object[] { new object[] { "StaticProperty" } }; }
        }

        [Test, TestCaseSource("InstanceProperty")]
        public void SourceCanBeInstanceProperty(string source)
        {
            Assert.AreEqual("InstanceProperty", source);
        }

        IEnumerable InstanceProperty
        {
            get { return new object[] { new object[] { "InstanceProperty" } }; }
        }

        [Test, TestCaseSource("StaticMethod")]
        public void SourceCanBeStaticMethod(string source)
        {
            Assert.AreEqual("StaticMethod", source);
        }

        static IEnumerable StaticMethod()
        {
            return new object[] { new object[] { "StaticMethod" } };
        }

        [Test, TestCaseSource("InstanceMethod")]
        public void SourceCanBeInstanceMethod(string source)
        {
            Assert.AreEqual("InstanceMethod", source);
        }

        IEnumerable InstanceMethod()
        {
            return new object[] { new object[] { "InstanceMethod" } };
        }

        [Test, TestCaseSource("StaticField")]
        public void SourceCanBeStaticField(string source)
        {
            Assert.AreEqual("StaticField", source);
        }

        static object[] StaticField =
            { new object[] { "StaticField" } };

        [Test, TestCaseSource("InstanceField")]
        public void SourceCanBeInstanceField(string source)
        {
            Assert.AreEqual("InstanceField", source);
        }

        static object[] InstanceField =
            { new object[] { "InstanceField" } };

        [Test, TestCaseSource("CheckCurrentDirectory")]
        public void SourceIsInvokedWithCorrectCurrentDirectory(bool isOK)
        {
            Assert.That(isOK);
        }

        [Test, TestCaseSource("MyData")]
        public void SourceMayReturnArgumentsAsObjectArray(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [TestCaseSource("MyData")]
        public void TestAttributeIsOptional(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [Test, TestCaseSource("MyIntData")]
        public void SourceMayReturnArgumentsAsIntArray(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [Test, TestCaseSource("EvenNumbers")]
        public void SourceMayReturnSinglePrimitiveArgumentAlone(int n)
        {
            Assert.AreEqual(0, n % 2);
        }

        [Test, TestCaseSource("Params")]
        public int SourceMayReturnArgumentsAsParamSet(int n, int d)
        {
            return n / d;
        }

        [Test]
        [TestCaseSource("MyData")]
        [TestCaseSource("MoreData")]
        [TestCase(12, 0, 0, ExpectedException = typeof(System.DivideByZeroException))]
        public void TestMayUseMultipleSourceAttributes(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [Test, TestCaseSource("FourArgs")]
        public void TestWithFourArguments(int n, int d, int q, int r)
        {
            Assert.AreEqual(q, n / d);
            Assert.AreEqual(r, n % d);
        }

        [Test, Category("Top"), TestCaseSource(typeof(DivideDataProvider), "HereIsTheData")]
        public void SourceMayBeInAnotherClass(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [Test, TestCaseSource(typeof(DivideDataProviderWithReturnValue), "TestCases")]
        public int SourceMayBeInAnotherClassWithReturn(int n, int d)
        {
            return n / d;
        }

        [TestCaseSource("ExpectNull")]
        public object ExpectedResultCanBeNull()
        {
            return null;
        }

        [Test]
        public void CanSpecifyExpectedException()
        {
            Test test = (Test)TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodThrowsExpectedException").Tests[0];
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Success, result.ResultState);
        }

        [Test]
        public void CanSpecifyExpectedException_WrongException()
        {
            Test test = (Test)TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodThrowsWrongException").Tests[0];
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            StringAssert.StartsWith("An unexpected exception type was thrown", result.Message);
        }

        [Test]
        public void CanSpecifyExpectedException_NoneThrown()
        {
            Test test = (Test)TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodThrowsNoException").Tests[0];
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            Assert.AreEqual("System.ArgumentNullException was expected", result.Message);
        }

        [Test]
        public void IgnoreTakesPrecedenceOverExpectedException()
        {
            Test test = (Test)TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodCallsIgnore").Tests[0];
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Ignored, result.ResultState);
            Assert.AreEqual("Ignore this", result.Message);
        }

        [Test]
        public void CanIgnoreIndividualTestCases()
        {
            Test test = TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodWithIgnoredTestCases");
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);

            ResultSummarizer summary = new ResultSummarizer(result);
            Assert.AreEqual( 3, summary.ResultCount );
            Assert.AreEqual( 2, summary.Ignored );
            Assert.AreEqual( "Don't Run Me!", ((TestResult)result.Results[2]).Message );
        }

        [Test]
        public void HandlesExceptionInTestCaseSource()
        {
            Test test = (Test)TestBuilder.MakeTestCase(
                typeof(TestCaseSourceAttributeFixture), "MethodWithSourceThrowingException").Tests[0];
            Assert.AreEqual(RunState.NotRunnable, test.RunState);
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.NotRunnable, result.ResultState);
            Assert.AreEqual("System.Exception : my message", result.Message);
        }

        [TestCaseSource("exception_source"), Explicit]
        public void HandlesExceptioninTestCaseSource_GuiDisplay(string lhs, string rhs)
        {
            Assert.AreEqual(lhs, rhs);
        }

        object[] testCases =
        {
            new TestCaseData(
                new string[] { "A" },
                new string[] { "B" })
        };

        [Test, TestCaseSource("testCases")]
        public void MethodTakingTwoStringArrays(string[] a, string[] b)
        {
            Assert.That(a, Is.TypeOf(typeof(string[])));
            Assert.That(b, Is.TypeOf(typeof(string[])));
        }

        #region Sources used by the tests
        static object[] MyData = new object[] {
            new object[] { 12, 3, 4 },
            new object[] { 12, 4, 3 },
            new object[] { 12, 6, 2 } };

        static object[] MyIntData = new object[] {
            new int[] { 12, 3, 4 },
            new int[] { 12, 4, 3 },
            new int[] { 12, 6, 2 } };

        static object[] FourArgs = new object[] {
            new TestCaseData( 12, 3, 4, 0 ),
            new TestCaseData( 12, 4, 3, 0 ),
            new TestCaseData( 12, 5, 2, 2 ) };

        static int[] EvenNumbers = new int[] { 2, 4, 6, 8 };

        static object[] ExpectNull = new object[] {
            new TestCaseData().Returns(null) };

        private static IEnumerable CheckCurrentDirectory
        {
            get
            {
                return new object[] { new object[] { System.IO.File.Exists("nunit.core.tests.dll") } };
            }
        }

        static object[] MoreData = new object[] {
            new object[] { 12, 1, 12 },
            new object[] { 12, 2, 6 } };

        static object[] Params = new object[] {
            new TestCaseData(24, 3).Returns(8),
            new TestCaseData(24, 2).Returns(12) };

        private class DivideDataProvider
        {
            public static IEnumerable HereIsTheData
            {
                get
                {
#if NET_2_0
                    yield return new TestCaseData(0, 0, 0)
                        .SetName("ThisOneShouldThrow")
                        .SetDescription("Demonstrates use of ExpectedException")
                        .SetCategory("Junk")
                        .SetProperty("MyProp", "zip")
                        .Throws(typeof(System.DivideByZeroException));
                    yield return new object[] { 100, 20, 5 };
                    yield return new object[] { 100, 4, 25 };
#else
                    ArrayList list = new ArrayList();
                    list.Add(
                        new TestCaseData( 0, 0, 0)
                            .SetName("ThisOneShouldThrow")
                            .SetDescription("Demonstrates use of ExpectedException")
							.SetCategory("Junk")
							.SetProperty("MyProp", "zip")
							.Throws( typeof (System.DivideByZeroException) ));
                    list.Add(new object[] { 100, 20, 5 });
                    list.Add(new object[] {100, 4, 25});
                    return list;
#endif
                }
            }
        }

        public class DivideDataProviderWithReturnValue
        {
            public static IEnumerable TestCases
            {
                get
                {
                    return new object[] {
                        new TestCaseData(12, 3).Returns(5).Throws(typeof(AssertionException)).SetName("TC1"),
                        new TestCaseData(12, 2).Returns(6).SetName("TC2"),
                        new TestCaseData(12, 4).Returns(3).SetName("TC3")
                    };
                }
            }
        }

        private static IEnumerable exception_source
        {
            get
            {
#if NET_2_0
                yield return new TestCaseData("a", "a");
                yield return new TestCaseData("b", "b");
#endif

                throw new System.Exception("my message");
            }
        }
        #endregion
    }
}
