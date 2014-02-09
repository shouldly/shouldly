// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.ProjectEditor.ViewElements
{
    public interface ISelection : IViewElement
    {
        /// <summary>
        /// Gets or sets the index of the currently selected item
        /// </summary>
        int SelectedIndex { get; set; }

        /// <summary>
        /// Event raised when the selection is changed by the user
        /// </summary>
        event ActionDelegate SelectionChanged;
    }
}
