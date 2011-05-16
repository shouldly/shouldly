// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Tests
{
	/// <summary>
	/// MessageCheckingTest is an abstract base for tests
	/// that check for an expected message in the exception
	/// handler.
	/// </summary>
	public abstract class MessageChecker : AssertionHelper, IExpectException
	{
		protected string expectedMessage;

		[SetUp]
		public void SetUp()
		{
			expectedMessage = null;
		}

		public void HandleException( Exception ex )
		{
			if ( expectedMessage != null )
				Assert.AreEqual( expectedMessage, ex.Message );
		}
	}
}
