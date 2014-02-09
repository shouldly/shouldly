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
    /// Displays a dialog for entry of a new name for an
    /// existing configuration. This dialog collects and
    /// validates the name. The caller is responsible for
    /// actually renaming the cofiguration.
    /// </summary>
	public partial class RenameConfigurationDialog : System.Windows.Forms.Form, IRenameConfigurationDialog
    {
        #region Instance Variables

        private ITextElement configurationName;
        private ICommand okButtonWrapper;
        private IMessageDisplay messageDisplay;

        #endregion

        #region Constructor

        public RenameConfigurationDialog()
		{
			InitializeComponent();

            configurationName = new TextElement(configurationNameTextBox);
            okButtonWrapper = new ButtonElement(okButton);

            messageDisplay = new MessageDisplay("Rename Configuration");
        }

		#endregion

		#region IRenameConfigurationDialogMembers

		public ITextElement ConfigurationName 
        {
            get { return configurationName; }
        }

        public ICommand OkButton 
        {
            get { return okButtonWrapper; }
        }

		#endregion

        #region IView Members

        public IMessageDisplay MessageDisplay 
        {
            get { return messageDisplay; }
        }

        #endregion
    }

    public interface IRenameConfigurationDialog : IDialog
    {
        ITextElement ConfigurationName { get; }
        ICommand OkButton { get; }
    }
}
