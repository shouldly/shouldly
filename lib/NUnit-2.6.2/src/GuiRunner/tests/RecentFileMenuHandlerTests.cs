// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Util;

namespace NUnit.Gui.Tests
{
	[TestFixture]
	public class RecentFileMenuHandlerTests
	{
		private MenuItem menu;
		private RecentFiles files;
		private RecentFileMenuHandler handler;
		
		[SetUp]
		public void SetUp()
		{
			menu = new MenuItem();
			files = new FakeRecentFiles();
			handler = new RecentFileMenuHandler( menu, files );
            handler.CheckFilesExist = false;
        }

		[Test]
		public void DisableOnLoadWhenEmpty()
		{
			handler.Load();
			Assert.IsFalse( menu.Enabled );
		}

		[Test]
		public void EnableOnLoadWhenNotEmpty()
		{
			files.SetMostRecent( "Test" );
			handler.Load();
			Assert.IsTrue( menu.Enabled );
		}
		[Test]
		public void LoadMenuItems()
		{
			files.SetMostRecent( "Third" );
			files.SetMostRecent( "Second" );
			files.SetMostRecent( "First" );
			handler.Load();
			Assert.AreEqual( 3, menu.MenuItems.Count );
			Assert.AreEqual( "1 First", menu.MenuItems[0].Text );
		}

		private class FakeRecentFiles : RecentFiles
		{
			private RecentFilesCollection files = new RecentFilesCollection();
			private int maxFiles = 24;

			public int Count
			{
				get { return files.Count; }
			}

			public int MaxFiles
			{
				get { return maxFiles; }
				set { maxFiles = value; }
			}

			public void SetMostRecent( string fileName )
			{
				SetMostRecent( new RecentFileEntry( fileName ) );
			}

			public void SetMostRecent( RecentFileEntry entry )
			{
				files.Insert( 0, entry );
			}

			public RecentFilesCollection Entries
			{
				get { return files; }
			}

			public void Clear()
			{
				files.Clear();
			}

			public void Remove( string fileName )
			{
				files.Remove( fileName );
			}
		}
	
		// TODO: Need mock loader to test clicking
	}
}
