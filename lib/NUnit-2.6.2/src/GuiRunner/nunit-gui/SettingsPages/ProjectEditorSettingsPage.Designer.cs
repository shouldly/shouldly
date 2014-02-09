namespace NUnit.Gui.SettingsPages
{
    partial class ProjectEditorSettingsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectEditorSettingsPage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editorPathTextBox = new System.Windows.Forms.TextBox();
            this.editorPathBrowseButton = new System.Windows.Forms.Button();
            this.useNUnitEditorRadioButton = new System.Windows.Forms.RadioButton();
            this.useOtherEditorRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(112, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Editor";
            // 
            // editorPathTextBox
            // 
            this.editorPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editorPathTextBox.Location = new System.Drawing.Point(112, 64);
            this.editorPathTextBox.Name = "editorPathTextBox";
            this.editorPathTextBox.Size = new System.Drawing.Size(298, 20);
            this.editorPathTextBox.TabIndex = 4;
            this.editorPathTextBox.TextChanged += new System.EventHandler(this.editorPathTextBox_TextChanged);
            // 
            // editorPathBrowseButton
            // 
            this.editorPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editorPathBrowseButton.Image = ((System.Drawing.Image)(resources.GetObject("editorPathBrowseButton.Image")));
            this.editorPathBrowseButton.Location = new System.Drawing.Point(418, 61);
            this.editorPathBrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.editorPathBrowseButton.Name = "editorPathBrowseButton";
            this.editorPathBrowseButton.Size = new System.Drawing.Size(27, 26);
            this.editorPathBrowseButton.TabIndex = 5;
            this.editorPathBrowseButton.Click += new System.EventHandler(this.editorPathBrowseButton_Click);
            // 
            // useNUnitEditorRadioButton
            // 
            this.useNUnitEditorRadioButton.AutoSize = true;
            this.useNUnitEditorRadioButton.Location = new System.Drawing.Point(20, 39);
            this.useNUnitEditorRadioButton.Name = "useNUnitEditorRadioButton";
            this.useNUnitEditorRadioButton.Size = new System.Drawing.Size(140, 17);
            this.useNUnitEditorRadioButton.TabIndex = 2;
            this.useNUnitEditorRadioButton.TabStop = true;
            this.useNUnitEditorRadioButton.Text = "Use NUnit Project Editor";
            this.useNUnitEditorRadioButton.UseVisualStyleBackColor = true;
            // 
            // useOtherEditorRadioButton
            // 
            this.useOtherEditorRadioButton.AutoSize = true;
            this.useOtherEditorRadioButton.Location = new System.Drawing.Point(20, 66);
            this.useOtherEditorRadioButton.Name = "useOtherEditorRadioButton";
            this.useOtherEditorRadioButton.Size = new System.Drawing.Size(89, 17);
            this.useOtherEditorRadioButton.TabIndex = 3;
            this.useOtherEditorRadioButton.TabStop = true;
            this.useOtherEditorRadioButton.Text = "Use Program:";
            this.useOtherEditorRadioButton.UseVisualStyleBackColor = true;
            // 
            // ProjectEditorSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.useOtherEditorRadioButton);
            this.Controls.Add(this.useNUnitEditorRadioButton);
            this.Controls.Add(this.editorPathBrowseButton);
            this.Controls.Add(this.editorPathTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "ProjectEditorSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editorPathTextBox;
        private System.Windows.Forms.Button editorPathBrowseButton;
        private System.Windows.Forms.RadioButton useNUnitEditorRadioButton;
        private System.Windows.Forms.RadioButton useOtherEditorRadioButton;
    }
}
