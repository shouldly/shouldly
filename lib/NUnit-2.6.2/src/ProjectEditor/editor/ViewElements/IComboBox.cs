// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor.ViewElements
{
    /// <summary>
    /// IComboBox is implemented by view elements that associate
    /// an editable TextBox with a SelectionList. The classic
    /// implementation is System.Windows.Forms.ComboBox. This 
    /// interface is only intended for use when the TextBox
    /// is editable. Otherwise, ISelectionList provides all
    /// the necessary functionality.
    /// </summary>
    public interface IComboBox : ISelectionList
    {
        /// <summary>
        /// Gets or sets the value of the TextBox associated
        /// with this ComboBox.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Event that is raised when the text has changed
        /// and the focus is moved away.
        /// </summary>
        event ActionDelegate TextValidated;
    }
}
