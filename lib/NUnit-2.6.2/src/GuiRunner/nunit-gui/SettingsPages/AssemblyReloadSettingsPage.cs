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

namespace NUnit.Gui.SettingsPages
{
	public class AssemblyReloadSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox rerunOnChangeCheckBox;
		private System.Windows.Forms.CheckBox reloadOnRunCheckBox;
		private System.Windows.Forms.CheckBox reloadOnChangeCheckBox;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.ComponentModel.IContainer components = null;

		public AssemblyReloadSettingsPage(string key) : base(key)
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
            this.rerunOnChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadOnRunCheckBox = new System.Windows.Forms.CheckBox();
            this.reloadOnChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Assembly Reload";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(181, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 8);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // rerunOnChangeCheckBox
            // 
            this.rerunOnChangeCheckBox.AutoSize = true;
            this.rerunOnChangeCheckBox.Enabled = false;
            this.helpProvider1.SetHelpString(this.rerunOnChangeCheckBox, "If checked, the last tests run will be re-run automatically whenever the assembly" +
                    " changes.");
            this.rerunOnChangeCheckBox.Location = new System.Drawing.Point(48, 96);
            this.rerunOnChangeCheckBox.Name = "rerunOnChangeCheckBox";
            this.helpProvider1.SetShowHelp(this.rerunOnChangeCheckBox, true);
            this.rerunOnChangeCheckBox.Size = new System.Drawing.Size(120, 17);
            this.rerunOnChangeCheckBox.TabIndex = 13;
            this.rerunOnChangeCheckBox.Text = "Re-run last tests run";
            // 
            // reloadOnRunCheckBox
            // 
            this.reloadOnRunCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.reloadOnRunCheckBox, "If checked, the assembly is reloaded before each run");
            this.reloadOnRunCheckBox.Location = new System.Drawing.Point(24, 32);
            this.reloadOnRunCheckBox.Name = "reloadOnRunCheckBox";
            this.helpProvider1.SetShowHelp(this.reloadOnRunCheckBox, true);
            this.reloadOnRunCheckBox.Size = new System.Drawing.Size(158, 17);
            this.reloadOnRunCheckBox.TabIndex = 11;
            this.reloadOnRunCheckBox.Text = "Reload before each test run";
            // 
            // reloadOnChangeCheckBox
            // 
            this.reloadOnChangeCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.reloadOnChangeCheckBox, "If checked, the assembly is reloaded whenever it changes. Changes to this setting" +
                    " do not take effect until the next time an assembly is loaded.");
            this.reloadOnChangeCheckBox.Location = new System.Drawing.Point(24, 64);
            this.reloadOnChangeCheckBox.Name = "reloadOnChangeCheckBox";
            this.helpProvider1.SetShowHelp(this.reloadOnChangeCheckBox, true);
            this.reloadOnChangeCheckBox.Size = new System.Drawing.Size(199, 17);
            this.reloadOnChangeCheckBox.TabIndex = 12;
            this.reloadOnChangeCheckBox.Text = "Reload when test assembly changes";
            this.reloadOnChangeCheckBox.CheckedChanged += new System.EventHandler(this.reloadOnChangeCheckBox_CheckedChanged);
            // 
            // AssemblyReloadSettingsPage
            // 
            this.Controls.Add(this.rerunOnChangeCheckBox);
            this.Controls.Add(this.reloadOnRunCheckBox);
            this.Controls.Add(this.reloadOnChangeCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AssemblyReloadSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
			reloadOnChangeCheckBox.Checked = settings.GetSetting( "Options.TestLoader.ReloadOnChange", true );
			rerunOnChangeCheckBox.Checked = settings.GetSetting( "Options.TestLoader.RerunOnChange", false );
			reloadOnRunCheckBox.Checked = settings.GetSetting( "Options.TestLoader.ReloadOnRun", false );
		}

		public override void ApplySettings()
		{
			settings.SaveSetting( "Options.TestLoader.ReloadOnChange", reloadOnChangeCheckBox.Checked );
			settings.SaveSetting( "Options.TestLoader.RerunOnChange", rerunOnChangeCheckBox.Checked );
			settings.SaveSetting( "Options.TestLoader.ReloadOnRun", reloadOnRunCheckBox.Checked );
		}



		private void reloadOnChangeCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			rerunOnChangeCheckBox.Enabled = reloadOnChangeCheckBox.Checked;
		}

		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			System.Diagnostics.Process.Start( "http://nunit.com/?p=optionsDialog&r=2.4.5" );
		}

	}
}

