// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.ProjectEditor.ViewElements;

namespace NUnit.ProjectEditor
{
    /// <summary>
    /// Common interface implemented by all views used in
    /// the ProjectEditor application
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Object that knows how to display various messages
        /// in a MessageBox.
        /// </summary>
        IMessageDisplay MessageDisplay { get; }

        /// <summary>
        /// Gets or sets the visibility of the view
        /// </summary>
        bool Visible { get; set; }
    }
}
