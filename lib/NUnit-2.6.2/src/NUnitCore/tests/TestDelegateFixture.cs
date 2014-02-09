// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Threading;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for TestDelegate.
	/// </summary>
	/// 
	[TestFixture]
	public class TestDelegateFixture
	{
		internal class TestDelegate 
		{ 
			public bool delegateCalled = false;
			public System.IAsyncResult ar;

			public delegate void CallBackFunction(); 

			public TestDelegate() 
			{ 
				ar = new CallBackFunction 
					(DoSomething).BeginInvoke 
					(null,null); 
			} 

			public void DoSomething() 
			{ 
				delegateCalled = true;
			} 
		} 

		[Test]
		public void DelegateTest()
		{
			TestDelegate testDelegate = new TestDelegate(); 
			testDelegate.ar.AsyncWaitHandle.WaitOne(1000, false );
			Assert.IsTrue(testDelegate.delegateCalled);
		}
	}
} 

