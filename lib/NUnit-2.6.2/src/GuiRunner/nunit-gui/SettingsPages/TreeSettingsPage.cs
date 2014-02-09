// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NUnit.UiKit;
using NUnit.Util;

namespace NUnit.Gui.SettingsPages
{
	public class TreeSettingsPage : NUnit.UiKit.SettingsPage
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox initialDisplayComboBox;
		private System.Windows.Forms.CheckBox clearResultsCheckBox;
		private System.Windows.Forms.CheckBox saveVisualStateCheckBox;
		private System.Windows.Forms.CheckBox showCheckBoxesCheckBox;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.RadioButton flatTestList;
		private System.Windows.Forms.RadioButton autoNamespaceSuites;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private Label label6;
        private PictureBox successImage;
        private PictureBox failureImage;
        private PictureBox ignoredImage;
        private PictureBox inconclusiveImage;
        private PictureBox skippedImage;
		private System.ComponentModel.IContainer components = null;
        private Label label4;
        private ListBox imageSetListBox;

        private static string treeImageDir = PathUtils.Combine(Assembly.GetExecutingAssembly(), "Images", "Tree");

		public TreeSettingsPage(string key) : base(key)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeSettingsPage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.initialDisplayComboBox = new System.Windows.Forms.ComboBox();
            this.clearResultsCheckBox = new System.Windows.Forms.CheckBox();
            this.saveVisualStateCheckBox = new System.Windows.Forms.CheckBox();
            this.showCheckBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.flatTestList = new System.Windows.Forms.RadioButton();
            this.autoNamespaceSuites = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.successImage = new System.Windows.Forms.PictureBox();
            this.failureImage = new System.Windows.Forms.PictureBox();
            this.ignoredImage = new System.Windows.Forms.PictureBox();
            this.inconclusiveImage = new System.Windows.Forms.PictureBox();
            this.skippedImage = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.imageSetListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.successImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.failureImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoredImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inconclusiveImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skippedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(144, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 8);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tree View";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Initial display on load";
            // 
            // initialDisplayComboBox
            // 
            this.initialDisplayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.helpProvider1.SetHelpString(this.initialDisplayComboBox, "Selects the initial display style of the tree when an assembly is loaded");
            this.initialDisplayComboBox.ItemHeight = 13;
            this.initialDisplayComboBox.Items.AddRange(new object[] {
            "Auto",
            "Expand",
            "Collapse",
            "HideTests"});
            this.initialDisplayComboBox.Location = new System.Drawing.Point(236, 24);
            this.initialDisplayComboBox.Name = "initialDisplayComboBox";
            this.helpProvider1.SetShowHelp(this.initialDisplayComboBox, true);
            this.initialDisplayComboBox.Size = new System.Drawing.Size(168, 21);
            this.initialDisplayComboBox.TabIndex = 33;
            // 
            // clearResultsCheckBox
            // 
            this.clearResultsCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.clearResultsCheckBox, "If checked, any prior results are cleared when reloading");
            this.clearResultsCheckBox.Location = new System.Drawing.Point(32, 129);
            this.clearResultsCheckBox.Name = "clearResultsCheckBox";
            this.helpProvider1.SetShowHelp(this.clearResultsCheckBox, true);
            this.clearResultsCheckBox.Size = new System.Drawing.Size(161, 17);
            this.clearResultsCheckBox.TabIndex = 34;
            this.clearResultsCheckBox.Text = "Clear results when reloading.";
            // 
            // saveVisualStateCheckBox
            // 
            this.saveVisualStateCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.saveVisualStateCheckBox, "If checked, the visual state of the project is saved on exit. This includes selec" +
                    "ted tests, categories and the state of the tree itself.");
            this.saveVisualStateCheckBox.Location = new System.Drawing.Point(32, 155);
            this.saveVisualStateCheckBox.Name = "saveVisualStateCheckBox";
            this.helpProvider1.SetShowHelp(this.saveVisualStateCheckBox, true);
            this.saveVisualStateCheckBox.Size = new System.Drawing.Size(184, 17);
            this.saveVisualStateCheckBox.TabIndex = 35;
            this.saveVisualStateCheckBox.Text = "Save Visual State of each project";
            // 
            // showCheckBoxesCheckBox
            // 
            this.showCheckBoxesCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.showCheckBoxesCheckBox, "If selected, the tree displays checkboxes for use in selecting multiple tests.");
            this.showCheckBoxesCheckBox.Location = new System.Drawing.Point(32, 181);
            this.showCheckBoxesCheckBox.Name = "showCheckBoxesCheckBox";
            this.helpProvider1.SetShowHelp(this.showCheckBoxesCheckBox, true);
            this.showCheckBoxesCheckBox.Size = new System.Drawing.Size(116, 17);
            this.showCheckBoxesCheckBox.TabIndex = 36;
            this.showCheckBoxesCheckBox.Text = "Show CheckBoxes";
            // 
            // flatTestList
            // 
            this.flatTestList.AutoCheck = false;
            this.flatTestList.AutoSize = true;
            this.helpProvider1.SetHelpString(this.flatTestList, "If selected, the tree will consist of a flat list of fixtures, without any higher" +
                    "-level structure beyond the assemblies.");
            this.flatTestList.Location = new System.Drawing.Point(32, 269);
            this.flatTestList.Name = "flatTestList";
            this.helpProvider1.SetShowHelp(this.flatTestList, true);
            this.flatTestList.Size = new System.Drawing.Size(129, 17);
            this.flatTestList.TabIndex = 40;
            this.flatTestList.Text = "Flat list of TestFixtures";
            this.flatTestList.Click += new System.EventHandler(this.toggleTestStructure);
            // 
            // autoNamespaceSuites
            // 
            this.autoNamespaceSuites.AutoCheck = false;
            this.autoNamespaceSuites.AutoSize = true;
            this.autoNamespaceSuites.Checked = true;
            this.helpProvider1.SetHelpString(this.autoNamespaceSuites, "If selected, the tree will follow the namespace structure of the tests, with suit" +
                    "es automatically created at each level.");
            this.autoNamespaceSuites.Location = new System.Drawing.Point(32, 243);
            this.autoNamespaceSuites.Name = "autoNamespaceSuites";
            this.helpProvider1.SetShowHelp(this.autoNamespaceSuites, true);
            this.autoNamespaceSuites.Size = new System.Drawing.Size(162, 17);
            this.autoNamespaceSuites.TabIndex = 39;
            this.autoNamespaceSuites.TabStop = true;
            this.autoNamespaceSuites.Text = "Automatic Namespace suites";
            this.autoNamespaceSuites.Click += new System.EventHandler(this.toggleTestStructure);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Test Structure";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(144, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 8);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(66, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 36);
            this.label6.TabIndex = 47;
            // 
            // successImage
            // 
            this.successImage.Image = ((System.Drawing.Image)(resources.GetObject("successImage.Image")));
            this.successImage.Location = new System.Drawing.Point(78, 92);
            this.successImage.Name = "successImage";
            this.successImage.Size = new System.Drawing.Size(16, 16);
            this.successImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.successImage.TabIndex = 48;
            this.successImage.TabStop = false;
            // 
            // failureImage
            // 
            this.failureImage.Image = ((System.Drawing.Image)(resources.GetObject("failureImage.Image")));
            this.failureImage.Location = new System.Drawing.Point(103, 92);
            this.failureImage.Name = "failureImage";
            this.failureImage.Size = new System.Drawing.Size(16, 16);
            this.failureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.failureImage.TabIndex = 49;
            this.failureImage.TabStop = false;
            // 
            // ignoredImage
            // 
            this.ignoredImage.Image = ((System.Drawing.Image)(resources.GetObject("ignoredImage.Image")));
            this.ignoredImage.Location = new System.Drawing.Point(128, 92);
            this.ignoredImage.Name = "ignoredImage";
            this.ignoredImage.Size = new System.Drawing.Size(16, 16);
            this.ignoredImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ignoredImage.TabIndex = 50;
            this.ignoredImage.TabStop = false;
            // 
            // inconclusiveImage
            // 
            this.inconclusiveImage.Image = ((System.Drawing.Image)(resources.GetObject("inconclusiveImage.Image")));
            this.inconclusiveImage.Location = new System.Drawing.Point(152, 92);
            this.inconclusiveImage.Name = "inconclusiveImage";
            this.inconclusiveImage.Size = new System.Drawing.Size(16, 16);
            this.inconclusiveImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.inconclusiveImage.TabIndex = 51;
            this.inconclusiveImage.TabStop = false;
            // 
            // skippedImage
            // 
            this.skippedImage.Image = ((System.Drawing.Image)(resources.GetObject("skippedImage.Image")));
            this.skippedImage.Location = new System.Drawing.Point(177, 92);
            this.skippedImage.Name = "skippedImage";
            this.skippedImage.Size = new System.Drawing.Size(16, 16);
            this.skippedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.skippedImage.TabIndex = 52;
            this.skippedImage.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Tree Images";
            // 
            // imageSetListBox
            // 
            this.imageSetListBox.FormattingEnabled = true;
            this.imageSetListBox.Location = new System.Drawing.Point(236, 61);
            this.imageSetListBox.Name = "imageSetListBox";
            this.imageSetListBox.Size = new System.Drawing.Size(168, 56);
            this.imageSetListBox.TabIndex = 54;
            this.imageSetListBox.SelectedIndexChanged += new System.EventHandler(this.imageSetListBox_SelectedIndexChanged);
            // 
            // TreeSettingsPage
            // 
            this.Controls.Add(this.imageSetListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.skippedImage);
            this.Controls.Add(this.inconclusiveImage);
            this.Controls.Add(this.ignoredImage);
            this.Controls.Add(this.failureImage);
            this.Controls.Add(this.successImage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flatTestList);
            this.Controls.Add(this.autoNamespaceSuites);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.showCheckBoxesCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.initialDisplayComboBox);
            this.Controls.Add(this.clearResultsCheckBox);
            this.Controls.Add(this.saveVisualStateCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "TreeSettingsPage";
            ((System.ComponentModel.ISupportInitialize)(this.successImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.failureImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoredImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inconclusiveImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skippedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
			initialDisplayComboBox.SelectedIndex = (int)(TestSuiteTreeView.DisplayStyle)settings.GetSetting( "Gui.TestTree.InitialTreeDisplay", TestSuiteTreeView.DisplayStyle.Auto );
			clearResultsCheckBox.Checked = settings.GetSetting( "Options.TestLoader.ClearResultsOnReload", true );
			saveVisualStateCheckBox.Checked = settings.GetSetting( "Gui.TestTree.SaveVisualState", true );
			showCheckBoxesCheckBox.Checked = settings.GetSetting( "Options.ShowCheckBoxes", false );

            string[] altDirs = Directory.Exists(treeImageDir)
                ? Directory.GetDirectories(treeImageDir)
                : new string[0];

            foreach (string altDir in altDirs)
                imageSetListBox.Items.Add(Path.GetFileName(altDir));
            string imageSet = settings.GetSetting("Gui.TestTree.AlternateImageSet", "Default");
            if (imageSetListBox.Items.Contains(imageSet))
                imageSetListBox.SelectedItem = imageSet;
		
			autoNamespaceSuites.Checked = settings.GetSetting( "Options.TestLoader.AutoNamespaceSuites", true );
			flatTestList.Checked = !autoNamespaceSuites.Checked;
		}

		public override void ApplySettings()
		{
			settings.SaveSetting( "Gui.TestTree.InitialTreeDisplay", (TestSuiteTreeView.DisplayStyle)initialDisplayComboBox.SelectedIndex );
			settings.SaveSetting( "Options.TestLoader.ClearResultsOnReload", clearResultsCheckBox.Checked );
			settings.SaveSetting( "Gui.TestTree.SaveVisualState", saveVisualStateCheckBox.Checked );
			settings.SaveSetting( "Options.ShowCheckBoxes", showCheckBoxesCheckBox.Checked );

            if (imageSetListBox.SelectedIndex >= 0)
                settings.SaveSetting("Gui.TestTree.AlternateImageSet", imageSetListBox.SelectedItem);

			settings.SaveSetting( "Options.TestLoader.AutoNamespaceSuites", autoNamespaceSuites.Checked );
		}

		private void toggleTestStructure(object sender, System.EventArgs e)
		{
			bool auto = autoNamespaceSuites.Checked = !autoNamespaceSuites.Checked;
			flatTestList.Checked = !auto;
		}
	
		public override bool HasChangesRequiringReload
		{
			get
			{
                return settings.GetSetting("Options.TestLoader.AutoNamespaceSuites", true) != autoNamespaceSuites.Checked;
			}
		}

        private void imageSetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string imageSet = imageSetListBox.SelectedItem as string;

            if (imageSet != null)
                DisplayImageSet(imageSet);
        }

        private void DisplayImageSet(string imageSet)
        {
            string imageSetDir = Path.Combine(treeImageDir, imageSet);

            DisplayImage(imageSetDir, "Success", successImage);
            DisplayImage(imageSetDir, "Failure", failureImage);
            DisplayImage(imageSetDir, "Ignored", ignoredImage);
            DisplayImage(imageSetDir, "Inconclusive", inconclusiveImage);
            DisplayImage(imageSetDir, "Skipped", skippedImage);
        }

        private void DisplayImage(string imageDir, string filename, PictureBox box)
        {
            string[] extensions = { ".png", ".jpg" };

            foreach (string ext in extensions)
            {
                string filePath = Path.Combine(imageDir, filename + ext);
                if (File.Exists(filePath))
                {
                    box.Load(filePath);
                    break;
                }
            }
        }
	}
}

