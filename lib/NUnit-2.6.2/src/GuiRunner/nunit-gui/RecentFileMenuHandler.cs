// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.Gui
{
	public class RecentFileMenuHandler
	{
		private MenuItem menu;
		private RecentFiles recentFiles;
        private bool checkFilesExist = true;
		private bool showNonRunnableFiles = false;

		public RecentFileMenuHandler( MenuItem menu, RecentFiles recentFiles )
		{
			this.menu = menu;
			this.recentFiles = recentFiles;
		}

		public bool CheckFilesExist
		{
			get { return checkFilesExist; }
			set { checkFilesExist = value; }
		}

		public bool ShowNonRunnableFiles
		{
			get { return showNonRunnableFiles; }
			set { showNonRunnableFiles = value; }
		}

		public MenuItem Menu
		{
			get { return menu; }
		}

		public string this[int index]
		{
			get { return menu.MenuItems[index].Text.Substring( 2 ); }
		}

		public void Load()
		{
			if ( recentFiles.Count == 0 )
				Menu.Enabled = false;
			else 
			{
				Menu.Enabled = true;
				Menu.MenuItems.Clear();
				int index = 1;
				foreach ( RecentFileEntry entry in recentFiles.Entries ) 
				{
                    // Rather than show files that don't exist, we skip them. As
                    // new recent files are opened, they will be pushed down and
                    // eventually fall off the list unless the file is re-created
					// and subsequently opened.
                    if ( !checkFilesExist || entry.Exists )
                    {
						// NOTE: In the current version, all the files listed should
						// have a compatible version, since we are using separate
						// settings for V1 and V2. This code will be changed in
						// a future release to allow running under other runtimes.
						if ( showNonRunnableFiles || entry.IsCompatibleCLRVersion )
						{
							MenuItem item = new MenuItem(String.Format("{0} {1}", index++, entry.Path));
							item.Click += new System.EventHandler(OnRecentFileClick);
							Menu.MenuItems.Add(item);
						}
                    }
				}		
			}
		}

		private void OnRecentFileClick( object sender, EventArgs e )
		{
			MenuItem item = (MenuItem) sender;
			string testFileName = item.Text.Substring( 2 );

            // TODO: Figure out a better way
            NUnitForm form = item.GetMainMenu().GetForm() as NUnit.Gui.NUnitForm;
            if ( form != null)
                form.Presenter.OpenProject( testFileName ); 
		}
	}
}
