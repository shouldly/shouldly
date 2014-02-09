namespace NUnit.Gui.SettingsPages
{
    partial class InternalTraceSettingsPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.traceLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.logDirectoryLabel = new CP.Windows.Forms.ExpandingLabel();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Internal Trace";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(158, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 8);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Trace Level:";
            // 
            // traceLevelComboBox
            // 
            this.traceLevelComboBox.FormattingEnabled = true;
            this.traceLevelComboBox.Items.AddRange(new object[] {
            "Default",
            "Off",
            "Error",
            "Warning",
            "Info",
            "Verbose"});
            this.traceLevelComboBox.Location = new System.Drawing.Point(134, 43);
            this.traceLevelComboBox.Name = "traceLevelComboBox";
            this.traceLevelComboBox.Size = new System.Drawing.Size(89, 21);
            this.traceLevelComboBox.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Log Directory:";
            // 
            // logDirectoryLabel
            // 
            this.logDirectoryLabel.Location = new System.Drawing.Point(131, 87);
            this.logDirectoryLabel.Name = "logDirectoryLabel";
            this.logDirectoryLabel.Size = new System.Drawing.Size(310, 22);
            this.logDirectoryLabel.TabIndex = 19;
            // 
            // InternalTraceSettingsPage
            // 
            this.Controls.Add(this.logDirectoryLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.traceLevelComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Name = "InternalTraceSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox traceLevelComboBox;
        private System.Windows.Forms.Label label4;
        private CP.Windows.Forms.ExpandingLabel logDirectoryLabel;
    }
}
