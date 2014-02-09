// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;

namespace NUnit.TestData
{
	public class DirectoryChangeFixture
	{
		[Test]
		public void ChangeCurrentDirectory()
		{
			Environment.CurrentDirectory = Path.GetTempPath();
		}
	}
}

