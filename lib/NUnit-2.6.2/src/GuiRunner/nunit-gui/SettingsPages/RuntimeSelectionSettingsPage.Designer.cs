namespace NUnit.Gui.SettingsPages
{
    partial class RuntimeSelectionSettingsPage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.runtimeSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Runtime Selection";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(169, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 8);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // runtimeSelectionCheckBox
            // 
            this.helpProvider1.SetHelpString(this.runtimeSelectionCheckBox, "If checked and no specific runtime is requested, NUnit examines each assembly and" +
                    " attempts to load it using the runtime version for which it was built. If not ch" +
                    "ecked, the current runtime is used.");
            this.runtimeSelectionCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.runtimeSelectionCheckBox.Location = new System.Drawing.Point(35, 28);
            this.runtimeSelectionCheckBox.Name = "runtimeSelectionCheckBox";
            this.helpProvider1.SetShowHelp(this.runtimeSelectionCheckBox, true);
            this.runtimeSelectionCheckBox.Size = new System.Drawing.Size(372, 48);
            this.runtimeSelectionCheckBox.TabIndex = 15;
            this.runtimeSelectionCheckBox.Text = "Select default runtime version based on target framework of test assembly";
            this.runtimeSelectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // RuntimeSelectionSettingsPage
            // 
            this.Controls.Add(this.runtimeSelectionCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Name = "RuntimeSelectionSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox runtimeSelectionCheckBox;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}
