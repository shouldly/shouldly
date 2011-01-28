// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Util;
using NUnit.UiKit;
using NUnit.Core;

namespace NUnit.Gui
{
	public class ProjectEditor : System.Windows.Forms.Form
	{
		#region Instance Variables

		private NUnitProject project;
		private ProjectConfig selectedConfig;
		private string selectedAssembly;
		private System.Windows.Forms.ColumnHeader fileNameHeader;
		private System.Windows.Forms.ColumnHeader fullPathHeader;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.Label label5;
		private CP.Windows.Forms.ExpandingLabel projectPathLabel;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox projectBaseTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl projectTabControl;
		private System.Windows.Forms.TabPage generalTabPage;
		private System.Windows.Forms.RadioButton autoBinPathRadioButton;
		private System.Windows.Forms.RadioButton manualBinPathRadioButton;
		private System.Windows.Forms.RadioButton noBinPathRadioButton;
		private System.Windows.Forms.TextBox privateBinPathTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox configFileTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox applicationBaseTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage assemblyTabPage;
		private System.Windows.Forms.ListBox assemblyListBox;
		private System.Windows.Forms.Button addAssemblyButton;
		private System.Windows.Forms.Button editConfigsButton;
		private System.Windows.Forms.ComboBox configComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button configBaseBrowseButton;
		private System.Windows.Forms.Button projectBaseBrowseButton;
		private System.Windows.Forms.Button removeAssemblyButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox assemblyPathTextBox;
		private System.Windows.Forms.Button assemblyPathBrowseButton;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox runtimeComboBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox processModelComboBox;
		private System.Windows.Forms.ComboBox domainUsageComboBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox runtimeVersionComboBox;
		private System.ComponentModel.IContainer components = null;

		#endregion

		#region Construction and Disposal

		public ProjectEditor( NUnitProject project )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.project = project;
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

