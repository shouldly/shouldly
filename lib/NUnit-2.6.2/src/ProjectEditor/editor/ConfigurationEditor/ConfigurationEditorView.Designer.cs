namespace NUnit.ProjectEditor
{
    partial class ConfigurationEditorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigurationEditorDialog));
            this.configListBox = new System.Windows.Forms.ListBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.activeButton = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // configListBox
            // 
            this.helpProvider1.SetHelpString(this.configListBox, "Selects the configuration to operate on.");
            this.configListBox.ItemHeight = 16;
            this.configListBox.Location = new System.Drawing.Point(8, 8);
            this.configListBox.Name = "configListBox";
            this.helpProvider1.SetShowHelp(this.configListBox, true);
            this.configListBox.Size = new System.Drawing.Size(168, 212);
            this.configListBox.TabIndex = 0;
            // 
            // removeButton
            // 
            this.helpProvider1.SetHelpString(this.removeButton, "Removes the selected configuration");
            this.removeButton.Location = new System.Drawing.Point(192, 8);
            this.removeButton.Name = "removeButton";
            this.helpProvider1.SetShowHelp(this.removeButton, true);
            this.removeButton.Size = new System.Drawing.Size(96, 32);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "&Remove";
            // 
            // renameButton
            // 
            this.helpProvider1.SetHelpString(this.renameButton, "Allows renaming the selected configuration");
            this.renameButton.Location = new System.Drawing.Point(192, 48);
            this.renameButton.Name = "renameButton";
            this.helpProvider1.SetShowHelp(this.renameButton, true);
            this.renameButton.Size = new System.Drawing.Size(96, 32);
            this.renameButton.TabIndex = 2;
            this.renameButton.Text = "Re&name...";
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.helpProvider1.SetHelpString(this.closeButton, "Closes this dialog");
            this.closeButton.Location = new System.Drawing.Point(192, 216);
            this.closeButton.Name = "closeButton";
            this.helpProvider1.SetShowHelp(this.closeButton, true);
            this.closeButton.Size = new System.Drawing.Size(96, 32);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            // 
            // addButton
            // 
            this.helpProvider1.SetHelpString(this.addButton, "Allows adding a new configuration");
            this.addButton.Location = new System.Drawing.Point(192, 88);
            this.addButton.Name = "addButton";
            this.helpProvider1.SetShowHelp(this.addButton, true);
            this.addButton.Size = new System.Drawing.Size(96, 32);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "&Add...";
            // 
            // activeButton
            // 
            this.helpProvider1.SetHelpString(this.activeButton, "Makes the selected configuration active");
            this.activeButton.Location = new System.Drawing.Point(192, 128);
            this.activeButton.Name = "activeButton";
            this.helpProvider1.SetShowHelp(this.activeButton, true);
            this.activeButton.Size = new System.Drawing.Size(96, 32);
            this.activeButton.TabIndex = 6;
            this.activeButton.Text = "&Make Active";
            // 
            // ConfigurationEditor
            // 
            this.AcceptButton = this.closeButton;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(297, 267);
            this.ControlBox = false;
            this.Controls.Add(this.activeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.configListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationEditor";
            this.helpProvider1.SetShowHelp(this, false);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration Editor";
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ListBox configListBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button activeButton;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button closeButton;

    }
}
