// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Collections;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// This fixture tests both AssemblyList and AssemblyListItem
	/// </summary>
	[TestFixture]
	public class AssemblyListTests
	{
		private AssemblyList assemblies;

        private string path1;
        private string path2;
        private string path3;

		private int events = 0;

		[SetUp]
		public void CreateAssemblyList()
		{
			assemblies = new AssemblyList();

            path1 = CleanPath("/tests/bin/debug/assembly1.dll");
            path2 = CleanPath("/tests/bin/debug/assembly2.dll");
            path3 = CleanPath("/tests/bin/debug/assembly3.dll");

			events = 0;

			assemblies.Changed += new EventHandler( assemblies_Changed );
        }

		private void assemblies_Changed( object sender, EventArgs e )
		{
			++events;
		}

		[Test]
		public void EmptyList()
		{
			Assert.AreEqual( 0, assemblies.Count );
		}

		[Test]
		public void CanAddAssemblies()
		{
			assemblies.Add( path1 );
			assemblies.Add( path2 );

			Assert.AreEqual( 2, assemblies.Count );
			Assert.AreEqual( path1, assemblies[0] );
			Assert.AreEqual( path2, assemblies[1] );
		}

		[Test, ExpectedException( typeof( ArgumentException ) )]
		public void MustAddAbsolutePath()
		{
			assemblies.Add( CleanPath( "bin/debug/assembly1.dll" ) );
		}

		[Test]
		public void AddFiresChangedEvent()
		{
			assemblies.Add( path1 );
			Assert.AreEqual( 1, events );
		}

		[Test]
		public void CanRemoveAssemblies()
		{
            assemblies.Add(path1);
            assemblies.Add(path2);
            assemblies.Add(path3);
			assemblies.Remove( path2 );

			Assert.AreEqual( 2, assemblies.Count );
			Assert.AreEqual( path1, assemblies[0] );
			Assert.AreEqual( path3, assemblies[1] );
		}

		[Test]
		public void RemoveAtFiresChangedEvent()
		{
			assemblies.Add( path1 );
			assemblies.RemoveAt(0);
			Assert.AreEqual( 2, events );
		}

		[Test]
		public void RemoveFiresChangedEvent()
		{
			assemblies.Add( path1 );
			assemblies.Remove( path1 );
			Assert.AreEqual( 2, events );
		}

		[Test]
		public void SettingFullPathFiresChangedEvent()
		{
			assemblies.Add( path1 );
			assemblies[0] = path2;
			Assert.AreEqual( 2, events );
		}
		
        private string CleanPath( string path )
        {
            return path.Replace( '/', Path.DirectorySeparatorChar );
        }
	}
}