		#endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectEditor));
            this.fileNameHeader = new System.Windows.Forms.ColumnHeader();
            this.fullPathHeader = new System.Windows.Forms.ColumnHeader();
            this.closeButton = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label5 = new System.Windows.Forms.Label();
            this.projectPathLabel = new CP.Windows.Forms.ExpandingLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.projectBaseTextBox = new System.Windows.Forms.TextBox();
            this.projectTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.runtimeVersionComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.runtimeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.autoBinPathRadioButton = new System.Windows.Forms.RadioButton();
            this.manualBinPathRadioButton = new System.Windows.Forms.RadioButton();
            this.noBinPathRadioButton = new System.Windows.Forms.RadioButton();
            this.configBaseBrowseButton = new System.Windows.Forms.Button();
            this.privateBinPathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.configFileTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.applicationBaseTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.assemblyTabPage = new System.Windows.Forms.TabPage();
            this.assemblyPathBrowseButton = new System.Windows.Forms.Button();
            this.assemblyPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.assemblyListBox = new System.Windows.Forms.ListBox();
            this.addAssemblyButton = new System.Windows.Forms.Button();
            this.removeAssemblyButton = new System.Windows.Forms.Button();
            this.editConfigsButton = new System.Windows.Forms.Button();
            this.configComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.projectBaseBrowseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.processModelComboBox = new System.Windows.Forms.ComboBox();
            this.domainUsageComboBox = new System.Windows.Forms.ComboBox();
            this.projectTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.assemblyTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileNameHeader
            // 
            this.fileNameHeader.Text = "File Name";
            this.fileNameHeader.Width = 100;
            // 
            // fullPathHeader
            // 
            this.fullPathHeader.Text = "Full Path";
            this.fullPathHeader.Width = 256;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(383, 496);
            this.closeButton.Name = "closeButton";
            this.helpProvider1.SetShowHelp(this.closeButton, false);
            this.closeButton.Size = new System.Drawing.Size(87, 24);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 7);
            this.label5.Name = "label5";
            this.helpProvider1.SetShowHelp(this.label5, false);
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Project Path:";
            // 
            // projectPathLabel
            // 
            this.projectPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectPathLabel.Location = new System.Drawing.Point(112, 7);
            this.projectPathLabel.Name = "projectPathLabel";
            this.helpProvider1.SetShowHelp(this.projectPathLabel, false);
            this.projectPathLabel.Size = new System.Drawing.Size(352, 14);
            this.projectPathLabel.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 45);
            this.label8.Name = "label8";
            this.helpProvider1.SetShowHelp(this.label8, false);
            this.label8.Size = new System.Drawing.Size(100, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "Project Base:";
            // 
            // projectBaseTextBox
            // 
            this.projectBaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.projectBaseTextBox, "The ApplicationBase for the project. Defaults to the location of the project file" +
                    ".");
            this.projectBaseTextBox.Location = new System.Drawing.Point(112, 44);
            this.projectBaseTextBox.Name = "projectBaseTextBox";
            this.helpProvider1.SetShowHelp(this.projectBaseTextBox, true);
            this.projectBaseTextBox.Size = new System.Drawing.Size(320, 22);
            this.projectBaseTextBox.TabIndex = 8;
            this.projectBaseTextBox.Validated += new System.EventHandler(this.projectBaseTextBox_Validated);
            this.projectBaseTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.projectBaseTextBox_Validating);
            // 
            // projectTabControl
            // 
            this.projectTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectTabControl.Controls.Add(this.generalTabPage);
            this.projectTabControl.Controls.Add(this.assemblyTabPage);
            this.projectTabControl.ItemSize = new System.Drawing.Size(49, 18);
            this.projectTabControl.Location = new System.Drawing.Point(7, 82);
            this.projectTabControl.Name = "projectTabControl";
            this.projectTabControl.SelectedIndex = 0;
            this.helpProvider1.SetShowHelp(this.projectTabControl, false);
            this.projectTabControl.Size = new System.Drawing.Size(446, 278);
            this.projectTabControl.TabIndex = 9;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.runtimeVersionComboBox);
            this.generalTabPage.Controls.Add(this.label11);
            this.generalTabPage.Controls.Add(this.runtimeComboBox);
            this.generalTabPage.Controls.Add(this.label7);
            this.generalTabPage.Controls.Add(this.autoBinPathRadioButton);
            this.generalTabPage.Controls.Add(this.manualBinPathRadioButton);
            this.generalTabPage.Controls.Add(this.noBinPathRadioButton);
            this.generalTabPage.Controls.Add(this.configBaseBrowseButton);
            this.generalTabPage.Controls.Add(this.privateBinPathTextBox);
            this.generalTabPage.Controls.Add(this.label6);
            this.generalTabPage.Controls.Add(this.configFileTextBox);
            this.generalTabPage.Controls.Add(this.label4);
            this.generalTabPage.Controls.Add(this.applicationBaseTextBox);
            this.generalTabPage.Controls.Add(this.label3);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.helpProvider1.SetShowHelp(this.generalTabPage, false);
            this.generalTabPage.Size = new System.Drawing.Size(438, 252);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            // 
            // runtimeVersionComboBox
            // 
            this.runtimeVersionComboBox.Items.AddRange(new object[] {
            "Default",
            "1.0",
            "1.1",
            "2.0",
            "4.0"});
            this.runtimeVersionComboBox.Location = new System.Drawing.Point(320, 16);
            this.runtimeVersionComboBox.Name = "runtimeVersionComboBox";
            this.runtimeVersionComboBox.Size = new System.Drawing.Size(101, 24);
            this.runtimeVersionComboBox.TabIndex = 14;
            this.runtimeVersionComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.runtimeVersionComboBox_Validating);
            this.runtimeVersionComboBox.Validated += new System.EventHandler(this.runtimeVersionComboBox_Validated);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(192, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Runtime Version";
            // 
            // runtimeComboBox
            // 
            this.runtimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.runtimeComboBox.Items.AddRange(new object[] {
            "Any",
            "Net",
            "Mono"});
            this.runtimeComboBox.Location = new System.Drawing.Point(87, 16);
            this.runtimeComboBox.Name = "runtimeComboBox";
            this.runtimeComboBox.Size = new System.Drawing.Size(81, 24);
            this.runtimeComboBox.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(13, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Runtime:";
            // 
            // autoBinPathRadioButton
            // 
            this.autoBinPathRadioButton.Location = new System.Drawing.Point(24, 154);
            this.autoBinPathRadioButton.Name = "autoBinPathRadioButton";
            this.helpProvider1.SetShowHelp(this.autoBinPathRadioButton, false);
            this.autoBinPathRadioButton.Size = new System.Drawing.Size(273, 21);
            this.autoBinPathRadioButton.TabIndex = 10;
            this.autoBinPathRadioButton.Text = "Use automatically generated path";
            this.autoBinPathRadioButton.CheckedChanged += new System.EventHandler(this.autoBinPathRadioButton_CheckedChanged);
            // 
            // manualBinPathRadioButton
            // 
            this.manualBinPathRadioButton.Location = new System.Drawing.Point(24, 186);
            this.manualBinPathRadioButton.Name = "manualBinPathRadioButton";
            this.helpProvider1.SetShowHelp(this.manualBinPathRadioButton, false);
            this.manualBinPathRadioButton.Size = new System.Drawing.Size(101, 20);
            this.manualBinPathRadioButton.TabIndex = 9;
            this.manualBinPathRadioButton.Text = "Use this path:";
            this.manualBinPathRadioButton.CheckedChanged += new System.EventHandler(this.manualBinPathRadioButton_CheckedChanged);
            // 
            // noBinPathRadioButton
            // 
            this.noBinPathRadioButton.Location = new System.Drawing.Point(24, 218);
            this.noBinPathRadioButton.Name = "noBinPathRadioButton";
            this.helpProvider1.SetShowHelp(this.noBinPathRadioButton, false);
            this.noBinPathRadioButton.Size = new System.Drawing.Size(353, 21);
            this.noBinPathRadioButton.TabIndex = 8;
            this.noBinPathRadioButton.Text = "None - or specified in Configuration File";
            this.noBinPathRadioButton.CheckedChanged += new System.EventHandler(this.noBinPathRadioButton_CheckedChanged);
            // 
            // configBaseBrowseButton
            // 
            this.configBaseBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.configBaseBrowseButton, "Browse to locate ApplicationBase directory.");
            this.configBaseBrowseButton.Image = ((System.Drawing.Image)(resources.GetObject("configBaseBrowseButton.Image")));
            this.configBaseBrowseButton.Location = new System.Drawing.Point(401, 56);
            this.configBaseBrowseButton.Name = "configBaseBrowseButton";
            this.helpProvider1.SetShowHelp(this.configBaseBrowseButton, true);
            this.configBaseBrowseButton.Size = new System.Drawing.Size(20, 20);
            this.configBaseBrowseButton.TabIndex = 7;
            this.configBaseBrowseButton.Click += new System.EventHandler(this.configBaseBrowseButton_Click);
            // 
            // privateBinPathTextBox
            // 
            this.privateBinPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.privateBinPathTextBox, "Path searched when probing for private asemblies. Directories must be descendants" +
                    " of the ApplicationBase.");
            this.privateBinPathTextBox.Location = new System.Drawing.Point(144, 186);
            this.privateBinPathTextBox.Name = "privateBinPathTextBox";
            this.helpProvider1.SetShowHelp(this.privateBinPathTextBox, true);
            this.privateBinPathTextBox.Size = new System.Drawing.Size(280, 22);
            this.privateBinPathTextBox.TabIndex = 5;
            this.privateBinPathTextBox.Validated += new System.EventHandler(this.privateBinPathTextBox_Validated);
            this.privateBinPathTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.privateBinPathTextBox_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 130);
            this.label6.Name = "label6";
            this.helpProvider1.SetShowHelp(this.label6, false);
            this.label6.Size = new System.Drawing.Size(105, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "PrivateBinPath:";
            // 
            // configFileTextBox
            // 
            this.configFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.configFileTextBox, "Configuration file to use when loading assemblies if it exists. Defaults to <proj" +
                    "ectname>.config. Must be located in the ApplicationBase directory.");
            this.configFileTextBox.Location = new System.Drawing.Point(168, 96);
            this.configFileTextBox.Name = "configFileTextBox";
            this.helpProvider1.SetShowHelp(this.configFileTextBox, true);
            this.configFileTextBox.Size = new System.Drawing.Size(256, 22);
            this.configFileTextBox.TabIndex = 3;
            this.configFileTextBox.Validated += new System.EventHandler(this.configFileTextBox_Validated);
            this.configFileTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.configFileTextBox_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 98);
            this.label4.Name = "label4";
            this.helpProvider1.SetShowHelp(this.label4, false);
            this.label4.Size = new System.Drawing.Size(163, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Configuration File Name:";
            // 
            // applicationBaseTextBox
            // 
            this.applicationBaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.applicationBaseTextBox, "The ApplicationBase for this configuration. May be absolute or relative to the pr" +
                    "oject base. Defaults to the project base if not set.");
            this.applicationBaseTextBox.Location = new System.Drawing.Point(128, 56);
            this.applicationBaseTextBox.Name = "applicationBaseTextBox";
            this.helpProvider1.SetShowHelp(this.applicationBaseTextBox, true);
            this.applicationBaseTextBox.Size = new System.Drawing.Size(264, 22);
            this.applicationBaseTextBox.TabIndex = 1;
            this.applicationBaseTextBox.Validated += new System.EventHandler(this.applicationBaseTextBox_Validated);
            this.applicationBaseTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.applicationBaseTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.helpProvider1.SetShowHelp(this.label3, false);
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "ApplicationBase:";
            // 
            // assemblyTabPage
            // 
            this.assemblyTabPage.Controls.Add(this.assemblyPathBrowseButton);
            this.assemblyTabPage.Controls.Add(this.assemblyPathTextBox);
            this.assemblyTabPage.Controls.Add(this.label2);
            this.assemblyTabPage.Controls.Add(this.assemblyListBox);
            this.assemblyTabPage.Controls.Add(this.addAssemblyButton);
            this.assemblyTabPage.Controls.Add(this.removeAssemblyButton);
            this.assemblyTabPage.Location = new System.Drawing.Point(4, 22);
            this.assemblyTabPage.Name = "assemblyTabPage";
            this.helpProvider1.SetShowHelp(this.assemblyTabPage, false);
            this.assemblyTabPage.Size = new System.Drawing.Size(438, 252);
            this.assemblyTabPage.TabIndex = 1;
            this.assemblyTabPage.Text = "Assemblies";
            this.assemblyTabPage.Visible = false;
            // 
            // assemblyPathBrowseButton
            // 
            this.assemblyPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.assemblyPathBrowseButton, "Browse to locate ApplicationBase directory.");
            this.assemblyPathBrowseButton.Image = ((System.Drawing.Image)(resources.GetObject("assemblyPathBrowseButton.Image")));
            this.assemblyPathBrowseButton.Location = new System.Drawing.Point(413, 192);
            this.assemblyPathBrowseButton.Name = "assemblyPathBrowseButton";
            this.helpProvider1.SetShowHelp(this.assemblyPathBrowseButton, true);
            this.assemblyPathBrowseButton.Size = new System.Drawing.Size(20, 20);
            this.assemblyPathBrowseButton.TabIndex = 11;
            this.assemblyPathBrowseButton.Click += new System.EventHandler(this.assemblyPathBrowseButton_Click);
            // 
            // assemblyPathTextBox
            // 
            this.assemblyPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyPathTextBox.Location = new System.Drawing.Point(13, 192);
            this.assemblyPathTextBox.Name = "assemblyPathTextBox";
            this.assemblyPathTextBox.Size = new System.Drawing.Size(387, 22);
            this.assemblyPathTextBox.TabIndex = 8;
            this.assemblyPathTextBox.Validated += new System.EventHandler(this.assemblyPathTextBox_Validated);
            this.assemblyPathTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.assemblyPathTextBox_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Assembly Path:";
            // 
            // assemblyListBox
            // 
            this.assemblyListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.assemblyListBox, resources.GetString("assemblyListBox.HelpString"));
            this.assemblyListBox.ItemHeight = 16;
            this.assemblyListBox.Location = new System.Drawing.Point(13, 24);
            this.assemblyListBox.Name = "assemblyListBox";
            this.helpProvider1.SetShowHelp(this.assemblyListBox, true);
            this.assemblyListBox.Size = new System.Drawing.Size(339, 132);
            this.assemblyListBox.TabIndex = 6;
            this.assemblyListBox.SelectedIndexChanged += new System.EventHandler(this.assemblyListBox_SelectedIndexChanged);
            // 
            // addAssemblyButton
            // 
            this.addAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.addAssemblyButton, "Add an assembly to this configuration.");
            this.addAssemblyButton.Location = new System.Drawing.Point(367, 21);
            this.addAssemblyButton.Name = "addAssemblyButton";
            this.helpProvider1.SetShowHelp(this.addAssemblyButton, true);
            this.addAssemblyButton.Size = new System.Drawing.Size(66, 20);
            this.addAssemblyButton.TabIndex = 2;
            this.addAssemblyButton.Text = "&Add...";
            this.addAssemblyButton.Click += new System.EventHandler(this.addAssemblyButton_Click);
            // 
            // removeAssemblyButton
            // 
            this.removeAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.removeAssemblyButton, "Remove the selected assembly from the configuration.");
            this.removeAssemblyButton.Location = new System.Drawing.Point(367, 49);
            this.removeAssemblyButton.Name = "removeAssemblyButton";
            this.helpProvider1.SetShowHelp(this.removeAssemblyButton, true);
            this.removeAssemblyButton.Size = new System.Drawing.Size(66, 19);
            this.removeAssemblyButton.TabIndex = 5;
            this.removeAssemblyButton.Text = "&Remove";
            this.removeAssemblyButton.Click += new System.EventHandler(this.removeAssemblyButton_Click);
            // 
            // editConfigsButton
            // 
            this.editConfigsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.editConfigsButton, "Add, remove or rename configurations.");
            this.editConfigsButton.Location = new System.Drawing.Point(352, 33);
            this.editConfigsButton.Name = "editConfigsButton";
            this.helpProvider1.SetShowHelp(this.editConfigsButton, true);
            this.editConfigsButton.Size = new System.Drawing.Size(95, 26);
            this.editConfigsButton.TabIndex = 8;
            this.editConfigsButton.Text = "&Edit Configs...";
            this.editConfigsButton.Click += new System.EventHandler(this.editConfigsButton_Click);
            // 
            // configComboBox
            // 
            this.configComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.configComboBox, "Select the configuration to edit");
            this.configComboBox.ItemHeight = 16;
            this.configComboBox.Location = new System.Drawing.Point(112, 34);
            this.configComboBox.Name = "configComboBox";
            this.helpProvider1.SetShowHelp(this.configComboBox, true);
            this.configComboBox.Size = new System.Drawing.Size(232, 24);
            this.configComboBox.TabIndex = 7;
            this.configComboBox.SelectedIndexChanged += new System.EventHandler(this.configComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.helpProvider1.SetShowHelp(this.label1, false);
            this.label1.Size = new System.Drawing.Size(102, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Configuration:";
            // 
            // projectBaseBrowseButton
            // 
            this.projectBaseBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpProvider1.SetHelpString(this.projectBaseBrowseButton, "Browse to locate ApplicationBase directory.");
            this.projectBaseBrowseButton.Image = ((System.Drawing.Image)(resources.GetObject("projectBaseBrowseButton.Image")));
            this.projectBaseBrowseButton.Location = new System.Drawing.Point(448, 45);
            this.projectBaseBrowseButton.Name = "projectBaseBrowseButton";
            this.helpProvider1.SetShowHelp(this.projectBaseBrowseButton, true);
            this.projectBaseBrowseButton.Size = new System.Drawing.Size(24, 20);
            this.projectBaseBrowseButton.TabIndex = 10;
            this.projectBaseBrowseButton.Click += new System.EventHandler(this.projectBaseBrowseButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.projectTabControl);
            this.groupBox1.Controls.Add(this.editConfigsButton);
            this.groupBox1.Controls.Add(this.configComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 366);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration Properties";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(20, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Process Model:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(248, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Domain Usage:";
            // 
            // processModelComboBox
            // 
            this.processModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processModelComboBox.Items.AddRange(new object[] {
            "Default",
            "Single",
            "Separate",
            "Multiple"});
            this.processModelComboBox.Location = new System.Drawing.Point(136, 82);
            this.processModelComboBox.Name = "processModelComboBox";
            this.processModelComboBox.Size = new System.Drawing.Size(104, 24);
            this.processModelComboBox.TabIndex = 13;
            // 
            // domainUsageComboBox
            // 
            this.domainUsageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.domainUsageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.domainUsageComboBox.Items.AddRange(new object[] {
            "Default",
            "None",
            "Single",
            "Multiple"});
            this.domainUsageComboBox.Location = new System.Drawing.Point(360, 82);
            this.domainUsageComboBox.Name = "domainUsageComboBox";
            this.domainUsageComboBox.Size = new System.Drawing.Size(112, 24);
            this.domainUsageComboBox.TabIndex = 14;
            // 
            // ProjectEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(490, 523);
            this.Controls.Add(this.domainUsageComboBox);
            this.Controls.Add(this.processModelComboBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.projectBaseBrowseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.projectBaseTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.projectPathLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(376, 400);
            this.Name = "ProjectEditor";
            this.helpProvider1.SetShowHelp(this, false);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NUnit Test Project Editor";
            this.TransparencyKey = System.Drawing.Color.Green;
            this.Load += new System.EventHandler(this.ProjectEditor_Load);
            this.projectTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTabPage.PerformLayout();
            this.assemblyTabPage.ResumeLayout(false);
            this.assemblyTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Config ComboBox Methods and Events

		private void configComboBox_Populate()
		{
			configComboBox.Items.Clear();

			if ( selectedConfig == null )
				selectedConfig = project.ActiveConfig;

			int selectedIndex = -1; 
			foreach( ProjectConfig config in project.Configs )
			{
				string name = config.Name;
				int index = configComboBox.Items.Add( name );
				if ( name == selectedConfig.Name )
					selectedIndex = index;
			}

			if ( selectedIndex == -1 && configComboBox.Items.Count > 0 )
			{
				selectedIndex = 0;
				selectedConfig = project.Configs[0];
			}

			if ( selectedIndex == -1 )
				selectedConfig = null;
			else
				configComboBox.SelectedIndex = selectedIndex;
		
			addAssemblyButton.Enabled = removeAssemblyButton.Enabled = project.Configs.Count > 0;
		}

		private void configComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			selectedConfig = project.Configs[(string)configComboBox.SelectedItem];

			RuntimeFramework framework = selectedConfig.RuntimeFramework;

			RuntimeType runtime = RuntimeType.Any;
            Version version = RuntimeFramework.DefaultVersion;

            if (framework != null)
            {
                runtime = framework.Runtime;
                version = framework.ClrVersion;
            }

			int index = runtimeComboBox.FindStringExact(runtime.ToString(), 0);
			if ( index < 0 ) index = 0;
			runtimeComboBox.SelectedIndex = index;

            if (framework == null || framework.AllowAnyVersion)
                runtimeVersionComboBox.SelectedIndex = 0;
            else
				runtimeVersionComboBox.Text = version.ToString();
			
			applicationBaseTextBox.Text = selectedConfig.RelativeBasePath;

			configFileTextBox.Text = selectedConfig.ConfigurationFile;

			switch ( selectedConfig.BinPathType )
			{
				case BinPathType.Auto:
					autoBinPathRadioButton.Checked = true;
					break;

				case BinPathType.Manual:
					manualBinPathRadioButton.Checked = true;
					privateBinPathTextBox.Text = selectedConfig.PrivateBinPath;
					break;

				default:
					noBinPathRadioButton.Checked = true;
					break;
			}

			assemblyListBox_Populate();
		}

		#endregion

		#region Assembly ListBox Methods and Events

		private void assemblyListBox_Populate()
		{
			assemblyListBox.Items.Clear();
			int selectedIndex = -1;

			foreach( string assembly in selectedConfig.Assemblies )
			{
				int index = assemblyListBox.Items.Add( Path.GetFileName( assembly ) );

				if ( assembly == selectedAssembly )
					selectedIndex = index;
			}

			if ( assemblyListBox.Items.Count > 0 && selectedIndex == -1)
				selectedIndex = 0;

            if (selectedIndex == -1)
            {
                removeAssemblyButton.Enabled = false;
                assemblyPathBrowseButton.Enabled = false;
            }
            else
            {
                assemblyListBox.SelectedIndex = selectedIndex;
                removeAssemblyButton.Enabled = true;
                assemblyPathBrowseButton.Enabled = true;
            }
		}

		private void assemblyListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( assemblyListBox.SelectedIndex == -1 )
			{
				assemblyPathTextBox.Text = selectedAssembly = null;
                removeAssemblyButton.Enabled = false;
                assemblyPathBrowseButton.Enabled = false;
            }
			else 
			{
				assemblyPathTextBox.Text = selectedAssembly = //(string)assemblyListBox.SelectedItem;
					selectedConfig.Assemblies[assemblyListBox.SelectedIndex];
				removeAssemblyButton.Enabled = true;
                assemblyPathBrowseButton.Enabled = true;
            }
		}

		#endregion

		#region Project Base Methods and Events
		private void projectBaseBrowseButton_Click(object sender, System.EventArgs e)
		{
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = string.Format("Select ApplicationBase for the project as a whole.");
            if (browser.ShowDialog(this) == DialogResult.OK)
            {
                string projectBase = browser.SelectedPath;
                if (projectBase != null && projectBase != project.BasePath)
                    UpdateProjectBase(projectBase);
            }
        }

		private void projectBaseTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( projectBaseTextBox.Text != string.Empty )
			{
				string projectBase = null;
				try
				{
					projectBase = projectBaseTextBox.Text;
					Directory.Exists( projectBase );
				}
				catch( Exception ex )
				{
					projectBaseTextBox.SelectAll();
					UserMessage.DisplayFailure( ex, "Invalid Project Base" );
					e.Cancel = true;
				}

				if ( !Directory.Exists( projectBase ) )
				{
					string msg = string.Format( 
						"The directory {0} does not exist. Do you want to create it?", 
						projectBase );
					switch ( UserMessage.Ask( msg, "Project Editor" ) )
					{
						case DialogResult.Yes:
							Directory.CreateDirectory( projectBase );
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
						case DialogResult.No:
						default:
							break;
					}
				}
			}
		}

		private void projectBaseTextBox_Validated(object sender, System.EventArgs e)
		{
			UpdateProjectBase( projectBaseTextBox.Text );
		}

		private void UpdateProjectBase( string projectBase )
		{
			if ( projectBase == string.Empty )
				projectBase = project.DefaultBasePath;

			project.BasePath = projectBaseTextBox.Text = projectBase;
		}
		#endregion

		#region Config Base Methods and Events
		private void configBaseBrowseButton_Click(object sender, System.EventArgs e)
		{
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = string.Format(
                "Select ApplicationBase for the {0} configuration, if different from the project as a whole.",
                selectedConfig.Name);
            //browser.RootFolder = project.BasePath;
            browser.SelectedPath = selectedConfig.BasePath;
            if (browser.ShowDialog(this) == DialogResult.OK)
            {
                string appbase = browser.SelectedPath;
                if (appbase != null && appbase != selectedConfig.BasePath)
                    UpdateApplicationBase(appbase);
            }
		}

		private void applicationBaseTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( applicationBaseTextBox.Text != String.Empty )
			{
				string applicationBase = null;

				try
				{
					applicationBase = Path.Combine( project.BasePath, applicationBaseTextBox.Text );
					Directory.Exists( applicationBase );
				}
				catch( Exception exception )
				{
					applicationBaseTextBox.SelectAll();
					UserMessage.DisplayFailure( exception, "Invalid ApplicationBase" );
					e.Cancel = true;
				}

	/*			if ( !PathUtils.SamePathOrUnder( project.BasePath, applicationBase ) )
				{
					applicationBaseTextBox.SelectAll();
					UserMessage.DisplayFailure( "Path must be equal to or under the project base", "Invalid Entry" );
					e.Cancel = true;
				}			
				else */
				if ( !Directory.Exists( applicationBase ) )
				{
					string msg = string.Format( 
						"The directory {0} does not exist. Do you want to create it?", 
						applicationBase );
					switch ( UserMessage.Ask( msg, "Project Editor" ) )
					{
						case DialogResult.Yes:
							Directory.CreateDirectory( applicationBase );
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
						case DialogResult.No:
						default:
							break;
					}
				}
			}
		}

		private void applicationBaseTextBox_Validated(object sender, System.EventArgs e)
		{
			UpdateApplicationBase( applicationBaseTextBox.Text );
		}

		private void UpdateApplicationBase( string appbase )
		{
			string basePath = null;

			if ( appbase != String.Empty )
			{
				basePath = Path.Combine( project.BasePath, appbase );
				if ( PathUtils.SamePath( project.BasePath, basePath ) )
					basePath = null;
			}

			selectedConfig.BasePath = basePath;

			// TODO: Test what happens if we set it the same as project base
			if ( selectedConfig.RelativeBasePath == null )
				applicationBaseTextBox.Text = string.Empty;
			else
				applicationBaseTextBox.Text = selectedConfig.RelativeBasePath;
		}
		#endregion

		#region Config File Methods and Events
		private void configFileTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string configFile = configFileTextBox.Text;
			if ( configFile != String.Empty )
			{
                try
                {
                    FileInfo info = new FileInfo(
                        Path.Combine(selectedConfig.BasePath, configFile));
                }
                catch (System.Exception exception)
                {
                    assemblyPathTextBox.SelectAll();
                    UserMessage.DisplayFailure(exception, "Invalid Config File Entry");
                    e.Cancel = true;
                }

				if ( configFile != Path.GetFileName( configFile ) )
				{
					configFileTextBox.SelectAll();
					UserMessage.DisplayFailure( "Specify configuration file as filename and extension only", "Invalid Entry" );
					e.Cancel = true;
				}
			}
		}

		private void configFileTextBox_Validated(object sender, System.EventArgs e)
		{
			if ( configFileTextBox.Text == String.Empty )
				selectedConfig.ConfigurationFile = null;
			else
				selectedConfig.ConfigurationFile = configFileTextBox.Text;
		}
		#endregion

        #region Process Model Methods and Events

        private void processModelComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            project.ProcessModel = this.ProcessModel;
            populateDomainUsageComboBox();
        }

        private ProcessModel ProcessModel
        {
            get { return (ProcessModel)processModelComboBox.SelectedIndex; }
            set { processModelComboBox.SelectedIndex = (int)value; }
        }

        #endregion

        #region DomainUsage MethodsAndEvents

        private void domainUsageComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            project.DomainUsage = this.DomainUsage;
        }

        private DomainUsage DomainUsage
        {
            get
            {
                int index = domainUsageComboBox.SelectedIndex;
                if (index < 0)
                    return DomainUsage.Default;

                return (DomainUsage)Enum.Parse(
                    typeof(DomainUsage),
                    domainUsageComboBox.SelectedItem.ToString());
            }
            set
            {
                domainUsageComboBox.SelectedIndex =
                    domainUsageComboBox.FindString(value.ToString());
            }
        }

        private void populateDomainUsageComboBox()
        {
            domainUsageComboBox.Items.Clear();
            domainUsageComboBox.Items.Add("Default");
            //domainUsageComboBox.Items.Add("None");
            domainUsageComboBox.Items.Add("Single");
            if (project.ProcessModel != ProcessModel.Multiple)
                domainUsageComboBox.Items.Add("Multiple");

            domainUsageComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Runtime Framework Methods and Events
		private void runtimeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            SetRuntimeFramework();
		}

        private void runtimeVersionComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Version v = this.RuntimeVersion;
            }
            catch (Exception ex)
            {
                runtimeVersionComboBox.SelectAll();
                UserMessage.DisplayFailure(ex, "Invalid Runtime Version");
                e.Cancel = true;
            }
        }

        private void runtimeVersionComboBox_Validated(object sender, System.EventArgs e)
        {
            SetRuntimeFramework();
        }

        private void SetRuntimeFramework()
        {
            selectedConfig.RuntimeFramework = new RuntimeFramework(this.RuntimeType, this.RuntimeVersion);
        }

        private RuntimeType RuntimeType
        {
            get
            {
                int index = runtimeComboBox.SelectedIndex;
                if (index < 0)
                    return RuntimeType.Any;

                string s = runtimeComboBox.SelectedItem.ToString();
                return (RuntimeType)Enum.Parse(typeof(RuntimeType), s);
            }
        }

        private Version RuntimeVersion
        {
            get
            {
                string s = runtimeVersionComboBox.Text;
                return s == string.Empty || s == "Default"
                    ? RuntimeFramework.DefaultVersion
                    : new Version(s);
            }
        }
        
        #endregion

		#region PrivateBinPath Methods and Events
		private void privateBinPathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string binPath = privateBinPathTextBox.Text;
			if ( binPath != String.Empty )
			{
				string[] elements = binPath.Split( new char[] { ';' } );
				foreach( string element in elements ) 
				{
					try
					{
						Directory.Exists( element );
					}
					catch(System.Exception exception)
					{
						privateBinPathTextBox.Select( binPath.IndexOf( element ), element.Length );
						UserMessage.DisplayFailure( exception, "Invalid Path: " + element );
						e.Cancel = true;
					}
				}
			}
		}

		private void privateBinPathTextBox_Validated(object sender, System.EventArgs e)
		{
			if ( privateBinPathTextBox.Text == String.Empty )
				selectedConfig.PrivateBinPath = null;
			else
				selectedConfig.PrivateBinPath = privateBinPathTextBox.Text;
		}

        private void autoBinPathRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (autoBinPathRadioButton.Checked)
            {
                selectedConfig.BinPathType = BinPathType.Auto;
                privateBinPathTextBox.Enabled = false;
            }
        }

        private void manualBinPathRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (manualBinPathRadioButton.Checked)
            {
                selectedConfig.BinPathType = BinPathType.Manual;
                privateBinPathTextBox.Enabled = true;
            }
        }

        private void noBinPathRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (noBinPathRadioButton.Checked)
            {
                selectedConfig.BinPathType = BinPathType.None;
                privateBinPathTextBox.Enabled = false;
            }
        }
        
        #endregion

		#region Assembly Path Methods and Events
		private void assemblyPathBrowseButton_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Select Assembly";
			
			dlg.Filter =
				"Assemblies (*.dll,*.exe)|*.dll;*.exe|" +
				"All Files (*.*)|*.*";

			dlg.InitialDirectory = System.IO.Path.GetDirectoryName( assemblyPathTextBox.Text );
			dlg.FilterIndex = 1;
			dlg.FileName = "";

			if ( dlg.ShowDialog( this ) == DialogResult.OK ) 
			{
				selectedConfig.Assemblies[assemblyListBox.SelectedIndex] = dlg.FileName;
				assemblyListBox_Populate();
            }
		}

		private void assemblyPathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string path = assemblyPathTextBox.Text;

            if (path != string.Empty)
            {
                try
                {
                    FileInfo info = new FileInfo(path);

                    if (!info.Exists)
                    {
                        DialogResult answer = UserMessage.Ask(string.Format(
                            "The path {0} does not exist. Do you want to use it anyway?", path));
                        if (answer != DialogResult.Yes)
                            e.Cancel = true;
                    }
                }
                catch (System.Exception exception)
                {
                    assemblyPathTextBox.SelectAll();
                    UserMessage.DisplayFailure(exception, "Invalid Assembly Path");
                    e.Cancel = true;
                }
            }
		}

		private void assemblyPathTextBox_Validated(object sender, System.EventArgs e)
		{
			selectedConfig.Assemblies[assemblyListBox.SelectedIndex] = assemblyPathTextBox.Text;
			assemblyListBox_Populate();
		}
		#endregion

		#region Other UI Events

		private void ProjectEditor_Load(object sender, System.EventArgs e)
		{
			this.Text = string.Format( "{0} - Project Editor", 
				project.Name );

			projectPathLabel.Text = project.ProjectPath;
			projectBaseTextBox.Text = project.BasePath;

            configComboBox_Populate();
			populateDomainUsageComboBox();

            this.ProcessModel = project.ProcessModel;
            this.DomainUsage = project.DomainUsage;

			this.processModelComboBox.SelectedIndexChanged += new System.EventHandler(this.processModelComboBox_SelectedIndexChanged);
			this.domainUsageComboBox.SelectedIndexChanged += new System.EventHandler(this.domainUsageComboBox_SelectedIndexChanged);
			this.runtimeComboBox.SelectedIndexChanged += new System.EventHandler(this.runtimeComboBox_SelectedIndexChanged);
		}

		private void editConfigsButton_Click(object sender, System.EventArgs e)
		{
			using( ConfigurationEditor editor = new ConfigurationEditor( project ) )
			{
				this.Site.Container.Add( editor );
				editor.ShowDialog();
			}
			configComboBox_Populate();
		}

		private void addAssemblyButton_Click(object sender, System.EventArgs e)
		{
			TestLoaderUI.AddToProject( this, selectedConfig.Name );
			assemblyListBox_Populate();
		}

		private void removeAssemblyButton_Click(object sender, System.EventArgs e)
		{
			if ( UserMessage.Ask( string.Format(
				"Remove {0} from project?", selectedAssembly ) ) == DialogResult.Yes )
			{
				int index = assemblyListBox.SelectedIndex;
				selectedConfig.Assemblies.RemoveAt( index );

				assemblyListBox.Items.RemoveAt( index );
				if ( index >= assemblyListBox.Items.Count )
					--index;

				if ( index >= 0 )
				{
					selectedAssembly = (string)assemblyListBox.Items[index];
					assemblyListBox.SelectedIndex = index;
				}
				else
					selectedAssembly = null;
			}
		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close();		
		}

        #endregion
	}
}
