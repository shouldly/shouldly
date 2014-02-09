// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using Microsoft.Win32;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// Summary description for UserSettingsService.
	/// </summary>
	public class SettingsService : SettingsGroup, NUnit.Core.IService
	{
		static readonly string settingsFileName = "NUnitSettings.xml";

        private bool writeable;

        public SettingsService() : this(true) { }

		public SettingsService(bool writeable)
		{
            this.writeable = writeable;
#if CLR_2_0 || CLR_4_0
            string settingsFile = System.Configuration.ConfigurationManager.AppSettings["settingsFile"];
#else
			string settingsFile = System.Configuration.ConfigurationSettings.AppSettings["settingsFile"];
#endif
			if ( settingsFile == null )
				settingsFile = Path.Combine( NUnitConfiguration.ApplicationDirectory, settingsFileName );

			this.storage = new XmlSettingsStorage( settingsFile, writeable );

			if ( File.Exists( settingsFile ) )
				storage.LoadSettings();
			else if (writeable)
				ConvertLegacySettings();
		}

		#region IService Implementation
		public void InitializeService()
		{
		}

		public void UnloadService()
		{
            if ( writeable )
			    storage.SaveSettings();

			this.Dispose();
		}
		#endregion

		#region ConvertLegacySettings
		void ConvertLegacySettings()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey( NUnitRegistry.KEY );
			if ( key == null )
				key = Registry.CurrentUser.OpenSubKey( NUnitRegistry.LEGACY_KEY );

			if ( key != null )
			{
				using( ISettingsStorage legacyStorage = new RegistrySettingsStorage( key ) )
				{
					new LegacySettingsConverter( legacyStorage, storage ).Convert();
				}

				storage.SaveSettings();
			}
		}

		private class LegacySettingsConverter : SettingsGroup
		{
			private ISettingsStorage legacy;

			public LegacySettingsConverter( ISettingsStorage legacy, ISettingsStorage current )
				: base( current )
			{
				this.legacy = legacy;
			}

			public void Convert()
			{
				Convert( "Form.x-location", "Gui.MainForm.Left" );
				Convert( "Form.x-location", "Gui.MiniForm.Left" );
				Convert( "Form.y-location", "Gui.MainForm.Top" );
				Convert( "Form.y-location", "Gui.MiniForm.Top" );
				Convert( "Form.width", "Gui.MainForm.Width" );
				Convert( "Form.width", "Gui.MiniForm.Width" );
				Convert( "Form.height", "Gui.MainForm.Height" );
				Convert( "Form.height", "Gui.MiniForm.Height" );
				Convert( "Form.maximized", "Gui.MainForm.Maximized", "False", "True" );
				Convert( "Form.maximized", "Gui.MiniForm.Maximized", "False", "True" );
				Convert( "Form.font", "Gui.MainForm.Font" );
				Convert( "Form.font", "Gui.MiniForm.Font" );
				Convert( "Form.tree-splitter-position", "Gui.MainForm.SplitPosition");
				Convert( "Form.tab-splitter-position", "Gui.ResultTabs.ErrorsTabSplitterPosition");
				Convert( "Options.TestLabels", "Gui.ResultTabs.DisplayTestLabels", "False", "True" );
				Convert( "Options.FailureToolTips", "Gui.ResultTabs.ErrorTab.ToolTipsEnabled", "False", "True" );
				Convert( "Options.EnableWordWrapForFailures", "Gui.ResultTabs.ErrorTab.WordWrapEnabled", "False", "True" );
				Convert( "Options.InitialTreeDisplay", "Gui.TestTree.InitialTreeDisplay", "Auto", "Expand", "Collapse", "HideTests"  );
				Convert( "Options.ShowCheckBoxes", "Gui.TestTree.ShowCheckBoxes", "False", "True" );
				Convert( "Options.LoadLastProject", "Options.LoadLastProject", "False", "True" );
				Convert( "Options.ClearResults", "Options.TestLoader.ClearResultsOnReload", "False", "True" ); 
				Convert( "Options.ReloadOnChange", "Options.TestLoader.ReloadOnChange", "False", "True" );
				Convert( "Options.RerunOnChange", "Options.TestLoader.RerunOnChange", "False", "True" );
				Convert( "Options.ReloadOnRun", "Options.TestLoader.ReloadOnRun", "False", "True" );
				Convert( "Options.MergeAssemblies", "Options.TestLoader.MergeAssemblies", "False", "True" );
				//Convert( "Options.MultiDomain", "Options.TestLoader.MultiDomain", "False", "True" );
				Convert( "Options.AutoNamespaceSuites", "Options.TestLoader.AutoNamespaceSuites", "False", "True" );
				Convert( "Options.VisualStudioSupport", "Options.TestLoader.VisualStudioSupport", "False", "True" );
				Convert( "Recent-Projects.MaxFiles", "RecentProjects.MaxFiles" );

                object val = legacy.GetSetting("Options.MultiDomain");
                if (val != null && (bool)val)
                    this.SaveSetting("Options.TestLoader.DomainUsage", NUnit.Core.DomainUsage.Multiple);

				int maxFiles = this.GetSetting( "RecentProjects.MaxFiles", 5 );
				for( int i = 1; i <= maxFiles; i++ )
				{
					string fileKey = string.Format( "File{0}", i );
					object fileEntry = legacy.GetSetting( "Recent-Projects." + fileKey );
					if ( fileEntry != null )
						this.SaveSetting( "RecentProjects." + fileKey, fileEntry );
				}
			}

			private void Convert( string legacyName, string currentName, params string[]values )
			{
				object val = legacy.GetSetting( legacyName );
				if ( val != null )
				{
					if ( val is int && values != null )
					{
						int ival = (int)val;
						if ( ival >= 0 && ival < values.Length )
							val = values[(int)val];
					}

					this.SaveSetting( currentName, val );
				}
			}
		}
		#endregion
	}
}
