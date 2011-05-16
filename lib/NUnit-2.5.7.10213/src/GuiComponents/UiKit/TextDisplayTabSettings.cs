// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;

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
					tab.Content = (TextDisplayContent)settings.GetSetting(prefix + ".Content", TextDisplayContent.Empty );
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
		    tab.Content =
		        TextDisplayContent.Out |
		        TextDisplayContent.Error |
		        TextDisplayContent.Trace |
		        TextDisplayContent.Log |
		        TextDisplayContent.Labels |
		        TextDisplayContent.LabelOnlyOnOutput;
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
				settings.SaveSetting( prefix + ".Content", tab.Content );
				settings.SaveSetting( prefix + ".Enabled", tab.Enabled );
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
	
		public class TabInfo
		{
			public string Name;
			public string Title;
			public TextDisplayContent Content = TextDisplayContent.Empty;
			public bool Enabled = true;

			public TabInfo( string name, string title )
			{
				this.Name = name;
				this.Title = title;
			}
		}

		public class TabInfoCollection : CollectionBase
		{
			public void Add( TabInfo tabInfo )
			{
				InnerList.Add( tabInfo );
			}

			public TabInfo AddNewTab( string title )
			{
				TabInfo tabInfo = new TabInfo( GetNextName(), title );
				InnerList.Add( tabInfo );
				return tabInfo;
			}

			public void Insert( int index, TabInfo tabInfo )
			{
				InnerList.Insert(index, tabInfo);
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

			public TabInfo this[int index]
			{
				get { return (TabInfo)InnerList[index]; }
				set { InnerList[index] = value; }
			}

			public TabInfo this[string name]
			{
				get
				{
					foreach ( TabInfo info in InnerList )
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
	}
}
