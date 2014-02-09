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

namespace NUnit.UiKit
{
	public class TabbedSettingsDialog : NUnit.UiKit.SettingsDialogBase
	{
		protected System.Windows.Forms.TabControl tabControl1;
		private System.ComponentModel.IContainer components = null;

		public static void Display( Form owner, params SettingsPage[] pages )
		{
			using( TabbedSettingsDialog dialog = new TabbedSettingsDialog() )
			{
				owner.Site.Container.Add( dialog );
				dialog.Font = owner.Font;
				dialog.SettingsPages.AddRange( pages ); 
				dialog.ShowDialog();
			}
		}

		public TabbedSettingsDialog()
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(394, 392);
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(306, 392);
			this.okButton.Name = "okButton";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.ItemSize = new System.Drawing.Size(46, 18);
			this.tabControl1.Location = new System.Drawing.Point(10, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(456, 376);
			this.tabControl1.TabIndex = 2;
			// 
			// TabbedSettingsDialog
			// 
			this.ClientSize = new System.Drawing.Size(474, 426);
			this.Controls.Add(this.tabControl1);
			this.Name = "TabbedSettingsDialog";
			this.Load += new System.EventHandler(this.TabbedSettingsDialog_Load);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.tabControl1, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void TabbedSettingsDialog_Load(object sender, System.EventArgs e)
		{
			foreach( SettingsPage page in SettingsPages )
			{
				TabPage tabPage = new TabPage(page.Title);
				tabPage.Controls.Add( page );
				page.Location = new Point(0, 16);
				this.tabControl1.TabPages.Add( tabPage );
			}
		}
	}
}

