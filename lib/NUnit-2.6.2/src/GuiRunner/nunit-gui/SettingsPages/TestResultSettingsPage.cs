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
	public class TestResultSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox errorsTabCheckBox;
		private System.Windows.Forms.CheckBox failureToolTips;
		private System.Windows.Forms.CheckBox enableWordWrap;
		private System.Windows.Forms.CheckBox notRunTabCheckBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.ComponentModel.IContainer components = null;

		public TestResultSettingsPage(string key) : base(key)
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorsTabCheckBox = new System.Windows.Forms.CheckBox();
            this.failureToolTips = new System.Windows.Forms.CheckBox();
            this.enableWordWrap = new System.Windows.Forms.CheckBox();
            this.notRunTabCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(149, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 8);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Errors Tab";
            // 
            // errorsTabCheckBox
            // 
            this.errorsTabCheckBox.AutoSize = true;
            this.errorsTabCheckBox.Checked = true;
            this.errorsTabCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.helpProvider1.SetHelpString(this.errorsTabCheckBox, "If checked, the Errors and Failures Tab will be displayed in the Gui");
            this.errorsTabCheckBox.Location = new System.Drawing.Point(32, 24);
            this.errorsTabCheckBox.Name = "errorsTabCheckBox";
            this.helpProvider1.SetShowHelp(this.errorsTabCheckBox, true);
            this.errorsTabCheckBox.Size = new System.Drawing.Size(172, 17);
            this.errorsTabCheckBox.TabIndex = 10;
            this.errorsTabCheckBox.Text = "Display Errors and Failures Tab";
            // 
            // failureToolTips
            // 
            this.failureToolTips.AutoSize = true;
            this.helpProvider1.SetHelpString(this.failureToolTips, "If checked, a tooltip will be displayed when hovering over an error that does not" +
                    " fit the display.");
            this.failureToolTips.Location = new System.Drawing.Point(48, 56);
            this.failureToolTips.Name = "failureToolTips";
            this.helpProvider1.SetShowHelp(this.failureToolTips, true);
            this.failureToolTips.Size = new System.Drawing.Size(137, 17);
            this.failureToolTips.TabIndex = 11;
            this.failureToolTips.Text = "Enable Failure ToolTips";
            // 
            // enableWordWrap
            // 
            this.enableWordWrap.AutoSize = true;
            this.helpProvider1.SetHelpString(this.enableWordWrap, "If checked, error messages will be word wrapped to subsequent display lines.");
            this.enableWordWrap.Location = new System.Drawing.Point(48, 88);
            this.enableWordWrap.Name = "enableWordWrap";
            this.helpProvider1.SetShowHelp(this.enableWordWrap, true);
            this.enableWordWrap.Size = new System.Drawing.Size(117, 17);
            this.enableWordWrap.TabIndex = 12;
            this.enableWordWrap.Text = "Enable Word Wrap";
            // 
            // notRunTabCheckBox
            // 
            this.notRunTabCheckBox.AutoSize = true;
            this.notRunTabCheckBox.Checked = true;
            this.notRunTabCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.helpProvider1.SetHelpString(this.notRunTabCheckBox, "If checked, the Tests Not Run Tab will be displayed in the Gui.");
            this.notRunTabCheckBox.Location = new System.Drawing.Point(32, 152);
            this.notRunTabCheckBox.Name = "notRunTabCheckBox";
            this.helpProvider1.SetShowHelp(this.notRunTabCheckBox, true);
            this.notRunTabCheckBox.Size = new System.Drawing.Size(154, 17);
            this.notRunTabCheckBox.TabIndex = 13;
            this.notRunTabCheckBox.Text = "Display Tests Not Run Tab";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(149, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 8);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Not Run Tab";
            // 
            // TestResultSettingsPage
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.errorsTabCheckBox);
            this.Controls.Add(this.failureToolTips);
            this.Controls.Add(this.enableWordWrap);
            this.Controls.Add(this.notRunTabCheckBox);
            this.Controls.Add(this.label1);
            this.Name = "TestResultSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
			errorsTabCheckBox.Checked = settings.GetSetting( "Gui.ResultTabs.DisplayErrorsTab", true );
			failureToolTips.Checked = settings.GetSetting( "Gui.ResultTabs.ErrorsTab.ToolTipsEnabled", true );
			enableWordWrap.Checked = settings.GetSetting( "Gui.ResultTabs.ErrorsTab.WordWrapEnabled", true );

			notRunTabCheckBox.Checked = settings.GetSetting( "Gui.ResultTabs.DisplayNotRunTab", true );
		}

		public override void ApplySettings()
		{
			settings.SaveSetting( "Gui.ResultTabs.DisplayErrorsTab", errorsTabCheckBox.Checked );
			settings.SaveSetting( "Gui.ResultTabs.ErrorsTab.ToolTipsEnabled", failureToolTips.Checked );
			settings.SaveSetting( "Gui.ResultTabs.ErrorsTab.WordWrapEnabled", enableWordWrap.Checked );

			settings.SaveSetting( "Gui.ResultTabs.DisplayNotRunTab", notRunTabCheckBox.Checked );
		}

		private void errorsTabCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			this.failureToolTips.Enabled = this.enableWordWrap.Enabled = this.errorsTabCheckBox.Checked;
		}
	}
}

