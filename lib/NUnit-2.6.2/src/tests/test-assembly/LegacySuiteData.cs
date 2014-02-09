// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Core;

namespace NUnit.TestData.LegacySuiteData
{
	public class Suite
	{
		[Suite]
		public static TestSuite MockSuite
		{
			get 
			{
				TestSuite testSuite = new TestSuite("TestSuite");
				return testSuite;
			}
		}
	}

	class NonConformingSuite
	{
		[Suite]
		public static int Integer
		{
			get 
			{
				return 5;
			}
		}
	}

    public class LegacySuiteReturningFixtureWithArguments
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();
                suite.Add(new TestClass(5));
                return suite;
            }
        }

        [TestFixture]
        public class TestClass
        {
            public int num;

            public TestClass(int num)
            {
                this.num = num;
            }
        }
    }
}
