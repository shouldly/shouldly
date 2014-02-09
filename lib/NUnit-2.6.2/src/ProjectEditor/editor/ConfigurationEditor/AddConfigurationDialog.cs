// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
	/// <summary>
    /// Displays a dialog for creation of a new configuration.
    /// The dialog collects and validates the name and the
    /// name of a configuration to be copied and then adds the
    /// new configuration to the doc.
    /// 
    /// A DialogResult of DialogResult.OK indicates that the
    /// configuration was added successfully.
    /// </summary>
	public partial class AddConfigurationDialog : System.Windows.Forms.Form, IAddConfigurationDialog
	{
        private static readonly string NONE_SELECTED = "<none>";

		#region Constructor

		public AddConfigurationDialog()
		{ 
			InitializeComponent();

            okButtonWrapper = new ButtonElement(okButton);
        }

		#endregion

		#region Properties

        private MessageDisplay mbox = new MessageDisplay("Add Configuration");
        public IMessageDisplay MessageDisplay { get { return mbox; } }

        private string[] configList;
        public string[] ConfigList 
        {
            get { return configList; }
            set 
            { 
                configList = value;

                configurationComboBox.Items.Clear();
                configurationComboBox.Items.Add(NONE_SELECTED);
                configurationComboBox.SelectedIndex = 0;

                foreach (string config in configList)
                    configurationComboBox.Items.Add(config);
            } 
        }

		public string ConfigToCreate 
        {
            get { return configurationNameTextBox.Text; }
        }

		public string ConfigToCopy 
        {
            get 
            { 
                string config = (string)configurationComboBox.SelectedItem;
                return config == NONE_SELECTED ? null : config;
            }
            set 
            {
                string config = string.IsNullOrEmpty(value) ? NONE_SELECTED : value;
                configurationComboBox.SelectedItem = config;
            }
        }

        private ICommand okButtonWrapper;
        public ICommand OkButton 
        {
            get { return okButtonWrapper; }
        }

		#endregion
    }

    public interface IAddConfigurationDialog : IDialog
    {
        string[] ConfigList { get; set; }

        string ConfigToCreate { get; }
        string ConfigToCopy { get; }

        ICommand OkButton { get; }
    }
}
