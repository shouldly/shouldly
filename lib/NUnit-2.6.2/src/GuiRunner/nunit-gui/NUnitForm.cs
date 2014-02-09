// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text;

namespace NUnit.Gui
{
	using NUnit.Core;
	using NUnit.Util;
	using NUnit.UiKit;
	using CP.Windows.Forms;

	public class NUnitForm : NUnitFormBase
	{
        static Logger log = InternalTrace.GetLogger(typeof(NUnitForm));
        
        #region Instance variables

		// Handlers for our recentFiles and recentProjects
		private RecentFileMenuHandler recentProjectsMenuHandler;

		private RecentFiles recentFilesService;
		private ISettings userSettings;

		private string displayFormat = "Full";

		private LongRunningOperationDisplay longOpDisplay;

		private System.Drawing.Font fixedFont;

		// Our current run command line options
		private GuiOptions guiOptions;

        // Our 'presenter' - under development
        private NUnitPresenter presenter;

		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.Panel leftPanel;
		public System.Windows.Forms.Splitter treeSplitter;
		public System.Windows.Forms.Panel rightPanel;

		private TestTree testTree;

		public System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button stopButton;
		public NUnit.UiKit.TestProgressBar progressBar;
		private CP.Windows.Forms.ExpandingLabel runCount;

		public NUnit.UiKit.ResultTabs resultTabs;

		public NUnit.UiKit.StatusBar statusBar;

		public System.Windows.Forms.ToolTip toolTip;

		public System.Windows.Forms.MainMenu mainMenu;
		
		public System.Windows.Forms.MenuItem fileMenu;
		private System.Windows.Forms.MenuItem saveMenuItem;
		private System.Windows.Forms.MenuItem saveAsMenuItem;
		private System.Windows.Forms.MenuItem newMenuItem;
		private System.Windows.Forms.MenuItem openMenuItem;
		private System.Windows.Forms.MenuItem recentProjectsMenu;
		private System.Windows.Forms.MenuItem fileMenuSeparator1;
		private System.Windows.Forms.MenuItem fileMenuSeparator2;
		public System.Windows.Forms.MenuItem fileMenuSeparator4;
		private System.Windows.Forms.MenuItem closeMenuItem;
		public System.Windows.Forms.MenuItem exitMenuItem;

		private System.Windows.Forms.MenuItem projectMenu;
		private System.Windows.Forms.MenuItem editProjectMenuItem;
		private System.Windows.Forms.MenuItem configMenuItem;
		private System.Windows.Forms.MenuItem projectMenuSeparator1;
		private System.Windows.Forms.MenuItem projectMenuSeparator2;

		private System.Windows.Forms.MenuItem toolsMenu;
		private System.Windows.Forms.MenuItem settingsMenuItem;
		private System.Windows.Forms.MenuItem saveXmlResultsMenuItem;

		public System.Windows.Forms.MenuItem helpMenuItem;
		public System.Windows.Forms.MenuItem helpItem;
		public System.Windows.Forms.MenuItem helpMenuSeparator1;
		public System.Windows.Forms.MenuItem aboutMenuItem;

		private System.Windows.Forms.MenuItem addVSProjectMenuItem;
		private System.Windows.Forms.MenuItem exceptionDetailsMenuItem;
		private System.Windows.Forms.MenuItem viewMenu;
		private System.Windows.Forms.MenuItem statusBarMenuItem;
		private System.Windows.Forms.MenuItem toolsMenuSeparator2;
		private System.Windows.Forms.MenuItem miniGuiMenuItem;
		private System.Windows.Forms.MenuItem fullGuiMenuItem;
		private System.Windows.Forms.MenuItem fontChangeMenuItem;
		private System.Windows.Forms.MenuItem defaultFontMenuItem;
		private System.Windows.Forms.MenuItem decreaseFontMenuItem;
		private System.Windows.Forms.MenuItem increaseFontMenuItem;
		private System.Windows.Forms.MenuItem testMenu;
		private System.Windows.Forms.MenuItem runAllMenuItem;
		private System.Windows.Forms.MenuItem runSelectedMenuItem;
		private System.Windows.Forms.MenuItem runFailedMenuItem;
		private System.Windows.Forms.MenuItem stopRunMenuItem;
		private System.Windows.Forms.MenuItem addinInfoMenuItem;
		private System.Windows.Forms.MenuItem viewMenuSeparator1;
		private System.Windows.Forms.MenuItem viewMenuSeparator2;
		private System.Windows.Forms.MenuItem viewMenuSeparator3;
		private System.Windows.Forms.MenuItem fontMenuSeparator;
		private System.Windows.Forms.MenuItem testMenuSeparator;
		private System.Windows.Forms.MenuItem guiFontMenuItem;
		private System.Windows.Forms.MenuItem fixedFontMenuItem;
		private System.Windows.Forms.MenuItem increaseFixedFontMenuItem;
		private System.Windows.Forms.MenuItem decreaseFixedFontMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem restoreFixedFontMenuItem;
		private System.Windows.Forms.MenuItem reloadTestsMenuItem;
		private System.Windows.Forms.MenuItem reloadProjectMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem toolsMenuSeparator1;
		private System.Windows.Forms.MenuItem assemblyDetailsMenuItem;
        private MenuItem runtimeMenuItem;
        private MenuItem openLogDirectoryMenuItem;
        private ExpandingLabel suiteName;
		private System.Windows.Forms.MenuItem addAssemblyMenuItem;

		#endregion
		
		#region Construction and Disposal

