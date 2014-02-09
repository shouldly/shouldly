// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Filters;
using NUnit.Util;

namespace NUnit.UiKit
{

	public delegate void SelectedTestChangedHandler( ITest test );
	public delegate void CheckedTestChangedHandler( ITest[] tests );

	/// <summary>
	/// TestSuiteTreeView is a tree view control
	/// specialized for displaying the tests
	/// in an assembly. Clients should always
	/// use TestNode rather than TreeNode when
	/// dealing with this class to be sure of
	/// calling the proper methods.
	/// </summary>
	public class TestSuiteTreeView : TreeView
	{
        static Logger log = InternalTrace.GetLogger(typeof(TestSuiteTreeView));

		#region DisplayStyle Enumeraton

		/// <summary>
		/// Indicates how a tree should be displayed
		/// </summary>
		public enum DisplayStyle
		{
			Auto,		// Select based on space available
			Expand,		// Expand fully
			Collapse,	// Collpase fully
			HideTests	// Expand all but the fixtures, leaving
			// leaf nodes hidden
		}

		#endregion

        #region TreeStructureChangedException
        private class TreeStructureChangedException : Exception
        {
            public TreeStructureChangedException(string message)
                :base( message ) { }
        }
        #endregion

        #region Instance Variables

        /// <summary>
		/// Hashtable provides direct access to TestNodes
		/// </summary>
		private Hashtable treeMap = new Hashtable();
	
		/// <summary>
		/// The TestNode on which a right click was done
		/// </summary>
		private TestSuiteTreeNode explicitlySelectedNode;

		/// <summary>
		/// Whether the browser supports running tests,
		/// or just loading and examining them
		/// </summary>
		private bool runCommandSupported = true;
		
		/// <summary>
		/// Whether or not we track progress of tests visibly in the tree
		/// </summary>
		private bool displayProgress = true;

		/// <summary>
		/// The properties dialog if displayed
		/// </summary>
		private TestPropertiesDialog propertiesDialog;

		/// <summary>
		/// Source of events that the tree responds to and
		/// target for the run command.
		/// </summary>
		private ITestLoader loader;
		
		public System.Windows.Forms.ImageList treeImages;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// True if the UI should allow a run command to be selected
		/// </summary>
		private bool runCommandEnabled = false;

		private ITest[] runningTests;

		private TestFilter categoryFilter = TestFilter.Empty;

		private bool suppressEvents = false;

		private bool fixtureLoaded = false;

        private bool showInconclusiveResults = false;

		#endregion

		#region Construction and Initialization

		public TestSuiteTreeView()
		{
			InitializeComponent();

			this.ContextMenu = new System.Windows.Forms.ContextMenu();
			this.ContextMenu.Popup += new System.EventHandler( ContextMenu_Popup );

            LoadAlternateImages();

            Services.UserSettings.Changed += new SettingsEventHandler(UserSettings_Changed);
		}

        private void UserSettings_Changed(object sender, SettingsEventArgs args)
        {
            if (args.SettingName == "Gui.TestTree.AlternateImageSet")
            {
                LoadAlternateImages();
                Invalidate();
            }
        }

        private void LoadAlternateImages()
        {
            string imageSet = Services.UserSettings.GetSetting("Gui.TestTree.AlternateImageSet") as string;

            if (imageSet != null)
            {
                string[] imageNames = { "Skipped", "Failure", "Success", "Ignored", "Inconclusive" };

                for (int index = 0; index < imageNames.Length; index++)
                    LoadAlternateImage(index, imageNames[index], imageSet);
            }
        }

