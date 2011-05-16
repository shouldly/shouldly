// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;

using NUnit.Framework;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class AssemblyVersionFixture
	{
		private static readonly string mockAssemblyName = "mock-test-assembly.dll";
		
		[TearDown]
		public void DeleteMockAssembly()
		{
			FileInfo info = new FileInfo(mockAssemblyName);
			if(info.Exists)
				info.Delete();
		}

		[Test]
		public void Version()
		{
			Version version = new Version("1.0.0.2002");
			string nameString = "TestAssembly";

			AssemblyName assemblyName = new AssemblyName(); 
			assemblyName.Name = nameString;
			assemblyName.Version = version;
			MakeDynamicAssembly(assemblyName, mockAssemblyName);

			Assembly assembly = FindAssemblyByName(nameString);

			System.Version foundVersion = assembly.GetName().Version;
			Assert.AreEqual(version, foundVersion);
		}

		private Assembly FindAssemblyByName(string name)
		{
			// Get all the assemblies currently loaded in the application domain.
			Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();

			Assembly assembly = null;
			for(int i = 0; i < myAssemblies.Length && assembly == null; i++)
			{
				if(String.Compare(myAssemblies[i].GetName().Name, name) == 0)
					assembly = myAssemblies[i];
			}
			return assembly;
		}

		public static void MakeDynamicAssembly(AssemblyName myAssemblyName, string fileName)
		{
			AssemblyBuilder myAssemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.RunAndSave);			
			myAssemblyBuilder.Save(fileName);
		}
	}
}