		public NUnitForm( GuiOptions guiOptions ) : base("NUnit")
		{
			InitializeComponent();

			this.guiOptions = guiOptions;
			this.recentFilesService = Services.RecentFiles;
			this.userSettings = Services.UserSettings;

            this.presenter = new NUnitPresenter(this, TestLoader);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NUnitForm));
            this.statusBar = new NUnit.UiKit.StatusBar();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.newMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.closeMenuItem = new System.Windows.Forms.MenuItem();
            this.fileMenuSeparator1 = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
            this.fileMenuSeparator2 = new System.Windows.Forms.MenuItem();
            this.reloadProjectMenuItem = new System.Windows.Forms.MenuItem();
            this.reloadTestsMenuItem = new System.Windows.Forms.MenuItem();
            this.runtimeMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.recentProjectsMenu = new System.Windows.Forms.MenuItem();
            this.fileMenuSeparator4 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenu = new System.Windows.Forms.MenuItem();
            this.fullGuiMenuItem = new System.Windows.Forms.MenuItem();
            this.miniGuiMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenuSeparator1 = new System.Windows.Forms.MenuItem();
            this.viewMenuSeparator2 = new System.Windows.Forms.MenuItem();
            this.guiFontMenuItem = new System.Windows.Forms.MenuItem();
            this.increaseFontMenuItem = new System.Windows.Forms.MenuItem();
            this.decreaseFontMenuItem = new System.Windows.Forms.MenuItem();
            this.fontMenuSeparator = new System.Windows.Forms.MenuItem();
            this.fontChangeMenuItem = new System.Windows.Forms.MenuItem();
            this.defaultFontMenuItem = new System.Windows.Forms.MenuItem();
            this.fixedFontMenuItem = new System.Windows.Forms.MenuItem();
            this.increaseFixedFontMenuItem = new System.Windows.Forms.MenuItem();
            this.decreaseFixedFontMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.restoreFixedFontMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenuSeparator3 = new System.Windows.Forms.MenuItem();
            this.statusBarMenuItem = new System.Windows.Forms.MenuItem();
            this.projectMenu = new System.Windows.Forms.MenuItem();
            this.configMenuItem = new System.Windows.Forms.MenuItem();
            this.projectMenuSeparator1 = new System.Windows.Forms.MenuItem();
            this.addAssemblyMenuItem = new System.Windows.Forms.MenuItem();
            this.addVSProjectMenuItem = new System.Windows.Forms.MenuItem();
            this.projectMenuSeparator2 = new System.Windows.Forms.MenuItem();
            this.editProjectMenuItem = new System.Windows.Forms.MenuItem();
            this.testMenu = new System.Windows.Forms.MenuItem();
            this.runAllMenuItem = new System.Windows.Forms.MenuItem();
            this.runSelectedMenuItem = new System.Windows.Forms.MenuItem();
            this.runFailedMenuItem = new System.Windows.Forms.MenuItem();
            this.testMenuSeparator = new System.Windows.Forms.MenuItem();
            this.stopRunMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsMenu = new System.Windows.Forms.MenuItem();
            this.assemblyDetailsMenuItem = new System.Windows.Forms.MenuItem();
            this.saveXmlResultsMenuItem = new System.Windows.Forms.MenuItem();
            this.exceptionDetailsMenuItem = new System.Windows.Forms.MenuItem();
            this.openLogDirectoryMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsMenuSeparator1 = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.toolsMenuSeparator2 = new System.Windows.Forms.MenuItem();
            this.addinInfoMenuItem = new System.Windows.Forms.MenuItem();
            this.helpItem = new System.Windows.Forms.MenuItem();
            this.helpMenuItem = new System.Windows.Forms.MenuItem();
            this.helpMenuSeparator1 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.treeSplitter = new System.Windows.Forms.Splitter();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.suiteName = new CP.Windows.Forms.ExpandingLabel();
            this.runCount = new CP.Windows.Forms.ExpandingLabel();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.progressBar = new NUnit.UiKit.TestProgressBar();
            this.resultTabs = new NUnit.UiKit.ResultTabs();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.testTree = new NUnit.UiKit.TestTree();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.DisplayTestProgress = true;
            this.statusBar.Location = new System.Drawing.Point(0, 407);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(744, 24);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "Status";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            this.viewMenu,
            this.projectMenu,
            this.testMenu,
            this.toolsMenu,
            this.helpItem});
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.closeMenuItem,
            this.fileMenuSeparator1,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.fileMenuSeparator2,
            this.reloadProjectMenuItem,
            this.reloadTestsMenuItem,
            this.runtimeMenuItem,
            this.menuItem2,
            this.recentProjectsMenu,
            this.fileMenuSeparator4,
            this.exitMenuItem});
            this.fileMenu.Text = "&File";
            this.fileMenu.Popup += new System.EventHandler(this.fileMenu_Popup);
            // 
            // newMenuItem
            // 
            this.newMenuItem.Index = 0;
            this.newMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.newMenuItem.Text = "&New Project...";
            this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 1;
            this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openMenuItem.Text = "&Open Project...";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Index = 2;
            this.closeMenuItem.Text = "&Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // fileMenuSeparator1
            // 
            this.fileMenuSeparator1.Index = 3;
            this.fileMenuSeparator1.Text = "-";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 4;
            this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Index = 5;
            this.saveAsMenuItem.Text = "Save &As...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // fileMenuSeparator2
            // 
            this.fileMenuSeparator2.Index = 6;
            this.fileMenuSeparator2.Text = "-";
            // 
            // reloadProjectMenuItem
            // 
            this.reloadProjectMenuItem.Index = 7;
            this.reloadProjectMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.reloadProjectMenuItem.Text = "Re&load Project";
            this.reloadProjectMenuItem.Click += new System.EventHandler(this.reloadProjectMenuItem_Click);
            // 
            // reloadTestsMenuItem
            // 
            this.reloadTestsMenuItem.Index = 8;
            this.reloadTestsMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.reloadTestsMenuItem.Text = "&Reload Tests";
            this.reloadTestsMenuItem.Click += new System.EventHandler(this.reloadTestsMenuItem_Click);
            // 
            // runtimeMenuItem
            // 
            this.runtimeMenuItem.Index = 9;
            this.runtimeMenuItem.Text = "  Select R&untime";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 10;
            this.menuItem2.Text = "-";
            // 
            // recentProjectsMenu
            // 
            this.recentProjectsMenu.Index = 11;
            this.recentProjectsMenu.Text = "Recent &Projects";
            // 
            // fileMenuSeparator4
            // 
            this.fileMenuSeparator4.Index = 12;
            this.fileMenuSeparator4.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 13;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.Index = 1;
            this.viewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fullGuiMenuItem,
            this.miniGuiMenuItem,
            this.viewMenuSeparator1,
            this.viewMenuSeparator2,
            this.guiFontMenuItem,
            this.fixedFontMenuItem,
            this.viewMenuSeparator3,
            this.statusBarMenuItem});
            this.viewMenu.Text = "&View";
            this.viewMenu.Popup += new System.EventHandler(this.viewMenu_Popup);
            // 
            // fullGuiMenuItem
            // 
            this.fullGuiMenuItem.Checked = true;
            this.fullGuiMenuItem.Index = 0;
            this.fullGuiMenuItem.RadioCheck = true;
            this.fullGuiMenuItem.Text = "&Full GUI";
            this.fullGuiMenuItem.Click += new System.EventHandler(this.fullGuiMenuItem_Click);
            // 
            // miniGuiMenuItem
            // 
            this.miniGuiMenuItem.Index = 1;
            this.miniGuiMenuItem.RadioCheck = true;
            this.miniGuiMenuItem.Text = "&Mini GUI";
            this.miniGuiMenuItem.Click += new System.EventHandler(this.miniGuiMenuItem_Click);
            // 
            // viewMenuSeparator1
            // 
            this.viewMenuSeparator1.Index = 2;
            this.viewMenuSeparator1.Text = "-";
            // 
            // viewMenuSeparator2
            // 
            this.viewMenuSeparator2.Index = 3;
            this.viewMenuSeparator2.Text = "-";
            // 
            // guiFontMenuItem
            // 
            this.guiFontMenuItem.Index = 4;
            this.guiFontMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.increaseFontMenuItem,
            this.decreaseFontMenuItem,
            this.fontMenuSeparator,
            this.fontChangeMenuItem,
            this.defaultFontMenuItem});
            this.guiFontMenuItem.Text = "GUI Fo&nt";
            // 
            // increaseFontMenuItem
            // 
            this.increaseFontMenuItem.Index = 0;
            this.increaseFontMenuItem.Text = "&Increase";
            this.increaseFontMenuItem.Click += new System.EventHandler(this.increaseFontMenuItem_Click);
            // 
            // decreaseFontMenuItem
            // 
            this.decreaseFontMenuItem.Index = 1;
            this.decreaseFontMenuItem.Text = "&Decrease";
            this.decreaseFontMenuItem.Click += new System.EventHandler(this.decreaseFontMenuItem_Click);
            // 
            // fontMenuSeparator
            // 
            this.fontMenuSeparator.Index = 2;
            this.fontMenuSeparator.Text = "-";
            // 
            // fontChangeMenuItem
            // 
            this.fontChangeMenuItem.Index = 3;
            this.fontChangeMenuItem.Text = "&Change...";
            this.fontChangeMenuItem.Click += new System.EventHandler(this.fontChangeMenuItem_Click);
            // 
            // defaultFontMenuItem
            // 
            this.defaultFontMenuItem.Index = 4;
            this.defaultFontMenuItem.Text = "&Restore";
            this.defaultFontMenuItem.Click += new System.EventHandler(this.defaultFontMenuItem_Click);
            // 
            // fixedFontMenuItem
            // 
            this.fixedFontMenuItem.Index = 5;
            this.fixedFontMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.increaseFixedFontMenuItem,
            this.decreaseFixedFontMenuItem,
            this.menuItem1,
            this.restoreFixedFontMenuItem});
            this.fixedFontMenuItem.Text = "Fi&xed Font";
            // 
            // increaseFixedFontMenuItem
            // 
            this.increaseFixedFontMenuItem.Index = 0;
            this.increaseFixedFontMenuItem.Text = "&Increase";
            this.increaseFixedFontMenuItem.Click += new System.EventHandler(this.increaseFixedFontMenuItem_Click);
            // 
            // decreaseFixedFontMenuItem
            // 
            this.decreaseFixedFontMenuItem.Index = 1;
            this.decreaseFixedFontMenuItem.Text = "&Decrease";
            this.decreaseFixedFontMenuItem.Click += new System.EventHandler(this.decreaseFixedFontMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // restoreFixedFontMenuItem
            // 
            this.restoreFixedFontMenuItem.Index = 3;
            this.restoreFixedFontMenuItem.Text = "&Restore";
            this.restoreFixedFontMenuItem.Click += new System.EventHandler(this.restoreFixedFontMenuItem_Click);
            // 
            // viewMenuSeparator3
            // 
            this.viewMenuSeparator3.Index = 6;
            this.viewMenuSeparator3.Text = "-";
            // 
            // statusBarMenuItem
            // 
            this.statusBarMenuItem.Checked = true;
            this.statusBarMenuItem.Index = 7;
            this.statusBarMenuItem.Text = "&Status Bar";
            this.statusBarMenuItem.Click += new System.EventHandler(this.statusBarMenuItem_Click);
            // 
            // projectMenu
            // 
            this.projectMenu.Index = 2;
            this.projectMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.configMenuItem,
            this.projectMenuSeparator1,
            this.addAssemblyMenuItem,
            this.addVSProjectMenuItem,
            this.projectMenuSeparator2,
            this.editProjectMenuItem});
            this.projectMenu.Text = "&Project";
            this.projectMenu.Visible = false;
            this.projectMenu.Popup += new System.EventHandler(this.projectMenu_Popup);
            // 
            // configMenuItem
            // 
            this.configMenuItem.Index = 0;
            this.configMenuItem.Text = "&Configurations";
            // 
            // projectMenuSeparator1
            // 
            this.projectMenuSeparator1.Index = 1;
            this.projectMenuSeparator1.Text = "-";
            // 
            // addAssemblyMenuItem
            // 
            this.addAssemblyMenuItem.Index = 2;
            this.addAssemblyMenuItem.Text = "Add Assembly...";
            this.addAssemblyMenuItem.Click += new System.EventHandler(this.addAssemblyMenuItem_Click);
            // 
            // addVSProjectMenuItem
            // 
            this.addVSProjectMenuItem.Index = 3;
            this.addVSProjectMenuItem.Text = "Add VS Project...";
            this.addVSProjectMenuItem.Click += new System.EventHandler(this.addVSProjectMenuItem_Click);
            // 
            // projectMenuSeparator2
            // 
            this.projectMenuSeparator2.Index = 4;
            this.projectMenuSeparator2.Text = "-";
            // 
            // editProjectMenuItem
            // 
            this.editProjectMenuItem.Index = 5;
            this.editProjectMenuItem.Text = "Edit...";
            this.editProjectMenuItem.Click += new System.EventHandler(this.editProjectMenuItem_Click);
            // 
            // testMenu
            // 
            this.testMenu.Index = 3;
            this.testMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.runAllMenuItem,
            this.runSelectedMenuItem,
            this.runFailedMenuItem,
            this.testMenuSeparator,
            this.stopRunMenuItem});
            this.testMenu.Text = "&Tests";
            // 
            // runAllMenuItem
            // 
            this.runAllMenuItem.Index = 0;
            this.runAllMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.runAllMenuItem.Text = "&Run All";
            this.runAllMenuItem.Click += new System.EventHandler(this.runAllMenuItem_Click);
            // 
            // runSelectedMenuItem
            // 
            this.runSelectedMenuItem.Index = 1;
            this.runSelectedMenuItem.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.runSelectedMenuItem.Text = "Run &Selected";
            this.runSelectedMenuItem.Click += new System.EventHandler(this.runSelectedMenuItem_Click);
            // 
            // runFailedMenuItem
            // 
            this.runFailedMenuItem.Enabled = false;
            this.runFailedMenuItem.Index = 2;
            this.runFailedMenuItem.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.runFailedMenuItem.Text = "Run &Failed";
            this.runFailedMenuItem.Click += new System.EventHandler(this.runFailedMenuItem_Click);
            // 
            // testMenuSeparator
            // 
            this.testMenuSeparator.Index = 3;
            this.testMenuSeparator.Text = "-";
            // 
            // stopRunMenuItem
            // 
            this.stopRunMenuItem.Index = 4;
            this.stopRunMenuItem.Text = "S&top Run";
            this.stopRunMenuItem.Click += new System.EventHandler(this.stopRunMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.Index = 4;
            this.toolsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.assemblyDetailsMenuItem,
            this.saveXmlResultsMenuItem,
            this.exceptionDetailsMenuItem,
            this.openLogDirectoryMenuItem,
            this.toolsMenuSeparator1,
            this.settingsMenuItem,
            this.toolsMenuSeparator2,
            this.addinInfoMenuItem});
            this.toolsMenu.Text = "T&ools";
            this.toolsMenu.Popup += new System.EventHandler(this.toolsMenu_Popup);
            // 
            // assemblyDetailsMenuItem
            // 
            this.assemblyDetailsMenuItem.Index = 0;
            this.assemblyDetailsMenuItem.Text = "&Test Assemblies...";
            this.assemblyDetailsMenuItem.Click += new System.EventHandler(this.assemblyDetailsMenuItem_Click);
            // 
            // saveXmlResultsMenuItem
            // 
            this.saveXmlResultsMenuItem.Index = 1;
            this.saveXmlResultsMenuItem.Text = "&Save Results as XML...";
            this.saveXmlResultsMenuItem.Click += new System.EventHandler(this.saveXmlResultsMenuItem_Click);
            // 
            // exceptionDetailsMenuItem
            // 
            this.exceptionDetailsMenuItem.Index = 2;
            this.exceptionDetailsMenuItem.Text = "&Exception Details...";
            this.exceptionDetailsMenuItem.Click += new System.EventHandler(this.exceptionDetailsMenuItem_Click);
            // 
            // openLogDirectoryMenuItem
            // 
            this.openLogDirectoryMenuItem.Index = 3;
            this.openLogDirectoryMenuItem.Text = "Open &Log Directory...";
            this.openLogDirectoryMenuItem.Click += new System.EventHandler(this.openLogDirectoryMenuItem_Click);
            // 
            // toolsMenuSeparator1
            // 
            this.toolsMenuSeparator1.Index = 4;
            this.toolsMenuSeparator1.Text = "-";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Index = 5;
            this.settingsMenuItem.Text = "&Settings...";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // toolsMenuSeparator2
            // 
            this.toolsMenuSeparator2.Index = 6;
            this.toolsMenuSeparator2.Text = "-";
            // 
            // addinInfoMenuItem
            // 
            this.addinInfoMenuItem.Index = 7;
            this.addinInfoMenuItem.Text = "Addins...";
            this.addinInfoMenuItem.Click += new System.EventHandler(this.addinInfoMenuItem_Click);
            // 
            // helpItem
            // 
            this.helpItem.Index = 5;
            this.helpItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.helpMenuItem,
            this.helpMenuSeparator1,
            this.aboutMenuItem});
            this.helpItem.Text = "&Help";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Index = 0;
            this.helpMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.helpMenuItem.Text = "NUnit &Help...";
            this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
            // 
            // helpMenuSeparator1
            // 
            this.helpMenuSeparator1.Index = 1;
            this.helpMenuSeparator1.Text = "-";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "&About NUnit...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // treeSplitter
            // 
            this.treeSplitter.Location = new System.Drawing.Point(240, 0);
            this.treeSplitter.MinSize = 240;
            this.treeSplitter.Name = "treeSplitter";
            this.treeSplitter.Size = new System.Drawing.Size(6, 407);
            this.treeSplitter.TabIndex = 2;
            this.treeSplitter.TabStop = false;
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.SystemColors.Control;
            this.rightPanel.Controls.Add(this.groupBox1);
            this.rightPanel.Controls.Add(this.resultTabs);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(246, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(498, 407);
            this.rightPanel.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.suiteName);
            this.groupBox1.Controls.Add(this.runCount);
            this.groupBox1.Controls.Add(this.stopButton);
            this.groupBox1.Controls.Add(this.runButton);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // suiteName
            // 
            this.suiteName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suiteName.AutoEllipsis = true;
            this.suiteName.Location = new System.Drawing.Point(145, 21);
            this.suiteName.Name = "suiteName";
            this.suiteName.Size = new System.Drawing.Size(343, 23);
            this.suiteName.TabIndex = 1;
            // 
            // runCount
            // 
            this.runCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.runCount.AutoEllipsis = true;
            this.runCount.Location = new System.Drawing.Point(8, 89);
            this.runCount.Name = "runCount";
            this.runCount.Size = new System.Drawing.Size(480, 21);
            this.runCount.TabIndex = 5;
            // 
            // stopButton
            // 
            this.stopButton.AutoSize = true;
            this.stopButton.Location = new System.Drawing.Point(75, 16);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(64, 31);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "&Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(8, 16);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(64, 31);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "&Run";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.CausesValidation = false;
            this.progressBar.Enabled = false;
            this.progressBar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.progressBar.Location = new System.Drawing.Point(8, 54);
            this.progressBar.Maximum = 100;
            this.progressBar.Minimum = 0;
            this.progressBar.Name = "progressBar";
            this.progressBar.Segmented = true;
            this.progressBar.Size = new System.Drawing.Size(480, 28);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 0;
            this.progressBar.Value = 0;
            // 
            // resultTabs
            // 
            this.resultTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTabs.Location = new System.Drawing.Point(0, 120);
            this.resultTabs.Name = "resultTabs";
            this.resultTabs.Size = new System.Drawing.Size(498, 284);
            this.resultTabs.TabIndex = 2;
            // 
            // testTree
            // 
            this.testTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testTree.Location = new System.Drawing.Point(0, 0);
            this.testTree.Name = "testTree";
            this.testTree.ShowCheckBoxes = false;
            this.testTree.Size = new System.Drawing.Size(240, 407);
            this.testTree.TabIndex = 0;
            this.testTree.SelectedTestsChanged += new NUnit.UiKit.SelectedTestsChangedEventHandler(this.testTree_SelectedTestsChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.testTree);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(240, 407);
            this.leftPanel.TabIndex = 4;
            // 
            // NUnitForm
            // 
            this.ClientSize = new System.Drawing.Size(744, 431);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.treeSplitter);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(160, 32);
            this.Name = "NUnitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NUnit";
            this.Load += new System.EventHandler(this.NUnitForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.NUnitForm_Closing);
            this.rightPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.leftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        #region Public Properties

        public NUnitPresenter Presenter
        {
            get { return presenter; }
        }

        #endregion

        #region Properties used internally

        private TestLoader _testLoader;
		private TestLoader TestLoader
		{
			get
			{ 
				if ( _testLoader == null )
					_testLoader = Services.TestLoader;
				return _testLoader;
			}
		}

		private bool IsProjectLoaded
		{
			get { return TestLoader.IsProjectLoaded; }
		}

		private NUnitProject TestProject
		{
			get { return TestLoader.TestProject; }
		}

		private bool IsTestLoaded
		{
			get { return TestLoader.IsTestLoaded; }
		}

		private bool IsTestRunning
		{
			get { return TestLoader.Running; }
		}
		#endregion

		#region Menu Handlers

		#region File Menu

		private void fileMenu_Popup(object sender, System.EventArgs e)
		{
			newMenuItem.Enabled = !IsTestRunning;
			openMenuItem.Enabled = !IsTestRunning;
			closeMenuItem.Enabled = IsProjectLoaded && !IsTestRunning;

			saveMenuItem.Enabled = IsProjectLoaded;
			saveAsMenuItem.Enabled = IsProjectLoaded;

			reloadTestsMenuItem.Enabled = IsTestLoaded && !IsTestRunning;
            reloadProjectMenuItem.Enabled = runtimeMenuItem.Enabled = 
                IsProjectLoaded && 
                File.Exists(TestProject.ProjectPath) &&
                !IsTestRunning;

            RuntimeFramework current = TestLoader.CurrentFramework;
            RuntimeFramework[] frameworks = RuntimeFramework.AvailableFrameworks;

            runtimeMenuItem.Visible = frameworks.Length > 1;

            if (runtimeMenuItem.Visible && runtimeMenuItem.Enabled)
            {
                runtimeMenuItem.MenuItems.Clear();

                foreach (RuntimeFramework framework in frameworks)
                {
                    MenuItem item = new MenuItem(framework.DisplayName);
                    item.Checked = current.Supports(framework);
                    item.Tag = framework;
                    item.Click += new EventHandler(runtimeFrameworkMenuItem_Click);
                    try
                    {
                        item.Enabled = TestLoader.CanReloadUnderRuntimeVersion(framework.ClrVersion);
                    }
                    catch
                    {
                        item.Enabled = false;
                    }
                    runtimeMenuItem.MenuItems.Add(item);
                }
            }

			recentProjectsMenu.Enabled = !IsTestRunning;

			if ( !IsTestRunning )
			{
				recentProjectsMenuHandler.Load();
			}
		}

		private void newMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.NewProject();
		}

		private void openMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.OpenProject();
		}

		private void closeMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.CloseProject();
		}

		private void saveMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.SaveProject();
		}

		private void saveAsMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.SaveProjectAs();
            SetTitleBar(TestProject.Name);
		}

        private void runtimeFrameworkMenuItem_Click(object sender, System.EventArgs e)
        {
            TestLoader.ReloadTest(((MenuItem)sender).Tag as RuntimeFramework);
        }

		private void reloadProjectMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.ReloadProject();
		}

		private void reloadTestsMenuItem_Click(object sender, System.EventArgs e)
		{
			TestLoader.ReloadTest();
		}

		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region View Menu
		private void viewMenu_Popup(object sender, System.EventArgs e)
		{
			assemblyDetailsMenuItem.Enabled = this.TestLoader.IsTestLoaded;
		}

		private void statusBarMenuItem_Click(object sender, System.EventArgs e)
		{
			statusBarMenuItem.Checked = !statusBarMenuItem.Checked;
			statusBar.Visible = statusBarMenuItem.Checked;
		}

		private void fontChangeMenuItem_Click(object sender, System.EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.FontMustExist = true;
			fontDialog.Font = this.Font;
			fontDialog.MinSize = 6;
			fontDialog.MaxSize = 12;
			fontDialog.AllowVectorFonts = false;
			fontDialog.ScriptsOnly = true;
			fontDialog.ShowEffects = false;
			fontDialog.ShowApply = true;
			fontDialog.Apply += new EventHandler(fontDialog_Apply);
			if( fontDialog.ShowDialog() == DialogResult.OK )
				applyFont( fontDialog.Font );
		}

		private void fontDialog_Apply(object sender, EventArgs e)
		{
			applyFont( ((FontDialog)sender).Font );
		}


		private void defaultFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFont( System.Windows.Forms.Form.DefaultFont );
		}

		private void fullGuiMenuItem_Click(object sender, System.EventArgs e)
		{
			if ( !fullGuiMenuItem.Checked )
				displayFullGui();
		}

		private void miniGuiMenuItem_Click(object sender, System.EventArgs e)
		{
			if ( !miniGuiMenuItem.Checked )
				displayMiniGui();
		}

		private void displayFullGui()
		{
			fullGuiMenuItem.Checked = true;
			miniGuiMenuItem.Checked = false;

			this.displayFormat = "Full";
			userSettings.SaveSetting( "Gui.DisplayFormat", "Full" );

            this.leftPanel.Visible = true;
            this.leftPanel.Dock = DockStyle.Left;
            this.treeSplitter.Visible = true;
            this.rightPanel.Visible = true;
            this.statusBar.Visible = true;

            resultTabs.TabsMenu.Visible = true;

            int x = userSettings.GetSetting("Gui.MainForm.Left", 10);
			int y = userSettings.GetSetting( "Gui.MainForm.Top", 10 );
			Point location = new Point( x, y );

			if ( !IsValidLocation( location ) )
				location = new Point( 10, 10 );
			this.Location = location;

			int width = userSettings.GetSetting( "Gui.MainForm.Width", this.Size.Width );
			int height = userSettings.GetSetting( "Gui.MainForm.Height", this.Size.Height );
			if ( width < 160 ) width = 160;
			if ( height < 32 ) height = 32;
			this.Size = new Size( width, height );

			// Set to maximized if required
			if ( userSettings.GetSetting( "Gui.MainForm.Maximized", false ) )
				this.WindowState = FormWindowState.Maximized;

			// Set the font to use
            applyFont(userSettings.GetSetting("Gui.MainForm.Font", Form.DefaultFont));
        }

		private void displayMiniGui()
		{
			miniGuiMenuItem.Checked = true;
			fullGuiMenuItem.Checked = false;
			
			this.displayFormat = "Mini";
			userSettings.SaveSetting( "Gui.DisplayFormat", "Mini" );

            this.leftPanel.Visible = true;
            this.leftPanel.Dock = DockStyle.Fill;
            this.treeSplitter.Visible = false;
            this.rightPanel.Visible = false;
            this.statusBar.Visible = false;

            resultTabs.TabsMenu.Visible = false;

            int x = userSettings.GetSetting("Gui.MiniForm.Left", 10);
			int y = userSettings.GetSetting( "Gui.MiniForm.Top", 10 );
			Point location = new Point( x, y );

			if ( !IsValidLocation( location ) )
				location = new Point( 10, 10 );
			this.Location = location;

			int width = userSettings.GetSetting( "Gui.MiniForm.Width", 300 );
			int height = userSettings.GetSetting( "Gui.MiniForm.Height", this.Size.Height );
			if ( width < 160 ) width = 160;
			if ( height < 32 ) height = 32;
			this.Size = new Size( width, height );

			// Set the font to use
            applyFont(userSettings.GetSetting("Gui.MiniForm.Font", Form.DefaultFont));
		}

		private void increaseFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFont( new Font( this.Font.FontFamily, this.Font.SizeInPoints * 1.2f, this.Font.Style ) );
		}

		private void decreaseFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFont( new Font( this.Font.FontFamily, this.Font.SizeInPoints / 1.2f, this.Font.Style ) );
		}

		private void applyFont( Font font )
		{
			this.Font = font;
			this.runCount.Font = font.FontFamily.IsStyleAvailable( FontStyle.Bold )
				? new Font( font, FontStyle.Bold )
				: font;
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			userSettings.SaveSetting( 
				displayFormat == "Mini" ? "Gui.MiniForm.Font" : "Gui.MainForm.Font", 
				converter.ConvertToString( null, CultureInfo.InvariantCulture, font ) );
		}

		private void increaseFixedFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFixedFont( new Font( fixedFont.FontFamily, fixedFont.SizeInPoints * 1.2f, fixedFont.Style ) );		
		}

		private void decreaseFixedFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFixedFont( new Font( fixedFont.FontFamily, fixedFont.SizeInPoints / 1.2f, fixedFont.Style ) );		
		}

		private void restoreFixedFontMenuItem_Click(object sender, System.EventArgs e)
		{
			applyFixedFont( new Font( FontFamily.GenericMonospace, 8.0f ) );
		}

		private void applyFixedFont( Font font )
		{
			this.fixedFont = font;
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			userSettings.SaveSetting( "Gui.FixedFont", converter.ConvertToString( null, CultureInfo.InvariantCulture, font ) );
		}
		#endregion

		#region Project Menu

		/// <summary>
		/// When the project menu pops up, we populate the
		/// submenu for configurations dynamically.
		/// </summary>
		private void projectMenu_Popup(object sender, System.EventArgs e)
		{
			int index = 0;
			configMenuItem.MenuItems.Clear();

			foreach ( ProjectConfig config in TestProject.Configs )
			{
				string text = string.Format( "&{0} {1}", index+1, config.Name );
				MenuItem item = new MenuItem( 
					text, new EventHandler( configMenuItem_Click ) );
				if ( config.Name == TestProject.ActiveConfigName ) 
					item.Checked = true;
				configMenuItem.MenuItems.Add( index++, item );
			}

			configMenuItem.MenuItems.Add( "-" );

			configMenuItem.MenuItems.Add( "&Add...",
				new System.EventHandler( addConfigurationMenuItem_Click ) );

			configMenuItem.MenuItems.Add( "&Edit...", 
				new System.EventHandler( editConfigurationsMenuItem_Click ) );

			addVSProjectMenuItem.Visible = userSettings.GetSetting( "Options.TestLoader.VisualStudioSupport", false );
			addAssemblyMenuItem.Enabled = TestProject.Configs.Count > 0;
		}

		private void configMenuItem_Click( object sender, EventArgs e )
		{
			MenuItem item = (MenuItem)sender;
            if (!item.Checked)
            {
                TestProject.SetActiveConfig(TestProject.Configs[item.Index].Name);
                LoadOrReloadTestAsNeeded();
            }
		}

		private void addConfigurationMenuItem_Click( object sender, System.EventArgs e )
		{
			using( AddConfigurationDialog dlg = new AddConfigurationDialog( TestProject ) )
			{
				this.Site.Container.Add( dlg );
				dlg.ShowDialog();
			}

            LoadOrReloadTestAsNeeded();
		}

		private void editConfigurationsMenuItem_Click( object sender, System.EventArgs e )
		{
			using( ConfigurationEditor editor = new ConfigurationEditor( TestProject ) )
			{
				this.Site.Container.Add( editor );
				editor.ShowDialog();
			}

            LoadOrReloadTestAsNeeded();
		}

		private void addAssemblyMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.AddAssembly();
            LoadOrReloadTestAsNeeded();
		}

		private void addVSProjectMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.AddVSProject();
            LoadOrReloadTestAsNeeded();
		}

		private void editProjectMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.EditProject();
        }

        private void LoadOrReloadTestAsNeeded()
        {
            if (TestProject.HasChangesRequiringReload)
            {
                if (TestProject.IsLoadable)
                {
                    if (IsTestLoaded)
                        TestLoader.ReloadTest();
                    else
                        TestLoader.LoadTest();
                }
                else
                    TestLoader.UnloadTest();
            }
        }

		#endregion

		#region Test Menu

		private void runAllMenuItem_Click(object sender, System.EventArgs e)
		{
			this.testTree.RunAllTests();
		}

		private void runSelectedMenuItem_Click(object sender, System.EventArgs e)
		{
			this.testTree.RunSelectedTests();
		
		}

		private void runFailedMenuItem_Click(object sender, System.EventArgs e)
		{
			this.testTree.RunFailedTests();
		}

		private void stopRunMenuItem_Click(object sender, System.EventArgs e)
		{
			CancelRun();
		}

		#endregion

		#region Tools Menu

		private void toolsMenu_Popup(object sender, System.EventArgs e)
		{
            assemblyDetailsMenuItem.Enabled = IsTestLoaded;
			saveXmlResultsMenuItem.Enabled = IsTestLoaded && TestLoader.TestResult != null;
			exceptionDetailsMenuItem.Enabled = TestLoader.LastException != null;
		}

		private void saveXmlResultsMenuItem_Click(object sender, System.EventArgs e)
		{
            presenter.SaveLastResult();
		}

		private void exceptionDetailsMenuItem_Click(object sender, System.EventArgs e)
		{
			using( ExceptionDetailsForm details = new ExceptionDetailsForm( TestLoader.LastException ) )
			{
				this.Site.Container.Add( details );
				details.ShowDialog();
			}
		}

		private void settingsMenuItem_Click(object sender, System.EventArgs e)
		{
			OptionsDialog.Display( this );
		}

		private void assemblyDetailsMenuItem_Click(object sender, System.EventArgs e)
		{
            new TestAssemblyInfoForm().ShowDialog();
        }

		private void addinInfoMenuItem_Click(object sender, System.EventArgs e)
		{
			AddinDialog dlg = new AddinDialog();
			dlg.ShowDialog();
		}

        private void openLogDirectoryMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(NUnitConfiguration.LogDirectory))
                Directory.CreateDirectory(NUnitConfiguration.LogDirectory);

            System.Diagnostics.Process.Start(NUnitConfiguration.LogDirectory);
        }

		#endregion

		#region Help Menu

		private void helpMenuItem_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( NUnitConfiguration.HelpUrl );
		}

		/// <summary>
		/// Display the about box when menu item is selected
		/// </summary>
		private void aboutMenuItem_Click(object sender, System.EventArgs e)
		{
			using( AboutBox aboutBox = new AboutBox() )
			{
				this.Site.Container.Add( aboutBox );
				aboutBox.ShowDialog();
			}
		}

		#endregion

		#endregion

		#region Form Level Events
		/// <summary>
		/// Get saved options when form loads
		/// </summary>
		private void NUnitForm_Load(object sender, System.EventArgs e)
		{
			if ( !this.DesignMode )
			{
				// TODO: Can these controls add their menus themselves?
				this.viewMenu.MenuItems.Add(3, resultTabs.TabsMenu);
				this.viewMenu.MenuItems.Add(4, testTree.TreeMenu);

				EnableRunCommand( false );
				EnableStopCommand( false );

				recentProjectsMenuHandler = new RecentFileMenuHandler( recentProjectsMenu, recentFilesService );
                recentProjectsMenuHandler.CheckFilesExist = userSettings.GetSetting("Gui.RecentProjects.CheckFilesExist", true);

				LoadFormSettings();
				SubscribeToTestEvents();
				InitializeControls();

				// Force display  so that any "Loading..." or error 
				// message overlays the main form.
				this.Show();
				this.Invalidate();
				this.Update();

                // Set Capture options for the TestLoader
                TestLoader.IsTracingEnabled = resultTabs.IsTracingEnabled;
                TestLoader.LoggingThreshold = resultTabs.MaximumLogLevel;

				// Load test specified on command line or
				// the most recent one if options call for it
				if ( guiOptions.ParameterCount != 0 )
                    presenter.OpenProject((string)guiOptions.Parameters[0], guiOptions.config, guiOptions.fixture);
				else if( userSettings.GetSetting( "Options.LoadLastProject", true ) && !guiOptions.noload )
				{
					foreach( RecentFileEntry entry in recentFilesService.Entries )
					{
						if ( entry != null && entry.Exists && entry.IsCompatibleCLRVersion )
						{
                            presenter.OpenProject(entry.Path, guiOptions.config, guiOptions.fixture);
							break;
						}
					}
				}

				if ( guiOptions.include != null )
				{
					string[] categories = guiOptions.include.Split( ',' );
					if ( categories.Length > 0 )
						this.testTree.SelectCategories( categories, false );
				}
				else if ( guiOptions.exclude != null )
				{
					string[] categories = guiOptions.exclude.Split( ',' );
					if ( categories.Length > 0 )
						this.testTree.SelectCategories( categories, true );
				}

				// Run loaded test automatically if called for
				if ( TestLoader.IsTestLoaded )
					if ( guiOptions.run || guiOptions.runselected )
					{
						// TODO: Temporary fix to avoid problem when /run is used 
						// with ReloadOnRun turned on. Refactor TestLoader so
						// we can just do a run without reload.
						bool reload = Services.UserSettings.GetSetting("Options.TestLoader.ReloadOnRun", false);
					
						try
						{
							Services.UserSettings.SaveSetting("Options.TestLoader.ReloadOnRun", false);
                            if (guiOptions.runselected)
                                testTree.RunSelectedTests();
                            else
                                testTree.RunAllTests(false);
						}
						finally
						{
							Services.UserSettings.SaveSetting("Options.TestLoader.ReloadOnRun", reload);
						}
					}
			}
		}

		private void LoadFormSettings()
		{
			this.displayFormat = userSettings.GetSetting( "Gui.DisplayFormat", "Full" );

            switch (displayFormat)
            {
                case "Full":
                    displayFullGui();
                    break;
                case "Mini":
                    displayMiniGui();
                    break;
                default:
                    throw new ApplicationException("Invalid Setting");
            }

			// Handle changes to form position
			this.Move += new System.EventHandler(this.NUnitForm_Move);
			this.Resize += new System.EventHandler(this.NUnitForm_Resize);

			// Set the splitter position
			int splitPosition = userSettings.GetSetting( "Gui.MainForm.SplitPosition", treeSplitter.SplitPosition );
			if ( splitPosition >= treeSplitter.MinSize && splitPosition < this.ClientSize.Width )
				this.treeSplitter.SplitPosition = splitPosition;

			// Handle changes in splitter positions
			this.treeSplitter.SplitterMoved += new SplitterEventHandler( treeSplitter_SplitterMoved );

			// Get the fixed font used by result tabs
            this.fixedFont = userSettings.GetSetting("Gui.FixedFont", new Font(FontFamily.GenericMonospace, 8.0f));

			// Handle changes in form settings
			userSettings.Changed += new SettingsEventHandler(userSettings_Changed);
		}

		private bool IsValidLocation( Point location )
		{
			Rectangle myArea = new Rectangle( location, this.Size );
			bool intersect = false;
			foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
			{
				intersect |= myArea.IntersectsWith(screen.WorkingArea);
			}
			return intersect;
		}

		private void SubscribeToTestEvents()
		{
			ITestEvents events = TestLoader.Events;

			events.RunStarting += new TestEventHandler( OnRunStarting );
			events.RunFinished += new TestEventHandler( OnRunFinished );

			events.ProjectLoaded	+= new TestEventHandler( OnTestProjectLoaded );
			events.ProjectLoadFailed+= new TestEventHandler( OnProjectLoadFailure );
			events.ProjectUnloading += new TestEventHandler( OnTestProjectUnloading );
			events.ProjectUnloaded	+= new TestEventHandler( OnTestProjectUnloaded );

			events.TestLoading		+= new TestEventHandler( OnTestLoadStarting );
			events.TestLoaded		+= new TestEventHandler( OnTestLoaded );
			events.TestLoadFailed	+= new TestEventHandler( OnTestLoadFailure );
			events.TestUnloading	+= new TestEventHandler( OnTestUnloadStarting );
			events.TestUnloaded		+= new TestEventHandler( OnTestUnloaded );
			events.TestReloading	+= new TestEventHandler( OnReloadStarting );
			events.TestReloaded		+= new TestEventHandler( OnTestChanged );
			events.TestReloadFailed	+= new TestEventHandler( OnTestLoadFailure );
		}

		private void InitializeControls()
		{
			// ToDo: Migrate more ui elements to handle events on their own.
			this.progressBar.Subscribe( TestLoader.Events );
			this.statusBar.Subscribe( TestLoader.Events );
		}

		// Save settings changed by moving the form
		private void NUnitForm_Move(object sender, System.EventArgs e)
		{
			switch( this.displayFormat )
			{
				case "Full":
				default:
					if ( this.WindowState == FormWindowState.Normal )
					{
						userSettings.SaveSetting( "Gui.MainForm.Left", this.Location.X );
						userSettings.SaveSetting( "Gui.MainForm.Top", this.Location.Y );
						userSettings.SaveSetting( "Gui.MainForm.Maximized", false );

						this.statusBar.SizingGrip = true;
					}
					else if ( this.WindowState == FormWindowState.Maximized )
					{
						userSettings.SaveSetting( "Gui.MainForm.Maximized", true );

						this.statusBar.SizingGrip = false;
					}
					break;
				case "Mini":
					if ( this.WindowState == FormWindowState.Normal )
					{
						userSettings.SaveSetting( "Gui.MiniForm.Left", this.Location.X );
						userSettings.SaveSetting( "Gui.MiniForm.Top", this.Location.Y );
						userSettings.SaveSetting( "Gui.MiniForm.Maximized", false );

						this.statusBar.SizingGrip = true;
					}
					else if ( this.WindowState == FormWindowState.Maximized )
					{
						userSettings.SaveSetting( "Gui.MiniForm.Maximized", true );

						this.statusBar.SizingGrip = false;
					}
					break;
			}
		}

		// Save settings that change when window is resized
		private void NUnitForm_Resize(object sender,System.EventArgs e)
		{
			if ( this.WindowState == FormWindowState.Normal )
			{
				if ( this.displayFormat == "Full" )
				{
					userSettings.SaveSetting( "Gui.MainForm.Width", this.Size.Width );
					userSettings.SaveSetting( "Gui.MainForm.Height", this.Size.Height );
				}
				else
				{
					userSettings.SaveSetting( "Gui.MiniForm.Width", this.Size.Width );
					userSettings.SaveSetting( "Gui.MiniForm.Height", this.Size.Height );
				}
			}
		}

		// Splitter moved so save it's position
		private void treeSplitter_SplitterMoved( object sender, SplitterEventArgs e )
		{
			userSettings.SaveSetting( "Gui.MainForm.SplitPosition", treeSplitter.SplitPosition );
		}

		/// <summary>
		///	Form is about to close, first see if we 
		///	have a test run going on and if so whether
		///	we should cancel it. Then unload the 
		///	test and save the latest form position.
		/// </summary>
		private void NUnitForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( IsTestRunning )
			{
                DialogResult dialogResult = MessageDisplay.Ask( 
					"A test is running, do you want to stop the test and exit?" );

				if ( dialogResult == DialogResult.No )
					e.Cancel = true;
				else
					TestLoader.CancelTestRun();
			}

			if ( !e.Cancel && IsProjectLoaded &&
                presenter.CloseProject() == DialogResult.Cancel)
				e.Cancel = true;
		}

		private void userSettings_Changed(object sender, SettingsEventArgs args)
		{
			if ( args.SettingName == "Gui.DisplayFormat" )
			{
				string newFormat = userSettings.GetSetting( "Gui.DisplayFormat", this.displayFormat );
				if ( newFormat != displayFormat )
					if ( newFormat == "Full" )
						displayFullGui();
					else
						displayMiniGui();
			}
            else if (args.SettingName.StartsWith("Gui.TextOutput.") && args.SettingName.EndsWith(".Content"))
            {
                TestLoader.IsTracingEnabled = resultTabs.IsTracingEnabled;
                TestLoader.LoggingThreshold = resultTabs.MaximumLogLevel;
            }
		}
		#endregion

		#region Other UI Event Handlers

		/// <summary>
		/// When the Run Button is clicked, run the selected test.
		/// </summary>
		private void runButton_Click(object sender, System.EventArgs e)
		{
			testTree.RunSelectedTests();
		}

		/// <summary>
		/// When the Stop Button is clicked, cancel running test
		/// </summary>
		private void stopButton_Click(object sender, System.EventArgs e)
		{
			CancelRun();
		}

		private void CancelRun()
		{
			EnableStopCommand( false );

			if ( IsTestRunning )
			{
                DialogResult dialogResult = MessageDisplay.Ask( 
					"Do you want to cancel the running test?" );

				if ( dialogResult == DialogResult.No )
					EnableStopCommand( true );
				else
					TestLoader.CancelTestRun();
			}
		}

		private void testTree_SelectedTestsChanged(object sender, SelectedTestsChangedEventArgs e)
		{
			if (!IsTestRunning) 
			{
				suiteName.Text = e.TestName;
				statusBar.Initialize(e.TestCount, e.TestName );
			}
		}

		#endregion

		#region Event Handlers for Project Load and Unload

		private void OnTestProjectLoaded( object sender, TestEventArgs e )
		{
            string projectPath = e.Name;

			SetTitleBar( projectPath );
			projectMenu.Visible = true;
			runCount.Text = "";

            // If this is an NUnit project, set up watcher
            if (NUnitProject.IsNUnitProjectFile(projectPath) && File.Exists(projectPath))
                presenter.WatchProject(projectPath);
		}

        private void OnTestProjectUnloading(object sender, TestEventArgs e)
		{
            // Remove any watcher
            if (e.Name != null && File.Exists(e.Name))
            {
                presenter.RemoveWatcher();

				Version version = Environment.Version;
				foreach( TestAssemblyInfo info in TestLoader.AssemblyInfo )
					if ( info.ImageRuntimeVersion < version )
						version = info.ImageRuntimeVersion;
			
				recentFilesService.SetMostRecent( new RecentFileEntry( e.Name, version ) );
			}
		}

		private void OnTestProjectUnloaded( object sender, TestEventArgs e )
		{
			SetTitleBar( null );
			projectMenu.Visible = false;
			runCount.Text = "";
		}

        private void OnProjectLoadFailure(object sender, TestEventArgs e)
        {
            MessageDisplay.Error("Project Not Loaded", e.Exception);

            recentFilesService.Remove(e.Name);

            EnableRunCommand(IsProjectLoaded);
        }

        #endregion

        #region Event Handlers for Test Load and Unload

        private void OnTestLoadStarting(object sender, TestEventArgs e)
		{
			EnableRunCommand( false );
			longOpDisplay = new LongRunningOperationDisplay( this, "Loading..." );
		}

		private void OnTestUnloadStarting( object sender, TestEventArgs e )
		{
			EnableRunCommand( false );
		}

		private void OnReloadStarting( object sender, TestEventArgs e )
		{
			EnableRunCommand( false );
			longOpDisplay = new LongRunningOperationDisplay( this, "Reloading..." );
		}

		/// <summary>
		/// A test suite has been loaded, so update 
		/// recent assemblies and display the tests in the UI
		/// </summary>
		private void OnTestLoaded( object sender, TestEventArgs e )
		{
			if ( longOpDisplay != null )
			{
				longOpDisplay.Dispose();
				longOpDisplay = null;
			}
			EnableRunCommand( true );
			
			if ( TestLoader.TestCount == 0 )
			{
				foreach( TestAssemblyInfo info in TestLoader.AssemblyInfo )
					if ( info.TestFrameworks.Count > 0 ) return;

                MessageDisplay.Error("This assembly was not built with any known testing framework.");
			}
		}

		/// <summary>
		/// A test suite has been unloaded, so clear the UI
		/// and remove any references to the suite.
		/// </summary>
		private void OnTestUnloaded( object sender, TestEventArgs e )
		{
			suiteName.Text = null;
            runCount.Text = null;
			EnableRunCommand( false );
            Refresh();
		}

		/// <summary>
		/// The current test suite has changed in some way,
		/// so update the info in the UI and clear the
		/// test results, since they are no longer valid.
		/// </summary>
		private void OnTestChanged( object sender, TestEventArgs e )
		{
            SetTitleBar(TestProject.Name);

			if ( longOpDisplay != null )
			{
				longOpDisplay.Dispose();
				longOpDisplay = null;
			}

            if (userSettings.GetSetting("Options.TestLoader.ClearResultsOnReload", false))
                runCount.Text = null;

			EnableRunCommand( true );
		}

		/// <summary>
		/// Event handler for assembly load failures. We do this via
		/// an event since some errors may occur asynchronously.
		/// </summary>
		private void OnTestLoadFailure( object sender, TestEventArgs e )
		{
			if ( longOpDisplay != null )
			{
				longOpDisplay.Dispose();
				longOpDisplay = null;
			}
			
			string message = e.Action == NUnit.Util.TestAction.TestReloadFailed
                ? "Test reload failed!"
                : "Test load failed!";
            string NL = Environment.NewLine;
			if ( e.Exception is BadImageFormatException )
				message += string.Format(NL + NL +
                    "The assembly could not be loaded by NUnit. PossibleProblems include:" + NL + NL +
                    "1. The assembly may not be a valid .NET assembly." + NL + NL +
                    "2. You may be attempting to load an assembly built with a later version of the CLR than the version under which NUnit is currently running ({0})." + NL + NL +
                    "3. You may be attempting to load a 64-bit assembly into a 32-bit process.",
					Environment.Version.ToString(3) );

            MessageDisplay.Error(message, e.Exception);

			if ( !IsTestLoaded )
				OnTestUnloaded( sender, e );
			else
				EnableRunCommand( true );
		}

		#endregion

		#region Handlers for Test Running Events

		private void OnRunStarting( object sender, TestEventArgs e )
		{
			suiteName.Text = e.Name;
			EnableRunCommand( false );
			EnableStopCommand( true );
			runCount.Text = "";
		}

		private void OnRunFinished( object sender, TestEventArgs e )
		{
			EnableStopCommand( false );
			EnableRunCommand( false );

			if ( e.Exception != null )
			{
				if ( ! ( e.Exception is System.Threading.ThreadAbortException ) )
                    MessageDisplay.Error("NUnit Test Run Failed", e.Exception);
			}

			ResultSummarizer summary = new ResultSummarizer( e.Result );
			this.runCount.Text = string.Format(
                "Passed: {0}   Failed: {1}   Errors: {2}   Inconclusive: {3}   Invalid: {4}   Ignored: {5}   Skipped: {6}   Time: {7}",
                summary.Passed, summary.Failures, summary.Errors, summary.Inconclusive, summary.NotRunnable, summary.Ignored, summary.Skipped, summary.Time);

            string resultPath = Path.Combine(this.TestProject.BasePath, "TestResult.xml");
            try
            {
                TestLoader.SaveLastResult(resultPath);
                log.Debug("Saved result to {0}", resultPath);
            }
            catch (Exception ex)
            {
                log.Warning("Unable to save result to {0}\n{1}", resultPath, ex.ToString());
            }

            EnableRunCommand(true);

            if (e.Result.ResultState == ResultState.Failure ||
                e.Result.ResultState == ResultState.Error ||
                e.Result.ResultState == ResultState.Cancelled)
            {
                this.Activate();
            }
		}

		#endregion

		#region Helper methods for modifying the UI display

		/// <summary>
		/// Set the title bar based on the loaded file or project
		/// </summary>
		/// <param name="fileName"></param>
		private void SetTitleBar( string fileName )
		{
			this.Text = fileName == null 
				? "NUnit"
				: string.Format( "{0} - NUnit", Path.GetFileName( fileName ) );
		}

		private void EnableRunCommand( bool enable )
		{
			runButton.Enabled = enable;
			runAllMenuItem.Enabled = enable;
			runSelectedMenuItem.Enabled = enable;
		    runFailedMenuItem.Enabled = enable && this.TestLoader.TestResult != null &&
		        (this.TestLoader.TestResult.ResultState == ResultState.Failure ||
		         this.TestLoader.TestResult.ResultState == ResultState.Error);
		}

		private void EnableStopCommand( bool enable )
		{
			stopButton.Enabled = enable;
			stopRunMenuItem.Enabled = enable;
		}

		#endregion	
	}
}

