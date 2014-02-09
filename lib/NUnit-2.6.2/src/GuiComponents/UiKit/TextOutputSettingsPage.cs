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
using NUnit.Core;
using NUnit.UiKit;
using NUnit.Util;
using System.Diagnostics;

namespace NUnit.UiKit
{
	public class TextOutputSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox showStandardOutput;
        private System.Windows.Forms.CheckBox showErrorOutput;
		private System.Windows.Forms.ComboBox tabSelectComboBox;
        private System.Windows.Forms.Button useDefaultsButton;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label5;

		private TextDisplayTabSettings tabSettings = new TextDisplayTabSettings();
		private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private Label label6;
        private ComboBox logLevelComboBox;
        private Label label3;
        private ComboBox labelsComboBox;
        private CheckBox showTraceOutput;
		private int selectedTabIndex = -1;

		public TextOutputSettingsPage(string key) : base(key)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

            logLevelComboBox.Items.Clear();
            foreach (string name in System.Enum.GetNames(typeof(LoggingThreshold)))
                logLevelComboBox.Items.Add(name);

            labelsComboBox.Items.Clear();
            foreach (string name in System.Enum.GetNames(typeof(TestLabelLevel)))
                labelsComboBox.Items.Add(name);
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
            this.label2 = new System.Windows.Forms.Label();
            this.showStandardOutput = new System.Windows.Forms.CheckBox();
            this.showErrorOutput = new System.Windows.Forms.CheckBox();
            this.tabSelectComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.useDefaultsButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label6 = new System.Windows.Forms.Label();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelsComboBox = new System.Windows.Forms.ComboBox();
            this.showTraceOutput = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Tab:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Content";
            // 
            // showStandardOutput
            // 
            this.showStandardOutput.AutoSize = true;
            this.helpProvider1.SetHelpString(this.showStandardOutput, "If checked, standard Console output is displayed on this Tab.");
            this.showStandardOutput.Location = new System.Drawing.Point(40, 128);
            this.showStandardOutput.Name = "showStandardOutput";
            this.helpProvider1.SetShowHelp(this.showStandardOutput, true);
            this.showStandardOutput.Size = new System.Drawing.Size(104, 17);
            this.showStandardOutput.TabIndex = 17;
            this.showStandardOutput.Text = "Standard Output";
            this.showStandardOutput.CheckedChanged += new System.EventHandler(this.showStandardOutput_CheckedChanged);
            // 
            // showErrorOutput
            // 
            this.showErrorOutput.AutoSize = true;
            this.helpProvider1.SetHelpString(this.showErrorOutput, "If checked, error output is displayed on this Tab.");
            this.showErrorOutput.Location = new System.Drawing.Point(242, 128);
            this.showErrorOutput.Name = "showErrorOutput";
            this.helpProvider1.SetShowHelp(this.showErrorOutput, true);
            this.showErrorOutput.Size = new System.Drawing.Size(83, 17);
            this.showErrorOutput.TabIndex = 18;
            this.showErrorOutput.Text = "Error Output";
            this.showErrorOutput.CheckedChanged += new System.EventHandler(this.showErrorOutput_CheckedChanged);
            // 
            // tabSelectComboBox
            // 
            this.tabSelectComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.helpProvider1.SetHelpString(this.tabSelectComboBox, "Allows the user to select an existing Tab, create a new Tab or edit the list of T" +
                    "abs.");
            this.tabSelectComboBox.Location = new System.Drawing.Point(105, 14);
            this.tabSelectComboBox.Name = "tabSelectComboBox";
            this.helpProvider1.SetShowHelp(this.tabSelectComboBox, true);
            this.tabSelectComboBox.Size = new System.Drawing.Size(195, 21);
            this.tabSelectComboBox.TabIndex = 22;
            this.tabSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.tabSelectComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(131, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 8);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            // 
            // useDefaultsButton
            // 
            this.useDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.useDefaultsButton.AutoSize = true;
            this.helpProvider1.SetHelpString(this.useDefaultsButton, "Restores the list of Tabs and their content to the default values.");
            this.useDefaultsButton.Location = new System.Drawing.Point(329, 12);
            this.useDefaultsButton.Name = "useDefaultsButton";
            this.helpProvider1.SetShowHelp(this.useDefaultsButton, true);
            this.useDefaultsButton.Size = new System.Drawing.Size(112, 23);
            this.useDefaultsButton.TabIndex = 25;
            this.useDefaultsButton.Text = "Restore Defaults";
            this.useDefaultsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.textBox1, "The title to be displayed on the selected Tab.");
            this.textBox1.Location = new System.Drawing.Point(105, 64);
            this.textBox1.Name = "textBox1";
            this.helpProvider1.SetShowHelp(this.textBox1, true);
            this.textBox1.Size = new System.Drawing.Size(215, 20);
            this.textBox1.TabIndex = 30;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enabledCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.enabledCheckBox, "If checked, the Tab is enabled. If not, it is hidden.");
            this.enabledCheckBox.Location = new System.Drawing.Point(350, 64);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.helpProvider1.SetShowHelp(this.enabledCheckBox, true);
            this.enabledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.enabledCheckBox.TabIndex = 31;
            this.enabledCheckBox.Text = "Enabled";
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.displayTab_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Title:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Log Output:";
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.logLevelComboBox, "Selects the logging threshold for display on this Tab.");
            this.logLevelComboBox.Location = new System.Drawing.Point(329, 161);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.helpProvider1.SetShowHelp(this.logLevelComboBox, true);
            this.logLevelComboBox.Size = new System.Drawing.Size(77, 21);
            this.logLevelComboBox.TabIndex = 36;
            this.logLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.logLevel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Test Case Labels:";
            // 
            // labelsComboBox
            // 
            this.labelsComboBox.FormattingEnabled = true;
            this.helpProvider1.SetHelpString(this.labelsComboBox, "Selects whether test case labels are displayed. Option \'On\' displays labels only " +
                    "when there is other output from the test.");
            this.labelsComboBox.Items.AddRange(new object[] {
            "Off",
            "On",
            "All"});
            this.labelsComboBox.Location = new System.Drawing.Point(176, 200);
            this.labelsComboBox.Name = "labelsComboBox";
            this.helpProvider1.SetShowHelp(this.labelsComboBox, true);
            this.labelsComboBox.Size = new System.Drawing.Size(77, 21);
            this.labelsComboBox.TabIndex = 38;
            this.labelsComboBox.SelectedIndexChanged += new System.EventHandler(this.labelsComboBox_SelectedIndexChanged);
            // 
            // showTraceOutput
            // 
            this.showTraceOutput.AutoSize = true;
            this.helpProvider1.SetHelpString(this.showTraceOutput, "If checked, trace output is displayed on this Tab.");
            this.showTraceOutput.Location = new System.Drawing.Point(40, 161);
            this.showTraceOutput.Name = "showTraceOutput";
            this.helpProvider1.SetShowHelp(this.showTraceOutput, true);
            this.showTraceOutput.Size = new System.Drawing.Size(89, 17);
            this.showTraceOutput.TabIndex = 39;
            this.showTraceOutput.Text = "Trace Output";
            this.showTraceOutput.UseVisualStyleBackColor = true;
            this.showTraceOutput.CheckedChanged += new System.EventHandler(this.showTraceOutput_CheckedChanged);
            // 
            // TextOutputSettingsPage
            // 
            this.Controls.Add(this.showTraceOutput);
            this.Controls.Add(this.labelsComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logLevelComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.useDefaultsButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabSelectComboBox);
            this.Controls.Add(this.showErrorOutput);
            this.Controls.Add(this.showStandardOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TextOutputSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
			tabSettings.LoadSettings(settings);
			InitializeTabSelectComboBox();
		}

		private void InitializeTabSelectComboBox()
		{
			FillTabSelectComboBox();

			if ( this.tabSelectComboBox.Items.Count > 0 )
			{
				this.tabSelectComboBox.SelectedIndex = this.selectedTabIndex = 0;
				this.InitDisplay(tabSettings.Tabs[0]);
			}
		}

		private void FillTabSelectComboBox()
		{
			tabSelectComboBox.Items.Clear();

			foreach( TextDisplayTabSettings.TabInfo tabInfo in this.tabSettings.Tabs )
				this.tabSelectComboBox.Items.Add( tabInfo.Title );

			tabSelectComboBox.Items.Add( "<New...>" );
			tabSelectComboBox.Items.Add( "<Edit...>" );
		}

		public override void ApplySettings()
		{
			tabSettings.ApplySettings();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			tabSettings.LoadDefaults();
			InitializeTabSelectComboBox();
		}

		private void InitDisplay(TextDisplayTabSettings.TabInfo tabInfo)
		{
			textBox1.Text = tabInfo.Title;

			TextDisplayContent content = tabInfo.Content;
            showStandardOutput.Checked = content.Out;
            showErrorOutput.Checked = content.Error;
            showTraceOutput.Checked = content.Trace;
            logLevelComboBox.SelectedIndex = (int)content.LogLevel;
            labelsComboBox.SelectedIndex = (int)content.Labels;

			enabledCheckBox.Checked = tabInfo.Enabled;
		}

		private void tabSelectComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int index = tabSelectComboBox.SelectedIndex;
			if ( index < tabSettings.Tabs.Count )
			{
				selectedTabIndex = index;
				InitDisplay(tabSettings.Tabs[index]);
			}
			else // Not a tab, but a "menu" item
			{
				tabSelectComboBox.SelectedIndex = selectedTabIndex;

				if ( index == tabSettings.Tabs.Count )
					addNewTabPage();
				else
					editTabPages();
			}
		}

		private void addNewTabPage()
		{
			using( AddTabPageDialog dlg = new AddTabPageDialog(tabSettings) )
			{
				this.ParentForm.Site.Container.Add( dlg );
				if ( dlg.ShowDialog(this) == DialogResult.OK )
				{
					FillTabSelectComboBox();
					this.tabSelectComboBox.SelectedIndex = tabSettings.Tabs.Count - 1;
				}
			}
		}

		private void editTabPages()
		{
			using( EditTabPagesDialog dlg = new EditTabPagesDialog( tabSettings) )
			{
				this.ParentForm.Site.Container.Add( dlg );
				dlg.ShowDialog(this);

				FillTabSelectComboBox();
					
				if ( tabSelectComboBox.Items.Count > 0 )
					tabSelectComboBox.SelectedIndex = selectedTabIndex = 0;
			}
		}

		private void showStandardOutput_CheckedChanged(object sender, System.EventArgs e)
		{
            tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Content.Out = showStandardOutput.Checked;
        }

		private void showErrorOutput_CheckedChanged(object sender, System.EventArgs e)
		{
            tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Content.Error = showErrorOutput.Checked;
        }

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Title = textBox1.Text;
		}

		private void displayTab_CheckedChanged(object sender, System.EventArgs e)
		{
			tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Enabled = enabledCheckBox.Checked;
		}

        private void labelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Content.Labels = (TestLabelLevel)labelsComboBox.SelectedIndex;
        }

        private void logLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Content.LogLevel = (LoggingThreshold)logLevelComboBox.SelectedIndex;
        }

        private void showTraceOutput_CheckedChanged(object sender, EventArgs e)
        {
            tabSettings.Tabs[tabSelectComboBox.SelectedIndex].Content.Trace = showTraceOutput.Checked;
        }
	}
}

