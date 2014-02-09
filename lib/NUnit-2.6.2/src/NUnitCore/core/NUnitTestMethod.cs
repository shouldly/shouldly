// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System.Reflection;

namespace NUnit.Core
{
	/// <summary>
	/// Class to implement an NUnit test method
	/// </summary>
	public class NUnitTestMethod : TestMethod
	{
		#region Constructor
		public NUnitTestMethod(MethodInfo method) : base(method) 
        {
        }
		#endregion

		#region TestMethod Overrides

        /// <summary>
		/// Run a test returning the result. Overrides TestMethod
		/// to count assertions.
		/// </summary>
		/// <param name="testResult"></param>
		public override TestResult RunTest()
		{
			TestResult testResult = base.RunTest();

			testResult.AssertCount = NUnitFramework.Assert.GetAssertCount();
			
			return testResult;
		}
        #endregion
	}
}
