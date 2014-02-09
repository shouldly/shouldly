// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// This interface describes a feature that can be added to the ErrorWindow
    /// in order to show relevant information about failures/errors after a
    /// test suite run.
    ///     Clients who wants to add their own display should implement this
    /// interface and register their instance to ErrorBrowser at run-time.
    /// 
    /// Direct known implementations are:
    ///     StackTraceDisplay
    ///     SourceCodeDisplay
    /// </summary>
    public interface IErrorDisplay
    {
        /// <summary>
        /// Gives access to the ToolStripButton that enables this display.
        /// </summary>
        ToolStripButton PluginItem { get; }

        /// <summary>
        /// Gives access to a possibly null collection of option controls that will
        /// be shown when this display has the focus.
        /// </summary>
        ToolStripItem[] OptionItems { get; }

        /// <summary>
        /// Gives access to the content control of this display.
        /// </summary>
        Control Content { get; }

        /// <summary>
        /// Called whenever the user changes the error selection in the detail list.
        /// This method is called to allow the display to update its content according
        /// the given stack trace.
        /// </summary>
        /// <param name="stackTrace"></param>
        void OnStackTraceChanged(string stackTrace);
    }
}
