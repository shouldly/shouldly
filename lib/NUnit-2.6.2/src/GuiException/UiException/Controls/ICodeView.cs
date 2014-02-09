// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.UiException.CodeFormatters;
using System.Windows.Forms;

namespace NUnit.UiException.Controls
{
    /// <summary>
    /// The interface through which SourceCodeDisplay interacts with the code display.
    /// 
    /// Direct implementation is:
    ///     - CodeBox
    /// </summary>
    public interface ICodeView
    {
        /// <summary>
        /// Gets or sets a text to display in the code display.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the language formatting of the code display.
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// Gets or sets the viewport location from a 0 based line index
        /// </summary>
        int CurrentLine { get; set; }

        /// <summary>
        /// Gives access to the underlying IFormatterCatalog.
        /// </summary>
        IFormatterCatalog Formatter { get; }
    }
}
