// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for OptionsDialogBase.
	/// </summary>
    public class SettingsDialogBase : NUnitFormBase
	{
		#region Instance Fields
		protected System.Windows.Forms.Button cancelButton;
		protected System.Windows.Forms.Button okButton;

		private SettingsPageCollection pageList;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool reloadProjectOnClose;
		#endregion

		#region Construction and Disposal
		public SettingsDialogBase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			pageList = new SettingsPageCollection( );
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.CausesValidation = false;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(256, 424);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 24);
			this.cancelButton.TabIndex = 18;
			this.cancelButton.Text = "Cancel";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(168, 424);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(72, 24);
			this.okButton.TabIndex = 17;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// SettingsDialogBase
			// 
			this.ClientSize = new System.Drawing.Size(336, 458);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialogBase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Closed += new System.EventHandler(this.SettingsDialogBase_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		public SettingsPageCollection SettingsPages
		{
			get { return pageList; }
		}

		#endregion

		#region Public Methods
		public void ApplySettings()
		{
			foreach( SettingsPage page in pageList )
				if ( page.SettingsLoaded )
					page.ApplySettings();
		}
		#endregion

		#region Event Handlers
		private void SettingsDialogBase_Closed(object sender, System.EventArgs e)
		{
			if (this.reloadProjectOnClose)
				Services.TestLoader.ReloadTest();
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			if ( Services.TestLoader.IsTestLoaded && this.HasChangesRequiringReload )
			{
                DialogResult answer = MessageDisplay.Ask( 
					"Some changes will only take effect when you reload the test project. Do you want to reload now?");
				
				if ( answer == DialogResult.Yes )
					this.reloadProjectOnClose = true;
			}

			ApplySettings();

			DialogResult = DialogResult.OK;

			Close();
		}
		#endregion

		#region Helper Methods
		private bool HasChangesRequiringReload
		{
			get
			{
				foreach( SettingsPage page in pageList )
					if ( page.SettingsLoaded && page.HasChangesRequiringReload )
						return true;

				return false;
			}
		}
		#endregion

		#region Nested SettingsPageCollection Class
		public class SettingsPageCollection : CollectionBase
		{
			public void Add( SettingsPage page )
			{
				this.InnerList.Add( page );
			}

			public void AddRange( params SettingsPage[] pages )
			{
				this.InnerList.AddRange( pages );
			}

			public SettingsPage this[int index]
			{
				get { return (SettingsPage)InnerList[index]; }
			}

			public SettingsPage this[string key]
			{
				get
				{
					foreach( SettingsPage page in InnerList )
						if ( page.Key == key )
							return page;

					return null;
				}
			}
		}
		#endregion
	}
}
