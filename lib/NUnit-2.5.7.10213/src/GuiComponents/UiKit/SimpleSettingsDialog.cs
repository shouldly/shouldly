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
	public class SimpleSettingsDialog : NUnit.UiKit.SettingsDialogBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.ComponentModel.IContainer components = null;

		public static void Display( Form owner, SettingsPage page )
		{
			using( SimpleSettingsDialog dialog = new SimpleSettingsDialog() )
			{
				owner.Site.Container.Add( dialog );
				dialog.Font = owner.Font;
				dialog.SettingsPages.Add( page ); 
				dialog.ShowDialog();
			}
		}

		public SimpleSettingsDialog()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(410, 392);
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(322, 392);
			this.okButton.Name = "okButton";
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(16, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 336);
			this.panel1.TabIndex = 21;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(16, 360);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 8);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			// 
			// SimpleSettingsDialog
			// 
			this.ClientSize = new System.Drawing.Size(490, 426);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Name = "SimpleSettingsDialog";
			this.Load += new System.EventHandler(this.SimpleSettingsDialog_Load);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void SimpleSettingsDialog_Load(object sender, System.EventArgs e)
		{
			SettingsPage page = this.SettingsPages[0];
			this.panel1.Controls.Add( page );
			this.Text = page.Title;
		}
	}
}

