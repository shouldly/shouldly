// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	public delegate void SelectedTestsChangedEventHandler(object sender, SelectedTestsChangedEventArgs e);
	/// <summary>
	/// Summary description for TestTree.
	/// </summary>
	public class TestTree : System.Windows.Forms.UserControl
	{
		#region Instance Variables

		// Contains all available categories, whether
		// selected or not. Unselected members of this
		// list are displayed in selectedList
		private IList availableCategories = new List<string>();

		// Our test loader
		private TestLoader loader;

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage testPage;
		private System.Windows.Forms.TabPage categoryPage;
		private System.Windows.Forms.Panel testPanel;
		private System.Windows.Forms.Panel categoryPanel;
		private System.Windows.Forms.Panel treePanel;
		private System.Windows.Forms.Panel buttonPanel;
		private NUnit.UiKit.TestSuiteTreeView tests;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox availableList;
		private System.Windows.Forms.GroupBox selectedCategories;
		private System.Windows.Forms.ListBox selectedList;
		private System.Windows.Forms.Panel categoryButtonPanel;
		private System.Windows.Forms.Button addCategory;
		private System.Windows.Forms.Button removeCategory;
		private System.Windows.Forms.Button clearAllButton;
		private System.Windows.Forms.Button checkFailedButton;
		private System.Windows.Forms.MenuItem treeMenu;
		private System.Windows.Forms.MenuItem checkBoxesMenuItem;
		private System.Windows.Forms.MenuItem treeMenuSeparatorItem1;
		private System.Windows.Forms.MenuItem expandMenuItem;
		private System.Windows.Forms.MenuItem collapseMenuItem;
		private System.Windows.Forms.MenuItem treeMenuSeparatorItem2;
		private System.Windows.Forms.MenuItem expandAllMenuItem;
		private System.Windows.Forms.MenuItem collapseAllMenuItem;
		private System.Windows.Forms.MenuItem hideTestsMenuItem;
		private System.Windows.Forms.MenuItem treeMenuSeparatorItem3;
		private System.Windows.Forms.MenuItem propertiesMenuItem;
		private System.Windows.Forms.CheckBox excludeCheckbox;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Properties

		public MenuItem TreeMenu 
		{
			get { return treeMenu; }
		}

		public string[] SelectedCategories
		{
			get
			{
				int n = this.selectedList.Items.Count;
				string[] categories = new string[n];
				for( int i = 0; i < n; i++ )
					categories[i] = this.selectedList.Items[i].ToString();
				return categories;
			}
		}

		[Browsable(false)]
		public bool ShowCheckBoxes
		{
			get { return tests.CheckBoxes; }
			set 
			{ 
				tests.CheckBoxes = value;
				buttonPanel.Visible	= value;
				clearAllButton.Visible = value;
				checkFailedButton.Visible = value;
				checkBoxesMenuItem.Checked = value;
			}
		}
		#endregion

		#region Construction and Initialization

		public TestTree()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			treeMenu = new MenuItem();
			this.checkBoxesMenuItem = new System.Windows.Forms.MenuItem();
			this.treeMenuSeparatorItem1 = new System.Windows.Forms.MenuItem();
			this.expandMenuItem = new System.Windows.Forms.MenuItem();
			this.collapseMenuItem = new System.Windows.Forms.MenuItem();
			this.treeMenuSeparatorItem2 = new System.Windows.Forms.MenuItem();
			this.expandAllMenuItem = new System.Windows.Forms.MenuItem();
			this.collapseAllMenuItem = new System.Windows.Forms.MenuItem();
			this.hideTestsMenuItem = new System.Windows.Forms.MenuItem();
			this.treeMenuSeparatorItem3 = new System.Windows.Forms.MenuItem();
			this.propertiesMenuItem = new System.Windows.Forms.MenuItem();

			// 
			// treeMenu
			// 
			this.treeMenu.MergeType = MenuMerge.Add;
			this.treeMenu.MergeOrder = 1;
			this.treeMenu.MenuItems.AddRange(
				new System.Windows.Forms.MenuItem[] 
				{
					this.checkBoxesMenuItem,
					this.treeMenuSeparatorItem1,
					this.expandMenuItem,
					this.collapseMenuItem,
					this.treeMenuSeparatorItem2,
					this.expandAllMenuItem,
					this.collapseAllMenuItem,
					this.hideTestsMenuItem,
					this.treeMenuSeparatorItem3,
					this.propertiesMenuItem 
				} );
			this.treeMenu.Text = "&Tree";
			this.treeMenu.Visible = false;
			this.treeMenu.Popup += new System.EventHandler(this.treeMenu_Popup);
			// 
			// checkBoxesMenuItem
			// 
			this.checkBoxesMenuItem.Index = 0;
			this.checkBoxesMenuItem.Text = "Show Check&Boxes";
			this.checkBoxesMenuItem.Click += new System.EventHandler(this.checkBoxesMenuItem_Click);
			// 
			// treeMenuSeparatorItem1
			// 
			this.treeMenuSeparatorItem1.Index = 1;
			this.treeMenuSeparatorItem1.Text = "-";
			// 
			// expandMenuItem
			// 
			this.expandMenuItem.Index = 2;
			this.expandMenuItem.Text = "&Expand";
			this.expandMenuItem.Click += new System.EventHandler(this.expandMenuItem_Click);
			// 
			// collapseMenuItem
			// 
			this.collapseMenuItem.Index = 3;
			this.collapseMenuItem.Text = "&Collapse";
			this.collapseMenuItem.Click += new System.EventHandler(this.collapseMenuItem_Click);
			// 
			// treeMenuSeparatorItem2
			// 
			this.treeMenuSeparatorItem2.Index = 4;
			this.treeMenuSeparatorItem2.Text = "-";
			// 
			// expandAllMenuItem
			// 
			this.expandAllMenuItem.Index = 5;
			this.expandAllMenuItem.Text = "Expand All";
			this.expandAllMenuItem.Click += new System.EventHandler(this.expandAllMenuItem_Click);
			// 
			// collapseAllMenuItem
			// 
			this.collapseAllMenuItem.Index = 6;
			this.collapseAllMenuItem.Text = "Collapse All";
			this.collapseAllMenuItem.Click += new System.EventHandler(this.collapseAllMenuItem_Click);
			// 
			// hideTestsMenuItem
			// 
			this.hideTestsMenuItem.Index = 7;
			this.hideTestsMenuItem.Text = "Hide Tests";
			this.hideTestsMenuItem.Click += new System.EventHandler(this.hideTestsMenuItem_Click);
			// 
			// treeMenuSeparatorItem3
			// 
			this.treeMenuSeparatorItem3.Index = 8;
			this.treeMenuSeparatorItem3.Text = "-";
			// 
			// propertiesMenuItem
			// 
			this.propertiesMenuItem.Index = 9;
			this.propertiesMenuItem.Text = "&Properties";
			this.propertiesMenuItem.Click += new System.EventHandler(this.propertiesMenuItem_Click);


			tests.SelectedTestChanged += new SelectedTestChangedHandler(tests_SelectedTestChanged);
			tests.CheckedTestChanged += new CheckedTestChangedHandler(tests_CheckedTestChanged);

			this.excludeCheckbox.Enabled = false;
		}

		protected override void OnLoad(EventArgs e)
		{
			if ( !this.DesignMode )
			{
				this.ShowCheckBoxes = 
					Services.UserSettings.GetSetting( "Options.ShowCheckBoxes", false );
				Initialize( Services.TestLoader );
				Services.UserSettings.Changed += new SettingsEventHandler(UserSettings_Changed);
			}

			base.OnLoad (e);
		}

		public void Initialize(TestLoader loader) 
		{
			this.tests.Initialize(loader, loader.Events);
			this.loader = loader;
			loader.Events.TestLoaded += new NUnit.Util.TestEventHandler(events_TestLoaded);
			loader.Events.TestReloaded += new NUnit.Util.TestEventHandler(events_TestReloaded);
			loader.Events.TestUnloaded += new NUnit.Util.TestEventHandler(events_TestUnloaded);
		}

		public void SelectCategories( string[] categories, bool exclude )
		{
			foreach( string category in categories )
			{
				if ( availableCategories.Contains( category ) )
				{
					if (!selectedList.Items.Contains(category))
					{
						selectedList.Items.Add(category);
					}
					availableList.Items.Remove( category );

					this.excludeCheckbox.Checked = exclude;
				}
			}

			UpdateCategoryFilter();
			if (this.SelectedCategories.Length > 0)
				this.excludeCheckbox.Enabled = true;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region View Menu Handlers

		private void treeMenu_Popup(object sender, System.EventArgs e)
		{
			TreeNode selectedNode = tests.SelectedNode;
			if ( selectedNode != null && selectedNode.Nodes.Count > 0 )
			{
				bool isExpanded = selectedNode.IsExpanded;
				collapseMenuItem.Enabled = isExpanded;
				expandMenuItem.Enabled = !isExpanded;		
			}
			else
			{
				collapseMenuItem.Enabled = expandMenuItem.Enabled = false;
			}
		}

		private void collapseMenuItem_Click(object sender, System.EventArgs e)
		{
			tests.SelectedNode.Collapse();
		}

		private void expandMenuItem_Click(object sender, System.EventArgs e)
		{
			tests.SelectedNode.Expand();
		}

		private void collapseAllMenuItem_Click(object sender, System.EventArgs e)
		{
			tests.BeginUpdate();
			tests.CollapseAll();
			tests.EndUpdate();

			// Compensate for a bug in the underlying control
			if ( tests.Nodes.Count > 0 )
				tests.SelectedNode = tests.Nodes[0];	
		}

		private void hideTestsMenuItem_Click(object sender, System.EventArgs e)
		{
			tests.HideTests();
		}

		private void expandAllMenuItem_Click(object sender, System.EventArgs e)
		{
			tests.BeginUpdate();
			tests.ExpandAll();
			tests.EndUpdate();
		}

		private void propertiesMenuItem_Click(object sender, System.EventArgs e)
		{
			if ( tests.SelectedTest != null )
				tests.ShowPropertiesDialog( tests.SelectedTest );
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabs = new System.Windows.Forms.TabControl();
			this.testPage = new System.Windows.Forms.TabPage();
			this.testPanel = new System.Windows.Forms.Panel();
			this.treePanel = new System.Windows.Forms.Panel();
			this.tests = new NUnit.UiKit.TestSuiteTreeView();
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.checkFailedButton = new System.Windows.Forms.Button();
			this.clearAllButton = new System.Windows.Forms.Button();
			this.categoryPage = new System.Windows.Forms.TabPage();
			this.categoryPanel = new System.Windows.Forms.Panel();
			this.categoryButtonPanel = new System.Windows.Forms.Panel();
			this.removeCategory = new System.Windows.Forms.Button();
			this.addCategory = new System.Windows.Forms.Button();
			this.selectedCategories = new System.Windows.Forms.GroupBox();
			this.selectedList = new System.Windows.Forms.ListBox();
			this.excludeCheckbox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.availableList = new System.Windows.Forms.ListBox();
			this.tabs.SuspendLayout();
			this.testPage.SuspendLayout();
			this.testPanel.SuspendLayout();
			this.treePanel.SuspendLayout();
			this.buttonPanel.SuspendLayout();
			this.categoryPage.SuspendLayout();
			this.categoryPanel.SuspendLayout();
			this.categoryButtonPanel.SuspendLayout();
			this.selectedCategories.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabs.Controls.Add(this.testPage);
			this.tabs.Controls.Add(this.categoryPage);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Multiline = true;
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(248, 496);
			this.tabs.TabIndex = 0;
			// 
			// testPage
			// 
			this.testPage.Controls.Add(this.testPanel);
			this.testPage.Location = new System.Drawing.Point(25, 4);
			this.testPage.Name = "testPage";
			this.testPage.Size = new System.Drawing.Size(219, 488);
			this.testPage.TabIndex = 0;
			this.testPage.Text = "Tests";
			// 
			// testPanel
			// 
			this.testPanel.Controls.Add(this.treePanel);
			this.testPanel.Controls.Add(this.buttonPanel);
			this.testPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testPanel.Location = new System.Drawing.Point(0, 0);
			this.testPanel.Name = "testPanel";
			this.testPanel.Size = new System.Drawing.Size(219, 488);
			this.testPanel.TabIndex = 0;
			// 
			// treePanel
			// 
			this.treePanel.Controls.Add(this.tests);
			this.treePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePanel.Location = new System.Drawing.Point(0, 0);
			this.treePanel.Name = "treePanel";
			this.treePanel.Size = new System.Drawing.Size(219, 448);
			this.treePanel.TabIndex = 0;
			// 
			// tests
			// 
			this.tests.AllowDrop = true;
			this.tests.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tests.HideSelection = false;
			this.tests.Location = new System.Drawing.Point(0, 0);
			this.tests.Name = "tests";
			this.tests.Size = new System.Drawing.Size(219, 448);
			this.tests.TabIndex = 0;
			this.tests.CheckBoxesChanged += new System.EventHandler(this.tests_CheckBoxesChanged);
			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.checkFailedButton);
			this.buttonPanel.Controls.Add(this.clearAllButton);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonPanel.Location = new System.Drawing.Point(0, 448);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(219, 40);
			this.buttonPanel.TabIndex = 1;
			// 
			// checkFailedButton
			// 
			this.checkFailedButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.checkFailedButton.Location = new System.Drawing.Point(117, 8);
			this.checkFailedButton.Name = "checkFailedButton";
			this.checkFailedButton.Size = new System.Drawing.Size(96, 23);
			this.checkFailedButton.TabIndex = 1;
			this.checkFailedButton.Text = "Check Failed";
			this.checkFailedButton.Click += new System.EventHandler(this.checkFailedButton_Click);
			// 
			// clearAllButton
			// 
			this.clearAllButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.clearAllButton.Location = new System.Drawing.Point(13, 8);
			this.clearAllButton.Name = "clearAllButton";
			this.clearAllButton.Size = new System.Drawing.Size(96, 23);
			this.clearAllButton.TabIndex = 0;
			this.clearAllButton.Text = "Clear All";
			this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
			// 
			// categoryPage
			// 
			this.categoryPage.Controls.Add(this.categoryPanel);
			this.categoryPage.Location = new System.Drawing.Point(25, 4);
			this.categoryPage.Name = "categoryPage";
			this.categoryPage.Size = new System.Drawing.Size(219, 488);
			this.categoryPage.TabIndex = 1;
			this.categoryPage.Text = "Categories";
			// 
			// categoryPanel
			// 
			this.categoryPanel.Controls.Add(this.categoryButtonPanel);
			this.categoryPanel.Controls.Add(this.selectedCategories);
			this.categoryPanel.Controls.Add(this.groupBox1);
			this.categoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.categoryPanel.Location = new System.Drawing.Point(0, 0);
			this.categoryPanel.Name = "categoryPanel";
			this.categoryPanel.Size = new System.Drawing.Size(219, 488);
			this.categoryPanel.TabIndex = 0;
			// 
			// categoryButtonPanel
			// 
			this.categoryButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.categoryButtonPanel.Controls.Add(this.removeCategory);
			this.categoryButtonPanel.Controls.Add(this.addCategory);
			this.categoryButtonPanel.Location = new System.Drawing.Point(8, 280);
			this.categoryButtonPanel.Name = "categoryButtonPanel";
			this.categoryButtonPanel.Size = new System.Drawing.Size(203, 40);
			this.categoryButtonPanel.TabIndex = 1;
			// 
			// removeCategory
			// 
			this.removeCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.removeCategory.Location = new System.Drawing.Point(109, 8);
			this.removeCategory.Name = "removeCategory";
			this.removeCategory.TabIndex = 1;
			this.removeCategory.Text = "Remove";
			this.removeCategory.Click += new System.EventHandler(this.removeCategory_Click);
			// 
			// addCategory
			// 
			this.addCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.addCategory.Location = new System.Drawing.Point(21, 8);
			this.addCategory.Name = "addCategory";
			this.addCategory.TabIndex = 0;
			this.addCategory.Text = "Add";
			this.addCategory.Click += new System.EventHandler(this.addCategory_Click);
			// 
			// selectedCategories
			// 
			this.selectedCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.selectedCategories.Controls.Add(this.selectedList);
			this.selectedCategories.Controls.Add(this.excludeCheckbox);
			this.selectedCategories.Location = new System.Drawing.Point(8, 328);
			this.selectedCategories.Name = "selectedCategories";
			this.selectedCategories.Size = new System.Drawing.Size(203, 144);
			this.selectedCategories.TabIndex = 2;
			this.selectedCategories.TabStop = false;
			this.selectedCategories.Text = "Selected Categories";
			// 
			// selectedList
			// 
			this.selectedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.selectedList.ItemHeight = 16;
			this.selectedList.Location = new System.Drawing.Point(8, 16);
			this.selectedList.Name = "selectedList";
			this.selectedList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.selectedList.Size = new System.Drawing.Size(187, 84);
			this.selectedList.TabIndex = 0;
			this.selectedList.DoubleClick += new System.EventHandler(this.removeCategory_Click);
			// 
			// excludeCheckbox
			// 
			this.excludeCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.excludeCheckbox.Location = new System.Drawing.Point(8, 120);
			this.excludeCheckbox.Name = "excludeCheckbox";
			this.excludeCheckbox.Size = new System.Drawing.Size(179, 16);
			this.excludeCheckbox.TabIndex = 1;
			this.excludeCheckbox.Text = "Exclude these categories";
			this.excludeCheckbox.CheckedChanged += new System.EventHandler(this.excludeCheckbox_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.availableList);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(203, 272);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Available Categories";
			// 
			// availableList
			// 
			this.availableList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.availableList.ItemHeight = 16;
			this.availableList.Location = new System.Drawing.Point(8, 24);
			this.availableList.Name = "availableList";
			this.availableList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.availableList.Size = new System.Drawing.Size(187, 244);
			this.availableList.TabIndex = 0;
			this.availableList.DoubleClick += new System.EventHandler(this.addCategory_Click);
			// 
			// TestTree
			// 
			this.Controls.Add(this.tabs);
			this.Name = "TestTree";
			this.Size = new System.Drawing.Size(248, 496);
			this.tabs.ResumeLayout(false);
			this.testPage.ResumeLayout(false);
			this.testPanel.ResumeLayout(false);
			this.treePanel.ResumeLayout(false);
			this.buttonPanel.ResumeLayout(false);
			this.categoryPage.ResumeLayout(false);
			this.categoryPanel.ResumeLayout(false);
			this.categoryButtonPanel.ResumeLayout(false);
			this.selectedCategories.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region SelectedTestsChanged Event

		public event SelectedTestsChangedEventHandler SelectedTestsChanged;

		#endregion


        public void RunAllTests()
        {
            RunAllTests(true);
        }

		public void RunAllTests(bool ignoreTests)
		{
			tests.RunAllTests(ignoreTests);
		}

		public void RunSelectedTests()
		{
			tests.RunSelectedTests();
		}

		public void RunFailedTests()
		{
			tests.RunFailedTests();
		}

		private void addCategory_Click(object sender, System.EventArgs e)
		{
			if (availableList.SelectedItems.Count > 0) 
			{
                // Create a separate list to avoid exception
                // when using the list box directly.
                List<string> categories = new List<string>();
				foreach ( string category in availableList.SelectedItems ) 
                    categories.Add(category);

                foreach ( string category in categories)
                {
					selectedList.Items.Add(category);
					availableList.Items.Remove(category);
				}

				UpdateCategoryFilter();
				if (this.SelectedCategories.Length > 0)
					this.excludeCheckbox.Enabled = true;
			}
		}

		private void removeCategory_Click(object sender, System.EventArgs e)
		{
			if (selectedList.SelectedItems.Count > 0) 
			{
                // Create a separate list to avoid exception
                // when using the list box directly.
                List<string> categories = new List<string>();
                foreach (string category in selectedList.SelectedItems)
                    categories.Add(category);

				foreach ( string category in categories )
				{
					selectedList.Items.Remove(category);
					availableList.Items.Add(category);
				}

				UpdateCategoryFilter();
				if (this.SelectedCategories.Length == 0)
				{
					this.excludeCheckbox.Checked = false;
					this.excludeCheckbox.Enabled = false;
				}
			}
		}

		private void clearAllButton_Click(object sender, System.EventArgs e)
		{
			tests.ClearCheckedNodes();
		}

		private void checkFailedButton_Click(object sender, System.EventArgs e)
		{
			tests.CheckFailedNodes();
		}

		private void tests_SelectedTestChanged(ITest test)
		{
			if (SelectedTestsChanged != null) 
			{
				SelectedTestsChangedEventArgs args = new SelectedTestsChangedEventArgs(test.TestName.Name, test.TestCount);
				SelectedTestsChanged(tests, args);
			}
		}

		private void events_TestLoaded(object sender, NUnit.Util.TestEventArgs args)
		{			
			treeMenu.Visible = true;

			availableCategories = this.loader.GetCategories();
			availableList.Items.Clear();
			selectedList.Items.Clear();
			
			availableList.SuspendLayout();
			foreach (string category in availableCategories) 
				availableList.Items.Add(category);

			// tree may have restored visual state
			if( !tests.CategoryFilter.IsEmpty )
			{
				ITestFilter filter = tests.CategoryFilter;
				if ( filter is NUnit.Core.Filters.NotFilter )
				{
					filter = ((NUnit.Core.Filters.NotFilter)filter).BaseFilter;
					this.excludeCheckbox.Checked = true;
				}

				foreach( string cat in ((NUnit.Core.Filters.CategoryFilter)filter).Categories )
					if ( this.availableCategories.Contains( cat ) )
					{
						this.availableList.Items.Remove( cat );
						this.selectedList.Items.Add( cat );
						this.excludeCheckbox.Enabled = true;
					}

				UpdateCategoryFilter();
			}

			availableList.ResumeLayout();
		}

		private void events_TestReloaded(object sender, NUnit.Util.TestEventArgs args)
		{
			// Get new list of available categories
			availableCategories = this.loader.GetCategories();

			// Remove any selected items that are no longer available
			int index = selectedList.Items.Count;
			selectedList.SuspendLayout();
			while( --index >= 0 )
			{
				string category = selectedList.Items[index].ToString();
				if ( !availableCategories.Contains( category )  )
					selectedList.Items.RemoveAt( index );
			}
			selectedList.ResumeLayout();

            // Clear check box if there are no more selected items.
            if (selectedList.Items.Count == 0)
                excludeCheckbox.Checked = excludeCheckbox.Enabled = false;

            // Put any unselected available items on availableList
			availableList.Items.Clear();
			availableList.SuspendLayout();
			foreach( string category in availableCategories )
				if( selectedList.FindStringExact( category ) < 0 )
					availableList.Items.Add( category );
			availableList.ResumeLayout();

			// Tell the tree what is selected
			UpdateCategoryFilter();
		}

		private void excludeCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateCategoryFilter();
		}

		private void events_TestUnloaded(object sender, NUnit.Util.TestEventArgs args)
		{
			availableCategories.Clear();
			availableList.Items.Clear();
			selectedList.Items.Clear();
			excludeCheckbox.Checked = false;
			excludeCheckbox.Enabled = false;
			treeMenu.Visible = false;
		}

		private void tests_CheckedTestChanged(ITest[] tests)
		{
			if (SelectedTestsChanged != null) 
			{
				SelectedTestsChangedEventArgs args = new SelectedTestsChangedEventArgs("", tests.Length);
				SelectedTestsChanged(tests, args);
			}

			if (tests.Length > 0) 
			{
			}
		}

		private void checkBoxesMenuItem_Click(object sender, System.EventArgs e)
		{
			Services.UserSettings.SaveSetting( "Options.ShowCheckBoxes", 
				ShowCheckBoxes = !checkBoxesMenuItem.Checked );
			
			// Temporary till we can save tree state and restore
			//this.SetInitialExpansion();
		}

		private void UpdateCategoryFilter()
		{
			TestFilter catFilter;

			if ( SelectedCategories == null || SelectedCategories.Length == 0 )
				catFilter = TestFilter.Empty;
			else
				catFilter = new NUnit.Core.Filters.CategoryFilter( SelectedCategories );

			if ( excludeCheckbox.Checked )
				catFilter = new NUnit.Core.Filters.NotFilter( catFilter, true );

			tests.CategoryFilter = catFilter;
		}

		private void tests_CheckBoxesChanged(object sender, System.EventArgs e)
		{
			ShowCheckBoxes = tests.CheckBoxes;
		}

		private void UserSettings_Changed(object sender, SettingsEventArgs args)
		{
			if ( args.SettingName == "Options.ShowCheckBoxes" )
				this.ShowCheckBoxes = Services.UserSettings.GetSetting( args.SettingName, false );
		}
	}

	public class SelectedTestsChangedEventArgs : EventArgs 
	{
		private string testName;
		private int count;

		public SelectedTestsChangedEventArgs(string testName, int count) 
		{
			this.testName = testName;
			this.count = count;
		}

		public string TestName 
		{
			get { return testName; }
		}

		public int TestCount 
		{
			get { return count; }
		}
	}
}
