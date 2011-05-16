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

namespace NUnit.Gui.SettingsPages
{
	public class VisualStudioSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox visualStudioSupportCheckBox;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.ComponentModel.IContainer components = null;

		public VisualStudioSettingsPage(string key) : base(key)
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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.visualStudioSupportCheckBox = new System.Windows.Forms.CheckBox();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Visual Studio";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(80, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 8);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// visualStudioSupportCheckBox
			// 
			this.helpProvider1.SetHelpString(this.visualStudioSupportCheckBox, "If checked, Visual Studio projects and solutions may be opened or added to existi" +
				"ng test projects.");
			this.visualStudioSupportCheckBox.Location = new System.Drawing.Point(24, 24);
			this.visualStudioSupportCheckBox.Name = "visualStudioSupportCheckBox";
			this.helpProvider1.SetShowHelp(this.visualStudioSupportCheckBox, true);
			this.visualStudioSupportCheckBox.Size = new System.Drawing.Size(224, 25);
			this.visualStudioSupportCheckBox.TabIndex = 30;
			this.visualStudioSupportCheckBox.Text = "Enable Visual Studio Support";
			// 
			// VisualStudioSettingsPage
			// 
			this.Controls.Add(this.visualStudioSupportCheckBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Name = "VisualStudioSettingsPage";
			this.ResumeLayout(false);

		}
		#endregion

		public override void LoadSettings()
		{
			visualStudioSupportCheckBox.Checked = settings.GetSetting( "Options.TestLoader.VisualStudioSupport", false );
		}

		public override void ApplySettings()
		{
			settings.SaveSetting( "Options.TestLoader.VisualStudioSupport", visualStudioSupportCheckBox.Checked );
		}


	}
}

