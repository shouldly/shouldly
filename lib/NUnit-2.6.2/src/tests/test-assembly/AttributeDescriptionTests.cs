// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.AttributeDescriptionFixture
{
	[TestFixture(Description = "Fixture Description")]
	public class MockFixture
	{
		[Test(Description = "Test Description")]
		public void Method()
		{}

		[Test]
		public void NoDescriptionMethod()
		{}

        [Test]
        [Description("Separate Description")]
        public void SeparateDescriptionMethod()
        { }
	}
}
