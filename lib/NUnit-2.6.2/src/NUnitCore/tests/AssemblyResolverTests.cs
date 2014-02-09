// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
namespace NUnit.Core.Tests
{
	using System;
	using NUnit.Framework;
	using System.IO;

	[TestFixture]
	public class AssemblyResolverTests
	{
		string appBasePath;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			this.appBasePath = Environment.ExpandEnvironmentVariables(@"%temp%\AssemblyResolver");
			if (Directory.Exists(this.appBasePath))
			{
				Directory.Delete(this.appBasePath, true);
			}
			Directory.CreateDirectory(this.appBasePath);
		}


		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			Directory.Delete(this.appBasePath, true);
		}

		[Test]
		public void AddFile()
		{
			AppDomain domain = null;
			try
			{
				domain = AppDomain.CreateDomain("AssemblyResolver", null, this.appBasePath, null, false);
				AssemblyResolver assemblyResolver = (AssemblyResolver)domain.CreateInstanceFromAndUnwrap(typeof(AssemblyResolver).Assembly.CodeBase, typeof(AssemblyResolver).FullName);
				using (assemblyResolver)
				{
					Type type = typeof(TestAttribute);
					// ensure that assembly containing type can be found
					assemblyResolver.AddFile(type.Assembly.CodeBase);
					domain.CreateInstance(type.Assembly.FullName, type.FullName);
				}
			}
			finally
			{
				AppDomain.Unload(domain);
			}

		}
	}
}
