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
using NUnit.Core;

namespace NUnit.Gui.SettingsPages
{
	public class TestLoaderSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.CheckBox mergeAssembliesCheckBox;
		private System.Windows.Forms.RadioButton singleDomainRadioButton;
		private System.Windows.Forms.RadioButton multiDomainRadioButton;
		private System.Windows.Forms.HelpProvider helpProvider1;
        private Label label3;
        private GroupBox groupBox3;
        private RadioButton multiProcessRadioButton;
        private RadioButton separateProcessRadioButton;
        private RadioButton sameProcessRadioButton;
        private Label label2;
        private GroupBox groupBox2;
		private System.ComponentModel.IContainer components = null;

		public TestLoaderSettingsPage(string key) : base(key)
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
            this.mergeAssembliesCheckBox = new System.Windows.Forms.CheckBox();
            this.singleDomainRadioButton = new System.Windows.Forms.RadioButton();
            this.multiDomainRadioButton = new System.Windows.Forms.RadioButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.multiProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.separateProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.sameProcessRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // mergeAssembliesCheckBox
            // 
            this.mergeAssembliesCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.mergeAssembliesCheckBox, "If checked, tests in each assembly will be merged into a single tree.");
            this.mergeAssembliesCheckBox.Location = new System.Drawing.Point(48, 221);
            this.mergeAssembliesCheckBox.Name = "mergeAssembliesCheckBox";
            this.helpProvider1.SetShowHelp(this.mergeAssembliesCheckBox, true);
            this.mergeAssembliesCheckBox.Size = new System.Drawing.Size(169, 17);
            this.mergeAssembliesCheckBox.TabIndex = 10;
            this.mergeAssembliesCheckBox.Text = "Merge tests across assemblies";
            // 
            // singleDomainRadioButton
            // 
            this.singleDomainRadioButton.AutoCheck = false;
            this.singleDomainRadioButton.AutoSize = true;
            this.singleDomainRadioButton.Checked = true;
            this.helpProvider1.SetHelpString(this.singleDomainRadioButton, "If selected, all test assemblies will be loaded in the same AppDomain.");
            this.singleDomainRadioButton.Location = new System.Drawing.Point(32, 190);
            this.singleDomainRadioButton.Name = "singleDomainRadioButton";
            this.helpProvider1.SetShowHelp(this.singleDomainRadioButton, true);
            this.singleDomainRadioButton.Size = new System.Drawing.Size(194, 17);
            this.singleDomainRadioButton.TabIndex = 9;
            this.singleDomainRadioButton.TabStop = true;
            this.singleDomainRadioButton.Text = "Use a single AppDomain for all tests";
            this.singleDomainRadioButton.Click += new System.EventHandler(this.toggleMultiDomain);
            // 
            // multiDomainRadioButton
            // 
            this.multiDomainRadioButton.AutoCheck = false;
            this.multiDomainRadioButton.AutoSize = true;
            this.helpProvider1.SetHelpString(this.multiDomainRadioButton, "If selected, each test assembly will be loaded in a separate AppDomain.");
            this.multiDomainRadioButton.Location = new System.Drawing.Point(32, 160);
            this.multiDomainRadioButton.Name = "multiDomainRadioButton";
            this.helpProvider1.SetShowHelp(this.multiDomainRadioButton, true);
            this.multiDomainRadioButton.Size = new System.Drawing.Size(220, 17);
            this.multiDomainRadioButton.TabIndex = 8;
            this.multiDomainRadioButton.Text = "Use a separate AppDomain per Assembly";
            this.multiDomainRadioButton.Click += new System.EventHandler(this.toggleMultiDomain);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Default Process Model";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(199, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 8);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            // 
            // multiProcessRadioButton
            // 
            this.multiProcessRadioButton.AutoSize = true;
            this.multiProcessRadioButton.Location = new System.Drawing.Point(32, 99);
            this.multiProcessRadioButton.Name = "multiProcessRadioButton";
            this.multiProcessRadioButton.Size = new System.Drawing.Size(239, 17);
            this.multiProcessRadioButton.TabIndex = 36;
            this.multiProcessRadioButton.Text = "Run tests in a separate process per Assembly";
            this.multiProcessRadioButton.CheckedChanged += new System.EventHandler(this.toggleProcessUsage);
            // 
            // separateProcessRadioButton
            // 
            this.separateProcessRadioButton.AutoSize = true;
            this.separateProcessRadioButton.Location = new System.Drawing.Point(32, 66);
            this.separateProcessRadioButton.Name = "separateProcessRadioButton";
            this.separateProcessRadioButton.Size = new System.Drawing.Size(204, 17);
            this.separateProcessRadioButton.TabIndex = 37;
            this.separateProcessRadioButton.Text = "Run tests in a single separate process";
            this.separateProcessRadioButton.CheckedChanged += new System.EventHandler(this.toggleProcessUsage);
            // 
            // sameProcessRadioButton
            // 
            this.sameProcessRadioButton.AutoSize = true;
            this.sameProcessRadioButton.Checked = true;
            this.sameProcessRadioButton.Location = new System.Drawing.Point(32, 33);
            this.sameProcessRadioButton.Name = "sameProcessRadioButton";
            this.sameProcessRadioButton.Size = new System.Drawing.Size(205, 17);
            this.sameProcessRadioButton.TabIndex = 38;
            this.sameProcessRadioButton.TabStop = true;
            this.sameProcessRadioButton.Text = "Run tests directly in the NUnit process";
            this.sameProcessRadioButton.CheckedChanged += new System.EventHandler(this.toggleProcessUsage);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Default Domain Usage";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(199, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 8);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            // 
            // TestLoaderSettingsPage
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.sameProcessRadioButton);
            this.Controls.Add(this.separateProcessRadioButton);
            this.Controls.Add(this.multiProcessRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.mergeAssembliesCheckBox);
            this.Controls.Add(this.singleDomainRadioButton);
            this.Controls.Add(this.multiDomainRadioButton);
            this.Name = "TestLoaderSettingsPage";
            this.Size = new System.Drawing.Size(456, 341);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		public override void LoadSettings()
		{
            switch( GetSavedProcessModel() )
            {
                case ProcessModel.Separate:
                    separateProcessRadioButton.Checked = true;
                    sameProcessRadioButton.Checked = false;
                    multiProcessRadioButton.Checked = false;
                    break;
                case ProcessModel.Multiple:
                    multiProcessRadioButton.Checked = true;
                    sameProcessRadioButton.Checked = false;
                    separateProcessRadioButton.Checked = false;
                    break;
                default:
                    sameProcessRadioButton.Checked = true;
                    multiProcessRadioButton.Checked = false;
                    separateProcessRadioButton.Checked = false;
                    break;
            }

			bool multiDomain = GetSavedDomainUsage() == DomainUsage.Multiple;
			multiDomainRadioButton.Checked = multiDomain;
			singleDomainRadioButton.Checked = !multiDomain;
            mergeAssembliesCheckBox.Enabled = !multiDomain;

			mergeAssembliesCheckBox.Checked = settings.GetSetting( "Options.TestLoader.MergeAssemblies", false );
		}

		public override void ApplySettings()
		{
            if (multiProcessRadioButton.Checked)
                settings.SaveSetting("Options.TestLoader.ProcessModel", ProcessModel.Multiple);
            else if (separateProcessRadioButton.Checked)
                settings.SaveSetting("Options.TestLoader.ProcessModel", ProcessModel.Separate);
            else
                settings.RemoveSetting("Options.TestLoader.ProcessModel");

            if (multiDomainRadioButton.Checked)
                settings.SaveSetting("Options.TestLoader.DomainUsage", DomainUsage.Multiple);
            else
                settings.RemoveSetting("Options.TestLoader.DomainUsage");
			
            settings.SaveSetting( "Options.TestLoader.MergeAssemblies", mergeAssembliesCheckBox.Checked );
		}

        // TODO: Combine toggleProcessUsage and toggleMultiDomain
        private void toggleProcessUsage(object sender, EventArgs e)
        {
            bool enable = sameProcessRadioButton.Checked || separateProcessRadioButton.Checked;
            singleDomainRadioButton.Enabled = enable;
            multiDomainRadioButton.Enabled = enable;
            mergeAssembliesCheckBox.Enabled = enable && singleDomainRadioButton.Checked;
        }
        
        private void toggleMultiDomain(object sender, System.EventArgs e)
		{
			bool multiDomain = multiDomainRadioButton.Checked = ! multiDomainRadioButton.Checked;
			singleDomainRadioButton.Checked = !multiDomain;
			mergeAssembliesCheckBox.Enabled = !multiDomain && !multiProcessRadioButton.Checked;
		}

		public override bool HasChangesRequiringReload
		{
			get 
			{
				return
                    GetSavedProcessModel() != GetSelectedProcessModel() ||
                    GetSavedDomainUsage() != GetSelectedDomainUsage() ||
					settings.GetSetting("Options.TestLoader.MergeAssemblies", false ) != mergeAssembliesCheckBox.Checked;
			}
		}

        private ProcessModel GetSavedProcessModel()
        {
            return (ProcessModel)settings.GetSetting("Options.TestLoader.ProcessModel", ProcessModel.Default);
        }

        private DomainUsage GetSavedDomainUsage()
        {
            return (DomainUsage)settings.GetSetting("Options.TestLoader.DomainUsage", DomainUsage.Default);
        }

        private ProcessModel GetSelectedProcessModel()
        {
            return separateProcessRadioButton.Checked
                ? ProcessModel.Separate
                : multiProcessRadioButton.Checked
                    ? ProcessModel.Multiple
                    : ProcessModel.Single;
        }

        private DomainUsage GetSelectedDomainUsage()
        {
            return multiDomainRadioButton.Checked
                ? DomainUsage.Multiple
                : DomainUsage.Single;
        }
	}
}

