// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Core;
using NUnit.Framework;

namespace NUnit.TestUtilities
{
    public class TestAssert
    {
        #region IsRunnable
        public static void IsRunnable(Test test)
        {
            Assert.AreEqual(RunState.Runnable, test.RunState);
        }

        public static void IsRunnable(Type type)
        {
            TestSuite suite = TestBuilder.MakeFixture(type);
            Assert.NotNull(suite, "Unable to construct fixture");
            Assert.AreEqual(RunState.Runnable, suite.RunState);
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Success, result.ResultState);
        }

        public static void IsRunnable(Type type, string name)
        {
            IsRunnable(type, name, ResultState.Success);
        }

        public static void IsRunnable(Type type, string name, ResultState resultState)
        {
            Test test = TestBuilder.MakeTestCase(type, name);
            Assert.That(test.RunState, Is.EqualTo(RunState.Runnable));
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            if (result.HasResults)
                result = (TestResult)result.Results[0];
            Assert.That(result.ResultState, Is.EqualTo(resultState));
        }
        #endregion

        #region IsNotRunnable
        public static void IsNotRunnable(Test test)
        {
            Assert.AreEqual(RunState.NotRunnable, test.RunState);
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.NotRunnable, result.ResultState);
        }

        public static void IsNotRunnable(Type type)
        {
            TestSuite fixture = TestBuilder.MakeFixture(type);
            Assert.NotNull(fixture, "Unable to construct fixture");
            IsNotRunnable(fixture);
        }

        public static void IsNotRunnable(Type type, string name)
        {
            IsNotRunnable(TestBuilder.MakeTestCase(type, name));
        }

        public static void ChildNotRunnable(Type type, string name)
        {
            IsNotRunnable((Test)TestBuilder.MakeTestCase(type, name).Tests[0]);
        }
        #endregion
        
        private TestAssert() { }
    }
}