        private void LoadAlternateImage(int index, string name, string imageSet)
        {
            string imageDir = PathUtils.Combine(Assembly.GetExecutingAssembly(), "Images", "Tree", imageSet);

            string[] extensions = { ".png", ".jpg" };

            foreach (string ext in extensions)
            {
                string filePath = Path.Combine(imageDir, name + ext);
                if (File.Exists(filePath))
                {
                    treeImages.Images[index] = Image.FromFile(filePath);
                    break;
                }
            }
        }

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSuiteTreeView));
            this.treeImages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeImages
            // 
            this.treeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImages.ImageStream")));
            this.treeImages.TransparentColor = System.Drawing.Color.White;
            this.treeImages.Images.SetKeyName(0, "Skipped.png");
            this.treeImages.Images.SetKeyName(1, "Failure.png");
            this.treeImages.Images.SetKeyName(2, "Success.png");
            this.treeImages.Images.SetKeyName(3, "Ignored.png");
            this.treeImages.Images.SetKeyName(4, "Inconclusive.png");
            // 
            // TestSuiteTreeView
            // 
            this.ImageIndex = 0;
            this.ImageList = this.treeImages;
            this.SelectedImageIndex = 0;
            this.DoubleClick += new System.EventHandler(this.TestSuiteTreeView_DoubleClick);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TestSuiteTreeView_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TestSuiteTreeView_DragEnter);
            this.ResumeLayout(false);

		}

		public void Initialize( ITestLoader loader, ITestEvents events )
		{
			this.loader = loader;

			events.TestLoaded	+= new TestEventHandler( OnTestLoaded );
			events.TestReloaded	+= new TestEventHandler( OnTestChanged );
			events.TestUnloaded	+= new TestEventHandler( OnTestUnloaded );
			
			events.RunStarting	+= new TestEventHandler( OnRunStarting );
			events.RunFinished	+= new TestEventHandler( OnRunFinished );
			events.TestFinished	+= new TestEventHandler( OnTestResult );
			events.SuiteFinished+= new TestEventHandler( OnTestResult );
		}

		#endregion

		#region Properties and Events

		/// <summary>
		/// Property determining whether the run command
		/// is supported from the tree context menu and
		/// by double-clicking test cases.
		/// </summary>
		[Category( "Behavior" ), DefaultValue( true )]
		[Description("Indicates whether the tree context menu should include a run command")]
		public bool RunCommandSupported
		{
			get { return runCommandSupported; }
			set { runCommandSupported = value; }
		}

		/// <summary>
		/// Property determining whether tree should reDraw nodes
		/// as tests are complete in order to show progress.
		/// </summary>
		[Category( "Behavior" ), DefaultValue( true )]
		[Description("Indicates whether results should be displayed in the tree as each test completes")]
		public bool DisplayTestProgress
		{
			get { return displayProgress; }
			set { displayProgress = value; }
		}

        [Category( "Appearance" ), DefaultValue( false )]
        [Description("Indicates whether checkboxes are displayed beside test nodes")]
        public new bool CheckBoxes
        {
            get { return base.CheckBoxes; }
            set 
            { 
                if ( base.CheckBoxes != value )
                {
                    VisualState visualState = !value && this.TopNode != null
                        ? new VisualState(this)
                        : null;

                    base.CheckBoxes = value;

                    if ( CheckBoxesChanged != null )
                        CheckBoxesChanged(this, new EventArgs());

                    if (visualState != null)
                    {
                        try
                        {
                            suppressEvents = true;
                            visualState.ShowCheckBoxes = this.CheckBoxes;
                            //RestoreVisualState( visualState );
                            visualState.Restore(this);
                        }
                        finally
                        {
                            suppressEvents = false;
                        }
                    }
                }
            }
        }

        public bool ShowInconclusiveResults
        {
            get { return showInconclusiveResults; }
        }

		/// <summary>
		/// The currently selected test.
		/// </summary>
		[Browsable( false )]
		public ITest SelectedTest
		{
			get 
			{ 
				TestSuiteTreeNode node = (TestSuiteTreeNode)SelectedNode;
				return node == null ? null : node.Test;
			}
		}

		[Browsable( false )]
		public ITest[] CheckedTests 
		{
			get 
			{
				CheckedTestFinder finder = new CheckedTestFinder( this );
				return finder.GetCheckedTests( CheckedTestFinder.SelectionFlags.All );
			}
		}

		[Browsable( false )]
		public ITest[] SelectedTests
		{
			get
			{
				ITest[] result = null;

				if ( this.CheckBoxes )
				{
					CheckedTestFinder finder = new CheckedTestFinder( this );
					result = finder.GetCheckedTests( 
						CheckedTestFinder.SelectionFlags.Top | CheckedTestFinder.SelectionFlags.Explicit );
				}

				if ( result == null || result.Length == 0 )
					if ( this.SelectedTest != null )
						result = new ITest[] { this.SelectedTest };

				return result;
			}	
		}

		[Browsable( false )]
		public ITest[] FailedTests
		{
			get
			{
				FailedTestsFilterVisitor visitor = new FailedTestsFilterVisitor();
				Accept(visitor);
				return visitor.Tests;
			}
		}

		/// <summary>
		/// The currently selected test result or null
		/// </summary>
		[Browsable( false )]
		public TestResult SelectedTestResult
		{
			get 
			{
				TestSuiteTreeNode node = (TestSuiteTreeNode)SelectedNode;
				return node == null ? null : node.Result; 
			}
		}

		[Browsable(false)]
		public TestFilter CategoryFilter
		{
			get { return categoryFilter; }
			set 
			{ 
				categoryFilter = value;
 
				TestFilterVisitor visitor = new TestFilterVisitor( categoryFilter );
				this.Accept( visitor );
			}
		}

		public event SelectedTestChangedHandler SelectedTestChanged;
		public event CheckedTestChangedHandler CheckedTestChanged;
		public event EventHandler CheckBoxesChanged;

		public TestSuiteTreeNode this[string uniqueName]
		{
			get { return treeMap[uniqueName] as TestSuiteTreeNode; }
		}

		/// <summary>
		/// Test node corresponding to a test
		/// </summary>
		private TestSuiteTreeNode this[ITest test]
		{
			get { return FindNode( test ); }
		}

		/// <summary>
		/// Test node corresponding to a TestResultInfo
		/// </summary>
		private TestSuiteTreeNode this[TestResult result]
		{
			get	{ return FindNode( result.Test ); }
		}

		#endregion

		#region Handlers for events related to loading and running tests
		private void OnTestLoaded( object sender, TestEventArgs e )
		{
			CheckPropertiesDialog();
			TestNode test = e.Test as TestNode;
			if ( test != null )
				Load( test );
			runCommandEnabled = true;
		}

		private void OnTestChanged( object sender, TestEventArgs e )
		{
			TestNode test = e.Test as TestNode;
			if ( test != null )
				Invoke( new LoadHandler( Reload ), new object[]{ test } );
		}

		private void OnTestUnloaded( object sender, TestEventArgs e)
		{
			ClosePropertiesDialog();
		
			if ( Services.UserSettings.GetSetting( "Gui.TestTree.SaveVisualState", true ) && loader != null)
				try
				{
					new VisualState(this).Save(VisualState.GetVisualStateFileName(loader.TestFileName));
				}
				catch(Exception ex)
				{
					Debug.WriteLine( "Unable to save visual state." );
					Debug.WriteLine( ex );
				}

			Clear();
			explicitlySelectedNode = null;
			runCommandEnabled = false;
		}

		private void OnRunStarting( object sender, TestEventArgs e )
		{
			CheckPropertiesDialog();
#if ACCUMULATE_RESULTS
			if ( runningTests != null )
				foreach( ITest test in runningTests )
					this[test].ClearResults();
#else
			ClearAllResults();
#endif
			runCommandEnabled = false;
		}

		private void OnRunFinished( object sender, TestEventArgs e )
		{
			if ( runningTests != null )
				foreach( ITest test in runningTests )
					this[test].Expand();

			if ( propertiesDialog != null )
				propertiesDialog.Invoke( new PropertiesDisplayHandler( propertiesDialog.DisplayProperties ) );

			runningTests = null;
			runCommandEnabled = true;
		}

		private void OnTestResult( object sender, TestEventArgs e )
		{
			SetTestResult(e.Result);
		}
		#endregion

		#region Context Menu
		/// <summary>
		/// Handles right mouse button down by
		/// remembering the proper context item
		/// and implements multiple select with the left button.
		/// </summary>
		/// <param name="e">MouseEventArgs structure with information about the mouse position and button state</param>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right )
			{
				CheckPropertiesDialog();
				TreeNode theNode = GetNodeAt( e.X, e.Y );
				explicitlySelectedNode = theNode as TestSuiteTreeNode;
			}
