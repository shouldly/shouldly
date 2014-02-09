// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.IO;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Tests of the DirectorySwapper class
	/// </summary>
	[TestFixture]
	public class DirectorySwapperTests
	{
		private string thisDirectory;
		private string driveRoot;
		
		[SetUp]
		public void Init()
		{
			thisDirectory = Environment.CurrentDirectory;
			driveRoot = Path.GetPathRoot( thisDirectory );
		}

		[TearDown]
		public void Cleanup()
		{
			Environment.CurrentDirectory = thisDirectory;
		}

		[Test]
		public void ChangeAndRestore()
		{
			using( new DirectorySwapper() )
			{
				Assert.AreEqual( thisDirectory, Environment.CurrentDirectory );
				Environment.CurrentDirectory = driveRoot;
				Assert.AreEqual( driveRoot, Environment.CurrentDirectory );
			}

			Assert.AreEqual( thisDirectory, Environment.CurrentDirectory );
		}

		[Test]
		public void SwapAndRestore()
		{
			using( new DirectorySwapper( driveRoot ) )
			{
				Assert.AreEqual( driveRoot, Environment.CurrentDirectory );
			}

			Assert.AreEqual( thisDirectory, Environment.CurrentDirectory );
		}
	}
}
