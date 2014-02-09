// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.Gui.SettingsPages
{
	public class AdvancedLoaderSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox enableShadowCopyCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.HelpProvider helpProvider1;
        private Label label4;
        private TextBox shadowCopyPathTextBox;
        private CheckBox principalPolicyCheckBox;
        private Label label7;
        private Label label6;
        private GroupBox groupBox1;
        private ListBox principalPolicyListBox;
        private Label label1;
		private System.ComponentModel.IContainer components = null;

		public AdvancedLoaderSettingsPage( string key ) : base( key )
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedLoaderSettingsPage));
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enableShadowCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.shadowCopyPathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.principalPolicyCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.principalPolicyListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Shadow Copy";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(139, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 8);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // enableShadowCopyCheckBox
            // 
            this.enableShadowCopyCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.enableShadowCopyCheckBox, resources.GetString("enableShadowCopyCheckBox.HelpString"));
            this.enableShadowCopyCheckBox.Location = new System.Drawing.Point(24, 32);
            this.enableShadowCopyCheckBox.Name = "enableShadowCopyCheckBox";
            this.helpProvider1.SetShowHelp(this.enableShadowCopyCheckBox, true);
            this.enableShadowCopyCheckBox.Size = new System.Drawing.Size(128, 17);
            this.enableShadowCopyCheckBox.TabIndex = 2;
            this.enableShadowCopyCheckBox.Text = "Enable Shadow Copy";
            this.enableShadowCopyCheckBox.CheckedChanged += new System.EventHandler(this.enableShadowCopyCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(139, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 59);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shadow copy should normally be enabled. If it is disabled, the NUnit Gui may not " +
                "function correctly.";
            // 
            // shadowCopyPathTextBox
            // 
            this.helpProvider1.SetHelpString(this.shadowCopyPathTextBox, "Leave this blank to permit NUnit to select a location under your temp directory.");
            this.shadowCopyPathTextBox.Location = new System.Drawing.Point(139, 65);
            this.shadowCopyPathTextBox.Name = "shadowCopyPathTextBox";
            this.helpProvider1.SetShowHelp(this.shadowCopyPathTextBox, true);
            this.shadowCopyPathTextBox.Size = new System.Drawing.Size(309, 20);
            this.shadowCopyPathTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cache Path:";
            // 
            // principalPolicyCheckBox
            // 
            this.principalPolicyCheckBox.AutoSize = true;
            this.principalPolicyCheckBox.Location = new System.Drawing.Point(24, 199);
            this.principalPolicyCheckBox.Name = "principalPolicyCheckBox";
            this.principalPolicyCheckBox.Size = new System.Drawing.Size(214, 17);
            this.principalPolicyCheckBox.TabIndex = 9;
            this.principalPolicyCheckBox.Text = "Set Principal Policy for test AppDomains";
            this.principalPolicyCheckBox.UseVisualStyleBackColor = true;
            this.principalPolicyCheckBox.CheckedChanged += new System.EventHandler(this.principalPolicyCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Policy:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Principal Policy";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(139, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 8);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // principalPolicyListBox
            // 
            this.principalPolicyListBox.FormattingEnabled = true;
            this.principalPolicyListBox.Items.AddRange(new object[] {
            "UnauthenticatedPrincipal",
            "NoPrincipal",
            "WindowsPrincipal"});
            this.principalPolicyListBox.Location = new System.Drawing.Point(139, 225);
            this.principalPolicyListBox.Name = "principalPolicyListBox";
            this.principalPolicyListBox.Size = new System.Drawing.Size(241, 69);
            this.principalPolicyListBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Warning:";
            // 
            // AdvancedLoaderSettingsPage
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.principalPolicyListBox);
            this.Controls.Add(this.principalPolicyCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.shadowCopyPathTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.enableShadowCopyCheckBox);
            this.Name = "AdvancedLoaderSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
			this.settings = Services.UserSettings;

			enableShadowCopyCheckBox.Checked = settings.GetSetting( "Options.TestLoader.ShadowCopyFiles", true );
            shadowCopyPathTextBox.Text = settings.GetSetting("Options.TestLoader.ShadowCopyPath", "");

            principalPolicyCheckBox.Checked = principalPolicyListBox.Enabled =
                settings.GetSetting( "Options.TestLoader.SetPrincipalPolicy", false );
            principalPolicyListBox.SelectedIndex = (int)(PrincipalPolicy)settings.GetSetting("Options.TestLoader.PrincipalPolicy", PrincipalPolicy.UnauthenticatedPrincipal);
		}

		public override void ApplySettings()
		{
			settings.SaveSetting( "Options.TestLoader.ShadowCopyFiles", enableShadowCopyCheckBox.Checked );

            if (shadowCopyPathTextBox.Text != "")
                settings.SaveSetting("Options.TestLoader.ShadowCopyPath", shadowCopyPathTextBox.Text);
            else
                settings.RemoveSetting("Options.TestLoader.ShadowCopyPath");

            settings.SaveSetting("Options.TestLoader.SetPrincipalPolicy", principalPolicyCheckBox.Checked);

            if (principalPolicyCheckBox.Checked)
                settings.SaveSetting("Options.TestLoader.PrincipalPolicy", (PrincipalPolicy)principalPolicyListBox.SelectedIndex);
            else
                settings.RemoveSetting("Options.TestLoader.PrincipalPolicy");
		}

		public override bool HasChangesRequiringReload
		{
			get
			{
				bool oldShadowCopyFiles = settings.GetSetting( "Options.TestLoader.ShadowCopyFiles", true );
                string oldShadowCopyPath = settings.GetSetting("Options.TestLoader.ShadowCopyPath", "");
                bool oldSetPrincipalPolicy = settings.GetSetting("Options.TestLoader.SetPrincipalPolicy", false);
                PrincipalPolicy oldPrincipalPolicy = (PrincipalPolicy)settings.GetSetting("Options.TestLoader.PrincipalPolicy", PrincipalPolicy.UnauthenticatedPrincipal);

                return enableShadowCopyCheckBox.Checked != oldShadowCopyFiles
                    || shadowCopyPathTextBox.Text != oldShadowCopyPath
                    || principalPolicyCheckBox.Checked != oldSetPrincipalPolicy
                    || principalPolicyListBox.SelectedIndex != (int)oldPrincipalPolicy;

			}
		}

        private void enableShadowCopyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            shadowCopyPathTextBox.Enabled = enableShadowCopyCheckBox.Checked;
        }

        private void principalPolicyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            principalPolicyListBox.Enabled = principalPolicyCheckBox.Checked;
        }
	}
}

