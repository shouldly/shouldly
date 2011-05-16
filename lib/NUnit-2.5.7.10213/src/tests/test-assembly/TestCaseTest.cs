// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.TestCaseTest
{
	[TestFixture]
	public class HasCategories 
	{
		[Test] 
		[Category("A category")]
		[Category("Another Category")]
		public void ATest()
		{}
	}
}
