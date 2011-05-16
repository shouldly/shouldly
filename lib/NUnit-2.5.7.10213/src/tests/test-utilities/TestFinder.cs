// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Core;
using NUnit.Framework;

namespace NUnit.TestUtilities
{
	/// <summary>
	/// Utility class used to locate tests by name in a test tree
	/// </summary>
	public class TestFinder
	{
		public static Test Find(string name, Test test, bool recursive)
		{
			if (test.Tests != null)
			{
				foreach(Test child in test.Tests) 
				{
                    if (child.TestName.Name == name)
                        return child;
                    if (recursive)
                    {
                        Test grandchild = Find(name, child, true);
                        if (grandchild != null)
                            return grandchild;
                    }
				}
			}

			return null;
		}
		
		public static TestResult Find(string name, TestResult result, bool recursive) 
		{
			if ( result.HasResults )
			{
                foreach (TestResult childResult in result.Results) 
				{
                    if (childResult.Test.TestName.Name == name)
                        return childResult;

                    if (recursive)
                    {
                        TestResult r = Find(name, childResult, true);
                        if (r != null)
                            return r;
                    }
				}
			}

			return null;
		}

        private TestFinder() { }
	}
}
