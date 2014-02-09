// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// NUnitSettingsPage is the base class for all pages used
	/// in a tabbed or tree-structured SettingsDialog.
	/// </summary>
	public class SettingsPage : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Settings are available to derived classes
		/// </summary>
		protected ISettings settings;

		private string key;
		private string title;

        private MessageDisplay messageDisplay;

		// Constructor used by the Windows.Forms Designer
		public SettingsPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		// Constructor we use in creating page for a Tabbed
		// or TreeBased dialog.
		public SettingsPage( string key) : this()
		{
			this.key = key;
			this.title = key;
			int dot = key.LastIndexOf( '.' );
			if ( dot >= 0 ) title = key.Substring(dot+1);
            this.messageDisplay = new MessageDisplay("NUnit Settings");
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

		#region Properties

		public string Key
		{
			get { return key; }
		}

		public string Title
		{
			get { return title; }
		}

		public bool SettingsLoaded
		{
			get { return settings != null; }
		}

		public virtual bool HasChangesRequiringReload
		{
			get { return false; }
		}

        public IMessageDisplay MessageDisplay
        {
            get { return messageDisplay; }
        }

		#endregion

		#region Public Methods
		public virtual void LoadSettings()
		{
		}

		public virtual void ApplySettings()
		{
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// SettingsPage
			// 
			this.Name = "SettingsPage";
			this.Size = new System.Drawing.Size(456, 336);

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			if ( !DesignMode )
			{
				this.settings = Services.UserSettings;
				this.LoadSettings();
			}
		}
	}
}
