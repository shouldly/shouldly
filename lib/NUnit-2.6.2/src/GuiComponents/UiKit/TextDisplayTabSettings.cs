// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using NUnit.Util;

namespace NUnit.UiKit
{
	public class TextDisplayTabSettings
	{
		private TabInfoCollection tabInfo;
		private NUnit.Util.ISettings settings;

		public static readonly string Prefix = "Gui.TextOutput.";

		public void LoadSettings()
		{
			LoadSettings( NUnit.Util.Services.UserSettings );
		}

		public void LoadSettings(NUnit.Util.ISettings settings)
		{
			this.settings = settings;

			TabInfoCollection info = new TabInfoCollection();
			string tabList = (string)settings.GetSetting( Prefix + "TabList" );

			if ( tabList != null ) 
			{
				string[] tabNames = tabList.Split( new char[] { ',' } );
				foreach( string name in tabNames )
				{
					string prefix = Prefix + name;
					string text = (string)settings.GetSetting(prefix + ".Title");
					if ( text == null )
						break;

					TabInfo tab = new TabInfo( name, text );

                    tab.Content = TextDisplayContent.FromSettings(name);
					tab.Enabled = settings.GetSetting( prefix + ".Enabled", true );

					info.Add( tab );
				}
			}

			if ( info.Count > 0 )		
				tabInfo = info;
			else 
				LoadDefaults();
		}

		public void LoadDefaults()
		{
			tabInfo = new TabInfoCollection();

            TabInfo tab = tabInfo.AddNewTab("Text Output");
		    tab.Content = new TextDisplayContent();
            tab.Content.Out = true;
            tab.Content.Error = true;
            tab.Content.Labels = TestLabelLevel.On;
		    tab.Enabled = true;
        }

		public void ApplySettings()
		{
			System.Text.StringBuilder tabNames = new System.Text.StringBuilder();
			foreach( TabInfo tab in tabInfo )
			{
				if ( tabNames.Length > 0 )
					tabNames.Append(",");
				tabNames.Append( tab.Name );

				string prefix = Prefix + tab.Name;

				settings.SaveSetting( prefix + ".Title", tab.Title );
				settings.SaveSetting( prefix + ".Enabled", tab.Enabled );
                tab.Content.SaveSettings(tab.Name);
			}

			string oldNames = settings.GetSetting( Prefix + "TabList", string.Empty );
			settings.SaveSetting( Prefix + "TabList", tabNames.ToString() );

			if (oldNames != string.Empty )
			{
				string[] oldTabs = oldNames.Split( new char[] { ',' } );
				foreach( string tabName in oldTabs )
					if ( tabInfo[tabName] == null )
						settings.RemoveGroup( Prefix + tabName );
			}
		}

		public TabInfoCollection Tabs
		{
			get { return tabInfo; }
        }

        #region Nested TabInfo Class

        public class TabInfo
		{
			public TabInfo( string name, string title )
			{
				this.Name = name;
				this.Title = title;
                this.Enabled = true;
                this.Content = new TextDisplayContent();
			}

            public string Name { get; set; }
            public string Title { get; set; }
            public TextDisplayContent Content { get; set; }
            public bool Enabled { get; set; }
        }

        #endregion

        #region Nested TabInfoCollectionClass

        public class TabInfoCollection : System.Collections.Generic.List<TabInfo>
		{
			public TabInfo AddNewTab( string title )
			{
				TabInfo tabInfo = new TabInfo( GetNextName(), title );
                this.Add(tabInfo);
				return tabInfo;
			}

			private string GetNextName()
			{
				for( int i = 0;;i++ )
				{
					string name = string.Format( "Tab{0}", i );
					if ( this[name] == null )
						return name;
				}
			}

			public TabInfo this[string name]
			{
				get
				{
					foreach ( TabInfo info in this )
						if ( info.Name == name )
							return info;

					return null;
				}
			}

			public bool Contains( string name )
			{
				return this[name] != null;
			}
        }

        #endregion
    }
}
