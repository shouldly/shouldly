// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************
#define TREE_BASED
using System;
using System.Windows.Forms;
using NUnit.UiKit;
using NUnit.Gui.SettingsPages;

namespace NUnit.Gui
{
	/// <summary>
	/// Summary description for OptionsDialog.
	/// </summary>
	public class OptionsDialog
	{
#if TREE_BASED
		public static void Display( Form owner )
		{
			TreeBasedSettingsDialog.Display( owner,
				new GuiSettingsPage("Gui.General"),
				new TreeSettingsPage("Gui.Tree Display"),
				new TestResultSettingsPage("Gui.Test Results"),
				new TextOutputSettingsPage("Gui.Text Output"),
                new ProjectEditorSettingsPage("Gui.Project Editor"),
                new TestLoaderSettingsPage("Test Loader.Assembly Isolation"),
				new AssemblyReloadSettingsPage("Test Loader.Assembly Reload"),
                new RuntimeSelectionSettingsPage("Test Loader.Runtime Selection"),
				new AdvancedLoaderSettingsPage("Test Loader.Advanced"),
				new VisualStudioSettingsPage("IDE Support.Visual Studio"),
                new InternalTraceSettingsPage("Advanced Settings.Internal Trace"));
		}
#else
		public static void Display( Form owner )
		{
			TabbedSettingsDialog.Display( owner,
				new GuiSettingsPage("General"),
				new TreeSettingsPage("Tree"),
				new TestResultSettingsPage("Results"),
				new TextOutputSettingsPage("Text Output"),
				new TestLoaderSettingsPage("Test Load"),
				new AssemblyReloadSettingsPage("Reload"),
				new VisualStudioSettingsPage("Visual Studio"));
		}
#endif

		private OptionsDialog() { }
	}
}