//			else if (e.Button == MouseButtons.Left )
//			{
//				if ( Control.ModifierKeys == Keys.Control )
//				{
//					TestSuiteTreeNode theNode = GetNodeAt( e.X, e.Y ) as TestSuiteTreeNode;
//					if ( theNode != null )
//						theNode.IsSelected = true;
//				}
//				else
//				{
//					ClearSelected();
//				}
//			}

			base.OnMouseDown( e );
		}

		/// <summary>
		/// Build treeview context menu dynamically on popup
		/// </summary>
		private void ContextMenu_Popup(object sender, System.EventArgs e)
		{
			this.ContextMenu.MenuItems.Clear();

			TestSuiteTreeNode targetNode = explicitlySelectedNode != null ? explicitlySelectedNode : (TestSuiteTreeNode)SelectedNode;
			if ( targetNode == null )
				return;
	
			if ( RunCommandSupported )
			{
				// TODO: handle in Starting event
				if ( loader.Running )
					runCommandEnabled = false;

				MenuItem runMenuItem = new MenuItem( "&Run", new EventHandler( runMenuItem_Click ) );
				runMenuItem.DefaultItem = runMenuItem.Enabled = runCommandEnabled && targetNode.Included &&
                    (targetNode.Test.RunState == RunState.Runnable || targetNode.Test.RunState == RunState.Explicit);
		
				this.ContextMenu.MenuItems.Add( runMenuItem );

                this.ContextMenu.MenuItems.Add("-");
			}

            TestSuiteTreeNode theoryNode = targetNode.GetTheoryNode();
            if (theoryNode != null)
            {
                MenuItem failedAssumptionsMenuItem = new MenuItem("Show Failed Assumptions", new EventHandler(failedAssumptionsMenuItem_Click));
                failedAssumptionsMenuItem.Checked = theoryNode.ShowFailedAssumptions;
                this.ContextMenu.MenuItems.Add(failedAssumptionsMenuItem);

                this.ContextMenu.MenuItems.Add("-");
            }


            MenuItem showCheckBoxesMenuItem = new MenuItem("Show CheckBoxes", new EventHandler(showCheckBoxesMenuItem_Click));
            showCheckBoxesMenuItem.Checked = this.CheckBoxes;
            this.ContextMenu.MenuItems.Add(showCheckBoxesMenuItem);
            this.ContextMenu.MenuItems.Add("-");

            MenuItem loadFixtureMenuItem = new MenuItem("Load Fixture", new EventHandler(loadFixtureMenuItem_Click));
			loadFixtureMenuItem.Enabled = targetNode.Test.IsSuite && targetNode != Nodes[0];
			this.ContextMenu.MenuItems.Add( loadFixtureMenuItem );

			MenuItem clearFixtureMenuItem = new MenuItem( "Clear Fixture", new EventHandler( clearFixtureMenuItem_Click ) );
			clearFixtureMenuItem.Enabled = fixtureLoaded;
			this.ContextMenu.MenuItems.Add( clearFixtureMenuItem );
			this.ContextMenu.MenuItems.Add( "-" );

			MenuItem propertiesMenuItem = new MenuItem(
				"&Properties", new EventHandler( propertiesMenuItem_Click ) );
		
			this.ContextMenu.MenuItems.Add( propertiesMenuItem );
		}

		private void showCheckBoxesMenuItem_Click( object sender, System.EventArgs e)
		{
			this.CheckBoxes = !this.CheckBoxes;
		}

		/// <summary>
		/// When Expand context menu item is clicked, expand the node
		/// </summary>
		private void expandMenuItem_Click(object sender, System.EventArgs e)
		{
			TestSuiteTreeNode targetNode = explicitlySelectedNode != null ? explicitlySelectedNode : (TestSuiteTreeNode)SelectedNode;
			if ( targetNode != null )
				targetNode.Expand();
		}

		/// <summary>
		/// When Collapse context menu item is clicked, collapse the node
		/// </summary>
		private void collapseMenuItem_Click(object sender, System.EventArgs e)
		{
			TestSuiteTreeNode targetNode = explicitlySelectedNode != null ? explicitlySelectedNode : (TestSuiteTreeNode)SelectedNode;
			if ( targetNode != null )
				targetNode.Collapse();
		}

		private void expandAllMenuItem_Click(object sender, System.EventArgs e)
		{
			this.BeginUpdate();
			this.ExpandAll();
			this.EndUpdate();
		}

		private void collapseAllMenuItem_Click(object sender, System.EventArgs e)
		{
			this.BeginUpdate();
			this.CollapseAll();
			this.EndUpdate();

			// Compensate for a bug in the underlying control
			if ( this.Nodes.Count > 0 )
				this.SelectedNode = this.Nodes[0];	
		}

        private void failedAssumptionsMenuItem_Click(object sender, System.EventArgs e)
        {
            TestSuiteTreeNode targetNode = explicitlySelectedNode != null ? explicitlySelectedNode : (TestSuiteTreeNode)SelectedNode;
            TestSuiteTreeNode theoryNode = targetNode != null ? targetNode.GetTheoryNode() : null;
            if (theoryNode != null)
            {
                MenuItem item = (MenuItem)sender;

                BeginUpdate();
                item.Checked = !item.Checked;
                theoryNode.ShowFailedAssumptions = item.Checked;
                EndUpdate();
            }
        }

        /// <summary>
		/// When Run context menu item is clicked, run the test that
		/// was selected when the right click was done.
		/// </summary>
		private void runMenuItem_Click(object sender, System.EventArgs e)
		{
			//TODO: some sort of lock on these booleans?
			if ( runCommandEnabled )
			{
				runCommandEnabled = false;

				if ( explicitlySelectedNode != null )
					RunTests( new ITest[] { explicitlySelectedNode.Test }, false );
				else
					RunSelectedTests();
			}
		}

		private void runAllMenuItem_Click(object sender, System.EventArgs e)
		{
			if ( runCommandEnabled )
			{
				runCommandEnabled = false;
				RunAllTests();
			}
		}

		private void runFailedMenuItem_Click(object sender, System.EventArgs e)
		{
			if ( runCommandEnabled )
			{
				runCommandEnabled = false;
				RunFailedTests();
			}
		}

		private void loadFixtureMenuItem_Click( object sender, System.EventArgs e)
		{
			if ( explicitlySelectedNode != null )
			{
				loader.LoadTest( explicitlySelectedNode.Test.TestName.FullName );
				fixtureLoaded = true;
			}
		}

		private void clearFixtureMenuItem_Click( object sender, System.EventArgs e)
		{
			loader.LoadTest();
			fixtureLoaded = false;
		}

		private void propertiesMenuItem_Click( object sender, System.EventArgs e)
		{
			TestSuiteTreeNode targetNode = explicitlySelectedNode != null ? explicitlySelectedNode : (TestSuiteTreeNode)SelectedNode;
			if ( targetNode != null )
				ShowPropertiesDialog( targetNode );
		}
		#endregion

		#region Drag and drop

		/// <summary>
		/// Helper method to determine if an IDataObject is valid
		/// for dropping on the tree view. It must be a the drop
		/// of a single file with a valid assembly file type.
		/// </summary>
		/// <param name="data">IDataObject to be tested</param>
		/// <returns>True if dropping is allowed</returns>
		private bool IsValidFileDrop( IDataObject data )
		{
			if ( !data.GetDataPresent( DataFormats.FileDrop ) )
				return false;

			string [] fileNames = data.GetData( DataFormats.FileDrop ) as string [];

			if ( fileNames == null || fileNames.Length == 0 )
				return false;
			
			// We can't open more than one project at a time
			// so handle length of 1 separately.
			if ( fileNames.Length == 1 )
			{
				string fileName = fileNames[0];
				bool isProject = NUnitProject.IsNUnitProjectFile( fileName );
				if ( Services.UserSettings.GetSetting( "Options.TestLoader.VisualStudioSupport", false ) )
					isProject |= Services.ProjectService.CanConvertFrom( fileName );
				
				return isProject || PathUtils.IsAssemblyFileType( fileName );
			}

			// Multiple assemblies are allowed - we
			// assume they are all in the same directory
			// since they are being dragged together.
			foreach( string fileName in fileNames )
			{
				if ( !PathUtils.IsAssemblyFileType( fileName ) )
					return false;
			}

			return true;
		}

		private void TestSuiteTreeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if ( IsValidFileDrop( e.Data ) )
			{
				string[] fileNames = (string[])e.Data.GetData( DataFormats.FileDrop );
				if ( fileNames.Length == 1 )
					loader.LoadProject( fileNames[0] );
				else
					loader.LoadProject( fileNames );

				if (loader.IsProjectLoaded && loader.TestProject.IsLoadable)
					loader.LoadTest();
			}
		}

		private void TestSuiteTreeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if ( IsValidFileDrop( e.Data ) )
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		#endregion

		#region UI Event Handlers

		private void TestSuiteTreeView_DoubleClick(object sender, System.EventArgs e)
		{
			TestSuiteTreeNode node = SelectedNode as TestSuiteTreeNode;
			if ( runCommandSupported && runCommandEnabled && node.Nodes.Count == 0 && node.Included )
			{
				runCommandEnabled = false;
				
				// TODO: Since this is a terminal node, don't use a category filter
				RunTests( new ITest[] { SelectedTest }, true );
			}
		}

		protected override void OnAfterSelect(System.Windows.Forms.TreeViewEventArgs e)
		{
			explicitlySelectedNode = null;

			if ( !suppressEvents )
			{
				if ( SelectedTestChanged != null )
					SelectedTestChanged( SelectedTest );

				base.OnAfterSelect( e );
			}
		}

		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			if ( !suppressEvents )
			{
				if (CheckedTestChanged != null)
					CheckedTestChanged(CheckedTests);

				base.OnAfterCheck (e);
			}
		}

		#endregion

		#region Public methods to manipulate the tree

		/// <summary>
		/// Clear all the results in the tree.
		/// </summary>
		public void ClearAllResults()
		{
			foreach ( TestSuiteTreeNode rootNode in Nodes )
				rootNode.ClearResults();
		}

		/// <summary>
		/// Load the tree with a test hierarchy
		/// </summary>
		/// <param name="test">Test to be loaded</param>
		public void Load( TestNode test )
		{
			using( new CP.Windows.Forms.WaitCursor() )
			{
				Clear();
				BeginUpdate();

				try
				{
			
					AddTreeNodes( Nodes, test, false );		
					SetInitialExpansion();
				}
				finally
				{
					EndUpdate();
					explicitlySelectedNode = null;
                    this.Select();
				}

				if ( Services.UserSettings.GetSetting( "Gui.TestTree.SaveVisualState", true ) && loader != null)
					RestoreVisualState();
			}
		}

		/// <summary>
		/// Load the tree from a test result
		/// </summary>
		/// <param name="result"></param>
		public void Load( TestResult result )
		{
			using ( new CP.Windows.Forms.WaitCursor( ) )
			{
				Clear();
				BeginUpdate();

				try
				{
					AddTreeNodes( Nodes, result, false );
					SetInitialExpansion();
				}
				finally
				{
					EndUpdate();
				}
			}
		}

		/// <summary>
		/// Reload the tree with a changed test hierarchy
		/// while maintaining as much gui state as possible.
		/// </summary>
		/// <param name="test">Test suite to be loaded</param>
		public void Reload( TestNode test )
		{
            TestResult result = ((TestSuiteTreeNode)Nodes[0]).Result;
            VisualState visualState = new VisualState(this);

            Load(test);

            visualState.Restore(this);

            if (result != null && !Services.UserSettings.GetSetting("Options.TestLoader.ClearResultsOnReload", false))
                RestoreResults(result);
		}

		/// <summary>
		/// Clear all the info in the tree.
		/// </summary>
		public void Clear()
		{
			treeMap.Clear();
			Nodes.Clear();
		}

		protected override void OnAfterCollapse(TreeViewEventArgs e)
		{
			if ( !suppressEvents )
				base.OnAfterCollapse (e);
		}

		protected override void OnAfterExpand(TreeViewEventArgs e)
		{
			if ( !suppressEvents )
				base.OnAfterExpand (e);
		}

		public void Accept(TestSuiteTreeNodeVisitor visitor) 
		{
			foreach(TestSuiteTreeNode node in Nodes) 
			{
				node.Accept(visitor);
			}
		}

		public void ClearCheckedNodes() 
		{
			Accept(new ClearCheckedNodesVisitor());
		}

		public void CheckFailedNodes() 
		{
			Accept(new CheckFailedNodesVisitor());
		}

		/// <summary>
		/// Add the result of a test to the tree
		/// </summary>
		/// <param name="result">The result of the test</param>
		public void SetTestResult(TestResult result)
		{
			TestSuiteTreeNode node = this[result];
            if (node == null)
            {
                Debug.WriteLine("Test not found in tree: " + result.Test.TestName.UniqueName);
            }
            else
            {
                node.Result = result;

                if (result.Test.TestType == "Theory")
                    node.RepopulateTheoryNode();

                if (DisplayTestProgress && node.IsVisible)
                {
                    Invalidate(node.Bounds);
                    Update();
                }
            }
		}

		public void HideTests()
		{
			this.BeginUpdate();
			foreach( TestSuiteTreeNode node in Nodes )
				HideTestsUnderNode( node );
			this.EndUpdate();
		}

		public void ShowPropertiesDialog( ITest test )
		{
			ShowPropertiesDialog( this[ test ] );
		}

		private void ShowPropertiesDialog( TestSuiteTreeNode node )
		{
			if ( propertiesDialog == null )
			{
				Form owner = this.FindForm();
				propertiesDialog = new TestPropertiesDialog( node );
				propertiesDialog.Owner = owner;
                propertiesDialog.Font = owner.Font;
				propertiesDialog.StartPosition = FormStartPosition.Manual;
				propertiesDialog.Left = Math.Max(0, owner.Left + ( owner.Width - propertiesDialog.Width ) / 2);
				propertiesDialog.Top = Math.Max(0, owner.Top + ( owner.Height - propertiesDialog.Height ) / 2);
				propertiesDialog.Show();
				propertiesDialog.Closed += new EventHandler( OnPropertiesDialogClosed );
			}
			else
			{
				propertiesDialog.DisplayProperties( node );
			}
		}

		private void ClosePropertiesDialog()
		{
			if ( propertiesDialog != null )
				propertiesDialog.Close();
		}

		private void CheckPropertiesDialog()
		{
			if ( propertiesDialog != null && !propertiesDialog.Pinned )
				propertiesDialog.Close();
		}

		private void OnPropertiesDialogClosed( object sender, System.EventArgs e )
		{
			propertiesDialog = null;
		}

		#endregion

		#region Running Tests

        public void RunAllTests()
        {
            RunAllTests(true);
        }

		public void RunAllTests(bool ignoreCategories)
		{
            if (Nodes.Count > 0)
            {
                runCommandEnabled = false;
                RunTests(new ITest[] { ((TestSuiteTreeNode)Nodes[0]).Test }, ignoreCategories);
            }
		}

		public void RunSelectedTests()
		{
			runCommandEnabled = false;
			RunTests( SelectedTests, false );
		}

		public void RunFailedTests()
		{
			runCommandEnabled = false;
			RunTests( FailedTests, true );
		}

		private void RunTests( ITest[] tests, bool ignoreCategories )
		{
            if (tests != null && tests.Length > 0)
            {
                runningTests = tests;

                ITestFilter filter = ignoreCategories
                    ? MakeNameFilter(tests)
                    : MakeFilter(tests);

                loader.RunTests(filter);
            }
		}

		private TestFilter MakeFilter( ITest[] tests )
		{
			TestFilter nameFilter = MakeNameFilter( tests );

			if ( nameFilter == TestFilter.Empty )
				return CategoryFilter;

			if ( tests.Length == 1 )
			{
				TestSuiteTreeNode rootNode = (TestSuiteTreeNode)Nodes[0];
				if ( tests[0] == rootNode.Test )
					return CategoryFilter;
			}

			if ( CategoryFilter.IsEmpty )
				return nameFilter;

			return new AndFilter( nameFilter, CategoryFilter );
		}

		private TestFilter MakeNameFilter( ITest[] tests )
		{
			if ( tests == null || tests.Length == 0 )
				return TestFilter.Empty;

			NameFilter nameFilter = new NameFilter();
			foreach( ITest test in tests )
				nameFilter.Add( test.TestName );

			return nameFilter;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Add nodes to the tree constructed from a test
		/// </summary>
		/// <param name="nodes">The TreeNodeCollection to which the new node should  be added</param>
		/// <param name="rootTest">The test for which a node is to be built</param>
		/// <param name="highlight">If true, highlight the text for this node in the tree</param>
		/// <returns>A newly constructed TestNode, possibly with descendant nodes</returns>
		private TestSuiteTreeNode AddTreeNodes( IList nodes, TestNode rootTest, bool highlight )
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( rootTest );
			//			if ( highlight ) node.ForeColor = Color.Blue;
			AddToMap( node );

			nodes.Add( node );
			
			if ( rootTest.IsSuite )
			{
				foreach( TestNode test in rootTest.Tests )
					AddTreeNodes( node.Nodes, test, highlight );
			}

			return node;
		}

		private TestSuiteTreeNode AddTreeNodes( IList nodes, TestResult rootResult, bool highlight )
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( rootResult );
			AddToMap( node );

			nodes.Add( node );
			
			if ( rootResult.HasResults )
			{
				foreach( TestResult result in rootResult.Results )
					AddTreeNodes( node.Nodes, result, highlight );
			}

			node.UpdateImageIndex();

			return node;
		}

		private void AddToMap( TestSuiteTreeNode node )
		{
			string key = node.Test.TestName.UniqueName;

			if ( treeMap.ContainsKey( key ) )
				log.Error( "Duplicate entry: " + key );
				//				UserMessage.Display( string.Format( 
				//					"The test {0} is duplicated\r\rResults will not be displayed correctly in the tree.", node.Test.FullName ), "Duplicate Test" );
			else
			{
				log.Debug( "Added to map: " + node.Test.TestName.UniqueName );
				treeMap.Add( key, node );
			}
		}

		private void RemoveFromMap( TestSuiteTreeNode node )
		{
			foreach( TestSuiteTreeNode child in node.Nodes )
				RemoveFromMap( child );
			treeMap.Remove( node.Test.TestName.UniqueName );
		}

		/// <summary>
		/// Remove a node from the tree itself and the hashtable
		/// </summary>
		/// <param name="node">Node to remove</param>
		private void RemoveNode( TestSuiteTreeNode node )
		{
			if ( explicitlySelectedNode == node )
				explicitlySelectedNode = null;
			RemoveFromMap( node );
			node.Remove();
		}

		/// <summary>
		/// Delegate for use in invoking the tree loader
		/// from the watcher thread.
		/// </summary>
		private delegate void LoadHandler( TestNode test );

		private delegate void PropertiesDisplayHandler();
		
		/// <summary>
		/// Helper collapses all fixtures under a node
		/// </summary>
		/// <param name="node">Node under which to collapse fixtures</param>
		private void HideTestsUnderNode( TestSuiteTreeNode node )
		{
            if (node.Test.IsSuite)
            {
                if (node.Test.TestType == "TestFixture")
                    node.Collapse();
                else
                {
                    node.Expand();

                    foreach (TestSuiteTreeNode child in node.Nodes)
                        HideTestsUnderNode(child);
                }
            }
		}

		/// <summary>
		/// Helper used to figure out the display style
		/// to use when the setting is Auto
		/// </summary>
		/// <returns>DisplayStyle to be used</returns>
		private DisplayStyle GetDisplayStyle()
		{
			DisplayStyle initialDisplay = (TestSuiteTreeView.DisplayStyle)
				Services.UserSettings.GetSetting( "Gui.TestTree.InitialTreeDisplay", DisplayStyle.Auto );

			if ( initialDisplay != DisplayStyle.Auto )
				return initialDisplay;

			if ( VisibleCount >= this.GetNodeCount( true ) )
				return DisplayStyle.Expand;

			return DisplayStyle.HideTests;
		}

		public void SetInitialExpansion()
		{
			CollapseAll();
			
			switch ( GetDisplayStyle() )
			{
				case DisplayStyle.Expand:
					ExpandAll();
					break;
				case DisplayStyle.HideTests:
					HideTests();
					break;
				case DisplayStyle.Collapse:
				default:
					break;
			}
			
			SelectedNode = Nodes[0];
			SelectedNode.EnsureVisible();
		}

		private TestSuiteTreeNode FindNode( ITest test )
		{
			TestSuiteTreeNode node = treeMap[test.TestName.UniqueName] as TestSuiteTreeNode;

            if (node == null)
                node = FindNodeByName(test.TestName.FullName);

            return node;
		}

        private TestSuiteTreeNode FindNodeByName( string fullName )
        {
            foreach( string uname in treeMap.Keys )
            {
                int rbrack = uname.IndexOf(']');
                string name = rbrack >=0 ? uname.Substring(rbrack+1) : uname;
                if ( name == fullName )
                    return treeMap[uname] as TestSuiteTreeNode;
            }

            return null;
        }

		private void RestoreVisualState()
		{
            if (loader != null)
            {
                string fileName = VisualState.GetVisualStateFileName(loader.TestFileName);
                if (File.Exists(fileName))
                {
                    VisualState.LoadFrom(fileName).Restore(this);
                }
            }
        }

        private void RestoreResults(TestResult result)
        {
            if (result.HasResults)
                foreach (TestResult childResult in result.Results)
                    RestoreResults(childResult);

            SetTestResult(result);
        }

		#endregion
	}

	#region Helper Classes

    #region ClearCheckedNodesVisitor

    internal class ClearCheckedNodesVisitor : TestSuiteTreeNodeVisitor
	{
		public override void Visit(TestSuiteTreeNode node)
		{
			node.Checked = false;
		}

    }

    #endregion

    #region CheckFailedNodesVisitor

    internal class CheckFailedNodesVisitor : TestSuiteTreeNodeVisitor 
	{
		public override void Visit(TestSuiteTreeNode node)
		{
			if (!node.Test.IsSuite && node.HasResult && 
                (node.Result.ResultState == ResultState.Failure || 
                 node.Result.ResultState == ResultState.Error) )
			{
				node.Checked = true;
				node.EnsureVisible();
			}
			else
				node.Checked = false;
		
		}
    }

    #endregion

    #region FailedTestsFilterVisitor

    internal class FailedTestsFilterVisitor : TestSuiteTreeNodeVisitor
	{
		List<ITest> tests = new List<ITest>();

		public ITest[] Tests
		{
			get { return tests.ToArray(); }
		}

		public override void Visit(TestSuiteTreeNode node)
		{
			if (!node.Test.IsSuite && node.HasResult && 
                    (node.Result.ResultState == ResultState.Failure || 
                     node.Result.ResultState == ResultState.Error) )
			{
				tests.Add(node.Test);
			}
		}
    }

    #endregion

    #region TestFilterVisitor

    public class TestFilterVisitor : TestSuiteTreeNodeVisitor
	{
		private ITestFilter filter;

		public TestFilterVisitor( ITestFilter filter )
		{
			this.filter = filter;
		}

		public override void Visit( TestSuiteTreeNode node )
		{
			node.Included = filter.Pass( node.Test );
		}
    }

    #endregion

    #region CheckedTestFinder

    internal class CheckedTestFinder
	{
		[Flags]
		public enum SelectionFlags
		{
			Top= 1,
			Sub = 2,
			Explicit = 4,
			All = Top + Sub
		}

		private List<CheckedTestInfo> checkedTests = new List<CheckedTestInfo>();
		private struct CheckedTestInfo
		{
			public ITest Test;
			public bool TopLevel;

			public CheckedTestInfo( ITest test, bool topLevel )
			{
				this.Test = test;
				this.TopLevel = topLevel;
			}
		}

		public ITest[] GetCheckedTests( SelectionFlags flags )
		{
			int count = 0;
			foreach( CheckedTestInfo info in checkedTests )
				if ( isSelected( info, flags ) ) count++;
	
			ITest[] result = new ITest[count];
			
			int index = 0;
			foreach( CheckedTestInfo info in checkedTests )
				if ( isSelected( info, flags ) )
					result[index++] = info.Test;

			return result;
		}

		private bool isSelected( CheckedTestInfo info, SelectionFlags flags )
		{
			if ( info.TopLevel && (flags & SelectionFlags.Top) != 0 )
				return true;
			else if ( !info.TopLevel && (flags & SelectionFlags.Sub) != 0 )
				return true;
			else if ( info.Test.RunState == RunState.Explicit && (flags & SelectionFlags.Explicit) != 0 )
				return true;
			else
				return false;
		}

		public CheckedTestFinder( TestSuiteTreeView treeView )
		{
			FindCheckedNodes( treeView.Nodes, true );
		}

		private void FindCheckedNodes( TestSuiteTreeNode node, bool topLevel )
		{
			if ( node.Checked )
			{
				checkedTests.Add( new CheckedTestInfo( node.Test, topLevel ) );
				topLevel = false;
			}
		
			FindCheckedNodes( node.Nodes, topLevel );
		}

		private void FindCheckedNodes( TreeNodeCollection nodes, bool topLevel )
		{
			foreach( TestSuiteTreeNode node in nodes )
				FindCheckedNodes( node, topLevel );
		}
    }

    #endregion

    #endregion
}