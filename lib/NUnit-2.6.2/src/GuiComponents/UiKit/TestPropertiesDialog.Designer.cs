namespace NUnit.UiKit
{
    partial class TestPropertiesDialog
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
            this.testResult = new System.Windows.Forms.Label();
            this.pinButton = new System.Windows.Forms.CheckBox();
            this.testName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hiddenProperties = new System.Windows.Forms.CheckBox();
            this.categories = new System.Windows.Forms.Label();
            this.properties = new System.Windows.Forms.ListBox();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.testCaseCount = new System.Windows.Forms.Label();
            this.ignoreReasonLabel = new System.Windows.Forms.Label();
            this.testCaseCountLabel = new System.Windows.Forms.Label();
            this.shouldRun = new System.Windows.Forms.Label();
            this.shouldRunLabel = new System.Windows.Forms.Label();
            this.testType = new System.Windows.Forms.Label();
            this.testTypeLabel = new System.Windows.Forms.Label();
            this.categoriesLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.fullNameLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.assertCount = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.elapsedTime = new System.Windows.Forms.Label();
            this.stackTraceLabel = new System.Windows.Forms.Label();
            this.message = new CP.Windows.Forms.ExpandingLabel();
            this.stackTrace = new CP.Windows.Forms.ExpandingLabel();
            this.description = new CP.Windows.Forms.ExpandingLabel();
            this.ignoreReason = new CP.Windows.Forms.ExpandingLabel();
            this.fullName = new CP.Windows.Forms.ExpandingLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // testResult
            // 
            this.testResult.Location = new System.Drawing.Point(9, 11);
            this.testResult.Name = "testResult";
            this.testResult.Size = new System.Drawing.Size(120, 16);
            this.testResult.TabIndex = 0;
            this.testResult.Text = "Inconclusive";
            // 
            // pinButton
            // 
            this.pinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pinButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.pinButton.Location = new System.Drawing.Point(416, 10);
            this.pinButton.Name = "pinButton";
            this.pinButton.Size = new System.Drawing.Size(20, 20);
            this.pinButton.TabIndex = 2;
            this.pinButton.Click += new System.EventHandler(this.pinButton_Click);
            // 
            // testName
            // 
            this.testName.Location = new System.Drawing.Point(135, 12);
            this.testName.Name = "testName";
            this.testName.Size = new System.Drawing.Size(280, 14);
            this.testName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hiddenProperties);
            this.groupBox1.Controls.Add(this.description);
            this.groupBox1.Controls.Add(this.categories);
            this.groupBox1.Controls.Add(this.properties);
            this.groupBox1.Controls.Add(this.propertiesLabel);
            this.groupBox1.Controls.Add(this.testCaseCount);
            this.groupBox1.Controls.Add(this.ignoreReason);
            this.groupBox1.Controls.Add(this.ignoreReasonLabel);
            this.groupBox1.Controls.Add(this.testCaseCountLabel);
            this.groupBox1.Controls.Add(this.shouldRun);
            this.groupBox1.Controls.Add(this.shouldRunLabel);
            this.groupBox1.Controls.Add(this.testType);
            this.groupBox1.Controls.Add(this.testTypeLabel);
            this.groupBox1.Controls.Add(this.categoriesLabel);
            this.groupBox1.Controls.Add(this.descriptionLabel);
            this.groupBox1.Controls.Add(this.fullName);
            this.groupBox1.Controls.Add(this.fullNameLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 218);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Details";
            // 
            // hiddenProperties
            // 
            this.hiddenProperties.AutoSize = true;
            this.hiddenProperties.Location = new System.Drawing.Point(139, 192);
            this.hiddenProperties.Name = "hiddenProperties";
            this.hiddenProperties.Size = new System.Drawing.Size(144, 17);
            this.hiddenProperties.TabIndex = 16;
            this.hiddenProperties.Text = "Display hidden properties";
            this.hiddenProperties.UseVisualStyleBackColor = true;
            this.hiddenProperties.CheckedChanged += new System.EventHandler(this.hiddenProperties_CheckedChanged);
            // 
            // categories
            // 
            this.categories.Location = new System.Drawing.Point(101, 86);
            this.categories.Name = "categories";
            this.categories.Size = new System.Drawing.Size(309, 16);
            this.categories.TabIndex = 7;
            // 
            // properties
            // 
            this.properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.properties.Location = new System.Drawing.Point(104, 143);
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(308, 43);
            this.properties.TabIndex = 15;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.Location = new System.Drawing.Point(13, 143);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(80, 16);
            this.propertiesLabel.TabIndex = 14;
            this.propertiesLabel.Text = "Properties:";
            // 
            // testCaseCount
            // 
            this.testCaseCount.Location = new System.Drawing.Point(101, 108);
            this.testCaseCount.Name = "testCaseCount";
            this.testCaseCount.Size = new System.Drawing.Size(48, 15);
            this.testCaseCount.TabIndex = 9;
            // 
            // ignoreReasonLabel
            // 
            this.ignoreReasonLabel.Location = new System.Drawing.Point(13, 125);
            this.ignoreReasonLabel.Name = "ignoreReasonLabel";
            this.ignoreReasonLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ignoreReasonLabel.Size = new System.Drawing.Size(80, 16);
            this.ignoreReasonLabel.TabIndex = 12;
            this.ignoreReasonLabel.Text = "Reason:";
            // 
            // testCaseCountLabel
            // 
            this.testCaseCountLabel.Location = new System.Drawing.Point(13, 108);
            this.testCaseCountLabel.Name = "testCaseCountLabel";
            this.testCaseCountLabel.Size = new System.Drawing.Size(80, 15);
            this.testCaseCountLabel.TabIndex = 8;
            this.testCaseCountLabel.Text = "Test Count:";
            // 
            // shouldRun
            // 
            this.shouldRun.Location = new System.Drawing.Point(299, 108);
            this.shouldRun.Name = "shouldRun";
            this.shouldRun.Size = new System.Drawing.Size(88, 15);
            this.shouldRun.TabIndex = 11;
            this.shouldRun.Text = "Yes";
            // 
            // shouldRunLabel
            // 
            this.shouldRunLabel.Location = new System.Drawing.Point(183, 108);
            this.shouldRunLabel.Name = "shouldRunLabel";
            this.shouldRunLabel.Size = new System.Drawing.Size(84, 15);
            this.shouldRunLabel.TabIndex = 10;
            this.shouldRunLabel.Text = "Should Run?";
            // 
            // testType
            // 
            this.testType.Location = new System.Drawing.Point(101, 24);
            this.testType.Name = "testType";
            this.testType.Size = new System.Drawing.Size(311, 16);
            this.testType.TabIndex = 1;
            // 
            // testTypeLabel
            // 
            this.testTypeLabel.Location = new System.Drawing.Point(13, 24);
            this.testTypeLabel.Name = "testTypeLabel";
            this.testTypeLabel.Size = new System.Drawing.Size(80, 16);
            this.testTypeLabel.TabIndex = 0;
            this.testTypeLabel.Text = "Test Type:";
            // 
            // categoriesLabel
            // 
            this.categoriesLabel.Location = new System.Drawing.Point(13, 86);
            this.categoriesLabel.Name = "categoriesLabel";
            this.categoriesLabel.Size = new System.Drawing.Size(80, 16);
            this.categoriesLabel.TabIndex = 6;
            this.categoriesLabel.Text = "Categories:";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Location = new System.Drawing.Point(13, 64);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(80, 17);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Description:";
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.Location = new System.Drawing.Point(13, 43);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(80, 17);
            this.fullNameLabel.TabIndex = 2;
            this.fullNameLabel.Text = "Full Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.assertCount);
            this.groupBox2.Controls.Add(this.messageLabel);
            this.groupBox2.Controls.Add(this.elapsedTime);
            this.groupBox2.Controls.Add(this.stackTraceLabel);
            this.groupBox2.Controls.Add(this.message);
            this.groupBox2.Controls.Add(this.stackTrace);
            this.groupBox2.Location = new System.Drawing.Point(12, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 136);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // assertCount
            // 
            this.assertCount.Location = new System.Drawing.Point(234, 26);
            this.assertCount.Name = "assertCount";
            this.assertCount.Size = new System.Drawing.Size(178, 16);
            this.assertCount.TabIndex = 1;
            this.assertCount.Text = "Assert Count:";
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(18, 47);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(80, 17);
            this.messageLabel.TabIndex = 2;
            this.messageLabel.Text = "Message:";
            // 
            // elapsedTime
            // 
            this.elapsedTime.Location = new System.Drawing.Point(18, 26);
            this.elapsedTime.Name = "elapsedTime";
            this.elapsedTime.Size = new System.Drawing.Size(192, 16);
            this.elapsedTime.TabIndex = 0;
            this.elapsedTime.Text = "Execution Time:";
            // 
            // stackTraceLabel
            // 
            this.stackTraceLabel.Location = new System.Drawing.Point(18, 70);
            this.stackTraceLabel.Name = "stackTraceLabel";
            this.stackTraceLabel.Size = new System.Drawing.Size(80, 15);
            this.stackTraceLabel.TabIndex = 4;
            this.stackTraceLabel.Text = "Stack:";
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.message.CopySupported = true;
            this.message.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.message.Location = new System.Drawing.Point(106, 47);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(306, 17);
            this.message.TabIndex = 3;
            // 
            // stackTrace
            // 
            this.stackTrace.CopySupported = true;
            this.stackTrace.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.stackTrace.Location = new System.Drawing.Point(106, 70);
            this.stackTrace.Name = "stackTrace";
            this.stackTrace.Size = new System.Drawing.Size(306, 50);
            this.stackTrace.TabIndex = 5;
            // 
            // description
            // 
            this.description.CopySupported = true;
            this.description.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Both;
            this.description.Location = new System.Drawing.Point(101, 64);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(311, 17);
            this.description.TabIndex = 5;
            // 
            // ignoreReason
            // 
            this.ignoreReason.CopySupported = true;
            this.ignoreReason.Expansion = CP.Windows.Forms.TipWindow.ExpansionStyle.Vertical;
            this.ignoreReason.Location = new System.Drawing.Point(101, 125);
            this.ignoreReason.Name = "ignoreReason";
            this.ignoreReason.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ignoreReason.Size = new System.Drawing.Size(311, 16);
            this.ignoreReason.TabIndex = 13;
            // 
            // fullName
            // 
            this.fullName.CopySupported = true;
            this.fullName.Location = new System.Drawing.Point(101, 43);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(309, 15);
            this.fullName.TabIndex = 3;
            // 
            // TestPropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 410);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.testName);
            this.Controls.Add(this.pinButton);
            this.Controls.Add(this.testResult);
            this.Name = "TestPropertiesDialog";
            this.Text = "Test Properties";
            this.Load += new System.EventHandler(this.TestPropertiesDialog_Load);
            this.SizeChanged += new System.EventHandler(this.TestPropertiesDialog_SizeChanged);
            this.ResizeEnd += new System.EventHandler(this.TestPropertiesDialog_ResizeEnd);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label testResult;
        private System.Windows.Forms.CheckBox pinButton;
        private System.Windows.Forms.Label testName;
        private System.Windows.Forms.GroupBox groupBox1;
        private CP.Windows.Forms.ExpandingLabel description;
        private System.Windows.Forms.Label categories;
        private System.Windows.Forms.ListBox properties;
        private System.Windows.Forms.Label propertiesLabel;
        private System.Windows.Forms.Label testCaseCount;
        private CP.Windows.Forms.ExpandingLabel ignoreReason;
        private System.Windows.Forms.Label ignoreReasonLabel;
        private System.Windows.Forms.Label testCaseCountLabel;
        private System.Windows.Forms.Label shouldRun;
        private System.Windows.Forms.Label shouldRunLabel;
        private System.Windows.Forms.Label testType;
        private System.Windows.Forms.Label testTypeLabel;
        private System.Windows.Forms.Label categoriesLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private CP.Windows.Forms.ExpandingLabel fullName;
        private System.Windows.Forms.Label fullNameLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label assertCount;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label elapsedTime;
        private System.Windows.Forms.Label stackTraceLabel;
        private CP.Windows.Forms.ExpandingLabel message;
        private CP.Windows.Forms.ExpandingLabel stackTrace;
        private System.Windows.Forms.CheckBox hiddenProperties;
    }
}