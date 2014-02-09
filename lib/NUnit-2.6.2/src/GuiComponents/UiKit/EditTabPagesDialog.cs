// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for EditTabPagesDialog.
	/// </summary>
	public class EditTabPagesDialog : NUnitFormBase
	{
		private TextDisplayTabSettings tabSettings;
		private int selectedIndex = -1;

		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.ListBox tabPageListBox;
		private System.Windows.Forms.Button moveDownButton;
		private System.Windows.Forms.Button moveUpButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditTabPagesDialog(TextDisplayTabSettings tabSettings)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.tabSettings = tabSettings;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.addButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.tabPageListBox = new System.Windows.Forms.ListBox();
			this.moveUpButton = new System.Windows.Forms.Button();
			this.moveDownButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(192, 48);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(96, 32);
			this.addButton.TabIndex = 10;
			this.addButton.Text = "&Add...";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(190, 218);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(96, 32);
			this.closeButton.TabIndex = 9;
			this.closeButton.Text = "Close";
			// 
			// removeButton
			// 
			this.removeButton.Location = new System.Drawing.Point(190, 10);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(96, 32);
			this.removeButton.TabIndex = 7;
			this.removeButton.Text = "&Remove";
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// tabPageListBox
			// 
			this.tabPageListBox.ItemHeight = 16;
			this.tabPageListBox.Location = new System.Drawing.Point(6, 10);
			this.tabPageListBox.Name = "tabPageListBox";
			this.tabPageListBox.Size = new System.Drawing.Size(168, 212);
			this.tabPageListBox.TabIndex = 6;
			this.tabPageListBox.SelectedIndexChanged += new System.EventHandler(this.tabPageListBox_SelectedIndexChanged);
			// 
			// moveUpButton
			// 
			this.moveUpButton.Location = new System.Drawing.Point(192, 88);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(96, 32);
			this.moveUpButton.TabIndex = 11;
			this.moveUpButton.Text = "Move Up";
			this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
			// 
			// moveDownButton
			// 
			this.moveDownButton.Location = new System.Drawing.Point(192, 128);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(96, 32);
			this.moveDownButton.TabIndex = 12;
			this.moveDownButton.Text = "Move Down";
			this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
			// 
			// EditTabPagesDialog
			// 
			this.AcceptButton = this.closeButton;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(292, 260);
			this.ControlBox = false;
			this.Controls.Add(this.moveDownButton);
			this.Controls.Add(this.moveUpButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.tabPageListBox);
			this.Name = "EditTabPagesDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Tab Pages";
			this.Load += new System.EventHandler(this.EditTabPagesDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void EditTabPagesDialog_Load(object sender, System.EventArgs e)
		{
			FillListBox();
			if ( tabPageListBox.Items.Count > 0 )
				tabPageListBox.SelectedIndex = selectedIndex = 0;
		}

		private void tabPageListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			selectedIndex = tabPageListBox.SelectedIndex;
			removeButton.Enabled = selectedIndex >= 0;
			moveUpButton.Enabled = selectedIndex > 0;
			moveDownButton.Enabled = selectedIndex >= 0 && selectedIndex < tabPageListBox.Items.Count - 1;
		}

		private void addButton_Click(object sender, System.EventArgs e)
		{
			using( AddTabPageDialog dlg = new AddTabPageDialog(tabSettings) )
			{
				this.Site.Container.Add( dlg );
				if ( dlg.ShowDialog() == DialogResult.OK )
					FillListBox();
			}		
		}

		private void removeButton_Click(object sender, System.EventArgs e)
		{
			tabSettings.Tabs.RemoveAt(selectedIndex);
			FillListBox();
		}

		private void renameButton_Click(object sender, System.EventArgs e)
		{
			tabSettings.Tabs[selectedIndex].Title = "";
		}

		#region Helper Methods

		private void FillListBox()
		{
			tabPageListBox.Items.Clear();

			foreach( TextDisplayTabSettings.TabInfo tab in tabSettings.Tabs )
				tabPageListBox.Items.Add( tab.Title );

			int count = tabPageListBox.Items.Count;
			if ( count > 0 )
			{
				if ( selectedIndex >= count )
					selectedIndex = count - 1;

				tabPageListBox.SelectedIndex = selectedIndex;
			}
			else selectedIndex = -1;
		}

		#endregion

		private void moveUpButton_Click(object sender, System.EventArgs e)
		{
			TextDisplayTabSettings.TabInfo tab = tabSettings.Tabs[selectedIndex];
			tabSettings.Tabs.RemoveAt(selectedIndex--);
			tabSettings.Tabs.Insert(selectedIndex, tab );
			FillListBox();
		}

		private void moveDownButton_Click(object sender, System.EventArgs e)
		{
			TextDisplayTabSettings.TabInfo tab = tabSettings.Tabs[selectedIndex];
			tabSettings.Tabs.RemoveAt(selectedIndex++);
			tabSettings.Tabs.Insert(selectedIndex, tab );
			FillListBox();
		}
	}
}
