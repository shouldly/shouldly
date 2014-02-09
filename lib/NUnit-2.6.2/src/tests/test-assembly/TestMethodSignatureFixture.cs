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
	public class TestMethodSignatureFixture
	{
		public static int Tests = 20;
		public static int Runnable = 12;
		public static int NotRunnable = 8;
	    public static int Errors = 3;
	    public static int Failures = 0;

		[Test]
		public void InstanceTestMethod() { }

		[Test]
		public static void StaticTestMethod() { }

        [Test]
        public void TestMethodWithArgumentsNotProvided(int x, int y, string label) { }

        [Test]
        public static void StaticTestMethodWithArgumentsNotProvided(int x, int y, string label) { }

		[TestCase(5, 2, "ABC")]
		public void TestMethodWithoutParametersWithArgumentsProvided() { }

        [TestCase(5, 2, "ABC")]
        public void TestMethodWithArgumentsProvided(int x, int y, string label)
        {
            Assert.AreEqual(5, x);
            Assert.AreEqual(2, y);
            Assert.AreEqual("ABC", label);
        }

        [TestCase(5, 2, "ABC")]
        public static void StaticTestMethodWithArgumentsProvided(int x, int y, string label)
        {
            Assert.AreEqual(5, x);
            Assert.AreEqual(2, y);
            Assert.AreEqual("ABC", label);
        }

        [TestCase(2, 2)]
        public void TestMethodWithWrongNumberOfArgumentsProvided(int x, int y, string label)
        {
        }

        [TestCase(2, 2, 3.5)]
        public void TestMethodWithWrongArgumentTypesProvided(int x, int y, string label)
        {
        }

        [TestCase(2, 2)]
        public static void StaticTestMethodWithWrongNumberOfArgumentsProvided(int x, int y, string label)
        {
        }

        [TestCase(2, 2, 3.5)]
        public static void StaticTestMethodWithWrongArgumentTypesProvided(int x, int y, string label)
        {
        }

        [TestCase(3.7, 2, 5.7)]
        public void TestMethodWithConvertibleArguments(double x, double y, double sum)
        {
            Assert.AreEqual(sum, x + y, 0.0001);
        }

        [TestCase(3.7, 2, 5.7)]
        public void TestMethodWithNonConvertibleArguments(int x, int y, int sum)
        {
            Assert.AreEqual(sum, x + y, 0.0001);
        }

        [TestCase(12, 3, 4)]
		[TestCase( 12, 2, 6 )]
		[TestCase( 12, 4, 3 )]
		public void TestMethodWithMultipleTestCases( int n, int d, int q )
		{
			Assert.AreEqual( q, n / d );
		}

        [TestCase(new int[] {1, 2, 3})]
        public void TestMethodWithParamsArgument(params int[] n)
        {
        }

//		[Test]
//		public abstract void AbstractTestMethod() { }

		[Test]
		protected void ProtectedTestMethod() { }

		[Test]
		private void PrivateTestMethod() { }

		[Test]
		public bool TestMethodWithReturnType() 
		{
			return true;
		}
	}
}
