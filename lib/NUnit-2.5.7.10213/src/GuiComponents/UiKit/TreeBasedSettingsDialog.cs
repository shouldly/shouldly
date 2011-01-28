// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.UiKit
{
	public class TreeBasedSettingsDialog : NUnit.UiKit.SettingsDialogBase
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox1;

		private SettingsPage current;

		public static void Display( Form owner, params SettingsPage[] pages )
		{
			using( TreeBasedSettingsDialog dialog = new TreeBasedSettingsDialog() )
			{
				owner.Site.Container.Add( dialog );
				dialog.Font = owner.Font;
				dialog.SettingsPages.AddRange( pages ); 
				dialog.ShowDialog();
			}
		}

		public TreeBasedSettingsDialog()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TreeBasedSettingsDialog));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(592, 392);
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(504, 392);
			this.okButton.Name = "okButton";
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeView1.HideSelection = false;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(16, 16);
			this.treeView1.Name = "treeView1";
			this.treeView1.PathSeparator = ".";
			this.treeView1.Size = new System.Drawing.Size(176, 350);
			this.treeView1.TabIndex = 19;
			this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
			this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(208, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 336);
			this.panel1.TabIndex = 20;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(208, 360);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 8);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			// 
			// TreeBasedSettingsDialog
			// 
			this.ClientSize = new System.Drawing.Size(682, 426);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.treeView1);
			this.Name = "TreeBasedSettingsDialog";
			this.Load += new System.EventHandler(this.TreeBasedSettingsDialog_Load);
			this.Controls.SetChildIndex(this.treeView1, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void TreeBasedSettingsDialog_Load(object sender, System.EventArgs e)
		{
			foreach( SettingsPage page in SettingsPages )
				AddBranchToTree( treeView1.Nodes, page.Key );

			if ( treeView1.VisibleCount >= treeView1.GetNodeCount( true ) )
				treeView1.ExpandAll();

            SelectInitialPage();

			treeView1.Select();
		}

        private void SelectInitialPage()
        {
            string initialPage = Services.UserSettings.GetSetting("Gui.Settings.InitialPage") as string;

            if (initialPage != null)
                SelectPage(initialPage);
            else if (treeView1.Nodes.Count > 0)
                SelectFirstPage(treeView1.Nodes);
        }

        private void SelectPage(string initialPage)
        {
            TreeNode node = FindNode(treeView1.Nodes, initialPage);
            if (node != null)
                treeView1.SelectedNode = node;
            else
                SelectFirstPage(treeView1.Nodes);
        }

        private TreeNode FindNode(TreeNodeCollection nodes, string key)
        {
            int dot = key.IndexOf('.');
            string tail = null;

            if (dot >= 0)
            {
                tail = key.Substring(dot + 1);
                key = key.Substring(0, dot);
            }

            foreach (TreeNode node in nodes)
                if (node.Text == key)
                    return tail == null
                        ? node
                        : FindNode(node.Nodes, tail);

            return null;
        }

		private void SelectFirstPage(TreeNodeCollection nodes)
		{
			if ( nodes[0].Nodes.Count == 0 )
				treeView1.SelectedNode = nodes[0];
			else
			{
				nodes[0].Expand();
				SelectFirstPage(nodes[0].Nodes);
			}
		}

		private void AddBranchToTree( TreeNodeCollection nodes, string key )
		{
			int dot = key.IndexOf( '.' );
			if ( dot < 0 )
			{
				nodes.Add( new TreeNode( key, 2, 2 ) );
				return;
			}

			string name = key.Substring( 0, dot );
			key = key.Substring(dot+1);

			TreeNode node = FindOrAddNode( nodes, name );
			
			if ( key != null )
				AddBranchToTree( node.Nodes, key );
		}

		private TreeNode FindOrAddNode( TreeNodeCollection nodes, string name )
		{
			foreach( TreeNode node in nodes )
				if ( node.Text == name )
					return node;

			TreeNode newNode = new TreeNode(name, 0, 0);
			nodes.Add( newNode );
			return newNode;
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			string key = e.Node.FullPath;
			SettingsPage page = SettingsPages[key];
            Services.UserSettings.SaveSetting("Gui.Settings.InitialPage", key);

			if ( page != null && page != current )
			{
				panel1.Controls.Clear();
				panel1.Controls.Add( page );
				page.Dock = DockStyle.Fill;
				current = page;
				return;
			}
		}

		private void treeView1_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = e.Node.SelectedImageIndex = 1;
		}

		private void treeView1_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = e.Node.SelectedImageIndex = 0;
		}
	}
}

