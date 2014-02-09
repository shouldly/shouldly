// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Framework;
using System.IO;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class AssemblyReaderTests
	{
		private AssemblyReader rdr;

		[SetUp]
		public void CreateReader()
		{
			rdr = new AssemblyReader( this.GetType().Assembly );
		}

		[TearDown]
		public void DisposeReader()
		{
			if ( rdr != null )
				rdr.Dispose();

			rdr = null;
		}

		[Test]
		public void CreateFromPath()
		{
            string path = AssemblyHelper.GetAssemblyPath(System.Reflection.Assembly.GetAssembly(GetType()));
            Assert.AreEqual(path, new AssemblyReader(path).AssemblyPath);
		}

		[Test]
		public void CreateFromAssembly()
		{
            string path = AssemblyHelper.GetAssemblyPath(System.Reflection.Assembly.GetAssembly(GetType()));
            Assert.AreEqual(path, rdr.AssemblyPath);
		}

		[Test]
		public void IsValidPeFile()
		{
			Assert.IsTrue( rdr.IsValidPeFile );
		}

		[Test]
		public void IsValidPeFile_Fails()
		{
			string configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			Assert.IsFalse( new AssemblyReader( configFile ).IsValidPeFile );
		}

		[Test]
		public void IsDotNetFile()
		{
			Assert.IsTrue( rdr.IsDotNetFile );
		}

		[Test]
		public void ImageRuntimeVersion()
		{
			string runtimeVersion = rdr.ImageRuntimeVersion;

			StringAssert.StartsWith( "v", runtimeVersion );
			new Version( runtimeVersion.Substring( 1 ) );
			// This fails when we force running under a prior version
			// Assert.LessOrEqual( version, Environment.Version );
		}

	}
}
