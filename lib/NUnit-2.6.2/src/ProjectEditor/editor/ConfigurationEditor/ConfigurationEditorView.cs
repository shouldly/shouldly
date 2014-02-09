// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
	/// <summary>
	/// ConfigurationEditor form is designed for adding, deleting
	/// and renaming configurations from a doc.
	/// </summary>
	public partial class ConfigurationEditorDialog : System.Windows.Forms.Form, IConfigurationEditorDialog
    {
        #region Instance Variables

        private ICommand addCommand;
        private ICommand removeCommand;
        private ICommand renameCommand;
        private ICommand activeCommand;

        private ISelectionList configList;

        private IMessageDisplay messageDisplay;

        #endregion

        #region Constructor

        public ConfigurationEditorDialog()
		{
			InitializeComponent();

            addCommand = new ButtonElement(addButton);
            removeCommand = new ButtonElement(removeButton);
            renameCommand = new ButtonElement(renameButton);
            activeCommand = new ButtonElement(activeButton);

            configList = new ListBoxElement(configListBox);

            messageDisplay = new MessageDisplay("NUnit Configuration Editor");
		}

		#endregion

        #region IConfigurationEditorDialog Members

        #region Properties

        public ICommand AddCommand 
        {
            get { return addCommand; }
        }

        public ICommand RemoveCommand 
        {
            get { return removeCommand; }
        }

        public ICommand RenameCommand 
        {
            get { return renameCommand; }
        }

        public ICommand ActiveCommand 
        {
            get { return activeCommand; }
        }

        public ISelectionList ConfigList 
        {
            get { return configList; }
        }

        public IAddConfigurationDialog AddConfigurationDialog
        {
            get { return new AddConfigurationDialog(); }
        }

        public IMessageDisplay MessageDisplay 
        {
            get { return messageDisplay; }
        }
        
        public IRenameConfigurationDialog RenameConfigurationDialog
        {
            get { return new RenameConfigurationDialog(); }
        }

        #endregion

        #endregion
    }
}
